using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	[SerializeField] MissionManager missionManager;
	[SerializeField] Collider damageableCollider;
	[SerializeField] float initialHealth;
	[SerializeField] ObjectIdentifier objectIdentifier;
	GameObject spawner;
	Vector3 correctedPos;
	public float InitialHealth { get { return initialHealth; }}
	public float Health { get; private set; }
	public bool Dead { get { return Health <= 0; }}
	
	void Start()
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
	
	void Die()
	{
		switch(objectIdentifier.ObjectType)
		{
		case "Player Aircraft":
			spawner = GameObject.Find("Player Spawner");
			PlayerSpawner playerSpawner = (PlayerSpawner)spawner.GetComponent(typeof(PlayerSpawner));
			playerSpawner.PlayerDeath();
			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Ally Aircraft":
			spawner = GameObject.Find("Ally Aircraft Spawner");
			break;
		case "Ally Objective":
			missionManager.AllyObjectiveDestroyed(objectIdentifier.transform.name);
			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Enemy Aircraft Easy":
			spawner = GameObject.Find("Enemy Aircraft Easy Spawner");
			break;
		case "Enemy Aircraft Medium":
			spawner = GameObject.Find("Enemy Aircraft Medium Spawner");
			break;
		case "Enemy Aircraft Hard":
			spawner = GameObject.Find("Enemy Aircraft Hard Spawner");
			break;
		case "Enemy Turret":
			Destroy(objectIdentifier.transform.gameObject);
			return;
		case "Enemy Missile Battery":
			Destroy(objectIdentifier.transform.gameObject);
			return;
		default:
			Debug.LogError("No Case Switch Defined: " + transform.parent.name);
			break;
		}

		ObjectSpawner spawnerEnemyID = (ObjectSpawner)spawner.GetComponent(typeof(ObjectSpawner));
		spawnerEnemyID.RemoveFromList(transform.parent.name);
		Destroy(objectIdentifier.transform.gameObject);
	}
}