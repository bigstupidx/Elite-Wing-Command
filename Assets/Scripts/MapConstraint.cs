using UnityEngine;
using System.Collections;

public class MapConstraint : MonoBehaviour
{
	[SerializeField] MapAircraftMover mapAircraftMover;
	[SerializeField] const float mapBoundary = 100f;

	void Update()
	{
		Vector3 objectPosition = transform.root.position;

		if (objectPosition.x < -mapBoundary)
		{
			mapAircraftMover.RepositionX(mapBoundary);
			objectPosition.x = mapBoundary;
			transform.root.position = objectPosition;
		}
		else if (objectPosition.x > mapBoundary)
		{
			mapAircraftMover.RepositionX(-mapBoundary);
			objectPosition.x = -mapBoundary;
			transform.root.position = objectPosition;
		}
		else if (objectPosition.z < -mapBoundary)
		{
			mapAircraftMover.RepositionZ(mapBoundary);
			objectPosition.z = mapBoundary;
			transform.root.position = objectPosition;
		}
		else if (objectPosition.z > mapBoundary)
		{
			mapAircraftMover.RepositionZ(-mapBoundary);
			objectPosition.z = -mapBoundary;
			transform.root.position = objectPosition;
		}
	}
}
