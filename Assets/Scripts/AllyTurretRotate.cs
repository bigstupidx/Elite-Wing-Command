using UnityEngine;
using System.Collections;

public class AllyTurretRotate : GenericTurretRotate
{
	[SerializeField] AllyAI allyAI;

	void FixedUpdate()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
	}
}