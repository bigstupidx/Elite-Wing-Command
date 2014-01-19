using UnityEngine;
using System.Collections;

public class GenericVehicleMovement : MonoBehaviour
{
	[SerializeField] NavMeshAgent navMeshAgent;
	[SerializeField] Vector3[] waypointPositions;
	Vector3 waypointPosition;

	void Start()
	{
		StartCoroutine(SetNavMeshTarget());
	}

	IEnumerator SetNavMeshTarget()
	{
		int i = -1;

		while (true)
		{
			i++;

			if (i > (waypointPositions.Length - 1))
				i = 0;

			navMeshAgent.SetDestination(waypointPositions[i]);
			yield return new WaitForSeconds(4.0f);
		}
	}
}