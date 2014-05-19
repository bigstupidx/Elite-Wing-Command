﻿using UnityEngine;
using System.Collections;

public class UpgradeAllyGroundWeapons : MonoBehaviour
{
	[SerializeField] UpgradesContainer upgradesContainer;
	[SerializeField] UILabel upgradeNameLabel;
	[SerializeField] UILabel upgradeCostLabel;
	[SerializeField] UISlider upgradeSlider;
	[SerializeField] Collider buttonCollider;
	[SerializeField] UILabel rewardPointsLabel;
	[SerializeField] GameObject purchasePrompt;

	void OnEnable()
	{
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}

	void OnClick()
	{
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyGroundWeaponUpgradeCost)
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Purchase", Fabric.EventAction.PlaySound);
			UpgradeWeapons();
		}
		else
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Error", Fabric.EventAction.PlaySound);
			purchasePrompt.SetActive(true);
		}
	}

	void UpdateLabels()
	{
		if (upgradesContainer.AllyGroundWeaponLevel < 6)
		{
			upgradeCostLabel.text = upgradesContainer.AllyGroundWeaponUpgradeCost.ToString("N0") + " RP";
		}
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.AllyGroundWeaponUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((EncryptedPlayerPrefs.GetInt("Ally Ground Weapon Level", 0) * 1.0f) + 1) / 7f;
		rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyGroundWeaponUpgradeCost)
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
		var gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");
		
		if (gameCenterObject != null)
		{
			PlayerPrefs.Save();
			EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();
			gameCenterScript.SubmitAchievement("purchase_an_upgrade", 100f);
		}

		float currentRP = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.AllyGroundWeaponUpgradeCost;
		EncryptedPlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = EncryptedPlayerPrefs.GetInt("Ally Ground Weapon Level", 0);
		int newLevel = currentLevel + 1;
		EncryptedPlayerPrefs.SetInt("Ally Ground Weapon Level", newLevel);

		switch(newLevel)
		{
		case 1:
			EncryptedPlayerPrefs.SetFloat("Ally Ground Weapon Multiplier", 1.4f);
			break;
		case 2:
			EncryptedPlayerPrefs.SetFloat("Ally Ground Weapon Multiplier", 1.7f);
			break;
		case 3:
			EncryptedPlayerPrefs.SetFloat("Ally Ground Weapon Multiplier", 2f);
			break;
		case 4:
			EncryptedPlayerPrefs.SetFloat("Ally Ground Weapon Multiplier", 2.4f);
			break;
		case 5:
			EncryptedPlayerPrefs.SetFloat("Ally Ground Weapon Multiplier", 2.7f);
			break;
		case 6:
			EncryptedPlayerPrefs.SetFloat("Ally Ground Weapon Multiplier", 3.0f);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradeAllyGroundWeaponLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
