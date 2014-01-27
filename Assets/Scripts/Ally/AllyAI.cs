using UnityEngine;
using System.Collections;

public class AllyAI : GenericAI
{
	AllyAircraftMovement allyAircraftMovement;

	void Start()
	{
		TargetTag = "Enemy";
		EnemyTurretID = "Enemy Turret";
		EnemyVehicleID = "Enemy Vehicle";
		StartCoroutine(FindClosestTarget());

		if (!IsGroundUnit)
		{
			allyAircraftMovement = transform.root.GetComponent<AllyAircraftMovement>();
			allyAircraftMovement.EnemyTurretID = EnemyTurretID;
			allyAircraftMovement.EnemyVehicleID = EnemyVehicleID;
		}
	}

	void Update()
	{
		if (IsGroundUnit)
			return;

		if (ClosestTarget != null && ClosestTargetDistance <= SightDistance)
		{
			allyAircraftMovement.Engage();
		}
		else
		{
			allyAircraftMovement.Wander();
		}
	}
}
