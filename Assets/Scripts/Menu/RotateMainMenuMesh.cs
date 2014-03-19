using UnityEngine;
using System.Collections;

public class RotateMainMenuMesh : MonoBehaviour
{
	[SerializeField] Transform mainMenuMesh;
	[SerializeField] float rotationSpeed = 20f;

	void Update()
	{
		var tempEulerRotation = mainMenuMesh.transform.eulerAngles;
		tempEulerRotation.y += Time.deltaTime * rotationSpeed;

		if (tempEulerRotation.y > 360f)
			tempEulerRotation.y -= 360f;

		mainMenuMesh.transform.eulerAngles = tempEulerRotation;
	}
}
