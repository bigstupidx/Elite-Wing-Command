using UnityEngine;
using System.Collections;

public class ResumeFreeplay : MonoBehaviour
{
	[SerializeField] GameObject panelToClose;
	[SerializeField] GameObject tutorialMenuButton;
	[SerializeField] GameObject boarderEffect;
	[SerializeField] GameObject guiObject;
	[SerializeField] Camera radarCamera;
	[SerializeField] KGFMapSystem mapScript;
	[SerializeField] EasyJoystick joystick;
	[SerializeField] EasyButton fireWeapon;
	[SerializeField] EasyButton dropBomb;
	
	void OnClick()
	{
		Screen.showCursor = false;
		panelToClose.SetActive(false);
		boarderEffect.SetActive(false);
		tutorialMenuButton.SetActive(true);
		guiObject.SetActive(true);
		radarCamera.enabled = true;
		mapScript.SetGlobalHideGui(false);
		joystick.enabled = true;
		fireWeapon.enabled = true;
		dropBomb.enabled = true;
		CustomTimeManager.FadeTo(1.1f, 1.0f);
	}
}
