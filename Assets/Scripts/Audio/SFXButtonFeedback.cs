using UnityEngine;
using System.Collections;

public class SFXButtonFeedback : MonoBehaviour
{
	[SerializeField] SFXSliderFeedbackManager sfxSliderFeedbackManager;
	[SerializeField] GameObject sfxSliderObject;
	bool buttonPressed = false;

	void Start()
	{
		UIEventListener.Get(sfxSliderObject).onPress += onButtonPressed;
	}

	void onButtonPressed(GameObject sender, bool isDown)
	{
		buttonPressed = isDown;
	}

	void Update()
	{
		if (buttonPressed)
			sfxSliderFeedbackManager.ButtonActive = true;
		else
			sfxSliderFeedbackManager.ButtonActive = false;
	}
}