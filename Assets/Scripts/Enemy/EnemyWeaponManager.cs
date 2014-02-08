using UnityEngine;
using System.Collections;

public class EnemyWeaponManager : GenericWeaponManager
{
	[SerializeField] EnemyAI enemyAI;
	[SerializeField] bool unitNeedsClearShot = false;
	
	void Start()
	{
		StartCoroutine(EnemyTargeting());
	}
	
	IEnumerator EnemyTargeting()
	{
		while (true)
		{
			ObjectiveAirTag = enemyAI.ObjectiveAirTag;
			ObjectiveGroundTag = enemyAI.ObjectiveGroundTag;
			ClosestTarget = enemyAI.ClosestTarget;
			ClosestTargetID = enemyAI.ClosestTargetID;
			EnemyTurretID = enemyAI.TargetTurretID;
			EnemyVehicleID = enemyAI.TargetVehicleID;
			NeedsClearShot = unitNeedsClearShot;
			yield return new WaitForSeconds(1.0f);
		}
	}
}
