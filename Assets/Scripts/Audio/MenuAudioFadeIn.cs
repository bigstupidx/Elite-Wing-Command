using UnityEngine;
using System.Collections;

public class MenuAudioFadeIn : MonoBehaviour
{
	[SerializeField] string eventName = "Music_Menu";
	[SerializeField] float fadeInTime;
	float currentVolume;
	bool fadeIn = false;

	void Start()
	{
		currentVolume = 0f;
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.SetVolume, currentVolume);
		StartCoroutine(WaitAndPlay());
	}

	IEnumerator WaitAndPlay()
	{
		yield return new WaitForSeconds(0.25f);
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
		fadeIn = true;
	}

	void Update()
	{
		if (fadeIn)
		{
			currentVolume += Time.deltaTime/fadeInTime;

			if (currentVolume >= 0.63f)
			{
				currentVolume = 0.63f;
				fadeIn = false;
			}

			Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.SetVolume, currentVolume);
		}
	}
}