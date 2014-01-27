using UnityEngine;
using System.Collections;

public class PlayerDamageable : Damageable
{
	void Update()
	{
		AddHealth(5f * Time.deltaTime);
	}
}