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
			if (defensiveAircraft && MissionManagerScript.EnemyObjectivesList != null && MissionManagerScript.EnemyObjectivesList.Count != 0)
			{
				GameObject[] enemyObjectives = MissionManagerScript.EnemyObjectivesList.ToArray();

				if (enemyObjectives != null && enemyObjectives.Length > 0)
				{
					GameObject targetObject = enemyObjectives[Random.Range(0, enemyObjectives.Length)];

					if (targetObject != null)
						TargetPosition = new Vector3(Random.Range(targetObject.transform.position.x - DefensiveAirPerimeter, targetObject.transform.position.x + DefensiveAirPerimeter), 0, 
					                                     Random.Range(targetObject.transform.position.z - DefensiveAirPerimeter, targetObject.transform.position.z + DefensiveAirPerimeter));

					return;
				}
			}
			else if (MissionManagerScript.AllyObjectivesList != null && MissionManagerScript.AllyObjectivesList.Count != 0)
			{
				GameObject[] allyObjectives = MissionManagerScript.AllyObjectivesList.ToArray();
				
				if (allyObjectives != null && allyObjectives.Length > 0)
				{
					GameObject targetObject = allyObjectives[Random.Range(0, allyObjectives.Length)];

					if (targetObject != null)
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