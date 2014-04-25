using UnityEngine;
using System.Collections;

public class EveryplayStopRecording : MonoBehaviour
{
	[SerializeField] GameObject shareRecordingButton;
	[SerializeField] float waitTime;

	void Start()
	{
		if (Everyplay.SharedInstance.IsRecording())
			StartCoroutine(WaitAndStopRecording());
	}

	IEnumerator WaitAndStopRecording()
	{
		yield return new WaitForSeconds(waitTime);
		Everyplay.SharedInstance.StopRecording();
		shareRecordingButton.SetActive(true);
	}
}
