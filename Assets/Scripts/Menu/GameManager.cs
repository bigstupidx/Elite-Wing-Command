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
	}

	void SetDefaultPrefs()
	{
		EncryptedPlayerPrefs.SetFloat("Reward Points", EncryptedPlayerPrefs.GetFloat("Reward Points", 0) + 35000f);
		EncryptedPlayerPrefs.SetInt("Player Weapon Level", 0);
		EncryptedPlayerPrefs.SetInt("Weapon Slots", 2);
		EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 1);
		EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
		EncryptedPlayerPrefs.SetInt("First Load", 0);
		PlayerPrefs.Save();
	}
}
