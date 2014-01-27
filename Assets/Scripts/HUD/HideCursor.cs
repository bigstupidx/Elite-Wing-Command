using UnityEngine;
using System.Collections;

public class HideCursor : MonoBehaviour
{
	[SerializeField] bool hideCursor = true;

	void Awake()
	{
		if (hideCursor)
			Screen.showCursor = false;
		else
			Screen.showCursor = true;
	}
}
