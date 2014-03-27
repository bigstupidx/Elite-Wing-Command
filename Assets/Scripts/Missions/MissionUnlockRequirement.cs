using UnityEngine;
using System.Collections;

public class MissionUnlockRequirement : MonoBehaviour
{
	[SerializeField] int missionCompleteRequired = 101;
	[SerializeField] UISprite missionSprite;
	[SerializeField] Collider missionCollider;

	void Start()
	{
		if (PlayerPrefs.GetInt("Mission " + missionCompleteRequired.ToString() + " Status", 0) == 0)
		{
			missionSprite.alpha = 0.15f;
			missionCollider.enabled = false;
		}
	}
}
