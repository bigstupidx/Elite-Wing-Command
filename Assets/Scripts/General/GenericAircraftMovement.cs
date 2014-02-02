using UnityEngine;
using System.Collections;

public class GenericAircraftMovement : MonoBehaviour
{
	[SerializeField] float engineForce = 25f;
	[SerializeField] float aircraftEvasionDistance = 8f;
	[SerializeField] float groundTargetEvasionDistance = 8f;
	[SerializeField] float escortPerimeter = 50f;
	//[SerializeField] bool isDefensiveAirUnit = false;
	[SerializeField] float defensiveAirPerimeter = 25f;
	MissionManager missionManager;
	string objectiveTag;
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
	//Vector3 targetXZPosition;
	//Vector2 unitXZPosition;
	Vector3 randomPosition;
	bool findRandomAngle = true;
	public MissionManager MissionManagerScript { get { return missionManager; }}
	public string ObjectiveTag { get { return objectiveTag; } set { objectiveTag = value; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; } set { closestTargetDistance = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}
	public Vector3 Offset { get { return offset; } set { offset = value; }}
	public float EscortPerimeter { get { return escortPerimeter; }}
	//public bool IsDefensiveAirUnit { get { return isDefensiveAirUnit; }}
	public float DefensiveAirPerimeter { get { return defensiveAirPerimeter; }}
	//public Vector3 TargetXZPosition { get { return targetXZPosition; } set { targetXZPosition = value; }}
	//public Vector2 UnitXZPosition { get { return unitXZPosition; } set { unitXZPosition = value; }}
	public Vector3 RandomPosition { get { return randomPosition; } set { randomPosition = value; }}
	public bool FindRandomAngle { get { return findRandomAngle; } set { findRandomAngle = value; }}

	void Start()
	{
		float forceRandomizer = Random.Range(0.9f, 1.1f);
		randomEngineForce = engineForce * forceRandomizer;
		RandomPosition = new Vector3(0f, 0f, 0f);

		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, randomEngineForce * forceMultiplier, timeModifier * Time.fixedDeltaTime);
		rigidbody.AddForce(transform.forward * currentForce, ForceMode.Acceleration);
		angle = Mathf.Rad2Deg * Mathf.Atan2(Offset.x, Offset.z);

		if (ClosestTargetID == EnemyTurretID || ClosestTargetID == enemyVehicleID || ClosestTargetID == ObjectiveTag)
			evasionDistance = groundTargetEvasionDistance;
		else
			evasionDistance = aircraftEvasionDistance;

		if (Mathf.Abs(angle) > 3f && ClosestTargetDistance > evasionDistance)
		{
			rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(angle), ForceMode.VelocityChange);
		}
		else
		{
			rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(-angle * evasionAngleModifier), ForceMode.VelocityChange);
		}
	}

	public void Engage()
	{
		if (ClosestTarget != null)
		{
			Vector3 targetPosition = ClosestTarget.transform.position;
			Offset = transform.InverseTransformPoint(targetPosition);
		}
		else
			Search();
	}

	public virtual void Search()
	{
		Debug.LogError("Function should have override");
	}

	public IEnumerator FindRandomAngleWait()
	{
		float waitTime = Random.Range(1f, 5f);
		yield return new WaitForSeconds(waitTime);
		FindRandomAngle = true;
	}
}
