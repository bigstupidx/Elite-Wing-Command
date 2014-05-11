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
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Ally Multipurpose":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Ally VIP Vehicle":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Ally VIP Vehicle Turret":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Ally VIP Vehicle Missile":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
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
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Enemy Multipurpose":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Enemy VIP Vehicle":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Enemy VIP Vehicle Turret":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Enemy VIP Vehicle Missile":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Enemy Turret":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
		case "Enemy Missile Battery":
			UnitObject = (GameObject)Instantiate(UnitPrefab, spawnPosition, spawnRotation);
			UnitObject.name = UnitPrefab.name + " " + NextNameNumber;
			UnitsInScene.Add(UnitObject.name);
			NextNameNumber++;
			return;
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
