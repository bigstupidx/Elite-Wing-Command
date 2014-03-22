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
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerWeaponUpgradeCost)
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

		upgradeSlider.value = ((PlayerPrefs.GetInt("Player Weapon Level", 0) * 1.0f) + 1) / 9f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerWeaponUpgradeCost)
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
		float newRP = currentRP - upgradesContainer.PlayerWeaponUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Player Weapon Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Player Weapon Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetInt("Weapon Slots", 3);
			PlayerPrefs.SetInt("Weapon Equip 1", 1);
			PlayerPrefs.SetInt("Weapon Equip 2", 1);
			PlayerPrefs.SetInt("Weapon Equip 3", 1);
			PlayerPrefs.Save();
			break;
		case 2:
			PlayerPrefs.SetInt("Weapon Slots", 3);
			PlayerPrefs.SetInt("Weapon Equip 1", 2);
			PlayerPrefs.SetInt("Weapon Equip 2", 1);
			PlayerPrefs.SetInt("Weapon Equip 3", 1);
			PlayerPrefs.Save();
			break;
		case 3:
			PlayerPrefs.SetInt("Weapon Slots", 3);
			PlayerPrefs.SetInt("Weapon Equip 1", 1);
			PlayerPrefs.SetInt("Weapon Equip 2", 2);
			PlayerPrefs.SetInt("Weapon Equip 3", 2);
			PlayerPrefs.Save();
			break;
		case 4:
			PlayerPrefs.SetInt("Weapon Slots", 4);
			PlayerPrefs.SetInt("Weapon Equip 1", 1);
			PlayerPrefs.SetInt("Weapon Equip 2", 1);
			PlayerPrefs.SetInt("Weapon Equip 3", 1);
			PlayerPrefs.SetInt("Weapon Equip 4", 1);
			PlayerPrefs.Save();
			break;
		case 5:
			PlayerPrefs.SetInt("Weapon Slots", 4);
			PlayerPrefs.SetInt("Weapon Equip 1", 2);
			PlayerPrefs.SetInt("Weapon Equip 2", 2);
			PlayerPrefs.SetInt("Weapon Equip 3", 1);
			PlayerPrefs.SetInt("Weapon Equip 4", 1);
			PlayerPrefs.Save();
			break;
		case 6:
			PlayerPrefs.SetInt("Weapon Slots", 5);
			PlayerPrefs.SetInt("Weapon Equip 1", 1);
			PlayerPrefs.SetInt("Weapon Equip 2", 1);
			PlayerPrefs.SetInt("Weapon Equip 3", 1);
			PlayerPrefs.SetInt("Weapon Equip 4", 1);
			PlayerPrefs.SetInt("Weapon Equip 5", 1);
			PlayerPrefs.Save();
			break;
		case 7:
			PlayerPrefs.SetInt("Weapon Slots", 5);
			PlayerPrefs.SetInt("Weapon Equip 1", 2);
			PlayerPrefs.SetInt("Weapon Equip 2", 1);
			PlayerPrefs.SetInt("Weapon Equip 3", 1);
			PlayerPrefs.SetInt("Weapon Equip 4", 1);
			PlayerPrefs.SetInt("Weapon Equip 5", 1);
			PlayerPrefs.Save();
			break;
		case 8:
			PlayerPrefs.SetInt("Weapon Slots", 5);
			PlayerPrefs.SetInt("Weapon Equip 1", 2);
			PlayerPrefs.SetInt("Weapon Equip 2", 2);
			PlayerPrefs.SetInt("Weapon Equip 3", 2);
			PlayerPrefs.SetInt("Weapon Equip 4", 1);
			PlayerPrefs.SetInt("Weapon Equip 5", 1);
			PlayerPrefs.Save();
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
