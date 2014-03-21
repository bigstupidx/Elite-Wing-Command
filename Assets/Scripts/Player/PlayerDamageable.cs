using UnityEngine;
using System.Collections;

public class PlayerDamageable : Damageable
{
	float healthMultiplierModifier = 10f;
	GameObject healthSliderObject;
	UISlider healthSlider;
	float previousHealth;

	public override void Start()
	{
		InitialHealth *= PlayerPrefs.GetFloat("Player Health Multiplier", 1f);
		Health = InitialHealth;
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		healthSliderObject = GameObject.FindGameObjectWithTag("HealthSlider");

		if (healthSliderObject != null)
		{
			healthSlider = healthSliderObject.GetComponent<UISlider>();
			healthSlider.value = Health/InitialHealth;
		}
		
		if (missionManagerObject != null)
		{
			MissionManagerScript = missionManagerObject.GetComponent<MissionManager>();
			int missionDifficulty = MissionManagerScript.MissionDifficultyLevel;
			
			switch (missionDifficulty)
			{
			case 1:
				healthMultiplierModifier = 10f * PlayerPrefs.GetFloat("Player Recovery Multiplier", 1f);
				break;
			case 2:
				healthMultiplierModifier = 8f * PlayerPrefs.GetFloat("Player Recovery Multiplier", 1f);
				break;
			case 3:
				healthMultiplierModifier = 5f * PlayerPrefs.GetFloat("Player Recovery Multiplier", 1f);
				break;
			default:
				Debug.LogError("No mission difficulty set");
				break;
			}
		}
	}

	void FixedUpdate()
	{
		AddHealth(healthMultiplierModifier * Time.deltaTime);

		if (healthSliderObject == null)
		{
			healthSliderObject = GameObject.FindGameObjectWithTag("HealthSlider");

			if (healthSliderObject != null)
				healthSlider = healthSliderObject.GetComponent<UISlider>();
		}

		if (healthSlider != null)
			healthSlider.value = Health/100f;
	}

	public override void Die()
	{
		Spawner = GameObject.Find("Player Spawner");
		PlayerSpawner playerSpawner = (PlayerSpawner)Spawner.GetComponent(typeof(PlayerSpawner));
		playerSpawner.PlayerDeath();
			
		if (ExplosionParticleEffect != null)
			Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

		if (healthSlider != null)
			healthSlider.value = 0f;

		Destroy(transform.root.gameObject);
	}
}