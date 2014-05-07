using UnityEngine;
using System.Collections;

public class IAPOKButton : MonoBehaviour
{
	[SerializeField] GameObject normalScreen;
	[SerializeField] GameObject screenToDisable;

	void OnClick()
	{
		normalScreen.SetActive(true);
		screenToDisable.SetActive(false);
	}
}
