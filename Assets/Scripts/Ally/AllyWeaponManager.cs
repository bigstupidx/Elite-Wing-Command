using UnityEngine;
using System.Collections;

public class AllyWeaponManager : GenericWeaponManager
{
	[SerializeField] AllyAI allyAI;

	void Start()
	{
		IsAirUnit = allyAI.IsAirUnit;
		StartCoroutine(AllyTargeting());
	}

	IEnumerator AllyTargeting()
	{
		yield return new WaitForSeconds(0.1f);
		ObjectiveAirTag = allyAI.ObjectiveAirTag;
		ObjectiveGroundTag = allyAI.ObjectiveGroundTag;
		EnemyTurretID = allyAI.TargetTurretID;
		EnemyVehicleID = allyAI.TargetVehicleID;

		while (true)
		{
			ClosestTarget = allyAI.ClosestTarget;
			ClosestTargetID = allyAI.ClosestTargetID;
			yield return new WaitForSeconds(Random.Range(1f, 3f));
		}
	}
}
