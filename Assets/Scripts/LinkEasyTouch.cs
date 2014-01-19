using UnityEngine;
using System.Collections;

public class LinkEasyTouch : MonoBehaviour
{
	[SerializeField] GameObject shipMovementObject;
	[SerializeField] GameObject weaponsManagerObject;

	void Start()
	{
		var playerSteerObject = GameObject.FindGameObjectWithTag("PlayerSteer");
		var playerSteer = playerSteerObject.GetComponent<EasyJoystick>();
		playerSteer.receiverGameObject = shipMovementObject;

		var playerFireWeaponObject = GameObject.FindGameObjectWithTag("PlayerFireWeapon");
		var playerFireWeapon = playerFireWeaponObject.GetComponent<EasyButton>();
		playerFireWeapon.receiverGameObject = weaponsManagerObject;

		var playerDropBombObject = GameObject.FindGameObjectWithTag("PlayerDropBomb");
		var playerDropBomb = playerDropBombObject.GetComponent<EasyButton>();
		playerDropBomb.receiverGameObject = weaponsManagerObject;
	}
}
