using UnityEngine;
using System.Collections;

public class EnemyTurretRotate : GenericTurretRotate
{
	[SerializeField] EnemyAI enemyAI;

	void FixedUpdate()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
	}
}