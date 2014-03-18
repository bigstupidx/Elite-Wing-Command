using UnityEngine;
using System.Collections;

public class ArcadeTimer : MonoBehaviour
{
	[SerializeField] int totalTimeInSecs = 120;
	[SerializeField] UISlider timerSlider;
	[SerializeField] UILabel timerLabel;
	[SerializeField] PlayerSpawner playerSpawner;
	int timeRemaining;
	int timePassed;
	int minutes;
	int seconds;

	void Start()
	{
		timeRemaining = totalTimeInSecs;
		timePassed = 0;
		InvokeRepeating("UpdateTimer", 1.0f, 1.0f);
	}

	void UpdateTimer()
	{
		++timePassed;
		timeRemaining = totalTimeInSecs - timePassed;

		minutes = Mathf.FloorToInt(timeRemaining / 60F);
		seconds = Mathf.FloorToInt(timeRemaining - minutes * 60);
		
		string formattedTime = string.Format("{0:0}:{1:00}", minutes, seconds);
		timerSlider.value = (1.0f * timeRemaining) / totalTimeInSecs;
		timerLabel.text = "Time Remaining: " + formattedTime;

		if (timeRemaining <= 0)
		{
			CancelInvoke("UpdateTimer");
			playerSpawner.ForceGameOver();
		}
	}
}
