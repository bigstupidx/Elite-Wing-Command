using UnityEngine;
using System.Collections;

public class IAPOKButton : MonoBehaviour
{
	[SerializeField] GameObject normalScreen;
	[SerializeField] GameObject errorScreen;

	void OnClick()
	{
		normalScreen.SetActive(true);
		errorScreen.SetActive(false);
	}
}
