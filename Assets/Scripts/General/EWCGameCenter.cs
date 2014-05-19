using UnityEngine;
using System.Collections;
// BELOW IS NEEDED FOR GAME CENTER
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class EWCGameCenter : MonoBehaviour {
	
	// THE LEADERBOARD INSTANCE
	static ILeaderboard m_Leaderboard;

	public string leaderboardName = "Arcade Mode High Score";
	public string leaderboardID = "arcade_high_score";

	public string achievement1Name = "complete_arcade_mode_round";
	public string achievement2Name = "complete_first_mission";
	public string achievement3Name = "purchase_an_upgrade";
	public string achievement4Name = "destroy_5000_air_units";
	public string achievement5Name = "destroy_1000_ground_units";
	public string achievement6Name = "complete_all_campaign_missions";

	GameObject newRecordObject;
	
// THIS MAKES SURE THE GAME CENTER INTEGRATION WILL ONLY WORK WHEN OPERATING ON AN APPLE IOS DEVICE (iPHONE, iPOD TOUCH, iPAD)
//#if UNITY_IPHONE
	
	// Use this for initialization
	void Start()
	{
		// AUTHENTICATE AND REGISTER A ProcessAuthentication CALLBACK
		// THIS CALL NEEDS OT BE MADE BEFORE WE CAN PROCEED TO OTHER CALLS IN THE Social API
        Social.localUser.Authenticate(ProcessAuthentication);
		
		// GET INSTANCE OF LEADERBOARD
		DoLeaderboard();

		newRecordObject = GameObject.FindGameObjectWithTag("NewRecord");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log("Arcade High Score Reset");
			EncryptedPlayerPrefs.SetInt("Arcade High Score", 0);
			Debug.Log(EncryptedPlayerPrefs.GetInt("Arcade High Score"));
			PlayerPrefs.Save();
		}
	}

	public void SubmitAchievement(string achievementID, double progress)
	{
		ReportAchievement(achievementID, progress);
	}
	
	public void StoreAndSubmitScore(int sessionScore)
	{
		int arcadeHighScore = EncryptedPlayerPrefs.GetInt("Arcade High Score", 0);

		if (sessionScore > arcadeHighScore)
		{
			if (newRecordObject != null)
			{
				Debug.Log("Enabling New Record Label");
				UILabel newRecordLabel = newRecordObject.GetComponent<UILabel>();
				newRecordLabel.enabled = true;
				TweenAlpha newRecordTween = newRecordObject.GetComponent<TweenAlpha>();
				newRecordTween.enabled = true;
			}

			EncryptedPlayerPrefs.SetInt("Arcade High Score", sessionScore);
			PlayerPrefs.Save();
		}

		ReportScore(EncryptedPlayerPrefs.GetInt("Arcade High Score"), leaderboardID);
    }
	
	///////////////////////////////////////////////////
	// INITAL AUTHENTICATION (MUST BE DONE FIRST)
	///////////////////////////////////////////////////
	
	// THIS FUNCTION GETS CALLED WHEN AUTHENTICATION COMPLETES
	// NOTE THAT IF THE OPERATION IS SUCCESSFUL Social.localUser WILL CONTAIN DATA FROM THE GAME CENTER SERVER
    void ProcessAuthentication(bool success)
	{
        if(success)
		{
            Debug.Log ("Authenticated");
			Application.LoadLevel(1);
			
			Social.LoadScores(leaderboardName, scores => {
    			if (scores.Length > 0) {
					// SHOW THE SCORES RECEIVED
        			Debug.Log ("Received " + scores.Length + " scores");
        			string myScores = "Leaderboard: \n";
        			foreach (IScore score in scores)
            			myScores += "\t" + score.userID + " " + score.formattedValue + " " + score.date + "\n";
        			Debug.Log (myScores);
    			}
    			else
        			Debug.Log ("No scores have been loaded.");
				});
        }
        else
		{
            Debug.Log ("Failed to authenticate with Game Center.");
			Application.LoadLevel(1);
		}
    }
	
	///////////////////////////////////////////////////
	// GAME CENTER ACHIEVEMENT INTEGRATION
	///////////////////////////////////////////////////
	
	void ReportAchievement(string achievementId, double progress)
	{
		Social.ReportProgress(achievementId, progress, (result) => {
			Debug.Log(result ? string.Format("Successfully reported achievement {0}", achievementId) 
			          : string.Format("Failed to report achievement {0}", achievementId));
		});
	}

	#region Game Center Integration
	///////////////////////////////////////////////////
	// GAME CENTER LEADERBOARD INTEGRATION
	///////////////////////////////////////////////////
	
	/// <summary>
	/// Reports the score to the leaderboards.
	/// </summary>
	/// <param name="score">Score.</param>
	/// <param name="leaderboardID">Leaderboard I.</param>
	void ReportScore(long score, string leaderboardID)
	{
    	Debug.Log ("Reporting score " + score + " on leaderboard " + leaderboardID);
    	Social.ReportScore (score, leaderboardID, success =>
		                    { Debug.Log(success ? "Reported score to leaderboard successfully" : "Failed to report score");});
	}
	
	/// <summary>
	/// Get the leaderboard.
	/// </summary>
	void DoLeaderboard()
	{
    	m_Leaderboard = Social.CreateLeaderboard();
    	m_Leaderboard.id = leaderboardID;  // YOUR CUSTOM LEADERBOARD NAME
    	m_Leaderboard.LoadScores(result => DidLoadLeaderboard(result));
	}

	/// <summary>
	/// RETURNS THE NUMBER OF LEADERBOARD SCORES THAT WERE RECEIVED BY THE APP
	/// </summary>
	/// <param name="result">If set to <c>true</c> result.</param>
	void DidLoadLeaderboard(bool result)
	{
    	Debug.Log("Received " + m_Leaderboard.scores.Length + " scores");
    	foreach (IScore score in m_Leaderboard.scores)
		{
        	Debug.Log(score);
		}
	}
	#endregion
}

//#endif
