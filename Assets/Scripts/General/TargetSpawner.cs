using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
	[SerializeField] GameObject targetPrefab;
	[SerializeField] int maxInGame = 20;
	[SerializeField] int totalRespawns = 50;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize = 5;
	[SerializeField] bool spawnTurret = false;
	int respawnNumber = 0;
	int respawnSquadCount = 1;
	bool canSpawn = true;
	int nextNameNumber = 1;
	float yPos;
	List<string> enemiesInScene;
	
	void Start()
	{
		enemiesInScene = new List<string>();
	}
	
	void Update()
	{
		if (canSpawn)
		{
			int totalEnemyCount = enemiesInScene.Count;

			if (totalEnemyCount < maxInGame)
				SpawnEnemy();
			else
				canSpawn = false;
		}
	}

	void SpawnEnemy()
	{
		respawnNumber++;
		if (respawnNumber > totalRespawns)
			return;

		if (spawnTurret)
			yPos = -7.5f;
		else
		{
			float heightRangeSelector = Random.Range(0, 2);

			if (heightRangeSelector == 0)
				yPos = Random.Range(-5, 0);
			else
				yPos = Random.Range(1, 35);
		}

		Vector3 randomPosition = new Vector3(Random.Range(-90f, 90f), yPos, Random.Range(-90f, 90f));
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		var enemyClone = Instantiate(targetPrefab, randomPosition, randomRotation);
		enemyClone.name = targetPrefab.name + " " + nextNameNumber;
		enemiesInScene.Add(enemyClone.name);
		nextNameNumber++;
	}

	public void RemoveFromList(string enemyType)
	{
		enemiesInScene.Remove(enemyType);

		if (squadSpawn)
		{
			if (respawnSquadCount < squadSpawnSize)
				respawnSquadCount++;
			else
			{
				canSpawn = true;
				respawnSquadCount = 0;
			}
		}
		else
			canSpawn = true;
	}
}
