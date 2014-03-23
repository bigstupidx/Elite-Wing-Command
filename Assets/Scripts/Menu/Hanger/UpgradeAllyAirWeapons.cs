﻿using UnityEngine;
using System.Collections;

public class UpgradeAllyAirWeapons : MonoBehaviour
{
	[SerializeField] UpgradesContainer upgradesContainer;
	[SerializeField] UILabel upgradeNameLabel;
	[SerializeField] UILabel upgradeCostLabel;
	[SerializeField] UISlider upgradeSlider;
	[SerializeField] Collider buttonCollider;
	[SerializeField] UILabel rewardPointsLabel;

	void OnEnable()
	{
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}

	void OnClick()
	{
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyAirWeaponUpgradeCost)
			UpgradeWeapons();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.AllyAirWeaponLevel < 6)
			upgradeCostLabel.text = upgradesContainer.AllyAirWeaponUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.AllyAirWeaponUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((PlayerPrefs.GetInt("Ally Air Weapon Level", 0) * 1.0f) + 1) / 7f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyAirWeaponUpgradeCost)
		{
			upgradeNameLabel.color = Color.white;
			upgradeCostLabel.color = Color.white;
		}
		else
		{
			upgradeNameLabel.color = Color.red;
			upgradeCostLabel.color = Color.red;
		}
	}

	void UpgradeWeapons()
	{
		float currentRP = PlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.AllyAirWeaponUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Ally Air Weapon Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Ally Air Weapon Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetFloat("Ally Air Weapon Multiplier", 1.4f);
			break;
		case 2:
			PlayerPrefs.SetFloat("Ally Air Weapon Multiplier", 1.7f);
			break;
		case 3:
			PlayerPrefs.SetFloat("Ally Air Weapon Multiplier", 2f);
			break;
		case 4:
			PlayerPrefs.SetFloat("Ally Air Weapon Multiplier", 2.4f);
			break;
		case 5:
			PlayerPrefs.SetFloat("Ally Air Weapon Multiplier", 2.7f);
			break;
		case 6:
			PlayerPrefs.SetFloat("Ally Air Weapon Multiplier", 3.0f);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradeAllyAirWeaponLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
