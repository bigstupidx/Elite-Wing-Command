using UnityEngine;
using System.Collections;

public class BaseObject : MonoBehaviour
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int difficultyLevel = 1;
	public GameObject UnitPrefab { get { return unitPrefab; }}
	public int DifficultyLevel { get { return difficultyLevel; }}
}
