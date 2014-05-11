using UnityEngine;
using System.Collections;

public class SpawnManager : FastSpawnManager
{
	// Singleton
	private static SpawnManager sharedInstance;
	public static SpawnManager SharedInstance
	{
		get
		{
			if(sharedInstance == null)
			{
				sharedInstance = (SpawnManager)FindObjectOfType(typeof(SpawnManager));
			}

			return sharedInstance;
		}
	}

	public void OnApplicationQuit()
	{
		sharedInstance = null;
	}

	[System.Serializable]
	public class PlayerUnits
	{
		public FastSpawnObject playerAircraft2Slots;
		public FastSpawnObject playerAircraft3Slots;
		public FastSpawnObject playerAircraft4Slots;
		public FastSpawnObject playerAircraft5Slots;
	}

	[System.Serializable]
	public class AllyUnits
	{
		public FastSpawnObject allyAircraft;
		public FastSpawnObject allyDefensiveAircraft;
	}

	[System.Serializable]
	public class EnemyUnits
	{
		public FastSpawnObject enemyAircraftEasy;
		public FastSpawnObject enemyAircraftMedium;
		public FastSpawnObject enemyAircraftHard;
		public FastSpawnObject enemyDefensiveAircraftEasy;
		public FastSpawnObject enemyDefensiveAircraftMedium;
		public FastSpawnObject enemyDefensiveAircraftHard;
	}

	public PlayerUnits playerUnits;
	public AllyUnits allyUnits;
	public EnemyUnits enemyUnits;

	void Start()
	{
		LoadObjects(playerUnits, allyUnits, enemyUnits);
	}
}