using UnityEngine;
using System.Collections;

public class LinkPlayerJoystick : MonoBehaviour
{

	void Start()
	{
		var easyJoystickObject = GameObject.FindGameObjectWithTag("Joystick");
		var easyJoystick = easyJoystickObject.GetComponent<EasyJoystick>();
		easyJoystick.receiverGameObject = transform.gameObject;
	}
}
