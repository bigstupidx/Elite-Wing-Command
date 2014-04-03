using UnityEngine;
using System.Collections;

public class MissionSetComplete : MonoBehaviour
{

	void Start()
	{
		int missionNumber = PlayerPrefs.GetInt("Mission Number", 0);
		PlayerPrefs.SetInt("Mission " + missionNumber.ToString() + " Status", 1);
	}
}
