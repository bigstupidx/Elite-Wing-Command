using UnityEngine;
using System.Collections;

public class PlayerDamageable : Damageable
{
	void Update()
	{
		AddHealth(10f * Time.deltaTime);
	}
}