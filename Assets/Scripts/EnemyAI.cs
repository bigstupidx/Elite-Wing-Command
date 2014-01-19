using UnityEngine;
using System.Collections;

public class EnemyAI : GenericAI
{
	EnemyAircraftMovement enemyAircraftMovement;
	
	void Start()
	{
		TargetTag = "Ally";
		EnemyTurretID = "Ally Turret";
		EnemyVehicleID = "Ally Vehicle";
		StartCoroutine(FindClosestTarget());

		if (!IsGroundUnit)
		{
			enemyAircraftMovement = transform.root.GetComponent<EnemyAircraftMovement>();
			enemyAircraftMovement.EnemyTurretID = EnemyTurretID;
			enemyAircraftMovement.EnemyVehicleID = EnemyVehicleID;
		}
	}

	void Update()
	{
		if (IsGroundUnit)
			return;
		
		if (ClosestTarget != null && ClosestTargetDistance <= SightDistance)
		{
			enemyAircraftMovement.Engage();
		}
		else
		{
			enemyAircraftMovement.Wander();
		}

	}
}
