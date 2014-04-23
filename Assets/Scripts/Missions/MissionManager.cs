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
	int totalAllyObjectives;
	int totalEnemyObjectives;
	UISlider objectivesRemainingSlider;
	UILabel objectivesRemainingLabel;
	int enemyAirDestroyed = 0;
	int enemyGroundDestroyed = 0;
	int missionObjectivesDestroyed = 0;
	int missionObjectivesRemaining = 0;
	int playerLivesRemaining = 3;
	public int MissionDifficultyLevel { get { return missionDifficultyLevel; }}
	public List<GameObject> AllyObjectivesList { get { return allyObjectivesInScene; }}
	public List<GameObject> EnemyObjectivesList { get { return enemyObjectivesInScene; }}
	public bool VIPDestinationReached { get { return vipDestinationReached; } set { vipDestinationReached = value; }}
	public int EnemyAirDestroyed { get { return enemyAirDestroyed; } set { enemyAirDestroyed = value; }}
	public int EnemyGroundDestroyed { get { return enemyGroundDestroyed; } set { enemyGroundDestroyed = value; }}
	public int MissionObjectivesDestroyed { get { return missionObjectivesDestroyed; } set { missionObjectivesDestroyed = value; }}
	public int MissionObjectivesRemaining { get { return missionObjectivesRemaining; } set { missionObjectivesRemaining = value; }}
	public int PlayerLivesRemaining { get { return playerLivesRemaining; } set { playerLivesRemaining = value; }}
	[SerializeField] GameObject missionCompleteScreen;
	[SerializeField] GameObject missionFailedScreen;
	[SerializeField] GameObject minimapObject;
	[SerializeField] GameObject guiObject;
	[SerializeField] UILabel missionTimer;
	int totalTime;
	int timeRemaining;
	int timePassed;
	int minutes;
	int seconds;

	void Awake()
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
			missionDifficultyLevel = 1;
			break;
		case "Medium":
			missionDifficultyLevel = 2;
			break;
		case "Hard":
			missionDifficultyLevel = 3;
			break;
		}

		GameObject ObjectivesRemainingSliderObject = GameObject.FindGameObjectWithTag("ObjectivesSlider");
		objectivesRemainingSlider = ObjectivesRemainingSliderObject.GetComponent<UISlider>();
		GameObject ObjectivesRemainingLabelObject = GameObject.FindGameObjectWithTag("ObjectivesLabel");
		objectivesRemainingLabel = ObjectivesRemainingLabelObject.GetComponent<UILabel>();
	}

	void Start()
	{
		if (missionTimer != null)
		{
			totalTime = (int)timerTime;
			timeRemaining = totalTime;
			timePassed = 0;
			InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
		}
	}

	void Update()
	{
		timerTime -= Time.deltaTime;

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
						missionFailedScreen.SetActive(true);
						StartCoroutine(WaitAndPause());
						gameOver = true;
					}
					else if (AllyObjectivesList.Count == 0)
					{
						missionCompleteScreen.SetActive(true);
						StartCoroutine(WaitAndPause());
						gameOver = true;
					}
				}
				break;
			case "Prevent_Enemy_Objectives":
				if (EnemyObjectivesList != null)
				{
					if (EnemyObjectivesList.Count == 0 || playerSpawner.GameOver)
					{
						missionFailedScreen.SetActive(true);
						StartCoroutine(WaitAndPause());
						gameOver = true;
					}
					else if (timerComplete)
					{
						missionCompleteScreen.SetActive(true);
						StartCoroutine(WaitAndPause());
						gameOver = true;
					}
				}
				break;
			case "Complete_and_Prevent":
				if (AllyObjectivesList != null && EnemyObjectivesList != null)
				{
					if (EnemyObjectivesList.Count == 0 || playerSpawner.GameOver)
					{
						missionFailedScreen.SetActive(true);
						StartCoroutine(WaitAndPause());
						gameOver = true;
					}
					else if (AllyObjectivesList.Count == 0)
					{
						missionCompleteScreen.SetActive(true);
						StartCoroutine(WaitAndPause());
						gameOver = true;
					}
				}
				break;
			case "Destroy_VIP":
				if (!firstVIPSpawned)
					return;

				if (VIPDestinationReached || playerSpawner.GameOver)
				{
					missionFailedScreen.SetActive(true);
					StartCoroutine(WaitAndPause());
					gameOver = true;
				}
				else if (AllyObjectivesList.Count == 0)
				{
					missionCompleteScreen.SetActive(true);
					StartCoroutine(WaitAndPause());
					gameOver = true;
				}
				break;
			case "Protect_VIP":
				if (!firstVIPSpawned)
					return;

				if (EnemyObjectivesList.Count == 0 || playerSpawner.GameOver)
				{
					missionFailedScreen.SetActive(true);
					StartCoroutine(WaitAndPause());
					gameOver = true;
				}
				else if (VIPDestinationReached)
				{
					missionCompleteScreen.SetActive(true);
					StartCoroutine(WaitAndPause());
					gameOver = true;
				}
				break;
			}
		}
	}

	IEnumerator BaseAttack()
	{
		yield return new WaitForSeconds(0.01f);
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

		totalAllyObjectives = AllyObjectivesList.Count;
		objectivesRemainingSlider.numberOfSteps = AllyObjectivesList.Count;
		MissionObjectivesDestroyed = 0;
		objectivesRemainingSlider.value = (float)MissionObjectivesDestroyed / (float)totalAllyObjectives;
		objectivesRemainingLabel.text = "Objectives Destroyed: " + MissionObjectivesDestroyed + "/" + totalAllyObjectives;
	}

	IEnumerator BaseDefense()
	{
		yield return new WaitForSeconds(0.01f);
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

		totalEnemyObjectives = EnemyObjectivesList.Count;
		objectivesRemainingSlider.numberOfSteps = EnemyObjectivesList.Count;
		MissionObjectivesRemaining = EnemyObjectivesList.Count;
		objectivesRemainingSlider.value = (float)MissionObjectivesRemaining / (float)totalEnemyObjectives;
		objectivesRemainingLabel.text = "Objectives Remaining: " + MissionObjectivesRemaining + "/" + totalEnemyObjectives;
	}

	IEnumerator BaseVsBase()
	{
		yield return new WaitForSeconds(0.1f);
		allyObjectivesInScene = new List<GameObject>();
		enemyObjectivesInScene = new List<GameObject>();
		GameObject[] allyAirObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
		GameObject[] allyGroundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
		GameObject[] allyObjectives = allyAirObjectives.Concat(allyGroundObjectives).ToArray();
		GameObject[] enemyAirObjectives = GameObject.FindGameObjectsWithTag("EnemyAirObjective");
		GameObject[] enemyGroundObjectives = GameObject.FindGameObjectsWithTag("EnemyGroundObjective");
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

		totalAllyObjectives = AllyObjectivesList.Count;
		objectivesRemainingSlider.numberOfSteps = AllyObjectivesList.Count;
		MissionObjectivesDestroyed = 0;
		objectivesRemainingSlider.value = (float)MissionObjectivesDestroyed / (float)totalAllyObjectives;
		objectivesRemainingLabel.text = "Objectives Destroyed: " + MissionObjectivesDestroyed + "/" + totalAllyObjectives;
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

		totalAllyObjectives = AllyObjectivesList.Count;
		objectivesRemainingSlider.numberOfSteps = AllyObjectivesList.Count;
		MissionObjectivesDestroyed = 0;
		objectivesRemainingSlider.value = (float)MissionObjectivesDestroyed / (float)totalAllyObjectives;
		objectivesRemainingLabel.text = "Objectives Destroyed: " + MissionObjectivesDestroyed + "/" + totalAllyObjectives;
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

		totalEnemyObjectives = EnemyObjectivesList.Count;
		objectivesRemainingSlider.numberOfSteps = EnemyObjectivesList.Count;
		MissionObjectivesRemaining = EnemyObjectivesList.Count;
		objectivesRemainingSlider.value = (float)MissionObjectivesRemaining / (float)totalEnemyObjectives;
		objectivesRemainingLabel.text = "Objectives Remaining: " + MissionObjectivesRemaining + "/" + totalEnemyObjectives;
		firstVIPSpawned = true;
	}

	public void AllyObjectiveDestroyed(GameObject objectiveName)
	{
		allyObjectivesInScene.Remove(objectiveName);
		MissionObjectivesDestroyed = totalAllyObjectives - AllyObjectivesList.Count;
		objectivesRemainingSlider.value = (float)MissionObjectivesDestroyed / (float)totalAllyObjectives;

		if (MissionObjectivesDestroyed == totalAllyObjectives)
			objectivesRemainingLabel.text = " ";
		else
			objectivesRemainingLabel.text = "Objectives Destroyed: " + MissionObjectivesDestroyed + "/" + totalAllyObjectives;
	}

	public void EnemyObjectiveDestroyed(GameObject objectiveName)
	{
		enemyObjectivesInScene.Remove(objectiveName);
		MissionObjectivesRemaining = EnemyObjectivesList.Count;
		objectivesRemainingSlider.value = (float)MissionObjectivesRemaining / (float)totalEnemyObjectives;
		
		if (MissionObjectivesRemaining == 0)
			objectivesRemainingLabel.text = " ";
		else
			objectivesRemainingLabel.text = "Objectives Remaining: " + MissionObjectivesRemaining + "/" + totalEnemyObjectives;
	}

	IEnumerator WaitAndPause()
	{
		minimapObject.SetActive(false);
		guiObject.SetActive(false);
		yield return new WaitForSeconds(3.0f);
		CustomTimeManager.FadeTo(0f, 0.25f);
	}

	void UpdateTimer()
	{
		++timePassed;
		timeRemaining = totalTime - timePassed;
		
		minutes = Mathf.FloorToInt(timeRemaining / 60F);
		seconds = Mathf.FloorToInt(timeRemaining - minutes * 60);
		
		string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
		missionTimer.text = formattedTime;
		
		if (timeRemaining <= 0)
			CancelInvoke("UpdateTimer");
	}
}