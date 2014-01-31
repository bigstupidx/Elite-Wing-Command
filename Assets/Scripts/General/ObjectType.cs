using UnityEngine;
using System.Collections;

public class ObjectType : MonoBehaviour
{
	[SerializeField] bool isAirUnit = false;
	[SerializeField] bool isGroundUnit = false;
	public bool IsAirUnit { get { return isAirUnit; }}
	public bool IsGroundUnit { get { return isGroundUnit; }}

	void Start()
	{
		if (!isAirUnit && !isGroundUnit)
			Debug.LogError("Object Type Not Defined: " + transform.name);
	}
}