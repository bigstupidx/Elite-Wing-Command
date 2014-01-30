using UnityEngine;
using System.Collections;

public class DisableTurretArcadeScripts : MonoBehaviour
{
	[SerializeField] bool isMission = false;
	[SerializeField] MapTurretMover mapTurretMover;
	[SerializeField] MinimapIdentifierMover minimapIdentifierMover;

	void Awake()
	{
		if (isMission)
		{
			if (mapTurretMover != null)
				mapTurretMover.enabled = false;

			if (minimapIdentifierMover != null)
				minimapIdentifierMover.enabled = false;
		}
	}

}
