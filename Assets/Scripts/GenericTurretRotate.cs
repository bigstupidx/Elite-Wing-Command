using UnityEngine;
using System.Collections;

public class GenericTurretRotate : MonoBehaviour
{
	[SerializeField] float turnSpeed = 2f;
	[SerializeField] Transform rotationReset;
	GameObject closestTarget;
	Transform targetTransform;
	Vector3 dir;
	Quaternion rot;
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}

	void Update()
	{
		if (ClosestTarget != null)
		{
			targetTransform = closestTarget.transform;
			dir = targetTransform.position - transform.position;
			dir.y = 0;
			rot = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
		}
		else if (rotationReset != null)
		{
			targetTransform = rotationReset;
			dir = targetTransform.position - transform.position;
			dir.y = 0;
			rot = Quaternion.LookRotation(dir);
			transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
		}
	}
}
