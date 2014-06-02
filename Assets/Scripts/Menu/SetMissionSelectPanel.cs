using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetMissionSelectPanel : MonoBehaviour
{
	[SerializeField] List<GameObject> missionObjects = new List<GameObject>();
	int activeMissionCount = 2;
	Vector3 modifiedPosition;

	void Start()
	{
		StartCoroutine(WaitAndFindPosition());
	}

	IEnumerator WaitAndFindPosition()
	{
		yield return new WaitForSeconds(0.01f);

		foreach (GameObject missionObject in missionObjects)
		{
			MissionUnlockRequirement unlockStatusScript = missionObject.GetComponent<MissionUnlockRequirement>();

			if (unlockStatusScript != null && unlockStatusScript.IsActive == true)
				activeMissionCount++;
		}
		
		if (activeMissionCount > 2 && activeMissionCount < (missionObjects.Count - 1))
		{
			modifiedPosition = transform.localPosition;
			modifiedPosition.y += (activeMissionCount - 2) * 130f;
			transform.localPosition = modifiedPosition;
		}
		else if (activeMissionCount >= (missionObjects.Count - 1))
		{
			modifiedPosition = transform.localPosition;
			modifiedPosition.y += (missionObjects.Count - 3) * 132f;
			transform.localPosition = modifiedPosition;
		}
	}
}