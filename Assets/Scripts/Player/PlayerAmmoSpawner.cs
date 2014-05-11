using UnityEngine;
using System.Collections;

public class PlayerAmmoSpawner : AmmoSpawner
{
	[SerializeField] int weaponSlotNumber = 1;
	[SerializeField] Rigidbody allyBullet1;
	[SerializeField] Rigidbody allyBullet2;
	int weaponCaseSwitch;

	void Awake()
	{
		switch(EncryptedPlayerPrefs.GetInt("Weapon Equip " + weaponSlotNumber.ToString(), 1))
		{
		case 1:
			Weapon = allyBullet1;
			FireRate = 0.12f;
			Force = 60f;
			break;
		case 2:
			Weapon = allyBullet2;
			FireRate = 0.2f;
			Force = 50f;
			break;
		default:
			Debug.LogError("Invalid Weapon Equip: " + transform.name);
			break;
		}
	}
}
