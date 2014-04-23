using UnityEngine;
using System.Collections;

public class LoadLevelFromMenu : MonoBehaviour
{
	[SerializeField] int loadLevel;
	[SerializeField] GameObject levelLoadSplash;
	[SerializeField] GameObject altCamera;
	
	void OnClick()
	{
		Fabric.EventManager.Instance.PostEvent("Music_Menu", Fabric.EventAction.StopSound);
		Fabric.EventManager.Instance.PostEvent("Music_Menu_Transition", Fabric.EventAction.PlaySound);

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
