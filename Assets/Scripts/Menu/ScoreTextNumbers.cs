using UnityEngine;
using System.Collections;

public class ScoreTextNumbers : MonoBehaviour
{
	[SerializeField] UILabel missionScoreObject;
	
	void Start()
	{
		string missionScores = "____________________________\n\n25\n\n8\n\nx4\n\nx5";
		missionScoreObject.text = missionScores;
	}
}
