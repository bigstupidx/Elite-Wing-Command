using UnityEngine;
using System.Collections;

public class ControlsAndHUD : MonoBehaviour
{
	[SerializeField] GameObject tutorialMenu;
	[SerializeField] GameObject controlsTutorial;
	[SerializeField] GameObject gui;
	[SerializeField] KGFMapSystem minimapScript;
	[SerializeField] Camera radarCamera;

	void OnClick()
	{
		controlsTutorial.SetActive(true);
		gui.SetActive(true);
		minimapScript.SetGlobalHideGui(false);
		radarCamera.enabled = true;
		tutorialMenu.SetActive(false);
	}
}
