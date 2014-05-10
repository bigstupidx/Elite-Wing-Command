using UnityEngine;
using System.Collections;

public class StartAudioOnStart : MonoBehaviour
{
	[SerializeField] string eventName;
	[SerializeField] float waitTime;

	void Start()
	{
		StartCoroutine(WaitAndPlay());
	}

	IEnumerator WaitAndPlay()
	{
		yield return new WaitForSeconds(waitTime);
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
	}
}