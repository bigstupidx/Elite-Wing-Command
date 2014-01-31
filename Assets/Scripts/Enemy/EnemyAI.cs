using UnityEngine;
using System.Collections;

public class EnemyAI : GenericAI
{
	EnemyAircraftMovement enemyAircraftMovement;
	
	void Start()
	{
		TargetTag = "Ally";
		TargetTurretID = "Ally Turret";
		TargetVehicleID = "Ally Vehicle";
		StartCoroutine(FindClosestTarget());

		if (!IsGroundUnit)
		{
			enemyAircraftMovement = transform.root.GetComponent<EnemyAircraftMovement>();
			enemyAircraftMovement.EnemyTurretID = TargetTurretID;
			enemyAircraftMovement.EnemyVehicleID = TargetVehicleID;
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
			enemyAircraftMovement.Search();
		}

	}
}
