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
	public List<GameObject> objectivesList { get { return objectivesInScene; }}
	List<GameObject> objectivesInScene;
	GameObject[] objectives;

	void Awake()
	{
		switch(missionType.ToString())
		{
		case "Base_Attack":
			Debug.Log("Mission Type: Base Attack");
			break;
		case "Base_Defense":
			Debug.Log("Mission Type: Base Defense");
			break;
		case "Base_vs_Base":
			Debug.Log("Mission Type: Base vs Base");
			break;
		}
	}

	void Start()
	{
		objectivesInScene = new List<GameObject>();
		objectives = GameObject.FindGameObjectsWithTag("Objective");

		if (objectives.Length > 0)
		{
			foreach(GameObject objective in objectives)
			{
				objectivesInScene.Add(objective);
			}
		}
	}
}