using UnityEngine;
using System.Collections;

public class MissionUnlockRequirement : MonoBehaviour
{
	[SerializeField] int missionCompleteRequired = 101;
	[SerializeField] UISprite missionSprite;
	[SerializeField] Collider missionCollider;
	bool isActive = true;
	public bool IsActive { get { return isActive; }}

	void Start()
	{
		if (EncryptedPlayerPrefs.GetInt("Mission " + missionCompleteRequired.ToString() + " Status", 0) == 0)
		{
			missionSprite.alpha = 0.15f;
			missionCollider.enabled = false;
			isActive = false;
		}
	}
}
