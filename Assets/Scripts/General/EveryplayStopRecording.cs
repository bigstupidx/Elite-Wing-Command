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
		else if (Everyplay.IsRecording())
			StartCoroutine(WaitAndStopRecording());
	}

	void OnClick()
	{
		if (Everyplay.IsRecording())
			Everyplay.StopRecording();
	}

	IEnumerator WaitAndStopRecording()
	{
		yield return new WaitForSeconds(waitTime);
		Everyplay.StopRecording();
		shareRecordingButton.SetActive(true);
	}
}
