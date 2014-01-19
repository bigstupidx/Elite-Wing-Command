using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinimapIdentifierMover : MonoBehaviour
{
	[SerializeField] float mapMaxX;
	[SerializeField] float mapMaxZ;
	Vector3 parentObjectPosition;
	Vector3 identifierPosition;

	void Update()
	{
		parentObjectPosition = transform.root.gameObject.transform.position;
		Vector3 correctedPosition = parentObjectPosition;
		
		if (correctedPosition.x > mapMaxX)
			correctedPosition.x = correctedPosition.x + (-mapMaxX * 2f);
		else if (correctedPosition.x < -mapMaxX)
			correctedPosition.x = correctedPosition.x + (mapMaxX * 2f);
		
		if (correctedPosition.z > mapMaxZ)
			correctedPosition.z = correctedPosition.z + (-mapMaxZ * 2f);
		else if (correctedPosition.z < -mapMaxZ)
			correctedPosition.z = correctedPosition.z + (mapMaxZ * 2f);
		
		identifierPosition = correctedPosition;

		transform.position = identifierPosition;
	}
}
