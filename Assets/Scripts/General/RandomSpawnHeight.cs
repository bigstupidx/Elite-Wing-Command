using UnityEngine;
using System.Collections;

public class RandomSpawnHeight : MonoBehaviour
{
	float yPos;

	void Start()
	{
		float heightRangeSelector = Random.Range(0, 2);
		
		if (heightRangeSelector == 0)
			yPos = Random.Range(-5, 0);
		else
			yPos = Random.Range(1, 35);

		Vector3 randomHeight = new Vector3(0, yPos, 0);
		transform.localPosition = randomHeight;
	}
}
