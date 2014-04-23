using UnityEngine;
using System.Collections;

public class ReplayMission : MonoBehaviour
{
	[SerializeField] GameObject loadingScreen;
	
	void OnClick()
	{
		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		if (loadingScreen != null)
			loadingScreen.SetActive(true);
		
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(EncryptedPlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
