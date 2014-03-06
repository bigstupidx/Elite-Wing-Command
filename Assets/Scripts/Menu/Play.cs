using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{
	[SerializeField] GameObject PlayMenu;
	[SerializeField] GameObject minimap;
	[SerializeField] GameObject gui;
	[SerializeField] EasyJoystick joystick;
	[SerializeField] EasyButton fireWeapon;
	[SerializeField] EasyButton dropBomb;
	[SerializeField] TweenAlpha backgroundFadeOut;

	void Start()
	{
		Screen.showCursor = true;
		CustomTimeManager.FadeTo(0f, 0.01f);
		gui.SetActive(false);
		joystick.enabled = false;
		fireWeapon.enabled = false;
		dropBomb.enabled = false;
		StartCoroutine(InitializeMinimap());
	}

	IEnumerator InitializeMinimap()
	{
		yield return null;
		minimap.SetActive(false);
	}

	void OnClick()
	{
		Screen.showCursor = false;
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		backgroundFadeOut.enabled = true;
		minimap.SetActive(true);
		gui.SetActive(true);
		joystick.enabled = true;
		fireWeapon.enabled = true;
		dropBomb.enabled = true;
		PlayMenu.SetActive(false);
	}
}
