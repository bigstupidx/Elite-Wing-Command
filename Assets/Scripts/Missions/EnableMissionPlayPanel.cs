using UnityEngine;
using System.Collections;

public class EnableMissionPlayPanel : MonoBehaviour
{
	[SerializeField] GameObject levelLoadSplashBaseAttack;
	[SerializeField] GameObject levelLoadSplashBaseDefense;
	[SerializeField] GameObject levelLoadSplashBaseVsBase;
	[SerializeField] GameObject levelLoadSplashVIPAttack;
	[SerializeField] GameObject levelLoadSplashVIPDefense;

	void Awake()
	{
		switch(PlayerPrefs.GetInt("Mission Type", 0))
		{
		case 1:
			levelLoadSplashBaseAttack.SetActive(true);
			break;
		case 2:
			levelLoadSplashBaseDefense.SetActive(true);
			break;
		case 3:
			levelLoadSplashBaseVsBase.SetActive(true);
			break;
		case 4:
			levelLoadSplashVIPAttack.SetActive(true);
			break;
		case 5:
			levelLoadSplashVIPDefense.SetActive(true);
			break;
		}
	}
}
