using UnityEngine;
using System.Collections;

public class PlayerAmmoSpawner : AmmoSpawner
{
	[SerializeField] int weaponSlotNumber = 1;
	[SerializeField] Rigidbody redBullet;
	[SerializeField] Rigidbody blueBullet;
	int weaponCaseSwitch;

	void Awake()
	{
		//weaponCaseSwitch = PlayerPrefs.GetInt("Weapon Equip " + weaponSlotNumber);

		switch(PlayerPrefs.GetInt("Weapon Equip " + weaponSlotNumber.ToString()))
		{
		case 1:
			Weapon = redBullet;
			FireRate = 0.12f;
			Force = 60f;
			break;
		case 2:
			Weapon = blueBullet;
			FireRate = 0.2f;
			Force = 50f;
			break;
		default:
			Debug.LogError("Invalid Weapon Equip: " + transform.name);
			break;
		}
	}
}
