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
	bool needsClearShot;
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public bool NeedsClearShot { get { return needsClearShot; } set { needsClearShot = value; }}

	void FixedUpdate()
	{
		if (ClosestTarget != null)
		{
			if (NeedsClearShot)
			{
				RaycastHit hit;
				
				if (Physics.Linecast(transform.position, ClosestTarget.transform.position, out hit))
				{
					if (hit.transform.name == ClosestTarget.transform.name)
						RotationSet();
				}
			}
			else if (rotationReset != null)
				RotationReset();
			else
				RotationSet();
		}
		else if (rotationReset != null)
			RotationReset();
	}

	void RotationSet()
	{
		targetTransform = closestTarget.transform;
		dir = targetTransform.position - transform.position;
		dir.y = 0;
		rot = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
	}

	void RotationReset()
	{
		targetTransform = rotationReset;
		dir = targetTransform.position - transform.position;
		dir.y = 0;
		rot = Quaternion.LookRotation(dir);
		transform.rotation = Quaternion.Slerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
	}
}
