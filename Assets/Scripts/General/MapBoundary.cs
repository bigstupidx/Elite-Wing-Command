using UnityEngine;
using System.Collections;

public class MapBoundary : MonoBehaviour
{
	[SerializeField] float countdownTimerValue = 3.0f;
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
			countdownTimer = countdownTimerValue;
		}

		playerDamageable = null;
	}

	void OnTriggerExit(Collider other)
	{
		playerDamageable = other.GetComponentInChildren<PlayerDamageable>();

		if (playerDamageable != null)
			runTimer = true;
	}

	void Update()
	{
		if (runTimer)
		{
			countdownTimer -= Time.deltaTime;
			Debug.Log("Map Boundary Timer: " + countdownTimer);
		}

		if (countdownTimer <= 0f)
		{
			GameObject[] allyUnits = GameObject.FindGameObjectsWithTag("Ally");

			foreach (GameObject ally in allyUnits)
			{
				PlayerDamageable playerDamageable = ally.GetComponentInChildren<PlayerDamageable>();

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
