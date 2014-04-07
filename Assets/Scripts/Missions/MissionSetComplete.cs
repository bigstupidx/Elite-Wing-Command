using UnityEngine;
using System.Collections;

public class MissionSetComplete : MonoBehaviour
{

	void Start()
	{
		int missionNumber = EncryptedPlayerPrefs.GetInt("Mission Number", 0);
		EncryptedPlayerPrefs.SetInt("Mission " + missionNumber.ToString() + " Status", 1);
	}
}
