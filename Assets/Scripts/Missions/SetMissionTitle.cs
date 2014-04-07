using UnityEngine;
using System.Collections;

public class SetMissionTitle : MonoBehaviour
{
	[SerializeField] UILabel missionTitleLabel;

	void Awake()
	{
		missionTitleLabel.text = EncryptedPlayerPrefs.GetString("Mission Title", " ");
	}
}
