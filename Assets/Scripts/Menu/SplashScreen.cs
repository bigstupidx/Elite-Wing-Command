using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{

	void Start()
	{
		EncryptedPlayerPrefs.SetString("Menu Screen", "Main");
		//StartCoroutine(WaitAndLoad());
	}

//	IEnumerator WaitAndLoad()
//	{
//		yield return new WaitForSeconds(3.0f);
//		Application.LoadLevel(1);
//	}
}
