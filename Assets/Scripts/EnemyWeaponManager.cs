using UnityEngine;
using System.Collections;

public class EnemyWeaponManager : GenericWeaponManager
{
	[SerializeField] EnemyAI enemyAI;

	void Start()
	{
		EnemyTurretID = enemyAI.EnemyTurretID;
		EnemyVehicleID = enemyAI.EnemyVehicleID;
	}

	void FixedUpdate()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
		ClosestTargetID = enemyAI.ClosestTargetID;
	}
}
