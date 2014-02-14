using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	[SerializeField] float initialHealth;
	[SerializeField] ObjectIdentifier objectIdentifier;
	[SerializeField] GameObject explosionParticleEffect;
	MissionManager missionManager;
	GameObject spawner;
	Vector3 correctedPos;
	public float InitialHealth { get { return initialHealth; }}
	public float Health { get; private set; }
	public bool Dead { get { return Health <= 0; }}
	
	void Start()
	{
		Health = InitialHealth;

		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();
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
	
	void Die()
	{
		switch(objectIdentifier.ObjectType)
		{
		case "Player Aircraft":
			spawner = GameObject.Find("Player Spawner");
			PlayerSpawner playerSpawner = (PlayerSpawner)spawner.GetComponent(typeof(PlayerSpawner));
			playerSpawner.PlayerDeath();
			Destroy(objectIdentifier.transform.gameObject);

			if (explosionParticleEffect != null)
				Instantiate(explosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Ally Objective":
			missionManager.AllyObjectiveDestroyed(objectIdentifier.gameObject);
			Destroy(objectIdentifier.transform.gameObject);

			if (explosionParticleEffect != null)
				Instantiate(explosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Ally Aircraft":
			spawner = GameObject.Find("Ally Aircraft Spawner");
			break;
		case "Ally Defensive Aircraft":
			spawner = GameObject.Find("Ally Defensive Aircraft Spawner");
			break;
		case "Ally Vehicle":
			spawner = GameObject.Find("Ally Tank Spawner");
			break;
		case "Ally Turret":
			Destroy(objectIdentifier.transform.gameObject);

			if (explosionParticleEffect != null)
				Instantiate(explosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Enemy Objective":
			missionManager.EnemyObjectiveDestroyed(objectIdentifier.gameObject);
			Destroy(objectIdentifier.transform.gameObject);

			if (explosionParticleEffect != null)
				Instantiate(explosionParticleEffect, transform.position, transform.rotation);

			return;
		case "Enemy Aircraft Easy":
			spawner = GameObject.Find("Enemy Aircraft Easy Spawner");
			break;
		case "Enemy Defensive Aircraft Easy":
			spawner = GameObject.Find("Enemy Defensive Aircraft Easy Spawner");
			break;
		case "Enemy Aircraft Medium":
			spawner = GameObject.Find("Enemy Aircraft Medium Spawner");
			break;
		case "Enemy Defensive Aircraft Medium":
			spawner = GameObject.Find("Enemy Defensive Aircraft Medium Spawner");
			break;
		case "Enemy Aircraft Hard":
			spawner = GameObject.Find("Enemy Aircraft Hard Spawner");
			break;
		case "Enemy Defensive Aircraft Hard":
			spawner = GameObject.Find("Enemy Defensive Aircraft Hard Spawner");
			break;
		case "Enemy Vehicle":
			spawner = GameObject.Find("Enemy Tank Spawner");
			break;
		case "Enemy Turret":
			Destroy(objectIdentifier.transform.gameObject);

			if (explosionParticleEffect != null)
				Instantiate(explosionParticleEffect, transform.position, transform.rotation);

			return;
		default:
			Debug.LogError("No Case Switch Defined: " + transform.parent.name);
			break;
		}

		if (missionManager != null)
		{
			MissionObjectSpawner spawnerEnemyID = (MissionObjectSpawner)spawner.GetComponent(typeof(MissionObjectSpawner));
			spawnerEnemyID.RemoveFromList(transform.parent.name);
		}
		else
		{
			ObjectSpawner spawnerEnemyID = (ObjectSpawner)spawner.GetComponent(typeof(ObjectSpawner));
			spawnerEnemyID.RemoveFromList(transform.parent.name);
		}

		if (explosionParticleEffect != null)
			Instantiate(explosionParticleEffect, transform.position, transform.rotation);

		Destroy(objectIdentifier.transform.gameObject);
	}
}