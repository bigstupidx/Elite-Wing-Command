using UnityEngine;
using System.Collections;

public class TutorialMenuButton : MonoBehaviour
{
	[SerializeField] GameObject panelToOpen;
	[SerializeField] GameObject tutorialMenuButton;
	[SerializeField] GameObject boarderEffect;
	[SerializeField] GameObject guiObject;
	[SerializeField] Camera radarCamera;
	[SerializeField] KGFMapSystem mapScript;
	
	void OnClick()
	{
		Screen.showCursor = true;
		panelToOpen.SetActive(true);
		boarderEffect.SetActive(true);
		tutorialMenuButton.SetActive(false);
		guiObject.SetActive(false);
		radarCamera.enabled = false;
		mapScript.SetGlobalHideGui(true);
		CustomTimeManager.FadeTo(0f, 1.0f);
	}
}
