using UnityEngine;
using System.Collections;

public class VIPVehicleMovement : MonoBehaviour
{
	[SerializeField] NavMeshAgent navMeshAgent;
	[SerializeField] float targetReachedDistance = 10f;
	MissionManager missionManager;
	Transform travelEndPoint;
	float targetDistance;
	Vector3 targetPosition;
	Vector2 targetXZPosition;
	Vector2 unitXZPosition;

	void Start()
	{
		var travelEndObject = GameObject.FindGameObjectWithTag("TravelEndPoint");
		travelEndPoint = travelEndObject.transform;
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();

		navMeshAgent.SetDestination(travelEndPoint.position);
		StartCoroutine(SetNavMeshTarget());
	}

	IEnumerator SetNavMeshTarget()
	{
		while (true)
		{
			targetXZPosition = new Vector2(travelEndPoint.position.x, travelEndPoint.position.z);
			unitXZPosition = new Vector2(transform.position.x, transform.position.z);
			targetDistance = Vector2.Distance(targetXZPosition, unitXZPosition);

			if (targetDistance < targetReachedDistance)
				missionManager.VIPDestinationReached = true;

			yield return new WaitForSeconds(0.5f);
		}
	}
}