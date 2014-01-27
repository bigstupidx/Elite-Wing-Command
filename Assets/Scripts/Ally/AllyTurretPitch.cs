using UnityEngine;
using System.Collections;

public class AllyTurretPitch : GenericTurretPitch
{
	[SerializeField] AllyAI allyAI;

	void Update()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
	}
}
