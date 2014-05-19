using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	public KGFMapIcon mapIcon;
	[SerializeField] float initialHealth;
	[SerializeField] ObjectIdentifier objectIdentifier;
	[SerializeField] GameObject explosionParticleEffect;
	[SerializeField] bool missileBattery = false;
	MissionManager missionManager;
	ArcadeStatHolder arcadeStatHolder;
	GameObject spawner;
	GameObject allyAircraftSpawner;
	GameObject allyDefensiveAircraftSpawner;
	GameObject allyTankSpawner;
	GameObject enemyAircraftEasySpawner;
	GameObject enemyDefensiveAircraftEasySpawner;
	GameObject enemyAircraftMediumSpawner;
	GameObject enemyDefensiveAircraftMediumSpawner;
	GameObject enemyAircraftHardSpawner;
	GameObject enemyDefensiveAircraftHardSpawner;
	GameObject enemyTankSpawner;
	GameObject enemyTurretSpawner;
	GameObject enemyMissileBatterySpawner;
	Vector3 correctedPos;
	FastSpawnObject thisSpawnObject;
	public MissionManager MissionManagerScript { get { return missionManager; } set { missionManager = value; }}
	public float InitialHealth { get { return initialHealth; } set { initialHealth = value; }}
	public ObjectIdentifier ObjectIdentifierScript { get { return objectIdentifier; }}
	public GameObject Spawner { get { return spawner; } set { spawner = value; }}
	public float Health { get; set; }
	public bool Dead { get { return Health <= 0; }}
	public GameObject ExplosionParticleEffect { get { return explosionParticleEffect; }}
	public FastSpawnObject ThisSpawnObject { get { return thisSpawnObject; } set { thisSpawnObject = value; }}
	float totalAirUnitsDestroyed;
	float totalGroundUnitsDestroyed;

