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
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopAll);
		Fabric.EventManager.Instance.PostEvent("SFX_Button_General", Fabric.EventAction.PlaySound);
		StartCoroutine(WaitAndLoad());
	}

	IEnumerator WaitAndLoad()
	{
		if (loadingScreen != null)
			loadingScreen.SetActive(true);

		CustomTimeManager.FadeTo(1.1f, 0.01f);

		foreach (GameObject allyUnit in allyUnits)
			Destroy(allyUnit);
		
		foreach (GameObject enemyUnit in enemyUnits)
			Destroy(enemyUnit);

		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(levelNumber);
	}
}
