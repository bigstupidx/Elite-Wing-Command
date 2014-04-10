using UnityEngine;
using System.Collections;

public class LoadMissionFromMenu : MonoBehaviour
{
	[SerializeField] GameObject levelLoadSplashBaseAttack;
	[SerializeField] GameObject levelLoadSplashBaseDefense;
	[SerializeField] GameObject levelLoadSplashBaseVsBase;
	[SerializeField] GameObject levelLoadSplashVIPAttack;
	[SerializeField] GameObject levelLoadSplashVIPDefense;
	[SerializeField] GameObject tutorialLoadSplash;
	
	void OnClick()
	{
		int missionToLoad = EncryptedPlayerPrefs.GetInt("Mission Scene Number", 0);

		if (missionToLoad == 3)
			tutorialLoadSplash.SetActive(true);
		else if (EncryptedPlayerPrefs.GetInt("Mission Type", 0) == 1)
			levelLoadSplashBaseAttack.SetActive(true);
		else if (EncryptedPlayerPrefs.GetInt("Mission Type", 0) == 2)
			levelLoadSplashBaseDefense.SetActive(true);
		else if (EncryptedPlayerPrefs.GetInt("Mission Type", 0) == 3)
			levelLoadSplashBaseVsBase.SetActive(true);
		else if (EncryptedPlayerPrefs.GetInt("Mission Type", 0) == 4)
			levelLoadSplashVIPAttack.SetActive(true);
		else if (EncryptedPlayerPrefs.GetInt("Mission Type", 0) == 5)
			levelLoadSplashVIPDefense.SetActive(true);

		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(4.0f);
		Application.LoadLevel(EncryptedPlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
