using UnityEngine;
using System.Collections;

public class StartAudioAtStart : MonoBehaviour
{
	[SerializeField] FabricObjectManager fabricObjectManager;
	[SerializeField] string eventName;

	void Start ()
	{
		if (!Fabric.EventManager.Instance.IsEventActive(eventName, fabricObjectManager.FabricPrefab))
			Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);
	}
}
