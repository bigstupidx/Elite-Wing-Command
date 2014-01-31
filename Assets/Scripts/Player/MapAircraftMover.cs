using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapAircraftMover : MonoBehaviour
{
	[SerializeField] Collider collectionArea;
	List<string> objectsInRange;
	Vector3 objectPosition;

	void Start()
	{
		if (collectionArea != null)
			collectionArea.enabled = true;

		objectPosition = transform.position;
		objectsInRange = new List<string>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.tag != "Terrain")
			objectsInRange.Add(other.transform.name);
	}

	void OnTriggerExit(Collider other)
	{
		objectsInRange.Remove(other.transform.name);
	}

	public void RepositionX(float mapBoundary)
	{
		if (collectionArea != null)
			collectionArea.enabled = false;

		foreach(string item in objectsInRange)
		{
			GameObject itemToBeMoved = GameObject.Find(item);

			if (itemToBeMoved != null)
			{
				objectPosition = itemToBeMoved.transform.position;
				objectPosition.x = objectPosition.x + (mapBoundary * 2f);
				itemToBeMoved.transform.position = objectPosition;
			}
		}

		objectsInRange.Clear();

		if (collectionArea != null)
			collectionArea.enabled = true;
	}

	public void RepositionZ(float mapBoundary)
	{
		if (collectionArea != null)
			collectionArea.enabled = false;

		foreach(string item in objectsInRange)
		{
			GameObject itemToBeMoved = GameObject.Find(item);

			if (itemToBeMoved != null)
			{
				objectPosition = itemToBeMoved.transform.position;
				objectPosition.z = objectPosition.z + (mapBoundary * 2f);
				itemToBeMoved.transform.position = objectPosition;
			}
		}

		objectsInRange.Clear();

		if (collectionArea != null)
			collectionArea.enabled = true;
	}
}
