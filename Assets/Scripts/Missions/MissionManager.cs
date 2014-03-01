using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class MissionManager : MonoBehaviour
{
	public enum MissionType
	{
		Base_Attack,
		Base_Defense,
		Base_vs_Base,
		VIP_Attack,
		VIP_Defense
	}

	public enum PlayerObjectivesType
	{
		Complete_Ally_Objectives,
		Prevent_Enemy_Objectives,
		Complete_and_Prevent,
		Destroy_VIP,
		Protect_VIP
	}

	public enum MissionDifficulty
	{
		Easy,
		Medium,
		Hard
	}

	[SerializeField] PlayerSpawner playerSpawner;
	[SerializeField] float timerTime;
	public MissionType missionType;
	public PlayerObjectivesType playerObjectivesType;
	int missionDifficultyLevel;
	public MissionDifficulty missionDifficulty;
	List<GameObject> allyObjectivesInScene;
	List<GameObject> enemyObjectivesInScene;
	bool gameOver = false;
	bool timerComplete = false;
	bool firstVIPSpawned = false;
	bool vipDestinationReached = false;
	public int MissionDifficultyLevel { get { return missionDifficultyLevel; }}
	public List<GameObject> AllyObjectivesList { get { return allyObjectivesInScene; }}
	public List<GameObject> EnemyObjectivesList { get { return enemyObjectivesInScene; }}
	public bool VIPDestinationReached { get { return vipDestinationReached; } set { vipDestinationReached = value; }}

	void Start()
	{
		switch(missionType.ToString())
		{
		case "Base_Attack":
			StartCoroutine(BaseAttack());
			break;
		case "Base_Defense":
			StartCoroutine(BaseDefense());
			break;
		case "Base_vs_Base":
			StartCoroutine(BaseVsBase());
			break;
		case "VIP_Attack":
			StartCoroutine(VIPAttack());
			break;
		case "VIP_Defense":
			StartCoroutine(VIPDefense());
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
				if (AllyObjectivesList != null)
				{
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
				}
				break;
			case "Prevent_Enemy_Objectives":
				if (EnemyObjectivesList != null)
				{
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
				}
				break;
			case "Complete_and_Prevent":
				if (AllyObjectivesList != null && EnemyObjectivesList != null)
				{
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
				}
				break;
			case "Destroy_VIP":
				if (!firstVIPSpawned)
					return;

				if (VIPDestinationReached || playerSpawner.GameOver)
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
			case "Protect_VIP":
				if (!firstVIPSpawned)
					return;

				if (EnemyObjectivesList.Count == 0 || playerSpawner.GameOver)
				{
					Debug.Log("GAME OVER");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				else if (VIPDestinationReached)
				{
					Debug.Log("MISSION COMPLETE");
					StartCoroutine(LoadMenu());
					gameOver = true;
				}
				break;
			}
		}
	}

	IEnumerator BaseAttack()
	{
		yield return new WaitForSeconds(0.1f);
		allyObjectivesInScene = new List<GameObject>();
		GameObject[] airObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
		GameObject[] groundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
		GameObject[] objectives = airObjectives.Concat(groundObjectives).ToArray();
		
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

	IEnumerator BaseDefense()
	{
		yield return new WaitForSeconds(0.1f);
		enemyObjectivesInScene = new List<GameObject>();
		GameObject[] airObjectives = GameObject.FindGameObjectsWithTag("EnemyAirObjective");
		GameObject[] groundObjectives = GameObject.FindGameObjectsWithTag("EnemyGroundObjective");
		GameObject[] objectives = airObjectives.Concat(groundObjectives).ToArray();
		
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

	IEnumerator BaseVsBase()
	{
		yield return new WaitForSeconds(0.1f);
		allyObjectivesInScene = new List<GameObject>();
		enemyObjectivesInScene = new List<GameObject>();
		GameObject[] allyAirObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
		GameObject[] allyGroundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
		GameObject[] allyObjectives = allyAirObjectives.Concat(allyGroundObjectives).ToArray();
		GameObject[] enemyAirObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
		GameObject[] enemyGroundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
		GameObject[] enemyObjectives = enemyAirObjectives.Concat(enemyGroundObjectives).ToArray();

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

	IEnumerator VIPAttack()
	{
		yield return new WaitForSeconds(0.1f);
		allyObjectivesInScene = new List<GameObject>();
		GameObject[] airObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
		GameObject[] groundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
		GameObject[] objectives = airObjectives.Concat(groundObjectives).ToArray();
		
		if (objectives.Length > 0)
		{
			foreach (GameObject objective in objectives)
			{
				allyObjectivesInScene.Add(objective);
			}
		}
		else
			Debug.LogError("No Ally Objectives!");
		
		Debug.Log("Remaining Enemy Objectives: " + AllyObjectivesList.Count);
		firstVIPSpawned = true;
	}

	IEnumerator VIPDefense()
	{
		yield return new WaitForSeconds(0.1f);
		enemyObjectivesInScene = new List<GameObject>();
		GameObject[] airObjectives = GameObject.FindGameObjectsWithTag("EnemyAirObjective");
		GameObject[] groundObjectives = GameObject.FindGameObjectsWithTag("EnemyGroundObjective");
		GameObject[] objectives = airObjectives.Concat(groundObjectives).ToArray();
		
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
		firstVIPSpawned = true;
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