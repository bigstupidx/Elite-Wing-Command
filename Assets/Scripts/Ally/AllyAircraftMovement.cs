using UnityEngine;
using System.Collections;

public class AllyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] AllyAI allyAI;

	void Update()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
		ClosestTargetDistance = allyAI.ClosestTargetDistance;
		ClosestTargetID = allyAI.ClosestTargetID;
	}
}