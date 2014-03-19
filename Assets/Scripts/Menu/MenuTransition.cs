using UnityEngine;
using System.Collections;

public class MenuTransition : MonoBehaviour
{
	[SerializeField] GameObject currentScreen;
	[SerializeField] GameObject nextScreen;
	[SerializeField] float tweenTime = 0.25f;
	[SerializeField] GameObject altCamera;
	[SerializeField] bool enableAltCamera = false;

	void OnClick()
	{
		StartCoroutine(TransitionToNext());
	}

	IEnumerator TransitionToNext()
	{
		TweenAlpha currentTweenAlpha = currentScreen.GetComponent<TweenAlpha>();
		currentTweenAlpha.from = 1f;
		currentTweenAlpha.to = 0f;
		currentTweenAlpha.ResetToBeginning();
		currentTweenAlpha.enabled = true;
		yield return new WaitForSeconds (tweenTime);

		if (altCamera != null)
			altCamera.SetActive(enableAltCamera);

		TweenAlpha nextTweenAlpha = nextScreen.GetComponent<TweenAlpha>();
		nextTweenAlpha.from = 0f;
		nextTweenAlpha.to = 1f;
		nextTweenAlpha.ResetToBeginning();
		nextTweenAlpha.enabled = true;
		nextScreen.SetActive(true);
		yield return new WaitForSeconds(tweenTime);

		currentTweenAlpha.enabled = false;
		nextTweenAlpha.enabled = false;
		currentScreen.SetActive(false);
	}
}
