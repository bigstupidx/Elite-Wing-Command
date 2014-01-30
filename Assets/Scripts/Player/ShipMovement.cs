using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
	[SerializeField] float turnSensitivity = 1.2f;
	[SerializeField] float engineForce = 35f;
	[SerializeField] float boostEngineForce = 2.5f;
	[SerializeField] float boosterTimeout = 3.5f;
	bool useArrows = true;
	float turn = 0f;
	float turnTarget = 0f;
	float boosterCooldown = 10f;
	float currentForce = 0f;
	float forceMultiplier = 1f;
	bool canBoost = true;

#if UNITY_IOS && !UNITY_EDITOR
	void Start()
	{
		useArrows = false;
	}
#endif

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

	void Update()
	{
		if (useArrows)
		{
			if (Input.GetKey(KeyCode.LeftArrow))
				turnTarget = -turnSensitivity;
			else if (Input.GetKey(KeyCode.RightArrow))
				turnTarget = turnSensitivity;
			else
				turnTarget = 0;
		}

		if (Input.GetKeyDown(KeyCode.Z) && canBoost)
		{
			StartCoroutine(Booster());
		}
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, engineForce * forceMultiplier, 120f * Time.fixedDeltaTime);
		rigidbody.AddForce (transform.forward * currentForce, ForceMode.Acceleration);

		turn = Mathf.Lerp(turn, turnTarget, Time.fixedTime);

		if (Mathf.Abs(turn) > 0.005f)
			rigidbody.AddTorque (Vector3.up * 0.16f * turn, ForceMode.VelocityChange);
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
