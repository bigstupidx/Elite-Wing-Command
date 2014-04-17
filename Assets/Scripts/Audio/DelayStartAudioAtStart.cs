using UnityEngine;
using System.Collections;

public class DelayStartAudioAtStart : MonoBehaviour
{
	[SerializeField] string eventName;
	[SerializeField] float waitTime = 0.3f;

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