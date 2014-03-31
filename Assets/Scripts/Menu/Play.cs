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
	[SerializeField] GameObject tutorialMenu;
	[SerializeField] Camera radarCamera;

	void Awake()
	{
		gui.SetActive(false);
		joystick.enabled = false;
		fireWeapon.enabled = false;
		dropBomb.enabled = false;
	}

	void Start()
	{
		minimapScript.SetGlobalHideGui(true);
		Screen.showCursor = true;
		CustomTimeManager.FadeTo(0f, 0.01f);
	}

	void OnClick()
	{
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		backgroundFadeOut.enabled = true;

		if (Application.loadedLevel == 3)
		{
			radarCamera.enabled = false;
			tutorialMenu.SetActive(true);
			CustomTimeManager.FadeTo(0f, 1f);
		}
		else
		{
			Screen.showCursor = false;
			minimapScript.SetGlobalHideGui(false);
			gui.SetActive(true);
			joystick.enabled = true;
			fireWeapon.enabled = true;
			dropBomb.enabled = true;
		}

		PlayMenu.SetActive(false);
	}
}
