using UnityEngine;
using System.Collections;

public class ShipBank : MonoBehaviour
{
	[SerializeField] Rigidbody rb;
	[SerializeField] float val = 80;
	[SerializeField] float angularVelocityModifier = 2f;
	float wantedRot;
	
	void Update ()
	{
		wantedRot = (Mathf.InverseLerp(-val, val, (rb.angularVelocity.y * angularVelocityModifier)) - 0.5f) * -150f;
		transform.localRotation = Quaternion.Euler(0, 0, wantedRot);
	}
}