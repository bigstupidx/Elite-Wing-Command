using UnityEngine;
using System.Collections;

public class VIPSpawner : MonoBehaviour
{
	[SerializeField] GameObject[] VIPObjects;
	[SerializeField] bool isAircraft = true;
	float yPos;

	void Start ()
	{
		if (isAircraft)
			yPos = 0;
		else
			yPos = -7.5f;
		
		foreach (GameObject unit in VIPObjects)
		{
			Instantiate(unit, new Vector3(Random.Range(transform.position.x + 20f, transform.position.x - 20f), yPos, 
			                              Random.Range(transform.position.z + 20f, transform.position.z - 20f)), transform.rotation);
		}
	}
}
