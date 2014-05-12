using UnityEngine;
using System.Collections;

public class VIPVehicleMovement : MonoBehaviour
{
	[SerializeField] float targetReachedDistance = 10f;
	NavMeshAgent navMeshAgent;
	MissionManager missionManager;
	GameObject travelEndObject;
	Transform travelEndPoint;
	float targetDistance;
	Vector3 targetPosition;
	Vector2 targetXZPosition;
	Vector2 unitXZPosition;

	void Start()
	{
		navMeshAgent = GetComponent<NavMeshAgent>();

		StartCoroutine(WaitAndSetNav());
		StartCoroutine(CheckNavMeshDistance());
	}

	IEnumerator WaitAndSetNav()
	{
		yield return new WaitForSeconds(1.0f);
		travelEndObject = GameObject.FindGameObjectWithTag("TravelEndPoint");
		travelEndPoint = travelEndObject.transform;
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();

		if (travelEndObject != null)
			navMeshAgent.SetDestination(travelEndPoint.position);
		else
			Debug.Log("No Travel End Point");
	}

	IEnumerator CheckNavMeshDistance()
	{
		yield return new WaitForSeconds(2.0f);
		while (true)
		{
			targetXZPosition = new Vector2(travelEndPoint.position.x, travelEndPoint.position.z);
			unitXZPosition = new Vector2(transform.position.x, transform.position.z);
			targetDistance = Vector2.Distance(targetXZPosition, unitXZPosition);

			if (targetDistance <= targetReachedDistance)
				missionManager.VIPDestinationReached = true;

			yield return new WaitForSeconds(1f);
		}
	}
}