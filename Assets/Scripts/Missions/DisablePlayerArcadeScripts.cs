using UnityEngine;
using System.Collections;

public class DisablePlayerArcadeScripts : MonoBehaviour
{
	[SerializeField] MapConstraint mapConstraint;
	[SerializeField] GameObject collectionArea;
	MissionManager missionManager;

	void Start()
	{
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");

		if (missionManagerObject != null)
		{
			mapConstraint.enabled = false;
			collectionArea.SetActive(false);
		}
	}
}
