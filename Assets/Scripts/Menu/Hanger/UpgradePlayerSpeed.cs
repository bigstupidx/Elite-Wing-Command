using UnityEngine;
using System.Collections;

public class UpgradePlayerSpeed : MonoBehaviour
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
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerSpeedUpgradeCost)
			UpgradeSpeed();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerSpeedLevel < 5)
		{
			upgradeCostLabel.text = upgradesContainer.PlayerSpeedUpgradeCost.ToString("N0") + " RP";

			if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) < upgradesContainer.PlayerSpeedUpgradeCost)
				buttonCollider.enabled = false;
			else
				buttonCollider.enabled = true;
		}
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerSpeedUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((EncryptedPlayerPrefs.GetInt("Player Speed Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerSpeedUpgradeCost)
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

	void UpgradeSpeed()
	{
		float currentRP = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerSpeedUpgradeCost;
		EncryptedPlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = EncryptedPlayerPrefs.GetInt("Player Speed Level", 0);
		int newLevel = currentLevel + 1;
		EncryptedPlayerPrefs.SetInt("Player Speed Level", newLevel);

		switch(newLevel)
		{
		case 1:
			EncryptedPlayerPrefs.SetFloat("Player Speed Multiplier", 1.05f);
			break;
		case 2:
			EncryptedPlayerPrefs.SetFloat("Player Speed Multiplier", 1.1f);
			break;
		case 3:
			EncryptedPlayerPrefs.SetFloat("Player Speed Multiplier", 1.15f);
			break;
		case 4:
			EncryptedPlayerPrefs.SetFloat("Player Speed Multiplier", 1.2f);
			break;
		case 5:
			EncryptedPlayerPrefs.SetFloat("Player Speed Multiplier", 1.25f);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradePlayerSpeedLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
