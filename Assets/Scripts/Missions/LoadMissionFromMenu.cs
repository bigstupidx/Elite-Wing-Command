using UnityEngine;
using System.Collections;

public class LoadMissionFromMenu : MonoBehaviour
{
	[SerializeField] GameObject levelLoadSplash;
	[SerializeField] GameObject tutorialLoadSplash;
	
	void OnClick()
	{
		int missionToLoad = PlayerPrefs.GetInt("Mission Scene Number", 0);

		if (missionToLoad != 3)
			levelLoadSplash.SetActive(true);
		else
			tutorialLoadSplash.SetActive(true);

		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(PlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
