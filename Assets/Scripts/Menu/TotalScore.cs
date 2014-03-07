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
		totalScoreObject.text = totalScore.ToString();
	}
}
