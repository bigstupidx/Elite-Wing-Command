using UnityEngine;
using System.Collections;

public class SFXSliderFeedback : MonoBehaviour
{
	[SerializeField] SFXSliderFeedbackManager sfxSliderFeedbackManager;
	[SerializeField] GameObject sfxSliderObject;
	bool sliderPressed = false;

	void Start()
	{
		UIEventListener.Get(sfxSliderObject).onPress += onSliderPressed;
	}

	void onSliderPressed(GameObject sender, bool isDown)
	{
		sliderPressed = isDown;
	}

	void Update()
	{
		if (sliderPressed)
			sfxSliderFeedbackManager.SliderActive = true;
		else
			sfxSliderFeedbackManager.SliderActive = false;
	}
}