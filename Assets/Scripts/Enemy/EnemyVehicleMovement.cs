using UnityEngine;
using System.Collections;

public class EnemyVehicleMovement : GenericVehicleMovement
{
	[SerializeField] EnemyAI enemyAI;

	void Update()
	{
		ClosestTarget = enemyAI.ClosestTarget;
		ClosestTargetDistance = enemyAI.ClosestTargetDistance;
		ClosestTargetID = enemyAI.ClosestTargetID;
	}

	public override void Search()
	{
		if (MissionManagerScript != null)
		{
			if (MissionManagerScript.EnemyObjectivesList != null && MissionManagerScript.EnemyObjectivesList.Count != 0)
			{
				int r = Random.Range(0, (MissionManagerScript.EnemyObjectivesList.Count));
				GameObject objectiveTarget = MissionManagerScript.EnemyObjectivesList[r];

				if (objectiveTarget != null)
					TargetTransform = objectiveTarget.transform;
			}
		}
	}
}