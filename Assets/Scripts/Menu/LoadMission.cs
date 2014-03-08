using UnityEngine;
using System.Collections;

public class LoadMission : MonoBehaviour
{
	[SerializeField] int loadLevel;
	[SerializeField] GameObject missionLoad;
	
	void OnClick()
	{
		missionLoad.SetActive(true);
		StartCoroutine(WaitAndLoad());
	}
	
	IEnumerator WaitAndLoad()
	{
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(loadLevel);
	}
}
