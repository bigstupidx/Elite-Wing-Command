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
			UnitPrefab = spawnDetails[arrayNumber - 1].UnitPrefab;
			MaxInGame = spawnDetails[arrayNumber - 1].MaxInGame;
			TotalRespawns = spawnDetails[arrayNumber - 1].TotalRespawns;
			SquadSpawnSize = spawnDetails[arrayNumber - 1].SquadSpawnSize;
			SpawnGroundUnit = spawnDetails[arrayNumber - 1].SpawnGroundUnit;
		}
		else
		{
			UnitPrefab = spawnDetails[0].UnitPrefab;
			MaxInGame = spawnDetails[0].MaxInGame;
			TotalRespawns = spawnDetails[0].TotalRespawns;
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
		{
			if (missionManager != null)
				yPos = -7.5f;
			else
				yPos = -7.75f;
		}
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
}

[System.Serializable]
public class SpawnDetails
{
	[SerializeField] string name;
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int maxInGame = 20;
	[SerializeField] int totalRespawns = 50;
	[SerializeField] int squadSpawnSize = 5;
	[SerializeField] bool spawnGroundUnit = false;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int MaxInGame { get { return maxInGame; }}
	public int TotalRespawns { get { return totalRespawns; }}
	public int SquadSpawnSize { get { return squadSpawnSize; }}
	public bool SpawnGroundUnit { get { return spawnGroundUnit; }}
}
