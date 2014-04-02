using UnityEngine;
using System.Collections;

public class ReplayMission : MonoBehaviour
{

	void OnClick()
	{
		Application.LoadLevel(PlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
