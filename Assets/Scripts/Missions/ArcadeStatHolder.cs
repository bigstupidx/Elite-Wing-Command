using UnityEngine;
using System.Collections;

public class ArcadeStatHolder : MonoBehaviour
{
	int timeElapsed = 0;
	int enemyAirDestroyed = 0;
	int enemyGroundDestroyed = 0;
	public int TimeElapsed { get { return timeElapsed; } set { timeElapsed = value; }}
	public int EnemyAirDestroyed { get { return enemyAirDestroyed; } set { enemyAirDestroyed = value; }}
	public int EnemyGroundDestroyed { get { return enemyGroundDestroyed; } set { enemyGroundDestroyed = value; }}

	void Start()
	{
		InvokeRepeating("TimerIncrease", 11.0f, 11.0f);
	}

	void TimerIncrease()
	{
		++TimeElapsed;
	}
}
