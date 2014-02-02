using UnityEngine;
using System.Collections;

public class AllyTurretRotate : GenericTurretRotate
{
	[SerializeField] AllyAI allyAI;

	void Start()
	{
		if (allyAI.IsGroundUnit)
			NeedsClearShot = true;
		else
			NeedsClearShot = false;
	}

	void Update()
	{
		ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
	}
}