using UnityEngine;
using System.Collections;
using System.Linq;

public class AllyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] AllyAI allyAI;
	[SerializeField] bool defensiveAircraft;
	GameObject closestAllyVehicle;
	float closestAllyDistance;

	void Awake()
	{
		ObjectiveGroundTag = allyAI.ObjectiveGroundTag;
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

			if (defensiveAircraft && MissionManagerScript.EnemyObjectivesList != null && MissionManagerScript.EnemyObjectivesList.Count != 0)
			{
				GameObject[] enemyAirObjectives = GameObject.FindGameObjectsWithTag("EnemyAirObjective");
				GameObject[] enemyGroundObjectives = GameObject.FindGameObjectsWithTag("EnemyGroundObjective");
				GameObject[] enemyObjectives = enemyAirObjectives.Concat(enemyGroundObjectives).ToArray();

				if (enemyObjectives.Length > 0)
				{
					GameObject targetObject = enemyObjectives[Random.Range(0, enemyObjectives.Length)];
					TargetPosition = new Vector3(Random.Range(targetObject.transform.position.x - DefensiveAirPerimeter, targetObject.transform.position.x + DefensiveAirPerimeter), 0, 
					                                     Random.Range(targetObject.transform.position.z - DefensiveAirPerimeter, targetObject.transform.position.z + DefensiveAirPerimeter));
					return;
				}
			}
			else if (MissionManagerScript.AllyObjectivesList != null && MissionManagerScript.AllyObjectivesList.Count != 0)
			{
				GameObject[] allyAirObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
				GameObject[] allyGroundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
				GameObject[] allyObjectives = allyAirObjectives.Concat(allyGroundObjectives).ToArray();
				
				if (allyObjectives.Length > 0)
				{
					GameObject targetObject = allyObjectives[Random.Range(0, allyObjectives.Length)];
					TargetPosition = new Vector3(targetObject.transform.position.x, 0, targetObject.transform.position.z);
					return;
				}
				else if (allyUnits.Length != 0)
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
					
					if (closestAllyVehicle != null)
					{
						TargetPosition = new Vector2(Random.Range(closestAllyVehicle.transform.position.x - EscortPerimeter, closestAllyVehicle.transform.position.x + EscortPerimeter), 
						                             Random.Range(closestAllyVehicle.transform.position.z - EscortPerimeter, closestAllyVehicle.transform.position.z + EscortPerimeter));
					}
				}
			}
		}
		else if (FindRandomAngle == true)
		{
			FindRandomAngle = false;
			RandomPosition = new Vector3(Random.Range(-90f, 90f), transform.position.y, Random.Range(-90f, 90f));
			StartCoroutine(FindRandomAngleWait());
			TargetPosition = RandomPosition;
		}
	}
}