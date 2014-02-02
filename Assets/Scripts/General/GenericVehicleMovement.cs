using UnityEngine;
using System.Collections;

public class GenericVehicleMovement : MonoBehaviour
{
	[SerializeField] NavMeshAgent navMeshAgent;
	[SerializeField] float standoffRange = 10f;
	MissionManager missionManager;
	string objectiveTag;
	float targetDistance;
	GameObject closestTarget;
	float closestTargetDistance;
	string closestTargetID;
	string enemyTurretID;
	string enemyVehicleID;
	Vector3 targetPosition;
	Vector2 targetXZPosition;
	Vector2 unitXZPosition;
	public MissionManager MissionManagerScript { get { return missionManager; }}
	public Vector3 TargetPosition { get { return targetPosition; } set { targetPosition = value; }}
	public string ObjectiveTag { get { return objectiveTag; } set { objectiveTag = value; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; } set { closestTargetDistance = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}

	void Start()
	{
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();

		StartCoroutine(SetNavMeshTarget());
	}

	IEnumerator SetNavMeshTarget()
	{
		while (true)
		{
			targetXZPosition = new Vector2(targetPosition.x, targetPosition.z);
			unitXZPosition = new Vector2(transform.position.x, transform.position.z);
			targetDistance = Vector2.Distance(targetXZPosition, unitXZPosition);

			if (targetDistance > standoffRange)
				navMeshAgent.SetDestination(targetPosition);
			else
				navMeshAgent.SetDestination(transform.position);

			yield return new WaitForSeconds(0.5f);
		}
	}

	public void Engage()
	{
		if (ClosestTarget != null)
			targetPosition = ClosestTarget.transform.position;
		else
			Search();
	}

	public virtual void Search()
	{
		Debug.LogError("Search should have override");
	}
}