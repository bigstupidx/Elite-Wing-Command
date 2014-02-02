using UnityEngine;
using System.Collections;

public class GenericAI : MonoBehaviour
{
	[SerializeField] float sightDistance = 32f;
	[SerializeField] bool isAirUnit = false;
	[SerializeField] bool isGroundUnit = false;
	[SerializeField] bool isStationaryUnit = false;
	[SerializeField] bool attackAirUnits = true;
	[SerializeField] bool attackGroundUnits = true;
	GameObject closestTarget;
	GameObject closestAirTarget;
	GameObject closestGroundTarget;
	GameObject closestObjectiveTarget;
	float closestTargetDistance;
	string closestTargetID;
	string objectiveTag;
	string targetTag;
	string targetTurretID;
	string targetVehicleID;
	ObjectType objectType;
	ObjectIdentifier objectID;
	public float SightDistance { get { return sightDistance; }}
	public bool IsAirUnit { get { return isAirUnit; }}
	public bool IsGroundUnit { get { return isGroundUnit; }}
	public bool IsStationaryUnit { get { return isStationaryUnit; }}
	public bool AttackAirUnits { get { return attackAirUnits; }}
	public bool AttackGroundUnits { get { return attackGroundUnits; }}
	public GameObject ClosestTarget { get { if (closestTarget != null) return closestTarget; else return null; } set { closestTarget = value; }}
	public string ClosestTargetName { get { if (closestTarget != null) return closestTarget.name; else return null; }}
	public float ClosestTargetDistance { get { return closestTargetDistance; } set { closestTargetDistance = value; }}
	public string ClosestTargetID { get { if (closestTarget != null) return closestTargetID; else return null; } set { closestTargetID = value; }}
	public string ObjectiveTag { get { return objectiveTag; } set { objectiveTag = value; }}
	public string TargetTag { get { return targetTag; } set { targetTag = value; }}
	public string TargetTurretID { get { return targetTurretID; } set { targetTurretID = value; }}
	public string TargetVehicleID { get { return targetVehicleID; } set { targetVehicleID = value; }}

	public IEnumerator FindClosestTarget()
	{
		while (true)
		{
			Collider[] objectsInRange = Physics.OverlapSphere(transform.position, SightDistance);
			int airTargets = 0;
			int groundTargets = 0;
			int objectiveTargets = 0;
			float closestAirTargetDistance = 100f;
			float closestGroundTargetDistance = 100f;
			float closestObjectiveTargetDistance = 100f;

			if (objectsInRange != null)
			{
				foreach (var target in objectsInRange)
				{
					GameObject targetObject = target.transform.gameObject;
					
					if (targetObject.transform.tag == TargetTag || targetObject.transform.tag == ObjectiveTag)
					{
						objectType = targetObject.transform.GetComponent<ObjectType>();
						Vector2 targetXZPosition = new Vector2(targetObject.transform.position.x, targetObject.transform.position.z);
						Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
						float distance = Vector2.Distance(targetXZPosition, unitXZPosition);

						if (AttackGroundUnits && objectType != null && objectType.IsGroundUnit)
						{
							if (targetObject.transform.tag == ObjectiveTag)
							{
								objectiveTargets++;

								if (distance < closestObjectiveTargetDistance)
								{
									closestObjectiveTargetDistance = distance;
									closestObjectiveTarget = targetObject;
								}
							}
							else
							{
								groundTargets++;
							
								if (distance < closestGroundTargetDistance)
								{
									closestGroundTargetDistance = distance;
									closestGroundTarget = targetObject;
								}
							}
						}
						else if (AttackAirUnits && objectType != null && objectType.IsAirUnit)
						{
							Debug.Log("SHOULD NEVER REACH HERE");
							if (targetObject.transform.tag == ObjectiveTag)
							{
								objectiveTargets++;
								
								if (distance < closestObjectiveTargetDistance)
								{
									closestObjectiveTargetDistance = distance;
									closestObjectiveTarget = targetObject;
								}
							}
							else
							{
								airTargets++;
								
								if (distance < closestAirTargetDistance)
								{
									closestAirTargetDistance = distance;
									closestAirTarget = targetObject;
								}
							}
						}

					}
				}
			}

			if (groundTargets != 0 && airTargets <= 2)
			{
				ClosestTarget = closestGroundTarget;
				ClosestTargetDistance = closestGroundTargetDistance;
				objectID = ClosestTarget.GetComponent<ObjectIdentifier>();
				ClosestTargetID = objectID.ObjectType;
			}
			else if (airTargets != 0)
			{
				ClosestTarget = closestAirTarget;
				ClosestTargetDistance = closestAirTargetDistance;
				objectID = ClosestTarget.GetComponent<ObjectIdentifier>();
				ClosestTargetID = objectID.ObjectType;
			}
			else if (objectiveTargets != 0)
			{
				ClosestTarget = closestObjectiveTarget;
				ClosestTargetDistance = closestObjectiveTargetDistance;
				objectID = ClosestTarget.GetComponent<ObjectIdentifier>();
				ClosestTargetID = objectID.ObjectType;
			}
			else
			{
				ClosestTarget = null;
				ClosestTargetDistance = 100f;
				ClosestTargetID = null;
			}
			
			yield return new WaitForSeconds(1.0f);
		}
	}
}
