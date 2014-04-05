using UnityEngine;
using System.Collections;

public class ScoreTextNumbersArcade : MonoBehaviour
{
	[SerializeField] UILabel arcadeScoreObject;
	ArcadeStatHolder arcadeStatHolder;
	int enemyAirDestroyed;
	int enemyGroundDestroyed;
	int timeElapsed;
	int totalScore = 0;
	public int TotalScore { get { return totalScore; }}
	
	void Start()
	{
		var arcadeStatHolderObject = GameObject.FindGameObjectWithTag("ArcadeStatHolder");
		arcadeStatHolder = arcadeStatHolderObject.GetComponent<ArcadeStatHolder>();
		enemyAirDestroyed = arcadeStatHolder.EnemyAirDestroyed;
		enemyGroundDestroyed = arcadeStatHolder.EnemyGroundDestroyed;
		timeElapsed = arcadeStatHolder.TimeElapsed;
		totalScore = (enemyAirDestroyed + enemyGroundDestroyed) * timeElapsed;

		string arcadeScores = "\n\n" + enemyAirDestroyed + "\n\n" + enemyGroundDestroyed + "\n\nx" + timeElapsed;
		arcadeScoreObject.text = arcadeScores;
	}
}
