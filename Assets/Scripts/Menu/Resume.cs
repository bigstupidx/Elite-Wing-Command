using UnityEngine;
using System.Collections;

public class Resume : MonoBehaviour
{
	[SerializeField] UISprite vignetteEffect;
	[SerializeField] GameObject minimap;
	[SerializeField] GameObject gui;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] EasyJoystick joystick;
	[SerializeField] EasyButton fireWeapon;
	[SerializeField] EasyButton dropBomb;
	
	void OnClick()
	{
		Fabric.EventManager.Instance.PostEvent("SFX_Button_General", Fabric.EventAction.PlaySound);
		pauseMenu.SetActive(false);
		Screen.showCursor = false;
		minimap.SetActive(true);
		gui.SetActive(true);
		joystick.enabled = true;
		fireWeapon.enabled = true;
		dropBomb.enabled = true;
		vignetteEffect.alpha = 0.45f;
		CustomTimeManager.FadeTo(1.1f, 0.2f);
	}
}
