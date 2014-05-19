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
	[SerializeField] GameObject purchasePrompt;

	void OnEnable()
	{
		transform.parent.gameObject.BroadcastMessage("UpdateLabels");
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
			transform.root.gameObject.BroadcastMessage("UpdateLabels");
	}

	void OnClick()
	{
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerHealthUpgradeCost)
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Purchase", Fabric.EventAction.PlaySound);
			UpgradeHealth();
		}
		else
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Error", Fabric.EventAction.PlaySound);
			purchasePrompt.SetActive(true);
		}
	}

	void UpdateLabels()
	{
		if (upgradesContainer.PlayerHealthLevel < 5)
		{
			upgradeCostLabel.text = upgradesContainer.PlayerHealthUpgradeCost.ToString("N0") + " RP";
		}
		else
		{
			upgradeCostLabel.text = "Upgrade Full";
			upgradesContainer.PlayerHealthUpgradeCost = 0;
			buttonCollider.enabled = false;
		}

		upgradeSlider.value = ((EncryptedPlayerPrefs.GetInt("Player Health Level", 0) * 1.0f) + 1) / 6f;
		rewardPointsLabel.text = EncryptedPlayerPrefs.GetFloat("Reward Points", 0).ToString("N0") + " RP";
		
		if (EncryptedPlayerPrefs.GetFloat("Reward Points", 0) >= upgradesContainer.PlayerHealthUpgradeCost)
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
		var gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");
		
		if (gameCenterObject != null)
		{
			PlayerPrefs.Save();
			EWCGameCenter gameCenterScript = gameCenterObject.GetComponent<EWCGameCenter>();
			gameCenterScript.SubmitAchievement("purchase_an_upgrade", 100f);
		}

		float currentRP = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
		float newRP = currentRP - upgradesContainer.PlayerHealthUpgradeCost;
		EncryptedPlayerPrefs.SetFloat("Reward Points", newRP);

		int currentLevel = EncryptedPlayerPrefs.GetInt("Player Health Level", 0);
		int newLevel = currentLevel + 1;
		EncryptedPlayerPrefs.SetInt("Player Health Level", newLevel);

		switch(newLevel)
		{
		case 1:
			EncryptedPlayerPrefs.SetFloat("Player Health Multiplier", 1.05f);
			break;
		case 2:
			EncryptedPlayerPrefs.SetFloat("Player Health Multiplier", 1.1f);
			break;
		case 3:
			EncryptedPlayerPrefs.SetFloat("Player Health Multiplier", 1.15f);
			break;
		case 4:
			EncryptedPlayerPrefs.SetFloat("Player Health Multiplier", 1.2f);
			break;
		case 5:
			EncryptedPlayerPrefs.SetFloat("Player Health Multiplier", 1.25f);
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
