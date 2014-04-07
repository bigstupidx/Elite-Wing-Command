using UnityEngine;
using System.Collections;

public class CompleteTutorial : MonoBehaviour
{
	
	void Start ()
	{
		EncryptedPlayerPrefs.SetInt("Mission 999 Status", 1);
	}
}
