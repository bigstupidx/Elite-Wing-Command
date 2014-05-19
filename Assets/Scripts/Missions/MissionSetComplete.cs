using UnityEngine;
using System.Collections;

public class MissionSetComplete : MonoBehaviour
{

	void Start()
	{
		int missionNumber = EncryptedPlayerPrefs.GetInt("Mission Number", 0);
		EncryptedPlayerPrefs.SetInt("Mission " + missionNumber.ToString() + " Status", 1);

		if (missionNumber == 101)
		{
			var gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");
			
			if (gameCenterObject != null)
			{
				PlayerPrefs.Save();
				EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();
				gameCenterScript.SubmitAchievement("complete_first_mission", 100f);
			}
		}
	}
}
