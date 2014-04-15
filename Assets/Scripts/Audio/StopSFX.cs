using UnityEngine;
using System.Collections;

public class StopSFX : MonoBehaviour
{
	
	void Start()
	{
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopAll);
	}
}
