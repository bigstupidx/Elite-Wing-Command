using UnityEngine;
using System.Collections;

public class FreezeTime : MonoBehaviour
{

	void Start()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.F))
			CustomTimeManager.FadeTo(0f, 0.01f);
		else if (Input.GetKeyDown(KeyCode.R))
			CustomTimeManager.FadeTo(1.1f, 0.01f);
	}
}
