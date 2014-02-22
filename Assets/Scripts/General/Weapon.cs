using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[SerializeField] AmmoSpawner[] ammoSpawners;
	[SerializeField] bool fireRandomSpawner = false;
	
	public void Equip()
	{
	}
	
	public void Unequip()
	{
	}
	
	public void Fire()
	{
		if (fireRandomSpawner)
		{
			int randomSpawner = Random.Range(0, ammoSpawners.Length);
			ammoSpawners[randomSpawner].Fire();
		}
		else
		{
			foreach(var ammo in ammoSpawners)
			{
				ammo.Fire();
			}
		}
	}
	
	public void Stop()
	{
		foreach(var ammo in ammoSpawners)
		{
			ammo.Stop();
		}
	}
}
