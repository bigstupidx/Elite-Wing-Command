using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	[SerializeField] float initialHealth;
	[SerializeField] ObjectIdentifier objectIdentifier;
	[SerializeField] GameObject explosionParticleEffect;
	MissionManager missionManager;
	ArcadeStatHolder arcadeStatHolder;
	GameObject spawner;
	Vector3 correctedPos;
	public MissionManager MissionManagerScript { get { return missionManager; } set { missionManager = value; }}
	public float InitialHealth { get { return initialHealth; } set { initialHealth = value; }}
	public ObjectIdentifier ObjectIdentifierScript { get { return objectIdentifier; }}
	public GameObject Spawner { get { return spawner; } set { spawner = value; }}
	public float Health { get; set; }
	public bool Dead { get { return Health <= 0; }}
	public GameObject ExplosionParticleEffect { get { return explosionParticleEffect; }}
	
	public virtual void Start()
	{
		Health = InitialHealth;
		var MissionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (MissionManagerObject != null)
			missionManager = MissionManagerObject.GetComponent<MissionManager>();

		var ArcadeStatHolderObject = GameObject.FindGameObjectWithTag("ArcadeStatHolder");
		
		if (ArcadeStatHolderObject != null)
			arcadeStatHolder = ArcadeStatHolderObject.GetComponent<ArcadeStatHolder>();
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
			missionManager.AllyObjectiveDestroyed(objectIdentifier.gameObject);

			if (missionManager != null)
				missionManager.MissionObjectivesDestroyed += 1;

			Destroy(objectIdentifier.transform.gameObject);

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Ally Aircraft":
			Spawner = GameObject.Find("Ally Aircraft Spawner");
			break;
		case "Ally Defensive Aircraft":
			Spawner = GameObject.Find("Ally Defensive Aircraft Spawner");
			break;
		case "Ally Vehicle":
			Spawner = GameObject.Find("Ally Tank Spawner");
			break;
		case "Ally Turret":
			Destroy(objectIdentifier.transform.gameObject);

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Enemy Objective":
			missionManager.EnemyObjectiveDestroyed(objectIdentifier.gameObject);

			if (missionManager != null)
				missionManager.MissionObjectivesRemaining -= 1;

			Destroy(objectIdentifier.transform.gameObject);

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Enemy Aircraft Easy":
			Spawner = GameObject.Find("Enemy Aircraft Easy Spawner");

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			break;
		case "Enemy Defensive Aircraft Easy":
			Spawner = GameObject.Find("Enemy Defensive Aircraft Easy Spawner");

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			break;
		case "Enemy Aircraft Medium":
			Spawner = GameObject.Find("Enemy Aircraft Medium Spawner");

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			break;
		case "Enemy Defensive Aircraft Medium":
			Spawner = GameObject.Find("Enemy Defensive Aircraft Medium Spawner");

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			break;
		case "Enemy Aircraft Hard":
			Spawner = GameObject.Find("Enemy Aircraft Hard Spawner");

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			break;
		case "Enemy Defensive Aircraft Hard":
			Spawner = GameObject.Find("Enemy Defensive Aircraft Hard Spawner");

			if (missionManager != null)
				missionManager.EnemyAirDestroyed += 1;
			else
				arcadeStatHolder.EnemyAirDestroyed += 1;

			break;
		case "Enemy Vehicle":
			Spawner = GameObject.Find("Enemy Tank Spawner");

			if (missionManager != null)
				missionManager.EnemyGroundDestroyed += 1;
			else
				arcadeStatHolder.EnemyGroundDestroyed += 5;

			break;
		case "Enemy Turret":
			Destroy(objectIdentifier.transform.gameObject);

			if (missionManager != null)
				missionManager.EnemyGroundDestroyed += 1;
			else
				arcadeStatHolder.EnemyGroundDestroyed += 5;

			if (ExplosionParticleEffect != null)
				Instantiate(ExplosionParticleEffect, transform.position, transform.rotation);

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

		Destroy(objectIdentifier.transform.gameObject);
	}
}