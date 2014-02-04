using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionObjectSpawner : ObjectSpawner
{
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
			spawnPosition = new Vector3(Random.Range(spawnLocation.x - 30f, spawnLocation.x + 30f), yPos, Random.Range(spawnLocation.z - 30f, spawnLocation.z + 30f));
			spawnRotation = transform.rotation;
		}
		else
		{
			spawnPosition = new Vector3(Random.Range(spawnLocation.x - 90f, spawnLocation.x + 90f), yPos, Random.Range(spawnLocation.z - 90f, spawnLocation.z + 90f));
			spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		}

		var unitClone = Instantiate(UnitPrefab, spawnPosition, spawnRotation);
		unitClone.name = UnitPrefab.name + " " + NextNameNumber;
		UnitsInScene.Add(unitClone.name);
		NextNameNumber++;
	}
}
