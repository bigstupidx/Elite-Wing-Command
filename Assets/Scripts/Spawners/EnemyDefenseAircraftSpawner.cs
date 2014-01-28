using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDefenseAircraftSpawner : TargetSpawner
{
	[SerializeField] MissionManager missionManager;
	[SerializeField] int unitArrayNumber;
	Vector3 spawnLocation;
	int respawnNumber = 0;
	int nextNameNumber = 1;
	float yPos;

	void Start()
	{
		spawnLocation = transform.position;
		TargetPrefab = missionManager.enemyDefenseAircraft[unitArrayNumber].UnitPrefab;
		MaxInGame = missionManager.enemyDefenseAircraft[unitArrayNumber].MaxInGame;
		TotalRespawns = missionManager.enemyDefenseAircraft[unitArrayNumber].TotalRespawns;
		SquadSpawn = missionManager.enemyDefenseAircraft[unitArrayNumber].SquadSpawn;
		SquadSpawnSize = missionManager.enemyDefenseAircraft[unitArrayNumber].SquadSpawnSize;
	}

	public override void SpawnUnit()
	{
		respawnNumber++;
		if (respawnNumber > TotalRespawns)
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
		
		Vector3 randomPosition = new Vector3(Random.Range(spawnLocation.x - 10f, spawnLocation.x + 10f), yPos, Random.Range(spawnLocation.z - 10f, spawnLocation.z + 10f));
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		var enemyClone = Instantiate(TargetPrefab, randomPosition, randomRotation);
		enemyClone.name = TargetPrefab.name + " " + nextNameNumber;
		EnemiesInScene.Add(enemyClone.name);
		nextNameNumber++;
	}
}
