using UnityEngine;
using System.Collections;

public class BaseObjectTurret : MonoBehaviour
{
	[SerializeField] GameObject unitPrefab;
	[SerializeField] int difficultyLevel = 1;
	[SerializeField] bool inverseDifficulty;
	[SerializeField] bool randomRotation = false;
	MissionManager missionManager;
	int missionDifficultyValue;
	Vector3 spawnLocation;
	float rotationAngle;
	int spawnNumber = 0;
	float yPos = -7.5f;
	
	void Start()
	{
		GameObject missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");

		if (missionManagerObject != null)
		{
			missionManager = missionManagerObject.GetComponent<MissionManager>();
			missionDifficultyValue = missionManager.MissionDifficultyLevel;

			if (difficultyLevel == 2 && missionDifficultyValue == 2)
			{
				int randomVal = Random.Range(0, 1);

				if (randomVal > 0)
					return;
			}

			StartCoroutine(SpawnBaseUnit());
		}
	}
	
	IEnumerator SpawnBaseUnit()
	{
		while (missionDifficultyValue == 0)
		{
			missionDifficultyValue = missionManager.MissionDifficultyLevel;
			yield return null;
		}

		if (!randomRotation)
		{
			int randomInt = Random.Range(0, 4);
			
			switch (randomInt)
			{
			case 0:
				rotationAngle = 0f;
				break;
			case 1:
				rotationAngle = 90f;
				break;
			case 2:
				rotationAngle = 180f;
				break;
			case 3:
				rotationAngle = 270f;
				break;
			default:
				Debug.LogError("No proper rotation angle set");
				break;
			}
		}
		else
			rotationAngle = Random.Range(0f, 360f);

		Vector3 modifyRotation = transform.eulerAngles;
		modifyRotation.y = rotationAngle;
		transform.eulerAngles = modifyRotation;

		if (!inverseDifficulty && difficultyLevel <= missionDifficultyValue)
		{
			spawnLocation = new Vector3(transform.position.x, yPos, transform.position.z);
			GameObject unitClone = (GameObject)Instantiate(unitPrefab, spawnLocation, transform.rotation);
			unitClone.name = unitPrefab.name + " " + spawnNumber;
			spawnNumber++;
		}
		else if (inverseDifficulty && difficultyLevel >= missionDifficultyValue)
		{
			spawnLocation = new Vector3(transform.position.x, yPos, transform.position.z);
			GameObject unitClone = (GameObject)Instantiate(unitPrefab, spawnLocation, transform.rotation);
			unitClone.name = unitPrefab.name + " " + spawnNumber;
			spawnNumber++;
		}
	}
}
