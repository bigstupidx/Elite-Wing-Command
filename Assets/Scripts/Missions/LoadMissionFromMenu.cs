using UnityEngine;
using System.Collections;

public class LoadMissionFromMenu : MonoBehaviour
{
	[SerializeField] GameObject levelLoadSplash;
	
	void OnClick()
	{
		levelLoadSplash.SetActive(true);
		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(PlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
