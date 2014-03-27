using UnityEngine;
using System.Collections;

public class MissionLabelStart : MonoBehaviour
{
	[SerializeField] UILabel label;
	[SerializeField] string labelText;

	void OnEnable()
	{
		label.text = labelText;
	}
}
