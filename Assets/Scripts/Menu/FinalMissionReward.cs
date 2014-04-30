using UnityEngine;
using System.Collections;

public class FinalMissionReward : MonoBehaviour
{
	[SerializeField] GameObject rewardPanel;
	[SerializeField] int rewardBonusTotal;

	void Awake()
	{
		if (EncryptedPlayerPrefs.GetInt("Mission 310 Status", 0) == 1 && EncryptedPlayerPrefs.GetInt("Final Mission Bonus Status", 0) == 0)
		{
			float rewardPoints = EncryptedPlayerPrefs.GetFloat("Reward Points", 0);
			rewardPoints += rewardBonusTotal;
			EncryptedPlayerPrefs.SetFloat("Reward Points", rewardPoints);
			rewardPanel.SetActive(true);
			EncryptedPlayerPrefs.SetInt("Final Mission Bonus Status", 1);
		}
	}
}
