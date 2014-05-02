using UnityEngine;
using System.Collections;

public class MenuScreenLoad : MonoBehaviour
{
	[SerializeField] GameObject mainScreen;
	[SerializeField] TweenAlpha mainScreenAlphaTween;
	[SerializeField] Camera aircraftCamera;
	[SerializeField] GameObject campaignScreen;
	[SerializeField] GameObject hangarScreen;
	[SerializeField] GameObject optionsScreen;

	void Awake()
	{
		string menuScreenToLoad = EncryptedPlayerPrefs.GetString("Menu Screen", "Main");

		switch(menuScreenToLoad)
		{
		case "Main":
			aircraftCamera.enabled = false;
			mainScreenAlphaTween.from = 0f;
			mainScreenAlphaTween.to = 1.0f;
			mainScreenAlphaTween.duration = 1.0f;
			mainScreenAlphaTween.ResetToBeginning();
			mainScreenAlphaTween.enabled = true;
			StartCoroutine(WaitAndSet());
			break;
		case "Campaign":
			campaignScreen.SetActive(true);
			break;
		case "Hangar":
			hangarScreen.SetActive(true);
			break;
		case "Options":
			optionsScreen.SetActive(true);
			break;
		}
	}

	IEnumerator WaitAndSet()
	{
		yield return new WaitForSeconds(0.1f);
		mainScreen.SetActive(true);
		aircraftCamera.enabled = true;
		yield return new WaitForSeconds(1.1f);
		mainScreenAlphaTween.duration = 0.25f;
	}
}
