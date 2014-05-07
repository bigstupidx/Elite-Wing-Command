using UnityEngine;
using System.Collections;

public class CloseOnClick : MonoBehaviour
{
	[SerializeField] GameObject objectToClose;

	void OnClick()
	{
		objectToClose.SetActive(false);
	}
}
