using UnityEngine;
using System.Collections;

public class TotalScoreArcade : MonoBehaviour
{
	[SerializeField] UILabel totalScoreObject;
	[SerializeField] ScoreTextNumbersArcade scoreNumbersArcade;
	int totalScore;

	void Start ()
	{
		Screen.showCursor = true;
		StartCoroutine(SetTotalScore());
	}

	IEnumerator SetTotalScore()
	{
		yield return new WaitForSeconds(0.1f);
		totalScore = scoreNumbersArcade.TotalScore;
		totalScoreObject.text = totalScore.ToString("N0");

		if (Everyplay.SharedInstance.IsRecording())
			Everyplay.SharedInstance.SetMetadata("score", totalScore.ToString("N0"));

		var gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");

		if (gameCenterObject != null)
		{
			EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();
			gameCenterScript.StoreAndSubmitScore(totalScore);
		}
	}
}
