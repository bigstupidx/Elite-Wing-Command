using UnityEngine;
using System.Collections;

public class MenuMissionSelect : MonoBehaviour
{
	[SerializeField] int missionNumber = 101;
	[SerializeField] int missionSceneNumber;
	[SerializeField] GameObject launchButton;
	[SerializeField] UILabel missionTitleLabel;
	[SerializeField] UILabel missionDescriptionLabel;
	[SerializeField] string missionDescription = "Mission Type:\n- Base vs. Base\n\nDifficulty:\n- Easy\n\n" +
		"Air Units:\n- Yes\n\nGround Units:\n- Yes\n\nStatus:\n- ";
	[SerializeField] bool isTutorial = false;

	public enum MissionType
	{
		Base_Attack,
		Base_Defense,
		Base_vs_Base,
		VIP_Attack,
		VIP_Defense
	}

	public MissionType missionType;

	void Start()
	{
		if (EncryptedPlayerPrefs.GetInt("Mission " + missionNumber.ToString() + " Status", 0) == 0)
			missionDescription += "[ff0000]Active";
		else
			missionDescription += "[008000]Complete";
	}

	void OnClick()
	{
		if (!isTutorial)
		{
			if (missionTitleLabel.text != "MISSION " + missionNumber.ToString())
			{
				missionTitleLabel.text = "MISSION " + missionNumber.ToString();
				missionTitleLabel.enabled = true;

				missionDescriptionLabel.text = missionDescription;
				missionDescriptionLabel.enabled = true;

				switch(missionType)
				{
				case MissionType.Base_Attack:
					EncryptedPlayerPrefs.SetInt("Mission Type", 1);
					break;
				case MissionType.Base_Defense:
					EncryptedPlayerPrefs.SetInt("Mission Type", 2);
					break;
				case MissionType.Base_vs_Base:
					EncryptedPlayerPrefs.SetInt("Mission Type", 3);
					break;
				case MissionType.VIP_Attack:
					EncryptedPlayerPrefs.SetInt("Mission Type", 4);
					break;
				case MissionType.VIP_Defense:
					EncryptedPlayerPrefs.SetInt("Mission Type", 5);
					break;
				}

				EncryptedPlayerPrefs.SetString("Mission Title", "Mission " + missionNumber.ToString());
			}
		}
		else
		{
			if (missionTitleLabel.text != "TUTORIAL")
			{
				missionTitleLabel.text = "TUTORIAL";
				missionTitleLabel.enabled = true;

				missionDescriptionLabel.text = missionDescription;
				missionDescriptionLabel.enabled = true;

				EncryptedPlayerPrefs.SetString("Mission Title", "Tutorial");
			}
		}

		EncryptedPlayerPrefs.SetInt("Mission Number", missionNumber);
		EncryptedPlayerPrefs.SetInt("Mission Scene Number", missionSceneNumber);
		PlayerPrefs.Save();
		launchButton.SetActive(true);
	}
}
