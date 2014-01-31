using UnityEngine;
using System.Collections;

public class DisableTurretArcadeScripts : MonoBehaviour
{
	[SerializeField] bool isMission = false;
	[SerializeField] MapTurretMover mapTurretMover;

	void Awake()
	{
		if (isMission && mapTurretMover != null)
			mapTurretMover.enabled = false;
	}

}
