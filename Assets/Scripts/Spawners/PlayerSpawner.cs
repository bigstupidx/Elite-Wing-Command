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
	GameObject playerPrefab;
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

	void Awake()
	{
		switch(EncryptedPlayerPrefs.GetInt("Weapon Slots"))
		{
		case 2:
			playerPrefab = playerPrefab2Slots;
			break;
		case 3:
			playerPrefab = playerPrefab3Slots;
			break;
		case 4:
			playerPrefab = playerPrefab4Slots;
			break;
		case 5:
			playerPrefab = playerPrefab5Slots;
			break;
		default:
			Debug.LogError("Invalid Player Spawn Prefab: " + transform.name);
			playerPrefab = playerPrefab2Slots;
			break;
		}
	}
	
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
		var playerClone = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
		playerClone.name = "Player Aircraft";
		playerInScene.Add(playerClone.name);
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