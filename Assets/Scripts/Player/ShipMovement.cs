using UnityEngine;
using System.Collections;

public class ShipMovement : MonoBehaviour
{
	[SerializeField] float engineForce = 35f;
	[SerializeField] float boostEngineForce = 2.5f;
	bool useArrows = true;
	float turnSensitivity = 1.2f;
	float turn = 0f;
	float turnTarget = 0f;
	float boosterTimeout = 3.0f;
	float boosterCooldown = 10f;
	float currentForce = 0f;
	float forceMultiplier = 1f;
	float speedMultiplier = 1f;
	bool canBoost = true;
	bool increaseVolume = false;
	bool decreaseVolume = false;
	float currentVolume;
	float minVolume = 0.02f;
	float maxVolume = 0.3f;

#if UNITY_IOS && !UNITY_EDITOR
	void Start()
	{
		useArrows = false;
	}
#endif

	void OnEnable()
	{
		turnTarget = 0;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;

		EasyJoystick.On_JoystickMove += On_JoystickMove;
		EasyJoystick.On_JoystickDoubleTap += On_JoystickDoubleTap;

		canBoost = true;
		StartCoroutine(BoosterAudioStart());
	}

	IEnumerator BoosterAudioStart()
	{
		yield return new WaitForSeconds(0.1f);
		currentVolume = minVolume;
		Fabric.EventManager.Instance.PostEvent("SFX_Player_Booster", Fabric.EventAction.SetVolume, currentVolume, gameObject);
		Fabric.EventManager.Instance.PostEvent("SFX_Player_Booster", Fabric.EventAction.PlaySound, gameObject);
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

	void Awake()
	{
		speedMultiplier = EncryptedPlayerPrefs.GetFloat("Player Speed Multiplier", 1f);
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

		if (increaseVolume)
		{
			currentVolume += Time.deltaTime/5f;

			if (currentVolume >= maxVolume)
			{
				currentVolume = maxVolume;
				increaseVolume = false;
			}

			Fabric.EventManager.Instance.PostEvent("SFX_Player_Booster", Fabric.EventAction.SetVolume, currentVolume, gameObject);
		}
		else if (decreaseVolume)
		{
			currentVolume -= Time.deltaTime/5f;
			
			if (currentVolume <= minVolume)
			{
				currentVolume = minVolume;
				decreaseVolume = false;
			}
			
			Fabric.EventManager.Instance.PostEvent("SFX_Player_Booster", Fabric.EventAction.SetVolume, currentVolume, gameObject);
		}

		if (Input.GetKeyDown(KeyCode.Z) && canBoost)
		{
			StartCoroutine(Booster());
		}
	}

	void FixedUpdate()
	{
		currentForce = Mathf.MoveTowards(currentForce, engineForce * speedMultiplier * forceMultiplier, 120f * Time.fixedDeltaTime);
		rigidbody.AddForce (transform.forward * currentForce, ForceMode.Acceleration);
		turn = Mathf.Lerp(turn, turnTarget, Time.fixedTime);

		if (Mathf.Abs(turn) > 0.005f)
			rigidbody.AddTorque (Vector3.up * 0.16f * turn, ForceMode.VelocityChange);
	}

	IEnumerator Booster()
	{
		canBoost = false;
		SetBoosting(true);
		decreaseVolume = false;
		increaseVolume = true;
		yield return new WaitForSeconds(boosterTimeout);
		SetBoosting(false);
		increaseVolume = false;
		decreaseVolume = true;
		yield return new WaitForSeconds(boosterCooldown);
		canBoost = true;
	}
}
