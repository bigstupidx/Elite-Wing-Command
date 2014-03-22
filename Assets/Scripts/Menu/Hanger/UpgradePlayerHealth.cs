using UnityEngine;
using System.Collections;

public class UpgradePlayerHealth : MonoBehaviour
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
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerHealthUpgradeCost)
			UpgradeHealth();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerHealthLevel < 5)
			upgradeCostLabel.text = upgradesContainer.PlayerHealthUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerHealthUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((PlayerPrefs.GetInt("Player Health Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerHealthUpgradeCost)
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

	void UpgradeHealth()
	{
		float currentRP = PlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerHealthUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Player Health Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Player Health Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetFloat("Player Health Multiplier", 1.05f);
			break;
		case 2:
			PlayerPrefs.SetFloat("Player Health Multiplier", 1.1f);
			break;
		case 3:
			PlayerPrefs.SetFloat("Player Health Multiplier", 1.15f);
			break;
		case 4:
			PlayerPrefs.SetFloat("Player Health Multiplier", 1.2f);
			break;
		case 5:
			PlayerPrefs.SetFloat("Player Health Multiplier", 1.25f);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradePlayerHealthLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
