using UnityEngine;
using System.Collections;

public class AllyWeaponManager : GenericWeaponManager
{
	[SerializeField] AllyAI allyAI;

	void Start()
	{
		StartCoroutine(AllyTargeting());
	}

	IEnumerator AllyTargeting()
	{
		while (true)
		{
			ObjectiveAirTag = allyAI.ObjectiveAirTag;
			ObjectiveGroundTag = allyAI.ObjectiveGroundTag;
			ClosestTarget = allyAI.ClosestTarget;
			ClosestTargetID = allyAI.ClosestTargetID;
			EnemyTurretID = allyAI.TargetTurretID;
			EnemyVehicleID = allyAI.TargetVehicleID;
			yield return new WaitForSeconds(1.0f);
		}
	}
}
