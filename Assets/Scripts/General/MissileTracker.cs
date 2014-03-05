using UnityEngine;
using System.Collections;

public class MissileTracker : MonoBehaviour
{
	[SerializeField] string TargetTag;
	[SerializeField] float sightDistance = 32f;
	[SerializeField] float sightWidth = 60f;
	[SerializeField] float engineForce = 15f;
	[SerializeField] bool attackAirUnits = true;
	[SerializeField] bool attackGroundUnits = true;
	GameObject closestTarget;
	GameObject closestAirTarget;
	GameObject closestGroundTarget;
	string targetTag;
	string enemyTurretID;
	string enemyVehicleID;
	float angle;
	Vector3 offset;
	float currentForce = 0f;
	float torqueModifier = 0.4f;
	float timeModifier = 120f;
	
	void Awake()
	{
		StartCoroutine(FindClosestTarget());
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, engineForce, timeModifier * Time.fixedDeltaTime);
		rigidbody.AddForce(transform.forward * currentForce, ForceMode.Acceleration);

		if (closestTarget != null)
		{
			Vector3 targetPosition = closestTarget.transform.position;
			offset = transform.InverseTransformPoint(targetPosition);
			angle = Mathf.Rad2Deg * Mathf.Atan2(offset.x, offset.z);

			if (Mathf.Abs(angle) > sightWidth)
				angle = 0;

			if (Mathf.Abs(angle) > 3f)
				rigidbody.AddTorque(Vector3.up * torqueModifier * Mathf.Sign(angle), ForceMode.VelocityChange);
		}
	}
	
	IEnumerator FindClosestTarget()
	{
		while(true)
		{
			Collider[] objectsInRange = Physics.OverlapSphere(transform.position, sightDistance);
			int airTargets = 0;
			int groundTargets = 0;
			float closestAirTargetDistance = 100f;
			float closestGroundTargetDistance = 100f;
			
			foreach (var target in objectsInRange)
			{
				if (target.transform.tag == TargetTag)
				{
					var objectType = target.transform.root.GetComponent<ObjectType>();
					Vector2 targetXZPosition = new Vector2(target.transform.position.x, target.transform.position.z);
					Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
					float distance = Vector2.Distance(targetXZPosition, unitXZPosition);
					
					if (attackGroundUnits && objectType.IsGroundUnit)
					{
						groundTargets++;
						
						if (distance < closestGroundTargetDistance)
						{
							closestGroundTargetDistance = distance;
							closestGroundTarget = target.gameObject;
						}
					}
					else if (attackAirUnits && objectType.IsAirUnit)
					{
						airTargets++;
						
						if (distance < closestAirTargetDistance)
						{
							closestAirTargetDistance = distance;
							closestAirTarget = target.gameObject;
						}
					}
					
					if (groundTargets != 0 && airTargets <= 2)
						closestTarget = closestGroundTarget;
					else
						closestTarget = closestAirTarget;
				}
			}
			
			if (airTargets == 0 && groundTargets == 0)
				closestTarget = null;

			yield return new WaitForSeconds(Random.Range(0.75f, 1f));
		}
	}
}
