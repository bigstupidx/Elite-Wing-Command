using UnityEngine;
using System.Collections;

public class PauseGame : MonoBehaviour
{
	UISprite vignetteEffect;
	GameObject minimap;
	GameObject gui;

	void Start()
	{
		Screen.showCursor = false;
		minimap = GameObject.FindGameObjectWithTag("Minimap");
		gui = GameObject.FindGameObjectWithTag("GUI");

		var vignetteEffectObject = GameObject.FindGameObjectWithTag("VignetteEffect");
		vignetteEffect = vignetteEffectObject.GetComponent<UISprite>();
		vignetteEffect.alpha = 0.45f;
	}

	void Update()
	{
		//Pause
		if (Input.GetKeyDown(KeyCode.N))
		{
			Screen.showCursor = true;
			minimap.SetActive(false);
			gui.SetActive(false);
			vignetteEffect.alpha = 1f;
			CustomTimeManager.FadeTo(0f, 0.25f);
		}
		//Unpause
		else if (Input.GetKeyDown(KeyCode.M))
		{
			Screen.showCursor = false;
			minimap.SetActive(true);
			gui.SetActive(true);
			vignetteEffect.alpha = 0.45f;
			CustomTimeManager.FadeTo(1.1f, 0.25f);
		}
	}
}