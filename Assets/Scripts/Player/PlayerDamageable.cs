using UnityEngine;
using System.Collections;

public class PlayerDamageable : Damageable
{
	float healthMultiplierModifier = 10f;
	UISlider healthSlider;

	public override void Start()
	{
		Health = InitialHealth;
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		var healthSliderObject = GameObject.FindGameObjectWithTag("HealthSlider");
		healthSlider = healthSliderObject.GetComponent<UISlider>();
		
		if (missionManagerObject != null)
		{
			MissionManagerScript = missionManagerObject.GetComponent<MissionManager>();
			int missionDifficulty = MissionManagerScript.MissionDifficultyLevel;
			
			switch (missionDifficulty)
			{
			case 1:
				healthMultiplierModifier = 10f;
				break;
			case 2:
				healthMultiplierModifier = 8f;
				break;
			case 3:
				healthMultiplierModifier = 5f;
				break;
			default:
				Debug.LogError("No mission difficulty set");
				break;
			}
		}
	}

	void Update()
	{
		AddHealth(healthMultiplierModifier * Time.deltaTime);
		healthSlider.value = Health/100f;
	}

	public override void Die()
	{
		Spawner = GameObject.Find("Player Spawner");
		PlayerSpawner playerSpawner = (PlayerSpawner)Spawner.GetComponent(typeof(PlayerSpawner));
		playerSpawner.PlayerDeath();
			
		if (ExplosionParticleEffect != null)
			Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

		healthSlider.value = 0f;
		Destroy(transform.root.gameObject);
	}
}