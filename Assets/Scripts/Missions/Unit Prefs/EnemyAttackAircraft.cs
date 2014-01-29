using UnityEngine;
using System.Collections;

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
