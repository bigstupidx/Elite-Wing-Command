using UnityEngine;
using System.Collections;

public class EveryplayStopRecording : MonoBehaviour
{
	[SerializeField] bool stopOnClick = false;
	[SerializeField] GameObject shareRecordingButton;
	[SerializeField] float waitTime;

	void Start()
	{
		if (stopOnClick)
			return;
		else if (Everyplay.SharedInstance.IsRecording())
			StartCoroutine(WaitAndStopRecording());
	}

	void OnClick()
	{
		if (Everyplay.SharedInstance.IsRecording())
			Everyplay.SharedInstance.StopRecording();
	}

	IEnumerator WaitAndStopRecording()
	{
		yield return new WaitForSeconds(waitTime);
		Everyplay.SharedInstance.StopRecording();
		shareRecordingButton.SetActive(true);
	}
}
