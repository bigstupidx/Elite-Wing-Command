using UnityEngine;
using System.Collections;

public class ObjectiveHealthIndicator : MonoBehaviour
{
	[SerializeField] GameObject objectiveHealthIndicator;
	[SerializeField] Damageable objectiveDamageable;
	[SerializeField] float targetOffsetNormal = 0f;
	[SerializeField] float targetOffsetRotated = 0f;
	GameObject healthIndicator;
	Camera mainCamera;
	Camera nguiCamera;
	UISlider healthIndicatorSlider;
	UIFollowTarget healthIndicatorFollowTarget;
	float objectiveInitialHealth;
	float objectivePreviousHealth;
	
	void Start ()
	{
		var ModifyPosition = transform.parent.transform.position;

		if (transform.parent.transform.eulerAngles.y == 0f || transform.parent.transform.eulerAngles.y == 180f)
			ModifyPosition.z = ModifyPosition.z + targetOffsetNormal;
		else
			ModifyPosition.z = ModifyPosition.z + targetOffsetRotated;

		transform.position = ModifyPosition;

		var mainCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
		mainCamera = mainCameraObject.GetComponent<Camera>();
		var nguiCameraObject = GameObject.FindGameObjectWithTag("UICamera");
		nguiCamera = nguiCameraObject.GetComponent<Camera>();

		GameObject nguiRoot = GameObject.FindGameObjectWithTag("UIRoot");
		healthIndicator = (GameObject)Instantiate(objectiveHealthIndicator, transform.position, objectiveHealthIndicator.transform.rotation);
		healthIndicator.transform.name = transform.parent.name;
		healthIndicator.transform.parent = nguiRoot.transform;
		healthIndicator.transform.localScale = new Vector3(1, 1, 1);

		healthIndicatorSlider = healthIndicator.GetComponentInChildren<UISlider>();
		healthIndicatorFollowTarget = healthIndicator.GetComponent<UIFollowTarget>();
		healthIndicatorFollowTarget.target = transform;
		healthIndicatorFollowTarget.gameCamera = mainCamera;
		healthIndicatorFollowTarget.uiCamera = nguiCamera;

		objectiveInitialHealth = objectiveDamageable.InitialHealth;
		objectivePreviousHealth = objectiveDamageable.Health;
		healthIndicatorSlider.value = objectivePreviousHealth/objectiveInitialHealth;
	}

	void FixedUpdate()
	{
		if (objectivePreviousHealth != objectiveDamageable.Health/objectiveInitialHealth)
		{
			healthIndicatorSlider.value = objectiveDamageable.Health/objectiveInitialHealth;
			objectivePreviousHealth = objectiveDamageable.Health;
		}
	}
}
