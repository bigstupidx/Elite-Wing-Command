using UnityEngine;
using System.Collections;

public class EnemyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] EnemyAI enemyAI;

	void Update()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
		ClosestTargetDistance = enemyAI.ClosestTargetDistance;
		ClosestTargetID = enemyAI.ClosestTargetID;
	}
}