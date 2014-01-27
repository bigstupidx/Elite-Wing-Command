using UnityEngine;
using System.Collections;

public class SavePrefs : MonoBehaviour
{

	void OnClick()
	{
		PlayerPrefs.Save();
	}
}
