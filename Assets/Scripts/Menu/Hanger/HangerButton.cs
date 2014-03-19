using UnityEngine;
using System.Collections;

public class HangerButton : MonoBehaviour
{
	[SerializeField] bool defaultButton;
	[SerializeField] GameObject buttonActivateContents;
	[SerializeField] GameObject[] buttonDeactivateContents;
	Vector3 tempActivatedScale;
	Vector3 tempDeactivatedScale;

	void OnEnable()
	{
		if (defaultButton)
			OnClick();
	}

	void OnClick()
	{
		buttonActivateContents.SetActive(true);

		foreach(GameObject contentsObject in buttonDeactivateContents)
			contentsObject.SetActive(false);
	}
}
