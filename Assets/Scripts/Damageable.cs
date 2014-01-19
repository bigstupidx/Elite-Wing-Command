using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour
{
	[SerializeField] float initialHealth;
	[SerializeField] ObjectIdentifier objectIdentifier;
	[SerializeField] bool groundUnit = false;
	public float InitialHealth { get { return initialHealth; } }
	public float Health { get; private set; }
	public bool Dead { get { return Health <= 0; }}
	Vector3 correctedPos;
	
	void Start()
	{
		Health = InitialHealth;

		if (!groundUnit)
		{
			correctedPos = new Vector3(0f, -transform.root.position.y, 0f);
			transform.localPosition = correctedPos;
		}
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
	
	protected void Die()
	{
		if (objectIdentifier != null)
		{
			if (objectIdentifier.ObjectType == "Enemy Aircraft Easy")
			{
				var spawner = GameObject.Find("Enemy Aircraft Easy Spawner");
				TargetSpawner spawnerEnemyID = (TargetSpawner)spawner.GetComponent(typeof(TargetSpawner));
				spawnerEnemyID.RemoveFromList(transform.root.name);
			}
			else if (objectIdentifier.ObjectType == "Enemy Aircraft Medium")
			{
				GameObject spawner = GameObject.Find("Enemy Aircraft Medium Spawner");
				TargetSpawner spawnerEnemyID = (TargetSpawner)spawner.GetComponent(typeof(TargetSpawner));
				spawnerEnemyID.RemoveFromList(transform.root.name);
			}
			else if (objectIdentifier.ObjectType == "Enemy Aircraft Hard")
			{
				GameObject spawner = GameObject.Find("Enemy Aircraft Hard Spawner");
				TargetSpawner spawnerEnemyID = (TargetSpawner)spawner.GetComponent(typeof(TargetSpawner));
				spawnerEnemyID.RemoveFromList(transform.root.name);
			}
			else if (objectIdentifier.ObjectType == "Enemy Turret")
			{
				GameObject spawner = GameObject.Find("Enemy Turret Spawner");
				TargetSpawner spawnerEnemyID = (TargetSpawner)spawner.GetComponent(typeof(TargetSpawner));
				spawnerEnemyID.RemoveFromList(transform.root.name);
			}
			else if (objectIdentifier.ObjectType == "Ally Aircraft")
			{
				GameObject spawner = GameObject.Find("Ally Aircraft Spawner");
				TargetSpawner spawnerEnemyID = (TargetSpawner)spawner.GetComponent(typeof(TargetSpawner));
				spawnerEnemyID.RemoveFromList(transform.root.name);
			}
			else if (objectIdentifier.ObjectType == "Player")
			{
				GameObject spawner = GameObject.Find("Player Spawner");
				PlayerSpawner playerSpawner = (PlayerSpawner)spawner.GetComponent(typeof(PlayerSpawner));
				playerSpawner.ClearList();
			}
		}

		Destroy(transform.root.gameObject);
	}
}