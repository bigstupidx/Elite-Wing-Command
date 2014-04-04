using UnityEngine;
using System.Collections;

public class ScoreTextNumbers : MonoBehaviour
{
	[SerializeField] UILabel missionScoreObject;
	MissionManager missionManager;
	PlayerSpawner playerSpawner;
	int enemyAirDestroyed;
	int enemyGroundDestroyed;
	int missionObjectivesBonus;
	int playerLivesBonus;
	int missionDifficultyBonus;
	int totalScore = 0;
	public int TotalScore { get { return totalScore; }}
	
	void Start()
	{
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		missionManager = missionManagerObject.GetComponent<MissionManager>();
		enemyAirDestroyed = missionManager.EnemyAirDestroyed;
		enemyGroundDestroyed = missionManager.EnemyGroundDestroyed;

		if (missionManager.playerObjectivesType.ToString() == "Prevent_Enemy_Objectives" || missionManager.playerObjectivesType.ToString() == "Protect_VIP")
			missionObjectivesBonus = missionManager.MissionObjectivesRemaining;
		else
			missionObjectivesBonus = missionManager.MissionObjectivesDestroyed - 1;

		if (missionObjectivesBonus < 1)
			missionObjectivesBonus = 1;

		playerLivesBonus = missionManager.PlayerLivesRemaining;

		if (playerLivesBonus < 1)
			playerLivesBonus = 1;

		missionDifficultyBonus = missionManager.MissionDifficultyLevel;

		totalScore = ((enemyAirDestroyed + enemyGroundDestroyed) * missionObjectivesBonus) * playerLivesBonus * missionDifficultyBonus;

		string missionScores = "\n" + enemyAirDestroyed + "\n\n" + enemyGroundDestroyed + "\n\nx" + missionObjectivesBonus + "\n\nx" + playerLivesBonus + "\n\nx" + missionDifficultyBonus;
		missionScoreObject.text = missionScores;
	}
}
