using UnityEngine;
using System.Collections;

public class AllyTurretPitch : GenericTurretPitch
{
	[SerializeField] AllyAI allyAI;

	void FixedUpdate()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
	}
}
