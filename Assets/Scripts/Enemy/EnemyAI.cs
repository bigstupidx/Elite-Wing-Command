using UnityEngine;
using System.Collections;

public class EnemyAI : GenericAI
{
	EnemyAircraftMovement enemyAircraftMovement;
	EnemyVehicleMovement enemyVehicleMovement;
	string objectiveTarget;
	
	void Start()
	{
		ObjectiveAirTag = "EnemyAirObjective";
		ObjectiveGroundTag = "EnemyGroundObjective";
		TargetTag = "Ally";
		TargetTurretID = "Ally Turret";
		TargetVehicleID = "Ally Vehicle";
		StartCoroutine(FindClosestTarget());

		if (IsAirUnit)
		{
			enemyAircraftMovement = transform.GetComponent<EnemyAircraftMovement>();
			enemyAircraftMovement.EnemyTurretID = TargetTurretID;
			enemyAircraftMovement.EnemyVehicleID = TargetVehicleID;
		}
		else if (IsGroundUnit && !IsStationaryUnit)
		{
			enemyVehicleMovement = transform.GetComponent<EnemyVehicleMovement>();
			enemyVehicleMovement.EnemyTurretID = TargetTurretID;
			enemyVehicleMovement.EnemyVehicleID = TargetVehicleID;
		}
	}

	void Update()
	{
		if(IsAirUnit)
		{
			if (ClosestTarget != null)
			{
				enemyAircraftMovement.Engage();
			}
			else
			{
				enemyAircraftMovement.Search();
			}
		}
		else if (IsGroundUnit && !IsStationaryUnit)
		{
			if (ClosestTarget != null)
			{
				enemyVehicleMovement.Engage();
			}
			else
			{
				enemyVehicleMovement.Search();
			}
		}
	}
}
