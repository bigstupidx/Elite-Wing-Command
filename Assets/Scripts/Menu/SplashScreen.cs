using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{

	void Start()
	{
		PlayerPrefs.SetString("Menu Screen", "Main");
		StartCoroutine(WaitAndLoad());
	}

	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(0.1f);
		Application.LoadLevel(1);
	}
}
