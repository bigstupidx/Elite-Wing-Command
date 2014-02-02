using UnityEngine;
using System.Collections;

public class AllyVehicleMovement : GenericVehicleMovement
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