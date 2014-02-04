using UnityEngine;
using System.Collections;

public class EnemyTurretRotate : GenericTurretRotate
{
	[SerializeField] EnemyAI enemyAI;

	void Start()
	{
		EnemyWeaponManager enemyWeaponManager = transform.GetComponent<EnemyWeaponManager>();
		
		if (enemyWeaponManager != null)
			NeedsClearShot = enemyWeaponManager.NeedsClearShot;
	}

	void Update()
	{
		ClosestTarget = enemyAI.ClosestTarget;
	}
}