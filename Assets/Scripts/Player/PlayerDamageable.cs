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
				healthMultiplierModifier = 10f * EncryptedPlayerPrefs.GetFloat("Player Recovery Multiplier", 1f);
				break;
			case 2:
				healthMultiplierModifier = 8f * EncryptedPlayerPrefs.GetFloat("Player Recovery Multiplier", 1f);
				break;
			case 3:
				healthMultiplierModifier = 5f * EncryptedPlayerPrefs.GetFloat("Player Recovery Multiplier", 1f);
				break;
			default:
				Debug.LogError("No mission difficulty set");
				break;
			}
		}

		if (Application.loadedLevel == 3)
			healthMultiplierModifier = 1000f;
	}

	public override void OnEnable()
	{
		InitialHealth *= EncryptedPlayerPrefs.GetFloat("Player Health Multiplier", 1f);
		Health = InitialHealth;
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

		GameObject tempObject = new GameObject("tempAudioObject_" + Time.time);
		tempObject.transform.position = transform.position;
		tempObject.transform.rotation = transform.rotation;
		Fabric.EventManager.Instance.PostEvent("SFX_Explosion_Objective", Fabric.EventAction.PlaySound, tempObject);

		//Destroy(transform.root.gameObject);
		Fabric.EventManager.Instance.PostEvent("SFX_Player_Booster", Fabric.EventAction.StopSound, transform.parent.gameObject);
		WeaponManager weaponManager = transform.parent.GetComponentInChildren<WeaponManager>();
		weaponManager.StopWeapon();

		mapIcon.SetVisibility(false);
		ThisSpawnObject = transform.parent.GetComponentInChildren<FastSpawnObject>();
		SpawnManager.SharedInstance.UnspawnObject(ThisSpawnObject);
	}
}