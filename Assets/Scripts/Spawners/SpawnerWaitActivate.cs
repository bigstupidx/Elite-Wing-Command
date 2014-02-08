using UnityEngine;
using System.Collections;

public class SpawnerWaitActivate : MonoBehaviour
{
	[SerializeField] GameObject[] spawners;
	[SerializeField] float waitTime = 0;

	void Start ()
	{
		StartCoroutine(EnableAfterWait());
	}

	IEnumerator EnableAfterWait()
	{
		yield return new WaitForSeconds(waitTime);

		foreach (GameObject spawner in spawners)
		{
			spawner.SetActive(true);
		}
	}
}
