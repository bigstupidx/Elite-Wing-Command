using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapAircraftMover : MonoBehaviour
{
	[SerializeField] Collider collectionArea;
	List<GameObject> objectsInRange;
	Vector3 objectPosition;

	void Start()
	{
		if (collectionArea != null)
			collectionArea.enabled = true;

		objectPosition = transform.position;
		objectsInRange = new List<GameObject>();
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.transform.root.tag != "Terrain")
			objectsInRange.Add(other.gameObject);
	}

	void OnTriggerExit(Collider other)
	{
		objectsInRange.Remove(other.gameObject);
	}

	public void RepositionX(float mapBoundary)
	{
		if (collectionArea != null)
			collectionArea.enabled = false;

		foreach(GameObject item in objectsInRange)
		{
			if (item != null)
			{
				objectPosition = item.transform.position;
				objectPosition.x = objectPosition.x + (mapBoundary * 2f);
				item.transform.position = objectPosition;
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

		foreach(GameObject item in objectsInRange)
		{
			if (item != null)
			{
				objectPosition = item.transform.position;
				objectPosition.z = objectPosition.z + (mapBoundary * 2f);
				item.transform.position = objectPosition;
			}
		}

		objectsInRange.Clear();

		if (collectionArea != null)
			collectionArea.enabled = true;
	}
}
