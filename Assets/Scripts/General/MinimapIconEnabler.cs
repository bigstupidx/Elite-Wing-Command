using UnityEngine;
using System.Collections;

public class MinimapIconEnabler : MonoBehaviour
{
	[SerializeField] GameObject mapIcon;

	void Start()
	{
		StartCoroutine(WaitAndEnable());
	}

	IEnumerator WaitAndEnable()
	{
		yield return new WaitForSeconds(0.2f);
		mapIcon.SetActive(true);
	}
}
