using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseObjectSpawner : MonoBehaviour
{
	[SerializeField] MissionManager missionManager;
	[SerializeField] BaseObject[] baseObject;
	[SerializeField] bool inverseDifficulty;
	Vector3 spawnLocation;
	int respawnNumber = 0;
	float yPos = -7.5f;

	public void Start()
	{
		int missionDifficultyValue = missionManager.MissionDifficultyLevel;

		foreach (BaseObject unit in baseObject)
		{
			if (!inverseDifficulty && unit.DifficultyLevel <= missionDifficultyValue)
			{
				spawnLocation = new Vector3(unit.transform.position.x, yPos, unit.transform.position.z);
				GameObject unitClone = (GameObject)Instantiate(unit.UnitPrefab, spawnLocation, unit.transform.rotation);
				DisableTurretArcadeScripts disableTurretArcadeScripts = unitClone.GetComponent<DisableTurretArcadeScripts>();
				
				if (disableTurretArcadeScripts != null)
					disableTurretArcadeScripts.IsMission = true;

				unitClone.name = unit.UnitPrefab.name + " " + respawnNumber;
				respawnNumber++;
			}
			else if (inverseDifficulty && unit.DifficultyLevel >= missionDifficultyValue)
			{
				spawnLocation = new Vector3(unit.transform.position.x, yPos, unit.transform.position.z);
				GameObject unitClone = (GameObject)Instantiate(unit.UnitPrefab, spawnLocation, unit.transform.rotation);
				DisableTurretArcadeScripts disableTurretArcadeScripts = unitClone.GetComponent<DisableTurretArcadeScripts>();
				
				if (disableTurretArcadeScripts != null)
					disableTurretArcadeScripts.IsMission = true;
				
				unitClone.name = unit.UnitPrefab.name + " " + respawnNumber;
				respawnNumber++;
			}
		}
	}
}
