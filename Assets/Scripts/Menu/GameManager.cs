using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	[SerializeField] UILabel rewardPointsLabel;
	int weaponSlots;
	int weaponEquip1;
	int weaponEquip2;
	int weaponEquip3;
	int weaponEquip4;
	int weaponEquip5;

	void Start()
	{
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		Screen.showCursor = true;

		if (EncryptedPlayerPrefs.GetInt("First Load", 1) == 1)
	    {
			SetDefaultPrefs();
			EncryptedPlayerPrefs.SetInt("First Load", 0);
			PlayerPrefs.Save();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			PlayerPrefs.DeleteAll();
			SetDefaultPrefs();
			Debug.Log("Player Prefs Reset");
		}
		else if (Input.GetKeyDown(KeyCode.I))
		{
			float currentRP = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
			EncryptedPlayerPrefs.SetFloat("Reward Points", currentRP + 1000f);
			rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
			PlayerPrefs.Save();
			Debug.Log("Added 1000 RP");
		}
	}

	void SetDefaultPrefs()
	{
		EncryptedPlayerPrefs.SetInt("Player Weapon Level", 0);
		EncryptedPlayerPrefs.SetInt("Weapon Slots", 2);
		EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 1);
		EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
		EncryptedPlayerPrefs.SetInt("First Load", 0);
		PlayerPrefs.Save();
	}
}
