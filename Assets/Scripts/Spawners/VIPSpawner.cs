using UnityEngine;
using System.Collections;

public class VIPSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] VIPObjects;
	[SerializeField] float xOffset = 50f;
	[SerializeField] float zOffset = 100f;
	float yPos;

	void Start ()
	{
		foreach (GameObject unit in VIPObjects)
		{
			if (unit.tag == "AllyAirObjective" || unit.tag == "EnemyAirObjective")
				yPos = 0;
			else
				yPos = -7.5f;

			Instantiate(unit, new Vector3(Random.Range(transform.position.x + xOffset, transform.position.x - xOffset), yPos, 
			                              Random.Range(transform.position.z + zOffset, transform.position.z - zOffset)), transform.rotation);
		}
	}
}
