using UnityEngine;
using System.Collections;

public class UpgradePlayerWeapons : MonoBehaviour
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
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerWeaponUpgradeCost)
			UpgradeWeapons();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerWeaponLevel < 8)
			upgradeCostLabel.text = upgradesContainer.PlayerWeaponUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerWeaponUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((EncryptedPlayerPrefs.GetInt("Player Weapon Level", 0) * 1.0f) + 1) / 9f;
		rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerWeaponUpgradeCost)
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
		float currentRP = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerWeaponUpgradeCost;
		EncryptedPlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = EncryptedPlayerPrefs.GetInt("Player Weapon Level", 0);
		int newLevel = currentLevel + 1;
		EncryptedPlayerPrefs.SetInt("Player Weapon Level", newLevel);

		switch(newLevel)
		{
		case 1:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 3);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 1);
			break;
		case 2:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 3);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 1);
			break;
		case 3:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 3);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 2);
			break;
		case 4:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 4);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 4", 1);
			break;
		case 5:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 4);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 4", 1);
			break;
		case 6:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 5);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 4", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 5", 1);
			break;
		case 7:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 5);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 4", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 5", 1);
			break;
		case 8:
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 5);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 1", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 2", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 3", 2);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 4", 1);
			EncryptedPlayerPrefs.SetInt("Weapon Equip 5", 1);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradePlayerWeaponLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
