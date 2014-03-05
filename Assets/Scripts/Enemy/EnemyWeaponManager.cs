using UnityEngine;
using System.Collections;

public class EnemyWeaponManager : GenericWeaponManager
{
	[SerializeField] EnemyAI enemyAI;
	
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
			yield return new WaitForSeconds(Random.Range(0.75f, 1f));
		}
	}
}
