using UnityEngine;
using System.Collections;

public class GenericAircraftMovement : MonoBehaviour
{
	[SerializeField] bool allyAircraft = false;
	[SerializeField] float engineForce = 25f;
	[SerializeField] float aircraftEvasionDistance = 8f;
	[SerializeField] float groundTargetEvasionDistance = 8f;
	[SerializeField] float escortPerimeter = 50f;
	[SerializeField] float defensiveAirPerimeter = 25f;
	MissionManager missionManager;
	string objectiveGroundTag;
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
	Vector3 targetPosition;
	Vector3 randomPosition;
	bool findRandomAngle = true;
	public MissionManager MissionManagerScript { get { return missionManager; }}
	public string ObjectiveGroundTag { get { return objectiveGroundTag; } set { objectiveGroundTag = value; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; } set { closestTargetDistance = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}
	public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; }}
	public float EscortPerimeter { get { return escortPerimeter; }}
	public float DefensiveAirPerimeter { get { return defensiveAirPerimeter; }}
	public Vector3 RandomPosition { get { return randomPosition; } set { randomPosition = value; }}
	public bool FindRandomAngle { get { return findRandomAngle; } set { findRandomAngle = value; }}

	void Start()
	{
		float forceRandomizer = Random.Range(0.9f, 1.1f);
		randomEngineForce = engineForce * forceRandomizer;
		RandomPosition = new Vector3(0f, 0f, 0f);

		if (allyAircraft)
			forceMultiplier = PlayerPrefs.GetFloat("Ally Air Speed Multiplier", 1f);

		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, randomEngineForce * forceMultiplier, timeModifier * Time.fixedDeltaTime);
		rigidbody.AddForce(transform.forward * currentForce, ForceMode.Acceleration);
		offset = transform.InverseTransformPoint(TargetPosition);
		angle = Mathf.Rad2Deg * Mathf.Atan2(offset.x, offset.z);

		if (ClosestTargetID == EnemyTurretID || ClosestTargetID == enemyVehicleID || ClosestTargetID == ObjectiveGroundTag)
			evasionDistance = groundTargetEvasionDistance;
		else
			evasionDistance = aircraftEvasionDistance;

		if (Mathf.Abs(angle) > 3f && ClosestTargetDistance > evasionDistance)
		{
			rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(angle), ForceMode.VelocityChange);
		}
		else if (ClosestTargetDistance <= evasionDistance)
		{
			rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(-angle * evasionAngleModifier), ForceMode.VelocityChange);
		}
	}

	public void Engage()
	{
		if (ClosestTarget != null)
		{
			TargetPosition = ClosestTarget.transform.position;
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
		yield return new WaitForSeconds(Random.Range(1f, 5f));
		FindRandomAngle = true;
	}
}
