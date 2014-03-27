using UnityEngine;
using System.Collections;

public class MenuMissionSelect : MonoBehaviour
{
	[SerializeField] int missionNumber = 101;
	[SerializeField] int missionSceneNumber;
	[SerializeField] GameObject launchButton;
	[SerializeField] UILabel missionTitleLabel;
	[SerializeField] UILabel missionDescriptionLabel;
	[SerializeField] string missionTitle = "MISSION 101";
	[SerializeField] string missionDescription = "Mission Type:\n- Base vs. Base\n\nDifficulty:\n- Easy\n\n" +
		"Air Units:\n- Yes\n\nGround Units:\n- Yes\n\nStatus:\n- ";

	void Start()
	{
		if (PlayerPrefs.GetInt("Mission " + missionNumber.ToString() + " Status", 0) == 0)
			missionDescription += "[ff0000]Active";
		else
			missionDescription += "[008000]Complete";
	}

	void OnClick()
	{
		if (missionTitleLabel.text != missionTitle)
		{
			missionTitleLabel.text = missionTitle;
			missionTitleLabel.enabled = true;

			missionDescriptionLabel.text = missionDescription;
			missionDescriptionLabel.enabled = true;

			PlayerPrefs.SetInt("Mission Scene Number", missionSceneNumber);
			launchButton.SetActive(true);
		}
	}
}
