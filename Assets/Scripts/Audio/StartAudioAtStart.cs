using UnityEngine;
using System.Collections;

public class StartAudioAtStart : MonoBehaviour
{
	[SerializeField] FabricObjectManager fabricObjectManager;
	[SerializeField] string eventName;
	[SerializeField] float waitTime = 0f;

	void Start()
	{
		StartCoroutine(WaitAndPlay());
	}

	IEnumerator WaitAndPlay()
	{
		yield return new WaitForSeconds(waitTime);

		if (!Fabric.EventManager.Instance.IsEventActive(eventName, fabricObjectManager.FabricPrefab))
			Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
	}
}