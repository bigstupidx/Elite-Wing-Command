using UnityEngine;
using System.Collections;

public class EnemyTurretPitch : GenericTurretPitch
{
	[SerializeField] EnemyAI enemyAI;

	void FixedUpdate()
	{
		ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
	}
}
