using UnityEngine;
using System.Collections;

public class ReplayMission : MonoBehaviour
{

	void OnClick()
	{
		Application.LoadLevel(EncryptedPlayerPrefs.GetInt("Mission Scene Number", 0));
	}
}
