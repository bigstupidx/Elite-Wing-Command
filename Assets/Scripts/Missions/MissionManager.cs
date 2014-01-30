using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class MissionManager : MonoBehaviour
{
	public enum MissionType
	{
		Base_Attack,
		Base_Defense,
		Base_vs_Base
	}

	public MissionType missionType;
	List<string> allyObjectivesInScene;
	public List<string> AllyObjectivesList { get { return allyObjectivesInScene; }}

	void Start()
	{
		switch(missionType.ToString())
		{
		case "Base_Attack":
			BaseAttack();
			break;
		case "Base_Defense":
			Debug.Log("Mission Type: Base Defense");
			break;
		case "Base_vs_Base":
			Debug.Log("Mission Type: Base vs Base");
			break;
		}
	}

	void BaseAttack()
	{
		allyObjectivesInScene = new List<string>();
		GameObject[] objectives = GameObject.FindGameObjectsWithTag("Enemy");

		if (objectives.Length > 0)
		{
			foreach (GameObject objective in objectives)
			{
				var unitTag = objective.GetComponent<ObjectIdentifier>();

				if (unitTag != null && unitTag.ObjectType == "Ally Objective")
				{
					allyObjectivesInScene.Add(objective.transform.name);
					Debug.Log ("Adding: " + objective.transform.name);
				}
			}
		}
		else
			Debug.LogError("No Ally Objectives!");

		Debug.Log("Remaining Objectives: " + AllyObjectivesList.Count);
	}

	public void AllyObjectiveDestroyed(string objectiveName)
	{
		allyObjectivesInScene.Remove(objectiveName);
		Debug.Log("Remaining Objectives: " + AllyObjectivesList.Count);
	}
}