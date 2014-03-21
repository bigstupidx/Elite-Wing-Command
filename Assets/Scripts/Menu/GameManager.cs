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
		Screen.showCursor = true;
		CustomTimeManager.FadeTo(1.1f, 0.01f);

		if (PlayerPrefs.GetInt("First Load", 1) == 1)
	    {
			SetDefaultPrefs();
			PlayerPrefs.SetInt("First Load", 0);
			PlayerPrefs.Save();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.O))
		{
			SetDefaultPrefs();
			Debug.Log("Reset to default weapons");
		}
		else if (Input.GetKeyDown(KeyCode.U))
		{
			float currentRP = PlayerPrefs.GetFloat("Reward Points", 0);
			PlayerPrefs.SetFloat("Reward Points", currentRP + 1000f);
			rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString() + " RP";
			PlayerPrefs.Save();
			Debug.Log("Added 1000 RP");
		}
		else if (Input.GetKeyDown(KeyCode.I))
		{
			PlayerPrefs.SetFloat("Reward Points", 0);
			Debug.Log("RP set to 0");
		}
	}

	void SetDefaultPrefs()
	{
		PlayerPrefs.SetInt("Player Weapon Level", 0);
		PlayerPrefs.SetInt("Weapon Slots", 2);
		PlayerPrefs.SetInt("Weapon Equip 1", 1);
		PlayerPrefs.SetInt("Weapon Equip 2", 1);
		PlayerPrefs.SetInt("First Load", 0);
		PlayerPrefs.Save();
	}
}
