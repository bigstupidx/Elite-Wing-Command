using UnityEngine;
using System.Collections;

public class DisplayHealth : MonoBehaviour
{
	[SerializeField] GUIText guiTextDisplay;
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
					guiText.text = "Player destroyed";
				else
				{
					guiTextDisplay.text = playerDamageable.Health.ToString("F0");
					previousHealth = playerDamageable.Health;
				}
			}
		}
		else
			guiText.text = "Player destroyed";
	}
}
