using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] string cameraFollowObjectName = "Player Aircraft";
	[SerializeField] float cameraHeight = 35f;
	[SerializeField] bool smoothFollow = true;
	[SerializeField] float cameraFollowSpeed = 12f;
	GameObject playerAircraft;
	Vector3 wantedPosition;
	bool haveFound = false;
	const float moveMultiplier = 2f;

	void Start()
	{
		if (playerAircraft != null)
		{
			haveFound = true;
			wantedPosition = playerAircraft.transform.position;
			wantedPosition.y = cameraHeight;
			transform.position = wantedPosition;
		}
	}

	void LateUpdate()
	{
		playerAircraft = GameObject.Find(cameraFollowObjectName);

		if (playerAircraft != null)
		{
			if (haveFound)
			{
				wantedPosition = playerAircraft.transform.position;
				wantedPosition.y = cameraHeight;
				if (smoothFollow)
					transform.position = Vector3.Lerp(transform.position, wantedPosition, Time.deltaTime * cameraFollowSpeed);
				else
					transform.position = wantedPosition;
			}
			else
			{
				wantedPosition = playerAircraft.transform.position;
				wantedPosition.y = cameraHeight;
				transform.position = wantedPosition;
				haveFound = true;
			}
		}
		else
			haveFound = false;
	}
}
