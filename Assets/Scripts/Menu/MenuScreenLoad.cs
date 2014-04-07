using UnityEngine;
using System.Collections;

public class MenuScreenLoad : MonoBehaviour
{
	[SerializeField] GameObject mainScreen;
	[SerializeField] GameObject campaignScreen;
	[SerializeField] GameObject hangerScreen;
	[SerializeField] GameObject optionsScreen;

	void Awake()
	{
		string menuScreenToLoad = EncryptedPlayerPrefs.GetString("Menu Screen", "Main");

		switch(menuScreenToLoad)
		{
		case "Main":
			mainScreen.SetActive(true);
			break;
		case "Campaign":
			campaignScreen.SetActive(true);
			break;
		case "Hanger":
			hangerScreen.SetActive(true);
			break;
		case "Options":
			optionsScreen.SetActive(true);
			break;
		}
	}
}
