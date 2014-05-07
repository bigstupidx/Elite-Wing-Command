using UnityEngine;
using System.Collections;

public class StartAudioOnStart : MonoBehaviour
{
	[SerializeField] string eventName;
	[SerializeField] float waitTime;
	Fabric.AudioComponent menuMusicComponent;

	void Start()
	{
		GameObject menuMusicObject = GameObject.FindGameObjectWithTag("MenuMusic");
		menuMusicComponent = menuMusicObject.GetComponent<Fabric.AudioComponent>();
		StartCoroutine(WaitAndPlay());
	}

	IEnumerator WaitAndPlay()
	{
		yield return new WaitForSeconds(waitTime);

		if (menuMusicComponent != null && !menuMusicComponent.IsPlaying())
			Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
	}
}