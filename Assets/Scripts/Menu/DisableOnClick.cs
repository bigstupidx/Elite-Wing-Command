using UnityEngine;
using System.Collections;

public class DisableOnClick : MonoBehaviour
{
	[SerializeField] GameObject[] disableObjects;

	void OnClick()
	{
		foreach (GameObject disableObject in disableObjects)
		{
			disableObject.SetActive(false);
		}
	}
}
