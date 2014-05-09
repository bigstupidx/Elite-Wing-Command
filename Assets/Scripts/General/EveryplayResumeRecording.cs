using UnityEngine;
using System.Collections;

public class EveryplayResumeRecording : MonoBehaviour
{

	void OnClick()
	{
		Everyplay.SharedInstance.ResumeRecording();
	}
}
