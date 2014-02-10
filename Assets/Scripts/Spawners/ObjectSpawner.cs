using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
	[SerializeField] MissionManager missionManager;
	[SerializeField] SpawnDetails[] spawnDetails;
	GameObject unitPrefab;
	int maxInGame;
	int totalRespawns;
	bool squadSpawn;
	int squadSpawnSize;
	bool spawnGroundUnit = false;
	int respawnNumber;
	int respawnSquadCount = 1;
	bool canSpawn = true;
	int nextNameNumber = 1;
	List<string> unitsInScene;
	public MissionManager MissionManagerScript { get { return missionManager; }}
	public GameObject UnitPrefab { get { return unitPrefab; } set { unitPrefab = value; }}
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

		if (missionManager != null)
		{
			int arrayNumber = missionManager.MissionDifficultyLevel;
			UnitPrefab = spawnDetails[arrayNumber].UnitPrefab;
			MaxInGame = spawnDetails[arrayNumber].MaxInGame;
			TotalRespawns = spawnDetails[arrayNumber].TotalRespawns;
			SquadSpawn = spawnDetails[arrayNumber].SquadSpawn;
			SquadSpawnSize = spawnDetails[arrayNumber].SquadSpawnSize;
			SpawnGroundUnit = spawnDetails[arrayNumber].SpawnGroundUnit;
		}
		else
		{
			UnitPrefab = spawnDetails[0].UnitPrefab;
			MaxInGame = spawnDetails[0].MaxInGame;
			TotalRespawns = spawnDetails[0].TotalRespawns;
			SquadSpawn = spawnDetails[0].SquadSpawn;
			SquadSpawnSize = spawnDetails[0].SquadSpawnSize;
			SpawnGroundUnit = spawnDetails[0].SpawnGroundUnit;
		}
	}
	
	void Update()
	{
		int totalUnitCount = UnitsInScene.Count;

		if (canSpawn)
		{
			if (totalUnitCount < MaxInGame)
				SpawnUnit();
			else
				canSpawn = false;
		}
	}

	public virtual void SpawnUnit()
	{
		if (respawnNumber > TotalRespawns)
			return;

		respawnNumber++;

		if (SpawnGroundUnit)
			yPos = -7.5f;
		else
			yPos = 0;

		Vector3 spawnPosition = new Vector3(Random.Range(-90f, 90f), yPos, Random.Range(-90f, 90f));
		Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		var unitClone = Instantiate(UnitPrefab, spawnPosition, spawnRotation);
		unitClone.name = UnitPrefab.name + " " + nextNameNumber;
		UnitsInScene.Add(unitClone.name);
		nextNameNumber++;
	}

	public void RemoveFromList(string unitType)
	{
		UnitsInScene.Remove(unitType);

		if (SquadSpawn)
		{
			if (respawnSquadCount < SquadSpawnSize)
			{
				respawnSquadCount++;
			}
			else
			{
				canSpawn = true;
				respawnSquadCount = 1;
			}
		}
		else
			canSpawn = true;
	}
}

[System.Serializable]
public class SpawnDetails
{
	[SerializeField] string name;
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int maxInGame = 20;
	[SerializeField] int totalRespawns = 50;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize = 5;
	[SerializeField] bool spawnGroundUnit = false;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int MaxInGame { get { return maxInGame; }}
	public int TotalRespawns { get { return totalRespawns; }}
	public bool SquadSpawn { get { return squadSpawn; }}
	public int SquadSpawnSize { get { return squadSpawnSize; }}
	public bool SpawnGroundUnit { get { return spawnGroundUnit; }}
}
