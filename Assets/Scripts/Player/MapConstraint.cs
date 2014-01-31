using UnityEngine;
using System.Collections;

public class MapConstraint : MonoBehaviour
{
	[SerializeField] MapAircraftMover mapAircraftMover;
	[SerializeField] const float mapBoundary = 100f;
	Transform mainCamera;
	Vector3 wantedPosition;
	float cameraHeight = 35f;

	void Start()
	{
		GameObject mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
		mainCamera = mainCameraObject.transform;
		wantedPosition = transform.position;
		wantedPosition.y = cameraHeight;
	}

	void Update()
	{
		Vector3 objectPosition = transform.position;

		if (objectPosition.x < -mapBoundary)
		{
			mapAircraftMover.RepositionX(mapBoundary);
			objectPosition.x = mapBoundary;
			var xDiff = transform.position.x - mainCamera.position.x;
			var zDiff = transform.position.z - mainCamera.position.z;
			transform.position = objectPosition;
			wantedPosition.x = transform.position.x - xDiff;
			wantedPosition.z = transform.position.z - zDiff;
			mainCamera.position = wantedPosition;
		}
		else if (objectPosition.x > mapBoundary)
		{
			mapAircraftMover.RepositionX(-mapBoundary);
			objectPosition.x = -mapBoundary;
			var xDiff = transform.position.x - mainCamera.position.x;
			var zDiff = transform.position.z - mainCamera.position.z;
			transform.position = objectPosition;
			wantedPosition.x = transform.position.x - xDiff;
			wantedPosition.z = transform.position.z - zDiff;
			mainCamera.position = wantedPosition;
		}
		else if (objectPosition.z < -mapBoundary)
		{
			mapAircraftMover.RepositionZ(mapBoundary);
			objectPosition.z = mapBoundary;
			var xDiff = transform.position.x - mainCamera.position.x;
			var zDiff = transform.position.z - mainCamera.position.z;
			transform.position = objectPosition;
			wantedPosition.x = transform.position.x - xDiff;
			wantedPosition.z = transform.position.z - zDiff;
			mainCamera.position = wantedPosition;
		}
		else if (objectPosition.z > mapBoundary)
		{
			mapAircraftMover.RepositionZ(-mapBoundary);
			objectPosition.z = -mapBoundary;
			var xDiff = transform.position.x - mainCamera.position.x;
			var zDiff = transform.position.z - mainCamera.position.z;
			transform.position = objectPosition;
			wantedPosition.x = transform.position.x - xDiff;
			wantedPosition.z = transform.position.z - zDiff;
			mainCamera.position = wantedPosition;
		}
	}
}
