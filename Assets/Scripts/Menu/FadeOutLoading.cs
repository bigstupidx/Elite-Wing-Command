using UnityEngine;
using System.Collections;

public class FadeOutLoading : MonoBehaviour
{
	[SerializeField] TweenAlpha loadingTweenAlpha;

	void Start()
	{
		loadingTweenAlpha.ResetToBeginning();
		loadingTweenAlpha.enabled = true;
	}
}
