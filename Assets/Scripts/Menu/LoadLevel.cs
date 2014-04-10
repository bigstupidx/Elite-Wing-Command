using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	[SerializeField] int levelNumber;

	void OnClick()
	{
		StartCoroutine(WaitAndLoad());
	}

	IEnumerator WaitAndLoad()
	{
		CustomTimeManager.FadeTo(1.1f, 0.01f);
		yield return new WaitForSeconds(2.0f);
		Application.LoadLevel(levelNumber);
	}

}
