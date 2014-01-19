using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour
{
	[SerializeField] GameObject playerPrefab;
	[SerializeField] int totalRespawns = 3;
	[SerializeField] EasyJoystick playerJoystick;
	int respawnNumber = 1;
	int playerIterations = 1;
	List<string> playerInScene;
	Vector3 spawnPosition;
	Quaternion spawnRotation;
	bool canSpawn = true;
	
	void Start()
	{
		playerInScene = new List<string>();
		spawnPosition = new Vector3(0f, 0f, 0f);
		spawnRotation = Quaternion.Euler(0f, 0f, 0f);
	}
	
	void Update()
	{
		int playerCount = playerInScene.Count;

		if (playerCount < playerIterations && canSpawn)
		{
			SpawnPlayer();
		}
	}

	void SpawnPlayer()
	{
		canSpawn = false;
		var playerClone = (GameObject)Instantiate(playerPrefab, spawnPosition, spawnRotation);
		playerClone.name = "Player Aircraft";
		playerJoystick.receiverGameObject = playerClone;
		playerInScene.Add(playerClone.name);
		Debug.Log("Lives Remaining: " + (4 - respawnNumber));
	}

	public void ClearList()
	{
		playerInScene.Clear();
		respawnNumber++;
		if (respawnNumber > totalRespawns)
		{
			Debug.Log("No more lives....");
			canSpawn = false;
			return;
		}

		StartCoroutine(RespawnTimer(2f));
	}

	IEnumerator RespawnTimer(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		canSpawn = true;
	}
}
