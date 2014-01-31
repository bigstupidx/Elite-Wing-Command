using UnityEngine;
using System.Collections;

public class AllyWeaponManager : GenericWeaponManager
{
	[SerializeField] AllyAI allyAI;

	void FixedUpdate()
	{
		if (EnemyTurretID == null || EnemyVehicleID == null)
		{
			EnemyTurretID = allyAI.TargetTurretID;
			EnemyVehicleID = allyAI.TargetVehicleID;
		}

		ObjectiveTag = allyAI.ObjectiveTag;
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
		ClosestTargetID = allyAI.ClosestTargetID;
	}
}
