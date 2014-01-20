using UnityEngine;
using System.Collections;

public class DisplayHealth : MonoBehaviour
{
	[SerializeField] TextMesh textDisplay;
	Damageable playerDamageable;
	float previousHealth;
	
	void Start()
	{
		previousHealth = 100f;
	}
	
	void Update()
	{
		GameObject player = GameObject.Find("Player Aircraft");

		if (player != null)
		{
			playerDamageable = (PlayerDamageable)player.GetComponentInChildren(typeof(PlayerDamageable));

			if (previousHealth != playerDamageable.Health || playerDamageable.Health == 100f)
			{
				if (playerDamageable.Dead)
					textDisplay.text = "Player destroyed";
				else
				{
					textDisplay.text = playerDamageable.Health.ToString("F0");
					previousHealth = playerDamageable.Health;
				}
			}
		}
		else
			textDisplay.text = "Player destroyed";
	}
}
