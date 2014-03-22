using UnityEngine;
using System.Collections;

public class UpgradePlayerRecovery : MonoBehaviour
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
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerRecoveryUpgradeCost)
			UpgradeRecovery();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerRecoveryLevel < 5)
			upgradeCostLabel.text = upgradesContainer.PlayerRecoveryUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerRecoveryUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((PlayerPrefs.GetInt("Player Recovery Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerRecoveryUpgradeCost)
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

	void UpgradeRecovery()
	{
		float currentRP = PlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerRecoveryUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Player Recovery Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Player Recovery Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetFloat("Player Recovery Multiplier", 1.1f);
			PlayerPrefs.Save();
			break;
		case 2:
			PlayerPrefs.SetFloat("Player Recovery Multiplier", 1.2f);
			PlayerPrefs.Save();
			break;
		case 3:
			PlayerPrefs.SetFloat("Player Recovery Multiplier", 1.3f);
			PlayerPrefs.Save();
			break;
		case 4:
			PlayerPrefs.SetFloat("Player Recovery Multiplier", 1.4f);
			PlayerPrefs.Save();
			break;
		case 5:
			PlayerPrefs.SetFloat("Player Recovery Multiplier", 1.5f);
			PlayerPrefs.Save();
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradePlayerRecoveryLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
