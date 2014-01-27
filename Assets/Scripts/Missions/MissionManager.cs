using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour
{
	public AllyAttackAircraft[] allyAttackAircraft;

	void Start()
	{
	
	}

	void Update()
	{
	
	}
}

[System.Serializable]
public class AllyAttackAircraft
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int maxInGame;
	[SerializeField] int totalRespawns;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int MaxInGame { get { return maxInGame; }}
	public int TotalRespawns { get { return totalRespawns; }}
	public bool SquadSpawn { get { return squadSpawn; }}
	public int SquadSpawnSize { get { return squadSpawnSize; }}
}

[System.Serializable]
public class AllyDefenseAircraft
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int maxInGame;
	[SerializeField] int totalRespawns;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize;
	[SerializeField] Transform defendTarget;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int MaxInGame { get { return maxInGame; }}
	public int TotalRespawns { get { return totalRespawns; }}
	public bool SquadSpawn { get { return squadSpawn; }}
	public int SquadSpawnSize { get { return squadSpawnSize; }}
	public Transform DefendTarget { get { return defendTarget; }}
}

[System.Serializable]
public class AllyAntiAir
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int maxInGame;
	[SerializeField] int totalRespawns;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize;
	[SerializeField] bool defensiveUnit;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int MaxInGame { get { return maxInGame; }}
	public int TotalRespawns { get { return totalRespawns; }}
	public bool SquadSpawn { get { return squadSpawn; }}
	public int SquadSpawnSize { get { return squadSpawnSize; }}
	public bool DefensiveUnit { get { return defensiveUnit; }}
}

[System.Serializable]
public class AllyTanks
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int maxInGame;
	[SerializeField] int totalRespawns;
	[SerializeField] bool squadSpawn;
	[SerializeField] int squadSpawnSize;
	[SerializeField] bool defensiveUnit;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int MaxInGame { get { return maxInGame; }}
	public int TotalRespawns { get { return totalRespawns; }}
	public bool SquadSpawn { get { return squadSpawn; }}
	public int SquadSpawnSize { get { return squadSpawnSize; }}
	public bool DefensiveUnit { get { return defensiveUnit; }}
}