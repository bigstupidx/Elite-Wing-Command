using UnityEngine;
using System.Collections;

public class EnemyTurretRotate : GenericTurretRotate
{
	[SerializeField] EnemyAI enemyAI;

	void Update()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
	}
}