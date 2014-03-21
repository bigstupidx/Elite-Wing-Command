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
		PlayerPrefs.SetFloat("Reward Points", PlayerPrefs.GetFloat("Reward Points", 0) + totalScore);
		PlayerPrefs.Save();
		totalScoreObject.text = totalScore.ToString();
	}
}
