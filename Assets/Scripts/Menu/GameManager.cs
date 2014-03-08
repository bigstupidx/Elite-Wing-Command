using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	int weaponSlots;
	int weaponEquip1;
	int weaponEquip2;
	int weaponEquip3;
	int weaponEquip4;
	int weaponEquip5;

	void Start()
	{
		Screen.showCursor = true;
		CustomTimeManager.FadeTo(1.1f, 0.01f);

		if (PlayerPrefs.GetInt("First Load", 1) == 1)
	    {
			SetDefaultPrefs();
			PlayerPrefs.SetInt("First Load", 0);
		}
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.I))
		{
			Debug.Log("Weapon Slots: " + PlayerPrefs.GetInt("Weapon Slots"));

			for (int i = 1; i <= PlayerPrefs.GetInt("Weapon Slots"); i++)
				Debug.Log("Weapon Equip " + i + ": " + PlayerPrefs.GetInt("Weapon Equip " + i.ToString()));
		}
		else if (Input.GetKeyDown(KeyCode.O))
		{
			PlayerPrefs.DeleteAll();
			Debug.Log("Player Prefs Reset");
			SetDefaultPrefs();
		}
	}

	void SetDefaultPrefs()
	{
		PlayerPrefs.SetInt("Weapon Slots", 2);
		PlayerPrefs.SetInt("Weapon Equip 1", 1);
		PlayerPrefs.SetInt("Weapon Equip 2", 1);
		PlayerPrefs.SetInt("Weapon Equip 3", 1);
		PlayerPrefs.SetInt("Weapon Equip 4", 1);
		PlayerPrefs.SetInt("Weapon Equip 5", 1);
		PlayerPrefs.SetInt("First Load", 0);
	}
}
