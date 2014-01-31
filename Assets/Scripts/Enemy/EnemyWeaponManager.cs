using UnityEngine;
using System.Collections;

public class EnemyWeaponManager : GenericWeaponManager
{
	[SerializeField] EnemyAI enemyAI;

	void Start()
	{
		EnemyTurretID = enemyAI.TargetTurretID;
		EnemyVehicleID = enemyAI.TargetVehicleID;
	}

	void FixedUpdate()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
		ClosestTargetID = enemyAI.ClosestTargetID;
	}
}
