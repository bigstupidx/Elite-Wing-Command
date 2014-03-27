using UnityEngine;
using System.Collections;

public class MissionSetComplete : MonoBehaviour
{
	[SerializeField] int missionNumber = 101;

	void Start()
	{
		PlayerPrefs.SetInt("Mission " + missionNumber.ToString() + " Status", 1);
	}
}
