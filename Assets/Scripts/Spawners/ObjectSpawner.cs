using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
	[SerializeField] GameObject targetPrefab;
	[SerializeField] int maxInGame = 20;
	[SerializeField] int totalRespawns = 50;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize = 5;
	[SerializeField] bool spawnGroundUnit = false;
	int respawnNumber = 0;
	int respawnSquadCount = 1;
	bool canSpawn = true;
	int nextNameNumber = 1;
	List<string> unitsInScene;
	public GameObject TargetPrefab { get { return targetPrefab; } set { targetPrefab = value; }}
	public int MaxInGame { get { return maxInGame; } set { maxInGame = value; }}
	public int TotalRespawns { get { return totalRespawns; } set { totalRespawns = value; }}
	public bool SquadSpawn { get { return squadSpawn; } set { squadSpawn = value; }}
	public int SquadSpawnSize { get { return squadSpawnSize; } set { squadSpawnSize = value; }}
	public bool SpawnGroundUnit { get { return spawnGroundUnit; } set { spawnGroundUnit = value; }}
	public int RespawnNumber { get { return respawnNumber; } set { respawnNumber = value; }}
	public int RespawnSquadCount { get { return respawnSquadCount; } set { respawnSquadCount = value; }}
	public bool CanSpawn { get { return canSpawn; } set { canSpawn = value; }}
	public int NextNameNumber { get { return nextNameNumber; } set { nextNameNumber = value; }}
	public float yPos { get; set; }
	public List<string> UnitsInScene { get { return unitsInScene; } set { unitsInScene = value; }}
	
	void Awake()
	{
		UnitsInScene = new List<string>();
	}
	
	void Update()
	{
		if (canSpawn)
		{
			int totalUnitCount = UnitsInScene.Count;

			if (totalUnitCount < MaxInGame)
				SpawnUnit();
			else
				canSpawn = false;
		}
	}

	public virtual void SpawnUnit()
	{
		respawnNumber++;
		if (respawnNumber > TotalRespawns)
			return;

		if (SpawnGroundUnit)
			yPos = -7.5f;
		else
			yPos = 0;

		Vector3 spawnPosition = new Vector3(Random.Range(-90f, 90f), yPos, Random.Range(-90f, 90f));
		Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		var unitClone = Instantiate(TargetPrefab, spawnPosition, spawnRotation);
		unitClone.name = TargetPrefab.name + " " + nextNameNumber;
		UnitsInScene.Add(unitClone.name);
		nextNameNumber++;
	}

	public void RemoveFromList(string unitType)
	{
		UnitsInScene.Remove(unitType);

		if (SquadSpawn)
		{
			if (respawnSquadCount < SquadSpawnSize)
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
