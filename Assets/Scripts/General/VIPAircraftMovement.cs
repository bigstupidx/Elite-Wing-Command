using UnityEngine;
using System.Collections;

public class VIPAircraftMovement : MonoBehaviour
{
	[SerializeField] float engineForce = 25f;
	[SerializeField] float targetReachedDistance = 10f;
	MissionManager missionManager;
	Transform travelEndPoint;
	float currentForce = 0f;
	float forceMultiplier = 1f;
	float timeModifier = 120f;
	float torqueModifier = 0.12f;
	float randomEngineForce;
	float angle;
	Vector3 offset;

	void Start()
	{
		float forceRandomizer = Random.Range(0.9f, 1.1f);
		randomEngineForce = engineForce * forceRandomizer;
		var travelEndObject = GameObject.FindGameObjectWithTag("TravelEndPoint");
		travelEndPoint = travelEndObject.transform;

		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, randomEngineForce * forceMultiplier, timeModifier * Time.fixedDeltaTime);
		GetComponent<Rigidbody>().AddForce(transform.forward * currentForce, ForceMode.Acceleration);
		var offset = transform.InverseTransformPoint(travelEndPoint.position);
		angle = Mathf.Rad2Deg * Mathf.Atan2(offset.x, offset.z);

		Vector2 targetXZPosition = new Vector2(travelEndPoint.position.x, travelEndPoint.position.z);
		Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
		float distance = Vector2.Distance(targetXZPosition, unitXZPosition);

		if (Mathf.Abs(angle) > 3f && distance > targetReachedDistance)
			GetComponent<Rigidbody>().AddTorque(Vector3.up * torqueModifier * Mathf.Sign(angle), ForceMode.VelocityChange);
		else if (distance <= targetReachedDistance)
			missionManager.VIPDestinationReached = true;
	}
}
