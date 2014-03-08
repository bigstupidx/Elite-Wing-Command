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
		Screen.showCursor = false;
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		backgroundFadeOut.enabled = true;
		minimapScript.SetGlobalHideGui(false);
		gui.SetActive(true);
		joystick.enabled = true;
		fireWeapon.enabled = true;
		dropBomb.enabled = true;
		PlayMenu.SetActive(false);
	}
}
