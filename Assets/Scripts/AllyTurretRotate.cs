using UnityEngine;
using System.Collections;

public class AllyTurretRotate : GenericTurretRotate
{
	[SerializeField] AllyAI allyAI;

	void Update()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
	}
}