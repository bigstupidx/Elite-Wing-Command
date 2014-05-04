using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
	[SerializeField] UISprite vignetteEffect;
	[SerializeField] GameObject minimap;
	[SerializeField] GameObject gui;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] EasyJoystick joystick;
	[SerializeField] EasyButton fireWeapon;
	[SerializeField] EasyButton dropBomb;

	void Start()
	{
		pauseMenu.SetActive(false);
		vignetteEffect.alpha = 0.45f;
	}
	
	void OnClick()
	{
		Fabric.EventManager.Instance.PostEvent("SFX_Button_General", Fabric.EventAction.PlaySound);
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.PauseSound);
		pauseMenu.SetActive(true);
		Screen.showCursor = true;
		minimap.SetActive(false);
		gui.SetActive(false);
		joystick.enabled = false;
		fireWeapon.enabled = false;
		dropBomb.enabled = false;
		vignetteEffect.alpha = 1f;
		CustomTimeManager.FadeTo(0f, 0.2f);
	}
}
