using UnityEngine;
using System.Collections;

public class AllyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] AllyAI allyAI;
	GameObject closestAllyVehicle;
	float closestAllyDistance;

	void Awake()
	{
		ObjectiveTag = allyAI.ObjectiveTag;
	}

	void Update()
	{
		ClosestTarget = allyAI.ClosestTarget;
		ClosestTargetDistance = allyAI.ClosestTargetDistance;
		ClosestTargetID = allyAI.ClosestTargetID;
	}

	public override void Search()
	{
		if (MissionManagerScript != null)
		{
			GameObject[] allyUnits = GameObject.FindGameObjectsWithTag("Ally");
			
			if (allyUnits.Length != 0)
			{
				closestAllyDistance = 100f;
				
				foreach (GameObject ally in allyUnits)
				{
					ObjectIdentifier objectID = ally.transform.GetComponent<ObjectIdentifier>();
					Vector2 allyXZPosition = new Vector2(ally.transform.position.x, ally.transform.position.z);
					Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
					float distance = Vector2.Distance(allyXZPosition, unitXZPosition);
					
					if (objectID != null && objectID.ObjectType == "Ally Vehicle" && distance < closestAllyDistance)
					{
						closestAllyDistance = distance;
						closestAllyVehicle = ally;
					}
				}
			}
			
			if (closestAllyVehicle != null)
			{
				Vector3 targetPosition = new Vector2(Random.Range(closestAllyVehicle.transform.position.x - EscortPerimeter, closestAllyVehicle.transform.position.x + EscortPerimeter), 
				                                     Random.Range(closestAllyVehicle.transform.position.z - EscortPerimeter, closestAllyVehicle.transform.position.z + EscortPerimeter));
				Offset = transform.InverseTransformPoint(targetPosition);
			}
			else if (MissionManagerScript.EnemyObjectivesList != null && MissionManagerScript.EnemyObjectivesList.Count != 0)
			{
				GameObject[] enemyObjectives = GameObject.FindGameObjectsWithTag("EnemyObjective");

				if (enemyObjectives.Length > 0)
				{
					GameObject targetObject = enemyObjectives[Random.Range(0, enemyObjectives.Length)];
					Vector3 targetPosition = new Vector3(Random.Range(targetObject.transform.position.x - DefensiveAirPerimeter, targetObject.transform.position.x + DefensiveAirPerimeter), 0, 
					                                     Random.Range(targetObject.transform.position.z - DefensiveAirPerimeter, targetObject.transform.position.z + DefensiveAirPerimeter));
					Offset = transform.InverseTransformPoint(targetPosition);
					return;
				}
			}
			
		}
		else if (FindRandomAngle == true)
		{
			FindRandomAngle = false;
			RandomPosition = new Vector3(Random.Range(-90f, 90f), transform.position.y, Random.Range(-90f, 90f));
			StartCoroutine(FindRandomAngleWait());
			Offset = transform.InverseTransformPoint(RandomPosition);
		}
	}
}