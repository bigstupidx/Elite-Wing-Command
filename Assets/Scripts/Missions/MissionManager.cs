using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour
{
	public AllyAttackAircraft[] allyAttackAircraft;
	public AllyDefenseAircraft[] allyDefenseAircraft;
	public AllyAntiAir[] allyAntiAir;
	public AllyTanks[] allyTanks;
	public EnemyAttackAircraft[] enemyAttackAircraft;
	public EnemyDefenseAircraft[] enemyDefenseAircraft;
	public EnemyAntiAir[] enemyAntiAir;
	public EnemyTanks[] enemyTanks;
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

[System.Serializable]
public class EnemyAttackAircraft
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
public class EnemyDefenseAircraft
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
public class EnemyAntiAir
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
public class EnemyTanks
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