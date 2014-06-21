using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	[SerializeField] int levelNumber;
	[SerializeField] GameObject loadingScreen;
	GameObject[] allyUnits;
	GameObject[] enemyUnits;

	void Start()
	{
		allyUnits = GameObject.FindGameObjectsWithTag("Ally");
		enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
	}

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

		foreach (GameObject allyUnit in allyUnits)
			Destroy(allyUnit);
		
		foreach (GameObject enemyUnit in enemyUnits)
			Destroy(enemyUnit);

		yield return new WaitForSeconds(3.0f);
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopAll);
		Application.LoadLevel(levelNumber);
	}
}
