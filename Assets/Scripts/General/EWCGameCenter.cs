using UnityEngine;
using System.Collections;
// BELOW IS NEEDED FOR GAME CENTER
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class EWCGameCenter : MonoBehaviour {
	
	// THE LEADERBOARD INSTANCE
	static ILeaderboard m_Leaderboard;
	public int highScoreInt = 123;
	public string leaderboardName = "Arcade Mode High Score";
	public string leaderboardID = "arcade_high_score";
	
// THIS MAKES SURE THE GAME CENTER INTEGRATION WILL ONLY WORK WHEN OPERATING ON AN APPLE IOS DEVICE (iPHONE, iPOD TOUCH, iPAD)
//#if UNITY_IPHONE
	
	// Use this for initialization
	void Start ()
	{
		// AUTHENTICATE AND REGISTER A ProcessAuthentication CALLBACK
		// THIS CALL NEEDS OT BE MADE BEFORE WE CAN PROCEED TO OTHER CALLS IN THE Social API
        Social.localUser.Authenticate(ProcessAuthentication);
		
		// GET INSTANCE OF LEADERBOARD
		DoLeaderboard();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			Debug.Log("Arcade High Score Reset");
			PlayerPrefs.SetInt("Arcade High Score", 0);
			Debug.Log(PlayerPrefs.GetInt("Arcade High Score"));
		}
	}
	
	public void StoreAndSubmitScore(int sessionScore)
	{
		int arcadeHighScore = PlayerPrefs.GetInt("Arcade High Score", 0);
		var newRecordObject = GameObject.FindGameObjectWithTag("NewRecord");

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

			PlayerPrefs.SetInt("Arcade High Score", sessionScore);
			ReportScore(sessionScore, leaderboardID);
		}
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
            Debug.Log ("Failed to authenticate with Game Center.");
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
