using UnityEngine;
using System.Collections;

public class Start3dAudioOnStart : MonoBehaviour
{
	[SerializeField] string eventName;

	void Start ()
	{
		Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound, gameObject);
	}
}
