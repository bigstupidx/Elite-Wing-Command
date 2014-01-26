using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapTurretMover : MonoBehaviour
{
	[SerializeField] float mapMaxX;
	[SerializeField] float mapMaxZ;
	Transform player;
	Vector3 turretPosition;

	void FixedUpdate()
	{
		GameObject playerAircraft = GameObject.Find("Player Aircraft");

		if (playerAircraft != null)
		{
			player = playerAircraft.transform;

			if (transform.position.x <= (-0.5f * mapMaxX) && player.position.x >= 0f)
				RepositionX(mapMaxX);
			else if (transform.position.x >= (0.5f * mapMaxX) && player.position.x < 0f)
				RepositionX(-mapMaxX);

			if (transform.position.z <= (-0.5f * mapMaxZ) && player.position.z >= 0f)
				RepositionZ(mapMaxZ);
			else if (transform.position.z >= (0.5f * mapMaxZ) && player.position.z < 0f)
				RepositionZ(-mapMaxZ);
		}
	}

	public void RepositionX(float mapBoundary)
	{
		turretPosition = transform.position;
		turretPosition.x = turretPosition.x + (mapBoundary * 2f);
		transform.position = turretPosition;
	}

	public void RepositionZ(float mapBoundary)
	{
		turretPosition = transform.position;
		turretPosition.z = turretPosition.z + (mapBoundary * 2f);
		transform.position = turretPosition;
	}
}
