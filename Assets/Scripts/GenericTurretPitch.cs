using UnityEngine;
using System.Collections;

public class GenericTurretPitch : MonoBehaviour
{
	[SerializeField] float maxDegreesPerSecond = 30.0f;
	GameObject closestTarget;
	Transform target;
	Quaternion qTo;
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}

	void Update()
	{
		if (ClosestTarget != null)
		{
			target = ClosestTarget.transform;
			qTo = target.localRotation;
			var v3T = target.position - transform.position;
			Vector3 v3Aim;
			v3Aim.x = 0f;
			v3Aim.y = v3T.y;
			v3T.y = 0f;
			v3Aim.z = v3T.magnitude;
			qTo = Quaternion.LookRotation(v3Aim, Vector3.up);
			transform.localRotation = Quaternion.RotateTowards(transform.localRotation, qTo, maxDegreesPerSecond * Time.deltaTime);
		}
	}
}
