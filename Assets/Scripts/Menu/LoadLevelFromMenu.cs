using UnityEngine;
using System.Collections;

public class LoadLevelFromMenu : MonoBehaviour
{
	[SerializeField] int loadLevel;
	[SerializeField] GameObject levelLoadSplash;
	
	void OnClick()
	{
		levelLoadSplash.SetActive(true);
		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(loadLevel);
	}
}
