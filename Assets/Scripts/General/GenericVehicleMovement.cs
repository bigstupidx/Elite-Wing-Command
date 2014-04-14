using UnityEngine;
using System.Collections;

public class GenericVehicleMovement : MonoBehaviour
{
	[SerializeField] NavMeshAgent navMeshAgent;
	[SerializeField] float standoffRange = 10f;
	MissionManager missionManager;
	float targetDistance;
	GameObject closestTarget;
	float closestTargetDistance;
	string closestTargetID;
	string enemyTurretID;
	string enemyVehicleID;
	Transform targetTransform;
	Vector2 targetXZPosition;
	Vector2 unitXZPosition;
	string previousClosestTargetName;
	public MissionManager MissionManagerScript { get { return missionManager; }}
	public Transform TargetTransform { get { return targetTransform; } set { targetTransform = value; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; } set { closestTargetDistance = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}

	void Start()
	{
		Fabric.EventManager.Instance.PostEvent("SFX_Vehicle_Movement", Fabric.EventAction.PlaySound, gameObject);

		previousClosestTargetName = "_";
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
			missionManager = missionManagerObject.GetComponent<MissionManager>();

		StartCoroutine(SetNavMeshTarget());
	}

	IEnumerator SetNavMeshTarget()
	{
		while (true)
		{
			if (TargetTransform != null)
			{
				targetXZPosition = new Vector2(TargetTransform.position.x, TargetTransform.position.z);
				unitXZPosition = new Vector2(transform.position.x, transform.position.z);
				targetDistance = Vector2.Distance(targetXZPosition, unitXZPosition);

				if (targetDistance > standoffRange && TargetTransform.name != previousClosestTargetName)
				{
					previousClosestTargetName = TargetTransform.name;
					navMeshAgent.SetDestination(TargetTransform.position);
				}
				else if (targetDistance <= standoffRange)
				{
					RaycastHit hit;
					
					if (Physics.Linecast(transform.position, ClosestTarget.transform.position, out hit))
					{
						if (hit.transform.name != ClosestTarget.transform.name)
							navMeshAgent.SetDestination(transform.position);
					}
				}
			}

			yield return new WaitForSeconds(Random.Range(0.75f, 1f));
		}
	}

	public void Engage()
	{
		if (ClosestTarget != null)
			TargetTransform = ClosestTarget.transform;
		else
			Search();
	}

	public virtual void Search()
	{
		Debug.LogError("Search should have override");
	}
}