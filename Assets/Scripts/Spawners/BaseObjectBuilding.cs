using UnityEngine;
using System.Collections;

public class BaseObjectBuilding : MonoBehaviour
{
	[SerializeField] GameObject[] unitPrefabs;
	[SerializeField] string spawnTag = "Unique Number Here";
	float rotationAngle;
	float yPos = -8f;
	
	void Start()
	{
		Vector3 spawnLocation = new Vector3(transform.position.x, yPos, transform.position.z);
		int randomInt = Random.Range(0, 4);

		switch (randomInt)
		{
		case 0:
			rotationAngle = 0f;
			break;
		case 1:
			rotationAngle = 90f;
			break;
		case 2:
			rotationAngle = 180f;
			break;
		case 3:
			rotationAngle = 270f;
			break;
		default:
			Debug.LogError("No proper rotation angle set");
			break;
		}

		Vector3 modifyRotation = transform.eulerAngles;
		modifyRotation.y = rotationAngle;
		transform.eulerAngles = modifyRotation;
		GameObject unitClone = (GameObject)Instantiate(unitPrefabs[Random.Range(0, unitPrefabs.Length)], spawnLocation, transform.rotation);
		unitClone.name = unitClone.name + " " + spawnTag;
	}
}