using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{
	[SerializeField] GameObject PlayMenu;
	[SerializeField] KGFMapSystem minimapScript;
	[SerializeField] GameObject gui;
	[SerializeField] EasyJoystick joystick;
	[SerializeField] EasyButton fireWeapon;
	[SerializeField] EasyButton dropBomb;
	[SerializeField] TweenAlpha backgroundFadeOut;
	[SerializeField] Camera radarCamera;
	[SerializeField] GameObject tutorialMenu;

	void Awake()
	{
		gui.SetActive(false);
		joystick.enabled = false;
		fireWeapon.enabled = false;
		dropBomb.enabled = false;
	}

	void Start()
	{
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopAll);
		minimapScript.SetGlobalHideGui(true);
		Screen.showCursor = true;
		CustomTimeManager.FadeTo(0f, 0.01f);
	}

	void OnClick()
	{
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		backgroundFadeOut.enabled = true;
		Fabric.EventManager.Instance.PostEvent("SFX_Button_General", Fabric.EventAction.PlaySound);

		if (Application.loadedLevel == 3)
		{
			gui.SetActive(false);
			radarCamera.enabled = false;
			minimapScript.SetGlobalHideGui(true);
			tutorialMenu.SetActive(true);
			joystick.enabled = false;
			fireWeapon.enabled = false;
			dropBomb.enabled = false;
			CustomTimeManager.FadeTo(0f, 1.5f);
		}
		else
		{
			Screen.showCursor = false;
			gui.SetActive(true);
			radarCamera.enabled = true;
			minimapScript.SetGlobalHideGui(false);
			joystick.enabled = true;
			fireWeapon.enabled = true;
			dropBomb.enabled = true;
		}

		PlayMenu.SetActive(false);
	}
}
