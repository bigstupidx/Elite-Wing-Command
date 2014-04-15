using UnityEngine;
using System.Collections;

public class SFXSliderFeedbackManager : MonoBehaviour
{
	bool sfxPlaying = false;
	bool sliderActive = false;
	bool buttonActive = false;
	public bool SliderActive { get { return sliderActive; } set { sliderActive = value; }}
	public bool ButtonActive { get { return buttonActive; } set { buttonActive = value; }}
	
	void Update()
	{
		if ((SliderActive || ButtonActive) && !sfxPlaying)
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Slider_Feedback_1", Fabric.EventAction.PlaySound);
			Fabric.EventManager.Instance.PostEvent("SFX_Slider_Feedback_2", Fabric.EventAction.PlaySound);
			sfxPlaying = true;
		}
		else if ((!SliderActive && !ButtonActive) && sfxPlaying)
		{
			Fabric.EventManager.Instance.PostEvent("SFX_Slider_Feedback_1", Fabric.EventAction.StopSound);
			Fabric.EventManager.Instance.PostEvent("SFX_Slider_Feedback_2", Fabric.EventAction.StopSound);
			sfxPlaying = false;
		}
	}
}
