using UnityEngine;
using System.Collections;

public class HangerMeshActivate : MonoBehaviour
{
	[SerializeField] GameObject meshObject;

	void OnEnable()
	{
		meshObject.SetActive(true);
	}

	void OnDisable()
	{
		if (meshObject != null)
			meshObject.SetActive(false);
	}
}
