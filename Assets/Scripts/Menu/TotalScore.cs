using UnityEngine;
using System.Collections;

public class TotalScore : MonoBehaviour
{
	[SerializeField] UILabel totalScoreObject;
	[SerializeField] ScoreTextNumbers scoreNumbers;
	int totalScore;

	void Start ()
	{
		StartCoroutine(SetTotalScore());
	}

	IEnumerator SetTotalScore()
	{
		yield return new WaitForSeconds(0.1f);
		totalScore = scoreNumbers.TotalScore;
		PlayerPrefs.SetInt("Reward Points", PlayerPrefs.GetInt("Reward Points", 0) + totalScore);
		totalScoreObject.text = totalScore.ToString();
	}
}
