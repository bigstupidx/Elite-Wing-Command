using UnityEngine;
using System.Collections;

public class LoadLevelFromMenu : MonoBehaviour
{
	[SerializeField] int loadLevel;
	[SerializeField] GameObject levelLoadSplash;
	[SerializeField] GameObject altCamera;
	
	void OnClick()
	{
		if (altCamera != null)
			altCamera.SetActive(false);

		levelLoadSplash.SetActive(true);
		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(4.0f);
		Application.LoadLevel(loadLevel);
	}
}
