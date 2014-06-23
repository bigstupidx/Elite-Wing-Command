using UnityEngine;
using System.Collections;

public class EveryplayShareRecording : MonoBehaviour
{

	void OnClick()
	{
		if (Everyplay.IsRecording())
			Everyplay.StopRecording();

		Everyplay.ShowSharingModal();
	}
}
