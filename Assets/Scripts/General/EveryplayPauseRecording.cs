using UnityEngine;
using System.Collections;

public class EveryplayPauseRecording : MonoBehaviour
{

	void OnClick()
	{
		Everyplay.SharedInstance.PauseRecording();
	}
}
