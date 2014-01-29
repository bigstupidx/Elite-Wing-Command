using UnityEngine;
using System.Collections;

public class GenericAI : MonoBehaviour
{
	[SerializeField] float sightDistance = 32f;
	[SerializeField] bool isGroundUnit = false;
	[SerializeField] bool attackAirUnits = true;
	[SerializeField] bool attackGroundUnits = true;
	GameObject closestTarget;
	GameObject closestAirTarget;
	GameObject closestGroundTarget;
	float closestTargetDistance;
	string closestTargetID;
	string targetTag;
	string enemyTurretID;
	string enemyVehicleID;
	public float SightDistance { get { return sightDistance; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; }}
	public string ClosestTargetName { get { if (closestTarget != null) return closestTarget.name; else return null; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; }}
	public string TargetTag { get { return targetTag; } set { targetTag = value; }}
	public bool IsGroundUnit { get { return isGroundUnit; }}
	public string EnemyTurretID { get { return enemyTurretID; } set { enemyTurretID = value; }}
	public string EnemyVehicleID { get { return enemyVehicleID; } set { enemyVehicleID = value; }}
	public bool AttackAirUnits { get { return attackAirUnits; } set { attackAirUnits = value; }}
	public bool AttackGroundUnits { get { return attackGroundUnits; } set { attackGroundUnits = value; }}
	
	public IEnumerator FindClosestTarget()
	{
		while (true)
		{
			Collider[] objectsInRange = Physics.OverlapSphere(transform.position, sightDistance);
			int airTargets = 0;
			int groundTargets = 0;
			float closestAirTargetDistance = 100f;
			float closestGroundTargetDistance = 100f;

			foreach (var target in objectsInRange)
			{
				GameObject targetObject = GameObject.Find(target.transform.root.name);
				
				if (targetObject.transform.tag == TargetTag)
				{
					var objectType = targetObject.transform.root.GetComponent<ObjectType>();
					var objectID = targetObject.transform.root.GetComponent<ObjectIdentifier>();
					Vector2 targetXZPosition = new Vector2(targetObject.transform.root.position.x, targetObject.transform.root.position.z);
					Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
					float distance = Vector2.Distance(targetXZPosition, unitXZPosition);

					if (AttackGroundUnits && objectType.IsGroundUnit)
					{
						groundTargets++;
						
						if (distance < closestGroundTargetDistance)
						{
							closestGroundTargetDistance = distance;
							closestGroundTarget = targetObject;
						}
					}
					else if (AttackAirUnits && objectType.IsAirUnit)
					{
						airTargets++;
						
						if (distance < closestAirTargetDistance)
						{
							closestAirTargetDistance = distance;
							closestAirTarget = targetObject;
						}
					}

					if (groundTargets != 0 && airTargets <= 2)
					{
						closestTarget = closestGroundTarget;
						closestTargetDistance = closestGroundTargetDistance;
						closestTargetID = objectID.ObjectType;
					}
					else
					{
						closestTarget = closestAirTarget;
						closestTargetDistance = closestAirTargetDistance;
						closestTargetID = objectID.ObjectType;
					}
				}
			}

			if (airTargets == 0 && groundTargets == 0)
			{
				closestTarget = null;
				closestTargetDistance = 100f;
				closestTargetID = null;
			}
			
			yield return new WaitForSeconds(0.5f);
		}
	}
}
