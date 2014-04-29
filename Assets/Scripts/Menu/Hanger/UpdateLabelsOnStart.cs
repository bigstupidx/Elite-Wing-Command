using UnityEngine;
using System.Collections;

public class UpdateLabelsOnStart : MonoBehaviour
{
	
	void Start()
	{
		StartCoroutine(WaitAndUpdate());
	}
	
	IEnumerator WaitAndUpdate()
	{
		yield return new WaitForSeconds(0.01f);
		transform.root.gameObject.BroadcastMessage("UpdateLabels");
	}
}
