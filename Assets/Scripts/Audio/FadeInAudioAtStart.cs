using UnityEngine;
using System.Collections;

public class FadeInAudioAtStart : MonoBehaviour
{
	[SerializeField] string eventName;
	float volume = 0f;
	bool startFadeIn = false;

	void Start()
	{
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.SetVolume, volume);
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
		startFadeIn = true;
	}

	void Update()
	{
		if (startFadeIn)
		{
			if (volume >= 1f)
			{
				volume = 1f;
				startFadeIn = false;
			}

			volume += Time.deltaTime/2.2f;
			Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.SetVolume, volume);
		}
	}
}