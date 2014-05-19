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
			PlayerPrefs.Save();
			EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();

			gameCenterScript.StoreAndSubmitScore(totalScore);
			gameCenterScript.SubmitAchievement("complete_arcade_mode_round", 100f);

			float totalAirUnitsDestroyed = EncryptedPlayerPrefs.GetFloat("Total Air Units Destroyed", 0f);
			gameCenterScript.SubmitAchievement("destroy_5000_air_units", (totalAirUnitsDestroyed/5000f * 100f));
			
			float totalGroundUnitsDestroyed = EncryptedPlayerPrefs.GetFloat("Total Ground Units Destroyed", 0f);
			gameCenterScript.SubmitAchievement("destroy_1000_ground_units", (totalGroundUnitsDestroyed/1000f * 100f));
		}
	}
}
