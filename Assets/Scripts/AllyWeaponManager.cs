using UnityEngine;
using System.Collections;

public class AllyWeaponManager : GenericWeaponManager
{
	[SerializeField] AllyAI allyAI;

	void FixedUpdate()
	{
		if (EnemyTurretID == null || EnemyVehicleID == null)
		{
			EnemyTurretID = allyAI.EnemyTurretID;
			EnemyVehicleID = allyAI.EnemyVehicleID;
		}

		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
		ClosestTargetID = allyAI.ClosestTargetID;
	}
}
