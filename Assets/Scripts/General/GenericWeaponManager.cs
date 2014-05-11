using UnityEngine;
using System.Collections;

public class GenericWeaponManager : MonoBehaviour
{
	[SerializeField] float airAttackWidth = 0.9f;
	[SerializeField] float airAttackDistance = 35f;
	[SerializeField] float groundAttackWidth = 0.9f;
	[SerializeField] float groundAttackDistance = 15f;
	[SerializeField] float minShootTime = 0.3f;
	[SerializeField] float maxShootTime = 1.0f;
	[SerializeField] float minShootCooldown = 0.5f;
	[SerializeField] float maxShootCooldown = 2.0f;
	[SerializeField] Weapon airWeapon;
	[SerializeField] Weapon groundWeapon;
	[SerializeField] bool missileBattery = false;
	[SerializeField] bool needsClearShot = false;
	[SerializeField] float airRetriggerRate = 0.12f;
	[SerializeField] float groundRetriggerRate = 0.2f;
	string objectiveAirTag;
	string objectiveGroundTag;
	GameObject closestTarget;
	string closestTargetID;
	string enemyTurretID;
	string enemyVehicleID;
	bool canShoot = true;
	bool isAirUnit = false;
	ObjectType unitType;
	public string ObjectiveAirTag { get { return objectiveAirTag; } set { objectiveAirTag = value; }}
	public string ObjectiveGroundTag { get { return objectiveGroundTag; } set { objectiveGroundTag = value; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}
	public bool NeedsClearShot { get { return needsClearShot; } set { needsClearShot = value; }}
	public bool IsAirUnit { get { return isAirUnit; } set { isAirUnit = value; }}

	void Update()
	{
		if (ClosestTarget != null)
		{
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			Vector3 correctedPosition = transform.position;
			correctedPosition.y = ClosestTarget.transform.position.y;
			Vector3 toOther = ClosestTarget.transform.position - correctedPosition;
			Vector3 normalizedToOther = toOther.normalized;

			Vector2 targetXZPosition = new Vector2(ClosestTarget.transform.position.x, ClosestTarget.transform.position.z);
			Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
			float distance = Vector2.Distance(targetXZPosition, unitXZPosition);

			if (ClosestTargetID == EnemyTurretID || ClosestTargetID == EnemyVehicleID || ClosestTarget.tag == ObjectiveGroundTag)
			{
				if (canShoot && Vector3.Dot(forward, normalizedToOther) > groundAttackWidth && distance < groundAttackDistance)
				{
					if (NeedsClearShot)
					{
						RaycastHit hit;

						if (Physics.Linecast(transform.position, ClosestTarget.transform.position, out hit))
						{
							if (hit.transform.name == ClosestTarget.transform.name)
							{
								StartCoroutine(GroundFireControl());
								canShoot = false;
							}
						}
					}
					else
					{
						StartCoroutine(GroundFireControl());
						canShoot = false;
					}
				}
			}
			else
			{
				if (canShoot && Vector3.Dot(forward, normalizedToOther) > airAttackWidth && distance < airAttackDistance)
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

		if (airWeapon != null)
		{
			airWeapon.Fire();

			if (!missileBattery)
				InvokeRepeating("AirWeaponSFX", 0f, airRetriggerRate);

			yield return new WaitForSeconds(shootTime);
			airWeapon.Stop();

			if (!missileBattery)
				CancelInvoke("AirWeaponSFX");

			yield return new WaitForSeconds(shootCooldown);
			canShoot = true;
		}
	}

	IEnumerator GroundFireControl()
	{
		float shootTime = Random.Range(minShootTime, maxShootTime);
		float shootCooldown = Random.Range(minShootCooldown, maxShootCooldown);

		if (groundWeapon != null)
		{
			groundWeapon.Fire();
			InvokeRepeating("GroundWeaponSFX", 0f, groundRetriggerRate);
			yield return new WaitForSeconds(shootTime);
			groundWeapon.Stop();
			CancelInvoke("GroundWeaponSFX");
			yield return new WaitForSeconds(shootCooldown);
			canShoot = true;
		}
	}

	public void StopWeapon()
	{
		CancelInvoke("AirWeaponSFX");
		CancelInvoke("GroundWeaponSFX");
	}

	void AirWeaponSFX()
	{
		Fabric.EventManager.Instance.PostEvent("SFX_Aircraft_Fire", Fabric.EventAction.PlaySound, gameObject.transform.root.gameObject);
	}

	void GroundWeaponSFX()
	{
		if (!isAirUnit)
			Fabric.EventManager.Instance.PostEvent("SFX_Vehicle_Fire", Fabric.EventAction.PlaySound, gameObject);
	}
}
