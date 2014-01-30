using UnityEngine;
using System.Collections;

public class ShipBank : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] float val = 80;
	const float turnSpeedModifier = 0.12f;
	const float rotCorrection = 0.5f;
	float wantedRot;
	
	void Update ()
	{
		wantedRot = Mathf.Lerp(-val, val, (rb.angularVelocity.y * -turnSpeedModifier) + rotCorrection);
		transform.localRotation = Quaternion.Euler(0, 0, wantedRot);
	}
}