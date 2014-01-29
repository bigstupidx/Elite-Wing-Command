using UnityEngine;
using System.Collections;

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
