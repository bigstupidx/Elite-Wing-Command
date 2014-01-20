using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
	[SerializeField] bool useArrows = true;
	[SerializeField] float turnSensitivity = 1.45f;
	[SerializeField] float engineForce = 35f;
	[SerializeField] float boostEngineForce = 2.5f;
	float turn = 0f;
	float turnTarget = 0f;
	float boosterTimeout = 3f;
	float boosterCooldown = 10f;
	float currentForce = 0f;
	float forceMultiplier = 1f;
	bool canBoost = true;

	void OnEnable()
	{
		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickDoubleTap += On_JoystickDoubleTap;
	}
	
	void OnDisable()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;
		EasyJoystick.On_JoystickDoubleTap -= On_JoystickDoubleTap;
	}
	
	void OnDestroy()
	{
		EasyJoystick.On_JoystickMove -= On_JoystickMove;
		EasyJoystick.On_JoystickDoubleTap -= On_JoystickDoubleTap;
	}

	void On_JoystickMove(MovingJoystick move)
	{
		turnTarget = move.joystickAxis.x * turnSensitivity;
	}

	void On_JoystickDoubleTap (MovingJoystick move)
	{
		if (canBoost)
			StartCoroutine(Booster());
	}

	public void SetBoosting(bool boosting)
	{
		if (boosting)
			forceMultiplier = boostEngineForce;
		else
			forceMultiplier = 1f;
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, engineForce * forceMultiplier, 120f * Time.deltaTime);
		rigidbody.AddForce (transform.forward * currentForce, ForceMode.Acceleration);

		if (useArrows)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
				turnTarget = -1;
			else if (Input.GetKey(KeyCode.RightArrow))
				turnTarget = 1;
			else
				turnTarget = 0;
		}

		turn = Mathf.Lerp(turn, turnTarget, Time.time);

		if (Mathf.Abs(turn) > 0.005f)
			rigidbody.AddTorque (Vector3.up * 0.12f * turn, ForceMode.VelocityChange);

		if (Input.GetKeyDown(KeyCode.Z) && canBoost)
		{
			StartCoroutine(Booster());
		}
	}

	IEnumerator Booster()
	{
		canBoost = false;
		SetBoosting(true);
		yield return new WaitForSeconds(boosterTimeout);
		SetBoosting(false);
		yield return new WaitForSeconds(boosterCooldown);
		canBoost = true;
	}
}
