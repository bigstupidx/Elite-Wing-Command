using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
	public enum MissionType
	{
		Base_Attack,
		Base_Defense,
		Base_vs_Base
	}

	public enum MissionDifficulty
	{
		Easy,
		Medium,
		Hard
	}

	public enum PlayerObjectivesType
	{
		Complete_Ally_Objectives,
		Prevent_Enemy_Objectives,
		Complete_and_Prevent
	}
	
	[SerializeField] PlayerSpawner playerSpawner;
	[SerializeField] float timerTime;
	public MissionType missionType;
	public MissionDifficulty missionDifficulty;
	int missionDifficultyLevel;
	public PlayerObjectivesType playerObjectivesType;
	List<GameObject> allyObjectivesInScene;
	List<GameObject> enemyObjectivesInScene;
	bool gameOver = false;
	bool timerComplete = false;
	public int MissionDifficultyLevel { get { return missionDifficultyLevel; }}
	public List<GameObject> AllyObjectivesList { get { return allyObjectivesInScene; }}
	public List<GameObject> EnemyObjectivesList { get { return enemyObjectivesInScene; }}

	void Start()
	{
		switch(missionType.ToString())
		{
		case "Base_Attack":
			BaseAttack();
			break;
		case "Base_Defense":
			BaseDefense();
			break;
		case "Base_vs_Base":
			BaseVsBase();
			break;
		}

		switch(missionDifficulty.ToString())
		{
		case "Easy":
			Debug.Log("Mission Difficulty: Easy");
			missionDifficultyLevel = 1;
			break;
		case "Medium":
			Debug.Log("Mission Difficulty: Medium");
			missionDifficultyLevel = 2;
			break;
		case "Hard":
			Debug.Log("Mission Difficulty: Hard");
			missionDifficultyLevel = 3;
			break;
		}
	}

	void Update()
	{
		timerTime -= Time.deltaTime;

//		if (missionType.ToString() == "Base_Defense" && timerTime > 0)
//			Debug.Log(timerTime);

		if (timerTime <= 0)
			timerComplete = true;

		if (!gameOver)
		{
			switch(playerObjectivesType.ToString())
			{
			case "Complete_Ally_Objectives":
				if (playerSpawner.GameOver)
				{
					Debug.Log("GAME OVER");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				else if (AllyObjectivesList.Count == 0)
				{
					Debug.Log("MISSION COMPLETE");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				break;
			case "Prevent_Enemy_Objectives":
				if (EnemyObjectivesList.Count == 0 || playerSpawner.GameOver)
				{
					Debug.Log("GAME OVER");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				else if (timerComplete)
				{
					Debug.Log("MISSION COMPLETE");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				break;
			case "Complete_and_Prevent":
				if (EnemyObjectivesList.Count == 0 || playerSpawner.GameOver)
				{
					Debug.Log("GAME OVER");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				else if (AllyObjectivesList.Count == 0)
				{
					Debug.Log("MISSION COMPLETE");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				break;
			}
		}
	}

	void BaseAttack()
	{
		allyObjectivesInScene = new List<GameObject>();
		GameObject[] objectives = GameObject.FindGameObjectsWithTag("AllyObjective");

		if (objectives.Length > 0)
		{
			foreach (GameObject objective in objectives)
			{
				allyObjectivesInScene.Add(objective);
			}
		}
		else
			Debug.LogError("No Ally Objectives!");

		Debug.Log("Remaining Ally Objectives: " + AllyObjectivesList.Count);
	}

	void BaseDefense()
	{
		enemyObjectivesInScene = new List<GameObject>();
		GameObject[] objectives = GameObject.FindGameObjectsWithTag("EnemyObjective");
		
		if (objectives.Length > 0)
		{
			foreach (GameObject objective in objectives)
			{
				enemyObjectivesInScene.Add(objective);
			}
		}
		else
			Debug.LogError("No Enemy Objectives!");
		
		Debug.Log("Remaining Enemy Objectives: " + EnemyObjectivesList.Count);
	}

	void BaseVsBase()
	{
		allyObjectivesInScene = new List<GameObject>();
		enemyObjectivesInScene = new List<GameObject>();
		GameObject[] allyObjectives = GameObject.FindGameObjectsWithTag("AllyObjective");
		GameObject[] enemyObjectives = GameObject.FindGameObjectsWithTag("EnemyObjective");

		if (allyObjectives.Length > 0)
		{
			foreach (GameObject allyObjective in allyObjectives)
			{
				allyObjectivesInScene.Add(allyObjective);
			}
		}
		else
			Debug.LogError("No Ally Objectives!");

		if (enemyObjectives.Length > 0)
		{
			foreach (GameObject enemyObjective in enemyObjectives)
			{
				enemyObjectivesInScene.Add(enemyObjective);
			}
		}
		else
			Debug.LogError("No Enemy Objectives!");

		Debug.Log("Remaining Ally Objectives: " + AllyObjectivesList.Count);
		Debug.Log("Remaining Enemy Objectives: " + EnemyObjectivesList.Count);
	}

	public void AllyObjectiveDestroyed(GameObject objectiveName)
	{
		allyObjectivesInScene.Remove(objectiveName);
		Debug.Log("Remaining Ally Objectives: " + AllyObjectivesList.Count);
	}

	public void EnemyObjectiveDestroyed(GameObject objectiveName)
	{
		enemyObjectivesInScene.Remove(objectiveName);
		Debug.Log("Remaining Enemy Objectives: " + EnemyObjectivesList.Count);
	}

	IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(0);
	}
}