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
	[SerializeField] EasyJoystick joystick;
	[SerializeField] EasyButton fireWeapon;
	[SerializeField] EasyButton dropBomb;
	
	void OnClick()
	{
		Cursor.visible = true;
		panelToOpen.SetActive(true);
		boarderEffect.SetActive(true);
		tutorialMenuButton.SetActive(false);
		guiObject.SetActive(false);
		radarCamera.enabled = false;
		mapScript.SetGlobalHideGui(true);
		joystick.enabled = false;
		fireWeapon.enabled = false;
		dropBomb.enabled = false;
		CustomTimeManager.FadeTo(0f, 1.0f);
	}
}
