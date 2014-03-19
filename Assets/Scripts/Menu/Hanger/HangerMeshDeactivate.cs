using UnityEngine;
using System.Collections;

public class HangerMeshDeactivate : MonoBehaviour
{
	[SerializeField] GameObject[] meshObjects;

	void OnClick()
	{
		foreach(GameObject mesh in meshObjects)
			mesh.SetActive(false);
	}
}
