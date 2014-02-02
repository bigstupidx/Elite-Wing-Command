using UnityEngine;
using System.Collections;

public class EnemyVehicleMovement : GenericVehicleMovement
{
	[SerializeField] EnemyAI enemyAI;

	void Awake()
	{
		ObjectiveTag = enemyAI.ObjectiveTag;
	}

	void Update()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
		ClosestTargetDistance = enemyAI.ClosestTargetDistance;
		ClosestTargetID = enemyAI.ClosestTargetID;
	}
}