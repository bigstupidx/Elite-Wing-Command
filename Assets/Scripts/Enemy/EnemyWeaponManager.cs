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
			ObjectiveTag = enemyAI.ObjectiveTag;
			EnemyTurretID = enemyAI.TargetTurretID;
			EnemyVehicleID = enemyAI.TargetVehicleID;
			ClosestTarget = GameObject.Find(enemyAI.ClosestTargetName);
			ClosestTargetID = enemyAI.ClosestTargetID;
			NeedsClearShot = unitNeedsClearShot;
			yield return new WaitForSeconds(0.5f);
		}
	}
}
