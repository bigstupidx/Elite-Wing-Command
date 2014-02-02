using UnityEngine;
using System.Collections;

public class AllyWeaponManager : GenericWeaponManager
{
	[SerializeField] AllyAI allyAI;
	[SerializeField] bool unitNeedsClearShot = false;

	void Start()
	{
		StartCoroutine(AllyTargeting());
	}

	IEnumerator AllyTargeting()
	{
		while (true)
		{
			ObjectiveTag = allyAI.ObjectiveTag;
			ClosestTarget = allyAI.ClosestTarget;
			ClosestTargetID = allyAI.ClosestTargetID;
			EnemyTurretID = allyAI.TargetTurretID;
			EnemyVehicleID = allyAI.TargetVehicleID;
			NeedsClearShot = unitNeedsClearShot;
			yield return new WaitForSeconds(1.0f);
		}
	}
}
