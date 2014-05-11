using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] MissionManager missionManager;
	[SerializeField] int totalRespawns = 3;
	[SerializeField] GameObject playerPrefab2Slots;
	[SerializeField] GameObject playerPrefab3Slots;
	[SerializeField] GameObject playerPrefab4Slots;
	[SerializeField] GameObject playerPrefab5Slots;
	[SerializeField] GameObject[] playerLifeIcon;
	bool gameOver = false;
	int respawnNumber = 1;
	int playerIterations = 1;
	List<string> playerInScene;
	Vector3 spawnPosition;
	Quaternion spawnRotation;
	bool canSpawn = true;
	public bool GameOver { get { return gameOver; }}
	[SerializeField] GameObject arcadeSessionCompleteScreen;
	[SerializeField] GameObject minimapObject;
	[SerializeField] GameObject guiObject;
	FastSpawnObject playerClone;

	void Start()
	{
		playerInScene = new List<string>();

		if (missionManager != null)
		{
			spawnPosition = transform.position;
			spawnRotation = transform.rotation;
		}
		else
		{
			spawnPosition = new Vector3(0f, 0f, 0f);
			spawnRotation = Quaternion.Euler(0f, 0f, 0f);
		}

		foreach (GameObject icon in playerLifeIcon)
			icon.SetActive(true);
	}
	
	void Update()
	{
		int playerCount = playerInScene.Count;

		if (!gameOver && playerCount < playerIterations && canSpawn)
		{
			SpawnPlayer();
		}
	}

	void SpawnPlayer()
	{
		canSpawn = false;
		//var playerClone = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);

		switch(EncryptedPlayerPrefs.GetInt("Weapon Slots"))
		{
		case 2:
			playerClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.playerUnits.playerAircraft2Slots, spawnPosition, spawnRotation);
			break;
		case 3:
			playerClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.playerUnits.playerAircraft3Slots, spawnPosition, spawnRotation);
			break;
		case 4:
			playerClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.playerUnits.playerAircraft4Slots, spawnPosition, spawnRotation);
			break;
		case 5:
			playerClone = SpawnManager.SharedInstance.SpawnObject(SpawnManager.SharedInstance.playerUnits.playerAircraft5Slots, spawnPosition, spawnRotation);
			break;
		default:
			Debug.LogError("Invalid Player Spawn Prefab: " + transform.name);
			return;
		}

		playerClone.transform.name = "Player Aircraft";
		playerInScene.Add(playerClone.transform.name);
	}

	public void PlayerDeath()
	{
		if (missionManager != null)
			missionManager.PlayerLivesRemaining -= 1;

		playerInScene.Clear();

		if (respawnNumber < 3)
			playerLifeIcon[2-respawnNumber].SetActive(false);

		respawnNumber++;

		if (respawnNumber > totalRespawns)
		{
			canSpawn = false;
			gameOver = true;

			if (missionManager == null)
			{
				arcadeSessionCompleteScreen.SetActive(true);
				StartCoroutine(WaitAndPause());
			}
		}
		else
			StartCoroutine(RespawnTimer(2f));
	}

	public void ForceGameOver()
	{
		canSpawn = false;
		gameOver = true;

		if (missionManager == null)
		{
			arcadeSessionCompleteScreen.SetActive(true);
			StartCoroutine(WaitAndPause());
		}
	}

	IEnumerator RespawnTimer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		canSpawn = true;
	}

	IEnumerator WaitAndPause()
	{
		minimapObject.SetActive(false);
		guiObject.SetActive(false);
		yield return new WaitForSeconds(3.0f);
		CustomTimeManager.FadeTo(0f, 0.25f);
	}
}