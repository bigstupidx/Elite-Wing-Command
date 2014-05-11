using UnityEngine;
using System.Collections;

public class EnemyWeaponManager : GenericWeaponManager
{
	[SerializeField] EnemyAI enemyAI;
	
	void Start()
	{
		IsAirUnit = enemyAI.IsAirUnit;
		StartCoroutine(EnemyTargeting());
	}
	
	IEnumerator EnemyTargeting()
	{
		yield return new WaitForSeconds(0.1f);
		ObjectiveAirTag = enemyAI.ObjectiveAirTag;
		ObjectiveGroundTag = enemyAI.ObjectiveGroundTag;
		EnemyTurretID = enemyAI.TargetTurretID;
		EnemyVehicleID = enemyAI.TargetVehicleID;

		while (true)
		{
			ClosestTarget = enemyAI.ClosestTarget;
			ClosestTargetID = enemyAI.ClosestTargetID;
			yield return new WaitForSeconds(Random.Range(1f, 3f));
		}
	}
}
