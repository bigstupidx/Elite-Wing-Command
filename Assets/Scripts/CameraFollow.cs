using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] string cameraFollowObjectName = "Player Aircraft(Clone)";
	GameObject playerAircraft;
	const float moveMultiplier = 2f;

	void FixedUpdate()
	{
		playerAircraft = GameObject.Find(cameraFollowObjectName);

		if (playerAircraft != null)
			transform.position = new Vector3(playerAircraft.transform.position.x, transform.position.y, playerAircraft.transform.position.z);
	}
}
