using UnityEngine;
using System.Collections;

public class DisableTurretArcadeScripts : MonoBehaviour
{
	[SerializeField] MapTurretMover mapTurretMover;
	public bool IsMission { get; set; }

	void Start()
	{
		if (IsMission && mapTurretMover != null)
			mapTurretMover.enabled = false;
	}

}
