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

		var unitClone = Instantiate(UnitPrefab, spawnPosition, spawnRotation);
		unitClone.name = UnitPrefab.name + " " + NextNameNumber;
		UnitsInScene.Add(unitClone.name);
		NextNameNumber++;
	}
}
