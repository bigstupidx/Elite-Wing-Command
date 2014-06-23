using UnityEngine;
using System.Collections;

public class ScrollText : MonoBehaviour
{
	[SerializeField] GameObject textObject;
	float initialXPosition = 1040f;
	float scrollSpeed = 80f;
	Vector3 textObjectPos;

	void Start ()
	{
		textObjectPos = new Vector3(initialXPosition, transform.localPosition.y, transform.localPosition.z);
		textObject.transform.localPosition = textObjectPos;
	}

	void Update()
	{
		textObjectPos.x -= CustomTimeManager.DeltaTime * scrollSpeed;

		if (textObjectPos.x < -initialXPosition)
			textObjectPos.x = initialXPosition;

		textObject.transform.localPosition = textObjectPos;
	}
}
