using UnityEngine;
using System.Collections;

public class CheckIfValid : MonoBehaviour
{
	
	void Start()
	{
		if (Application.genuineCheckAvailable && !Application.genuine)
		{
			Application.Quit();
		}
	}
}
