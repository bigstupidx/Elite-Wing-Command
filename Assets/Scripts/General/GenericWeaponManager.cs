using UnityEngine;
using System.Collections;

public class GenericWeaponManager : MonoBehaviour
{
	[SerializeField] float attackWidth = 0.9f;
	[SerializeField] float airAttackDistance = 35f;
	[SerializeField] float groundAttackDistance = 15f;
	[SerializeField] float minShootTime = 0.3f;
	[SerializeField] float maxShootTime = 1.0f;
	[SerializeField] float minShootCooldown = 0.5f;
	[SerializeField] float maxShootCooldown = 2.0f;
	[SerializeField] Weapon airWeapon;
	[SerializeField] Weapon groundWeapon;
	GameObject closestTarget;
	string closestTargetID;
	string enemyTurretID;
	string enemyVehicleID;
	bool canShoot = true;
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}

	void Update()
	{
		if (closestTarget != null)
		{
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 correctedPosition = transform.position;
			correctedPosition.y = ClosestTarget.transform.position.y;
			Vector3 toOther = ClosestTarget.transform.root.position - correctedPosition;
			Vector3 normalizedToOther = toOther.normalized;

			Vector2 targetXZPosition = new Vector2(closestTarget.transform.root.position.x, closestTarget.transform.root.position.z);
			Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
			float distance = Vector2.Distance(targetXZPosition, unitXZPosition);

			if (ClosestTargetID == EnemyTurretID || ClosestTargetID == EnemyVehicleID)
			{
				if (canShoot && Vector3.Dot(forward, normalizedToOther) > attackWidth && distance < groundAttackDistance)
				{
					StartCoroutine(GroundFireControl());
					canShoot = false;
				}
			}
			else
			{
				if (canShoot && Vector3.Dot(forward, normalizedToOther) > attackWidth && distance < airAttackDistance)
				{
					StartCoroutine(AirFireControl());
					canShoot = false;
				}
			}
		}
	}

	IEnumerator AirFireControl()
	{
		float shootTime = Random.Range(minShootTime, maxShootTime);
		float shootCooldown = Random.Range(minShootCooldown, maxShootCooldown);
		airWeapon.Fire();
		yield return new WaitForSeconds(shootTime);
		airWeapon.Stop();
		yield return new WaitForSeconds(shootCooldown);
		canShoot = true;
	}

	IEnumerator GroundFireControl()
	{
		float shootTime = Random.Range(minShootTime, maxShootTime);
		float shootCooldown = Random.Range(minShootCooldown, maxShootCooldown);
		groundWeapon.Fire();
		yield return new WaitForSeconds(shootTime);
		groundWeapon.Stop();
		yield return new WaitForSeconds(shootCooldown);
		canShoot = true;
	}
}
