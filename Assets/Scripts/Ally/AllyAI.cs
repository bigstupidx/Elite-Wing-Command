using UnityEngine;
using System.Collections;

public class AllyAI : GenericAI
{
	AllyAircraftMovement allyAircraftMovement;
	AllyVehicleMovement allyVehicleMovement;
	string objectiveTarget;

	void Start()
	{
		ObjectiveAirTag = "AllyAirObjective";
		ObjectiveGroundTag = "AllyGroundObjective";
		TargetTag = "Enemy";
		TargetTurretID = "Enemy Turret";
		TargetVehicleID = "Enemy Vehicle";
		StartCoroutine(FindClosestTarget());

		if (IsAirUnit)
		{
			allyAircraftMovement = transform.GetComponent<AllyAircraftMovement>();
			allyAircraftMovement.EnemyTurretID = TargetTurretID;
			allyAircraftMovement.EnemyVehicleID = TargetVehicleID;
		}
		else if (IsGroundUnit && !IsStationaryUnit)
		{
			allyVehicleMovement = transform.GetComponent<AllyVehicleMovement>();
			allyVehicleMovement.EnemyTurretID = TargetTurretID;
			allyVehicleMovement.EnemyVehicleID = TargetVehicleID;
		}
	}

	void Update()
	{
		if(IsAirUnit)
		{
			if (ClosestTarget != null)
			{
				allyAircraftMovement.Engage();
			}
			else
			{
				allyAircraftMovement.Search();
			}
		}
		else if (IsGroundUnit && !IsStationaryUnit)
		{
			if (ClosestTarget != null)
			{
				//Debug.Log("Closest Target: " + ClosestTarget);
				allyVehicleMovement.Engage();
			}
			else
			{
				//Debug.Log("Searching....");
				allyVehicleMovement.Search();
			}
		}
	}
}
