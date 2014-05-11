using UnityEngine;
using System.Collections;

public class MinimapIconEnabler : MonoBehaviour
{
	[SerializeField] KGFMapIcon mapIcon;

	void OnEnable()
	{
		mapIcon.SetVisibility(true);
	}
}
