using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionObjectSpawner : ObjectSpawner
{
	[SerializeField] float spawnPosX = 30f;
	[SerializeField] float spawnPosZ = 30f;
	Vector3 spawnLocation;
	Vector3 spawnPosition;
	Quaternion spawnRotation;

	void Start()
	{
		spawnLocation = transform.position;
	}

	public override void SpawnUnit()
	{
		if (RespawnNumber > TotalRespawns)
			return;
		
		RespawnNumber++;

		if (SpawnGroundUnit)
			yPos = -7.5f;

		if (MissionManagerScript != null)
		{
			spawnPosition = new Vector3(Random.Range(spawnLocation.x - spawnPosX, spawnLocation.x + spawnPosX), yPos, Random.Range(spawnLocation.z - spawnPosZ, spawnLocation.z + spawnPosZ));
			spawnRotation = transform.rotation;
		}
		else
		{
			spawnPosition = new Vector3(Random.Range(spawnLocation.x - 90f, spawnLocation.x + 90f), yPos, Random.Range(spawnLocation.z - 90f, spawnLocation.z + 90f));
			spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		}

		switch(UnitPrefab.name)
		{
		case "Ally Aircraft":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyAircraft, spawnPosition, spawnRotation);
			break;
		case "Ally Defensive Aircraft":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyDefensiveAircraft, spawnPosition, spawnRotation);
			break;
		case "Ally Tank":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyTank, spawnPosition, spawnRotation);
			break;
		case "Ally Multipurpose":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyMultipurpose, spawnPosition, spawnRotation);
			break;
		case "Ally VIP Vehicle":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyVIPVehicle, spawnPosition, spawnRotation);
			break;
		case "Ally VIP Vehicle Turret":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyVIPVehicleTurret, spawnPosition, spawnRotation);
			break;
		case "Ally VIP Vehicle Missile":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.allyUnits.allyVIPVehicleMissile, spawnPosition, spawnRotation);
			break;
		case "Enemy Aircraft Easy":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyAircraftEasy, spawnPosition, spawnRotation);
			break;
		case "Enemy Aircraft Medium":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyAircraftMedium, spawnPosition, spawnRotation);
			break;
		case "Enemy Aircraft Hard":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyAircraftHard, spawnPosition, spawnRotation);
			break;
		case "Enemy Defensive Aircraft Easy":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyDefensiveAircraftEasy, spawnPosition, spawnRotation);
			break;
		case "Enemy Defensive Aircraft Medium":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyDefensiveAircraftMedium, spawnPosition, spawnRotation);
			break;
		case "Enemy Defensive Aircraft Hard":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyDefensiveAircraftHard, spawnPosition, spawnRotation);
			break;
		case "Enemy Tank":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyTank, spawnPosition, spawnRotation);
			break;
		case "Enemy Multipurpose":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyMultipurpose, spawnPosition, spawnRotation);
			break;
		case "Enemy VIP Vehicle":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyVIPVehicle, spawnPosition, spawnRotation);
			break;
		case "Enemy VIP Vehicle Turret":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyVIPVehicleTurret, spawnPosition, spawnRotation);
			break;
		case "Enemy VIP Vehicle Missile":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyVIPVehicleMissile, spawnPosition, spawnRotation);
			break;
		case "Enemy Turret":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyTurret, spawnPosition, spawnRotation);
			break;
		case "Enemy Missile Battery":
			UnitClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.enemyUnits.enemyMissileBattery, spawnPosition, spawnRotation);
			break;
		default:
			Debug.LogError("No Correct Unit To Spawn!");
			return;
		}

		//var UnitClone = Instantiate(UnitPrefab, spawnPosition, spawnRotation);
		UnitClone.transform.name = UnitPrefab.name + " " + NextNameNumber;
		UnitsInScene.Add(UnitClone.transform.name);
		NextNameNumber++;
	}
}
