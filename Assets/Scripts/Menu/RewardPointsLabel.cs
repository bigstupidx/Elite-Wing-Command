using UnityEngine;
using System.Collections;

public class RewardPointsLabel : MonoBehaviour
{
	[SerializeField] UILabel rewardPointsLabel;

	void OnEnable()
	{
		UpdateRPLabel();
	}

	void UpdateRPLabel()
	{
		int totalRewardPoints = PlayerPrefs.GetInt("Reward Points", 0);
		rewardPointsLabel.text = totalRewardPoints.ToString("N0") + " RP";
	}
}
