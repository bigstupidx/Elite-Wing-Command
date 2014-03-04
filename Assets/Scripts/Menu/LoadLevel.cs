using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour
{
	[SerializeField] int levelNumber;

	void OnClick()
	{
		Application.LoadLevel(levelNumber);
	}
}
