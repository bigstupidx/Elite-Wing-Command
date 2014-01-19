using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
	[SerializeField] AmmoSpawner[] ammoSpawners;
	
	public void Equip()
	{
	}
	
	public void Unequip()
	{
	}
	
	public void Fire()
	{
		foreach(var ammo in ammoSpawners)
		{
			ammo.Fire();
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
