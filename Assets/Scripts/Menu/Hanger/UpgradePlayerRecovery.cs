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
	[SerializeField] GameObject purchasePrompt;

	void OnEnable()
	{
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}

	void OnClick()
	{
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerRecoveryUpgradeCost)
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Purchase", Fabric.EventAction.PlaySound);
			UpgradeRecovery();
		}
		else
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Error", Fabric.EventAction.PlaySound);
			purchasePrompt.SetActive(true);
		}
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerRecoveryLevel < 5)
		{
			upgradeCostLabel.text = upgradesContainer.PlayerRecoveryUpgradeCost.ToString("N0") + " RP";
		}
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerRecoveryUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((EncryptedPlayerPrefs.GetInt("Player Recovery Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerRecoveryUpgradeCost)
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
		float currentRP = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerRecoveryUpgradeCost;
		EncryptedPlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = EncryptedPlayerPrefs.GetInt("Player Recovery Level", 0);
		int newLevel = currentLevel + 1;
		EncryptedPlayerPrefs.SetInt("Player Recovery Level", newLevel);

		switch(newLevel)
		{
		case 1:
			EncryptedPlayerPrefs.SetFloat("Player Recovery Multiplier", 1.1f);
			break;
		case 2:
			EncryptedPlayerPrefs.SetFloat("Player Recovery Multiplier", 1.2f);
			break;
		case 3:
			EncryptedPlayerPrefs.SetFloat("Player Recovery Multiplier", 1.3f);
			break;
		case 4:
			EncryptedPlayerPrefs.SetFloat("Player Recovery Multiplier", 1.4f);
			break;
		case 5:
			EncryptedPlayerPrefs.SetFloat("Player Recovery Multiplier", 1.5f);
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
