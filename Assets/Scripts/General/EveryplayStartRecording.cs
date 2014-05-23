using UnityEngine;
using System.Collections;

public class EveryplayStartRecording : MonoBehaviour
{

	void OnClick()
	{
		if (Everyplay.IsRecordingSupported())
			Everyplay.StartRecording();
	}
}
