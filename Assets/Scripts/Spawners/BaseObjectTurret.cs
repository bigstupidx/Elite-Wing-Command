using UnityEngine;
using System.Collections;

public class BaseObjectTurret : MonoBehaviour
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int difficultyLevel = 1;
	[SerializeField] bool inverseDifficulty;
	MissionManager missionManager;
	int missionDifficultyValue;
	Vector3 spawnLocation;
	int spawnNumber = 0;
	float yPos = -7.5f;
	
	void Start()
	{
		GameObject missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");

		if (missionManagerObject != null)
		{
			missionManager = missionManagerObject.GetComponent<MissionManager>();
			missionDifficultyValue = missionManager.MissionDifficultyLevel;
			StartCoroutine(SpawnBaseUnit());
		}
	}
	
	IEnumerator SpawnBaseUnit()
	{
		while (missionDifficultyValue == 0)
		{
			missionDifficultyValue = missionManager.MissionDifficultyLevel;
			yield return null;
		}

		if (!inverseDifficulty && difficultyLevel <= missionDifficultyValue)
		{
			spawnLocation = new Vector3(transform.position.x, yPos, transform.position.z);
			GameObject unitClone = (GameObject)Instantiate(unitPrefab, spawnLocation, transform.rotation);
			DisableTurretArcadeScripts disableTurretArcadeScripts = unitClone.GetComponent<DisableTurretArcadeScripts>();
			
			if (disableTurretArcadeScripts != null)
				disableTurretArcadeScripts.IsMission = true;
			
			unitClone.name = unitPrefab.name + " " + spawnNumber;
			spawnNumber++;
		}
		else if (inverseDifficulty && difficultyLevel >= missionDifficultyValue)
		{
			spawnLocation = new Vector3(transform.position.x, yPos, transform.position.z);
			GameObject unitClone = (GameObject)Instantiate(unitPrefab, spawnLocation, transform.rotation);
			DisableTurretArcadeScripts disableTurretArcadeScripts = unitClone.GetComponent<DisableTurretArcadeScripts>();
			
			if (disableTurretArcadeScripts != null)
				disableTurretArcadeScripts.IsMission = true;
			
			unitClone.name = unitPrefab.name + " " + spawnNumber;
			spawnNumber++;
		}
	}
}
