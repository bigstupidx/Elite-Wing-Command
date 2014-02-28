using UnityEngine;
using System.Collections;

public class PlayerDamageable : Damageable
{
	float healthMultiplierModifier = 10f;

	public override void Start()
	{
		Health = InitialHealth;
		var missionManagerObject = GameObject.FindGameObjectWithTag("MissionManager");
		
		if (missionManagerObject != null)
		{
			MissionManagerScript = missionManagerObject.GetComponent<MissionManager>();
			int missionDifficulty = MissionManagerScript.MissionDifficultyLevel;
			
			switch (missionDifficulty)
			{
			case 1:
				healthMultiplierModifier = 10f;
				break;
			case 2:
				healthMultiplierModifier = 8f;
				break;
			case 3:
				healthMultiplierModifier = 5f;
				break;
			default:
				Debug.LogError("No mission difficulty set");
				break;
			}
		}
	}

	void Update()
	{
		AddHealth(healthMultiplierModifier * Time.deltaTime);
	}
}