using UnityEngine;
using System.Collections;

public class SavePlayerPrefsOnClick : MonoBehaviour
{
	void OnClick()
	{
		PlayerPrefs.Save();
	}
}
