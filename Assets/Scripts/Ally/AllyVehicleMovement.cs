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
		ClosestTarget = allyAI.ClosestTarget;
		ClosestTargetDistance = allyAI.ClosestTargetDistance;
		ClosestTargetID = allyAI.ClosestTargetID;
	}

	public override void Search()
	{
		if (MissionManagerScript != null)
		{
			if (MissionManagerScript.AllyObjectivesList != null && MissionManagerScript.AllyObjectivesList.Count != 0)
			{
				int r = Random.Range(0, (MissionManagerScript.AllyObjectivesList.Count));
				GameObject objectiveTarget = MissionManagerScript.AllyObjectivesList[r];
				TargetPosition = objectiveTarget.transform.position;
			}
		}
	}
}