using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] EnemyAI enemyAI;
	GameObject closestEnemyVehicle;
	float closestEnemyDistance;
	
	void Awake()
	{
		ObjectiveGroundTag = enemyAI.ObjectiveGroundTag;
	}
	
	void Update()
	{
		ClosestTarget = enemyAI.ClosestTarget;
		ClosestTargetDistance = enemyAI.ClosestTargetDistance;
		ClosestTargetID = enemyAI.ClosestTargetID;
	}
	
	public override void Search()
	{
		if (MissionManagerScript != null)
		{
			GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");

			if (MissionManagerScript.AllyObjectivesList != null && MissionManagerScript.AllyObjectivesList.Count != 0)
			{
				GameObject[] allyAirObjectives = GameObject.FindGameObjectsWithTag("AllyAirObjective");
				GameObject[] allyGroundObjectives = GameObject.FindGameObjectsWithTag("AllyGroundObjective");
				GameObject[] allyObjectives = allyAirObjectives.Concat(allyGroundObjectives).ToArray();
				
				if (allyObjectives.Length > 0)
				{
					GameObject targetObject = allyObjectives[Random.Range(0, allyObjectives.Length)];
					TargetPosition = new Vector3(Random.Range(targetObject.transform.position.x - DefensiveAirPerimeter, targetObject.transform.position.x + DefensiveAirPerimeter), 0, 
					                                     Random.Range(targetObject.transform.position.z - DefensiveAirPerimeter, targetObject.transform.position.z + DefensiveAirPerimeter));
					return;
				}
			}
			else if (enemyUnits.Length != 0)
			{
				closestEnemyDistance = 100f;
				
				foreach (GameObject enemy in enemyUnits)
				{
					ObjectIdentifier objectID = enemy.transform.GetComponent<ObjectIdentifier>();
					Vector2 enemyXZPosition = new Vector2(enemy.transform.position.x, enemy.transform.position.z);
					Vector2 unitXZPosition = new Vector2(transform.position.x, transform.position.z);
					float distance = Vector2.Distance(enemyXZPosition, unitXZPosition);
					
					if (objectID != null && objectID.ObjectType == "Enemy Vehicle" && distance < closestEnemyDistance)
					{
						closestEnemyDistance = distance;
						closestEnemyVehicle = enemy;
					}
				}

				if (closestEnemyVehicle != null)
				{
					TargetPosition = new Vector2(Random.Range(closestEnemyVehicle.transform.position.x - EscortPerimeter, closestEnemyVehicle.transform.position.x + EscortPerimeter), 
					                                     Random.Range(closestEnemyVehicle.transform.position.z - EscortPerimeter, closestEnemyVehicle.transform.position.z + EscortPerimeter));
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