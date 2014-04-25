using UnityEngine;
using System.Collections;

public class EveryplayShareRecording : MonoBehaviour
{

	void OnClick()
	{
		Everyplay.SharedInstance.ShowSharingModal();
	}
}
