using UnityEngine;
using System.Collections;

public class AllyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] AllyAI allyAI;

	void Awake()
	{
		ObjectiveTag = allyAI.ObjectiveTag;
	}

	void Update()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
		ClosestTargetDistance = allyAI.ClosestTargetDistance;
		ClosestTargetID = allyAI.ClosestTargetID;
	}
}