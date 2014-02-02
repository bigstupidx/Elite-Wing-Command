using UnityEngine;
using System.Collections;

public class EnemyTurretPitch : GenericTurretPitch
{
	[SerializeField] EnemyAI enemyAI;

	void Update()
	{
		ClosestTarget = enemyAI.ClosestTarget;
	}
}
