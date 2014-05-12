using UnityEngine;
using System.Collections;

public class MapBoundary : MonoBehaviour
{
	[SerializeField] float countdownTimerValue = 4.0f;
	[SerializeField] GameObject countdownPanel;
	[SerializeField] UILabel timerLabel;
	GameObject playerAircraft;
	PlayerDamageable playerDamageable;
	float countdownTimer;
	bool runTimer = false;

	void Start()
	{
		countdownTimer = countdownTimerValue;
	}

	void OnTriggerEnter(Collider other)
	{
		playerDamageable = other.GetComponentInChildren<PlayerDamageable>();
		
		if (playerDamageable != null)
		{
			runTimer = false;
			countdownPanel.SetActive(false);
			countdownTimer = countdownTimerValue;
		}

		playerDamageable = null;
	}

	void OnTriggerExit(Collider other)
	{
		playerDamageable = other.GetComponentInChildren<PlayerDamageable>();

		if (playerDamageable != null)
		{
			runTimer = true;
			countdownPanel.SetActive(true);
		}
	}

	void Update()
	{
		if (runTimer)
		{
			countdownTimer -= Time.deltaTime;
			timerLabel.text = countdownTimer.ToString("N2");
		}

		if (countdownTimer <= 0f)
		{
			timerLabel.text = "0.00";

			if (playerAircraft == null)
				playerAircraft = GameObject.Find("Player Aircraft");

			if (playerAircraft != null)
			{
				PlayerDamageable playerDamageable = playerAircraft.GetComponentInChildren<PlayerDamageable>();

				if (playerDamageable != null && Application.loadedLevel == 3)
				{
					Vector3 tempPosition = new Vector3(0, 0, 0);
					playerDamageable.transform.parent.position = tempPosition;
				}
				else if (playerDamageable != null)
					playerDamageable.ApplyDamage(100f);

				runTimer = false;
				countdownTimer = countdownTimerValue;
			}
		}
	}
}
