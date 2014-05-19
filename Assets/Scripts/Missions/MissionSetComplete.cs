using UnityEngine;
using System.Collections;

public class MissionSetComplete : MonoBehaviour
{

	void Start()
	{
		int missionNumber = EncryptedPlayerPrefs.GetInt("Mission Number", 0);
		EncryptedPlayerPrefs.SetInt("Mission " + missionNumber.ToString() + " Status", 1);

		var gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");

		if (missionNumber == 101)
		{
			
			if (gameCenterObject != null)
			{
				PlayerPrefs.Save();
				EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();
				gameCenterScript.SubmitAchievement("complete_first_mission", 100f);
			}
		}
		else if (missionNumber == 310)
		{
			
			if (gameCenterObject != null)
			{
				PlayerPrefs.Save();
				EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();
				gameCenterScript.SubmitAchievement("complete_all_campaign_missions", 100f);
			}
		}
	}
}
