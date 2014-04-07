using UnityEngine;
using System.Collections;

public class AllyVehicleMovementMultiplier : MonoBehaviour
{
	[SerializeField] NavMeshAgent allyVehicle;

	void Start()
	{
		allyVehicle.speed *= EncryptedPlayerPrefs.GetFloat("Ally Ground Speed Multiplier", 1f);
	}
}
