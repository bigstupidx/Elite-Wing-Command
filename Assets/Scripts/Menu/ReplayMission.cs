using UnityEngine;
using System.Collections;

public class ReplayMission : MonoBehaviour
{
	[SerializeField] GameObject loadingScreen;
	
	void OnClick()
	{
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopAll);
		Fabric.EventManager.Instance.PostEvent("SFX_Button_General", Fabric.EventAction.PlaySound);
		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		if (loadingScreen != null)
			loadingScreen.SetActive(true);

		yield return new WaitForSeconds(2.0f);
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopAll);
		Application.LoadLevel(EncryptedPlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