//	void OnEnable()
//	{
//		mapIcon = transform.parent.GetComponentInChildren<KGFMapIcon>();
//		mapIcon.itsDataMapIcon.itsIsVisible = true;
//	}
//
//	void OnDisable()
//	{
//		mapIcon.itsDataMapIcon.itsIsVisible = false;
//	}

	public virtual void Start()
	{
		totalAirUnitsDestroyed = EncryptedPlayerPrefs.GetFloat("Total Air Units Destroyed", 0f);
		totalGroundUnitsDestroyed = EncryptedPlayerPrefs.GetFloat("Total Ground Units Destroyed", 0f);

		var MissionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (MissionManagerObject != null)
			missionManager = MissionManagerObject.GetComponent<MissionManager>();

		var ArcadeStatHolderObject = GameObject.FindGameObjectWithTag("ArcadeStatHolder");
		
		if (ArcadeStatHolderObject != null)
			arcadeStatHolder = ArcadeStatHolderObject.GetComponent<ArcadeStatHolder>();

		allyAircraftSpawner = GameObject.Find("Ally Aircraft Spawner");
		allyDefensiveAircraftSpawner = GameObject.Find("Ally Defensive Aircraft Spawner");
		allyTankSpawner = GameObject.Find("Ally Tank Spawner");

		enemyAircraftEasySpawner = GameObject.Find("Enemy Aircraft Easy Spawner");
		enemyDefensiveAircraftEasySpawner = GameObject.Find("Enemy Defensive Aircraft Easy Spawner");
		enemyAircraftMediumSpawner = GameObject.Find("Enemy Aircraft Medium Spawner");
		enemyDefensiveAircraftMediumSpawner = GameObject.Find("Enemy Defensive Aircraft Medium Spawner");
		enemyAircraftHardSpawner = GameObject.Find("Enemy Aircraft Hard Spawner");
		enemyDefensiveAircraftHardSpawner = GameObject.Find("Enemy Defensive Aircraft Hard Spawner");
		enemyTankSpawner = GameObject.Find("Enemy Tank Spawner");

		if (missileBattery)
			enemyMissileBatterySpawner = GameObject.Find("Enemy Missile Battery Spawner");
		else
			enemyTurretSpawner = GameObject.Find("Enemy Turret Spawner");
	}

	public virtual void OnEnable()
	{
		Health = InitialHealth;
	}

	public void AddHealth(float amount)
	{
		if (Dead)
			return;
		
		Health += amount;
		if (Health > InitialHealth)
			Health = InitialHealth;
	}
	
	public void ApplyDamage(float damage)
	{
		if (Dead)
			return;
		
		Health -= damage;
		if (Health <= 0f)
		{
			Health = 0f;
			Die();
		}
	}
	
	public virtual void Die()
	{
		switch(ObjectIdentifierScript.ObjectType)
		{
		case "Ally Objective":
			if (missionManager != null)
			{
				missionManager.AllyObjectiveDestroyed(objectIdentifier.gameObject);
				missionManager.MissionObjectivesDestroyed += 1;
			}

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Ally Aircraft":
			Spawner = allyAircraftSpawner;
			break;
		case "Ally Defensive Aircraft":
			Spawner = allyDefensiveAircraftSpawner;
			break;
		case "Ally Vehicle":
			Spawner = allyTankSpawner;

			if (missionManager != null)
			{
				MissionObjectSpawner spawnerUnitID = (MissionObjectSpawner)Spawner.GetComponent(typeof(MissionObjectSpawner));
				spawnerUnitID.RemoveFromList(transform.parent.name);
			}
			else
			{
				ObjectSpawner spawnerUnitID = (ObjectSpawner)Spawner.GetComponent(typeof(ObjectSpawner));
				spawnerUnitID.RemoveFromList(transform.parent.name);
			}

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);
			
			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Ally Turret":
			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Enemy Objective":
			if (missionManager != null)
			{
				missionManager.EnemyObjectiveDestroyed(objectIdentifier.gameObject);
				missionManager.MissionObjectivesRemaining -= 1;
			}

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Enemy Aircraft Easy":
			Spawner = enemyAircraftEasySpawner;

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			totalAirUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Air Units Destroyed", totalAirUnitsDestroyed);
			break;
		case "Enemy Defensive Aircraft Easy":
			Spawner = enemyDefensiveAircraftEasySpawner;

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			totalAirUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Air Units Destroyed", totalAirUnitsDestroyed);
			break;
		case "Enemy Aircraft Medium":
			Spawner = enemyAircraftMediumSpawner;

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			totalAirUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Air Units Destroyed", totalAirUnitsDestroyed);
			break;
		case "Enemy Defensive Aircraft Medium":
			Spawner = enemyDefensiveAircraftMediumSpawner;

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			totalAirUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Air Units Destroyed", totalAirUnitsDestroyed);
			break;
		case "Enemy Aircraft Hard":
			Spawner = enemyAircraftHardSpawner;

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			totalAirUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Air Units Destroyed", totalAirUnitsDestroyed);
			break;
		case "Enemy Defensive Aircraft Hard":
			Spawner = enemyDefensiveAircraftHardSpawner;

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			totalAirUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Air Units Destroyed", totalAirUnitsDestroyed);
			break;
		case "Enemy Vehicle":
			Spawner = enemyTankSpawner;

			if (missionManager != null)
				missionManager.EnemyGroundDestroyed += 1;
			else
				arcadeStatHolder.EnemyGroundDestroyed += 5;

			if (missionManager != null)
			{
				MissionObjectSpawner spawnerUnitID = (MissionObjectSpawner)Spawner.GetComponent(typeof(MissionObjectSpawner));
				spawnerUnitID.RemoveFromList(transform.parent.name);
			}
			else
			{
				ObjectSpawner spawnerUnitID = (ObjectSpawner)Spawner.GetComponent(typeof(ObjectSpawner));
				spawnerUnitID.RemoveFromList(transform.parent.name);
			}

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			totalGroundUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Ground Units Destroyed", totalGroundUnitsDestroyed);
			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Enemy Turret":
			if (missionManager == null)
			{
				if (missileBattery)
					Spawner = enemyMissileBatterySpawner;
				else
					Spawner = enemyTurretSpawner;

				ObjectSpawner spawnerUnitID = (ObjectSpawner)Spawner.GetComponent(typeof(ObjectSpawner));
				spawnerUnitID.RemoveFromList(transform.parent.name);
			}

			if (missionManager != null)
				missionManager.EnemyGroundDestroyed += 1;
			else
				arcadeStatHolder.EnemyGroundDestroyed += 5;

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			totalGroundUnitsDestroyed += 1;
			EncryptedPlayerPrefs.SetFloat("Total Ground Units Destroyed", totalGroundUnitsDestroyed);
			Destroy(objectIdentifier.transform.gameObject);
			return;
		default:
			Debug.LogError("No Case Switch Defined: " + transform.parent.name);
			break;
		}

		if (missionManager != null)
		{
			MissionObjectSpawner spawnerUnitID = (MissionObjectSpawner)Spawner.GetComponent(typeof(MissionObjectSpawner));
			spawnerUnitID.RemoveFromList(transform.parent.name);
		}
		else
		{
			ObjectSpawner spawnerUnitID = (ObjectSpawner)Spawner.GetComponent(typeof(ObjectSpawner));
			spawnerUnitID.RemoveFromList(transform.parent.name);
		}

		if (ExplosionParticleEffect != null)
			Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

		//Destroy(objectIdentifier.transform.gameObject);
		Fabric.EventManager.Instance.PostEvent("SFX_Aircraft_Fire", Fabric.EventAction.StopSound, transform.root.gameObject);
		Fabric.EventManager.Instance.PostEvent("SFX_Vehicle_Fire", Fabric.EventAction.StopSound, transform.root.gameObject);
		Fabric.EventManager.Instance.PostEvent("SFX_Vehicle_Movement", Fabric.EventAction.StopSound, transform.root.gameObject);
		AllyWeaponManager allyWeaponManager = transform.parent.GetComponentInChildren<AllyWeaponManager>();
		EnemyWeaponManager enemyWeaponManager = transform.parent.GetComponentInChildren<EnemyWeaponManager>();

		if (allyWeaponManager != null)
			allyWeaponManager.StopWeapon();
		else if (enemyWeaponManager != null)
			enemyWeaponManager.StopWeapon();

		mapIcon.SetVisibility(false);
		ThisSpawnObject = transform.parent.GetComponentInChildren<FastSpawnObject>();
		SpawnManager.SharedInstance.UnspawnObject(ThisSpawnObject);
	}
}