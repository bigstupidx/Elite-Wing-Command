using UnityEngine;
using System.Collections;

public class AllyTurretRotate : GenericTurretRotate
{
	[SerializeField] AllyAI allyAI;

	void Start()
	{
		AllyWeaponManager allyWeaponManager = transform.GetComponent<AllyWeaponManager>();

		if (allyWeaponManager != null)
			NeedsClearShot = allyWeaponManager.NeedsClearShot;
	}

	void Update()
	{
		ClosestTarget = allyAI.ClosestTarget;
	}
}