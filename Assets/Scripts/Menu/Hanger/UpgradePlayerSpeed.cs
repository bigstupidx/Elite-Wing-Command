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
		UpdateLabels();
	}

	void OnClick()
	{
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerSpeedUpgradeCost)
			UpgradeSpeed();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerSpeedLevel < 5)
			upgradeCostLabel.text = upgradesContainer.PlayerSpeedUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerSpeedUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((PlayerPrefs.GetInt("Player Speed Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerSpeedUpgradeCost)
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
		float currentRP = PlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerSpeedUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Player Speed Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Player Speed Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetFloat("Player Speed Multiplier", 1.05f);
			PlayerPrefs.Save();
			break;
		case 2:
			PlayerPrefs.SetFloat("Player Speed Multiplier", 1.1f);
			PlayerPrefs.Save();
			break;
		case 3:
			PlayerPrefs.SetFloat("Player Speed Multiplier", 1.15f);
			PlayerPrefs.Save();
			break;
		case 4:
			PlayerPrefs.SetFloat("Player Speed Multiplier", 1.2f);
			PlayerPrefs.Save();
			break;
		case 5:
			PlayerPrefs.SetFloat("Player Speed Multiplier", 1.25f);
			PlayerPrefs.Save();
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradePlayerSpeedLevel();
		UpdateLabels();
	}
}
