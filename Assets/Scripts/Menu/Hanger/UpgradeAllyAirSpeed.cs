using UnityEngine;
using System.Collections;

public class UpgradeAllyAirSpeed : MonoBehaviour
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
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyAirSpeedUpgradeCost)
			UpgradeSpeed();
		else
			Debug.Log("Not enough RP for upgrade purchase....");
	}

	void UpdateLabels()
	{
		if (upgradesContainer.AllyAirSpeedLevel < 5)
			upgradeCostLabel.text = upgradesContainer.AllyAirSpeedUpgradeCost.ToString("N0") + " RP";
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.AllyAirSpeedUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((PlayerPrefs.GetInt("Ally Air Speed Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = PlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (PlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.AllyAirSpeedUpgradeCost)
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
		float newRP = currentRP - upgradesContainer.AllyAirSpeedUpgradeCost;
		PlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = PlayerPrefs.GetInt("Ally Air Speed Level", 0);
		int newLevel = currentLevel + 1;
		PlayerPrefs.SetInt("Ally Air Speed Level", newLevel);

		switch(newLevel)
		{
		case 1:
			PlayerPrefs.SetFloat("Ally Air Speed Multiplier", 1.2f);
			break;
		case 2:
			PlayerPrefs.SetFloat("Ally Air Speed Multiplier", 1.4f);
			break;
		case 3:
			PlayerPrefs.SetFloat("Ally Air Speed Multiplier", 1.6f);
			break;
		case 4:
			PlayerPrefs.SetFloat("Ally Air Speed Multiplier", 1.8f);
			break;
		case 5:
			PlayerPrefs.SetFloat("Ally Air Speed Multiplier", 2.0f);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
		upgradesContainer.UpgradeAllyAirSpeedLevel();
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}
}
