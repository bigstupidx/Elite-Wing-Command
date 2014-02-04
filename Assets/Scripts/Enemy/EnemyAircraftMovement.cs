using UnityEngine;
using System.Collections;

public class EnemyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] EnemyAI enemyAI;
	GameObject closestEnemyVehicle;
	float closestEnemyDistance;
	
	void Awake()
	{
		ObjectiveTag = enemyAI.ObjectiveTag;
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
			
			if (enemyUnits.Length != 0)
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
			}
			
			if (closestEnemyVehicle != null)
			{
				Vector3 targetPosition = new Vector2(Random.Range(closestEnemyVehicle.transform.position.x - EscortPerimeter, closestEnemyVehicle.transform.position.x + EscortPerimeter), 
				                                     Random.Range(closestEnemyVehicle.transform.position.z - EscortPerimeter, closestEnemyVehicle.transform.position.z + EscortPerimeter));
				Offset = transform.InverseTransformPoint(targetPosition);
			}
			else if (MissionManagerScript.AllyObjectivesList != null && MissionManagerScript.AllyObjectivesList.Count != 0)
			{
				GameObject[] allyObjectives = GameObject.FindGameObjectsWithTag("AllyObjective");
				
				if (allyObjectives.Length > 0)
				{
					GameObject targetObject = allyObjectives[Random.Range(0, allyObjectives.Length)];
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