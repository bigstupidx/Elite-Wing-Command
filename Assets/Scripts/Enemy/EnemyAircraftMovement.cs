using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyAircraftMovement : GenericAircraftMovement
{
	[SerializeField] EnemyAI enemyAI;
	[SerializeField] bool defensiveAircraft;
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
			if (defensiveAircraft && MissionManagerScript.AllyObjectivesList != null && MissionManagerScript.AllyObjectivesList.Count != 0)
			{
				GameObject[] allyObjectives = MissionManagerScript.AllyObjectivesList.ToArray();
				
				if (allyObjectives.Length > 0)
				{
					GameObject targetObject = allyObjectives[Random.Range(0, allyObjectives.Length)];
					TargetPosition = new Vector3(Random.Range(targetObject.transform.position.x - DefensiveAirPerimeter, targetObject.transform.position.x + DefensiveAirPerimeter), 0, 
					                                     Random.Range(targetObject.transform.position.z - DefensiveAirPerimeter, targetObject.transform.position.z + DefensiveAirPerimeter));
					return;
				}
			}
			else if (MissionManagerScript.EnemyObjectivesList != null && MissionManagerScript.EnemyObjectivesList.Count != 0)
			{
				GameObject[] enemyObjectives = MissionManagerScript.EnemyObjectivesList.ToArray();
				
				if (enemyObjectives.Length > 0)
				{
					GameObject targetObject = enemyObjectives[Random.Range(0, enemyObjectives.Length)];
					TargetPosition = new Vector3(targetObject.transform.position.x, 0, targetObject.transform.position.z);
					return;
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