using UnityEngine;
using System.Collections;

public class GenericAircraftMovement : MonoBehaviour
{
	[SerializeField] float engineForce = 25f;
	[SerializeField] float aircraftEvasionDistance = 8f;
	[SerializeField] float groundTargetEvasionDistance = 8f;
	GameObject closestTarget;
	float closestTargetDistance;
	string closestTargetID;
	string enemyTurretID;
	string enemyVehicleID;
	float evasionDistance;
	float currentForce = 0f;
	float forceMultiplier = 1f;
	float timeModifier = 120f;
	float torqueModifier = 0.12f;
	float evasionAngleModifier = 10f;
	float randomEngineForce;
	float angle;
	Vector3 offset;
	Vector3 randomPosition;
	bool findRandomAngle = true;
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; } set { closestTargetDistance = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}

	void Start()
	{
		float forceRandomizer = Random.Range(0.9f, 1.1f);
		randomEngineForce = engineForce * forceRandomizer;
		randomPosition = new Vector3(0f, 0f, 0f);
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, randomEngineForce * forceMultiplier, timeModifier * Time.fixedDeltaTime);
		rigidbody.AddForce(transform.forward * currentForce, ForceMode.Acceleration);
		angle = Mathf.Rad2Deg * Mathf.Atan2(offset.x, offset.z);

		if (closestTargetID == EnemyTurretID || closestTargetID == enemyVehicleID)
			evasionDistance = groundTargetEvasionDistance;
		else
			evasionDistance = aircraftEvasionDistance;

		if (Mathf.Abs(angle) > 3f && closestTargetDistance > evasionDistance)
			rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(angle), ForceMode.VelocityChange);
		else
			rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(-angle * evasionAngleModifier), ForceMode.VelocityChange);
	}

	public void Engage()
	{
		if (closestTarget != null)
		{
			Vector3 targetPosition = closestTarget.transform.position;
			offset = transform.InverseTransformPoint(targetPosition);
		}
		else
			Wander();
	}

	public void Wander()
	{
		if (findRandomAngle == true)
		{
			findRandomAngle = false;
			randomPosition = new Vector3(Random.Range(-90f, 90f), transform.position.y, Random.Range(-90f, 90f));
			StartCoroutine(FindRandomAngleWait());
		}
		
		offset = transform.InverseTransformPoint(randomPosition);
	}

	IEnumerator FindRandomAngleWait()
	{
		float waitTime = Random.Range(1f, 5f);
		yield return new WaitForSeconds(waitTime);
		findRandomAngle = true;
	}
}
