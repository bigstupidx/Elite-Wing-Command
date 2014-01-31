using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionObjectSpawner : ObjectSpawner
{
	[SerializeField] MissionManager missionManager;
	Vector3 spawnLocation;
	Vector3 spawnPosition;
	Quaternion spawnRotation;

	void Start()
	{
		spawnLocation = transform.position;
	}

	public override void SpawnUnit()
	{
		RespawnNumber++;
		if (RespawnNumber > TotalRespawns)
			return;
		
		if (SpawnTurret)
			yPos = -7.5f;
		else
		{
			float heightRangeSelector = Random.Range(0, 2);
			
			if (heightRangeSelector == 0)
				yPos = Random.Range(-5, 0);
			else
				yPos = Random.Range(1, 35);
		}

		if (missionManager != null)
		{
			spawnPosition = new Vector3(Random.Range(spawnLocation.x - 20f, spawnLocation.x + 20f), yPos, Random.Range(spawnLocation.z - 20f, spawnLocation.z + 20f));
			spawnRotation = transform.rotation;
		}
		else
		{
			spawnPosition = new Vector3(Random.Range(spawnLocation.x - 90f, spawnLocation.x + 90f), yPos, Random.Range(spawnLocation.z - 90f, spawnLocation.z + 90f));
			spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		}

		var unitClone = Instantiate(TargetPrefab, spawnPosition, spawnRotation);
		unitClone.name = TargetPrefab.name + " " + NextNameNumber;
		UnitsInScene.Add(unitClone.name);
		NextNameNumber++;
	}
}
