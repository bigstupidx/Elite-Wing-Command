using UnityEngine;
using System.Collections;

public class AllyAI : GenericAI
{
	AllyAircraftMovement allyAircraftMovement;
	string objectiveTarget;

	void Start()
	{
		ObjectiveTag = "AllyObjective";
		TargetTag = "Enemy";
		TargetTurretID = "Enemy Turret";
		TargetVehicleID = "Enemy Vehicle";
		StartCoroutine(FindClosestTarget());

		if (!IsGroundUnit)
		{
			allyAircraftMovement = transform.root.GetComponent<AllyAircraftMovement>();
			allyAircraftMovement.EnemyTurretID = TargetTurretID;
			allyAircraftMovement.EnemyVehicleID = TargetVehicleID;
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
			allyAircraftMovement.Search();
		}
	}
}
