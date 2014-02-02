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
			EnemyTurretID = allyAI.TargetTurretID;
			EnemyVehicleID = allyAI.TargetVehicleID;
			ClosestTarget = GameObject.Find(allyAI.ClosestTargetName);
			ClosestTargetID = allyAI.ClosestTargetID;
			NeedsClearShot = unitNeedsClearShot;
			yield return new WaitForSeconds(0.5f);
		}
	}
}
