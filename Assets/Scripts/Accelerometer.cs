using UnityEngine;
using System.Collections;

public class Accelerometer : MonoBehaviour
{
	[SerializeField] TextMesh tm;
	
	void Update ()
	{
		tm.text = RotationYXAngle180Degree(Input.acceleration).ToString("F2");
	}
	
	public static float RotationYXAngle180Degree(Vector3 currentAcc)
	{
#if UNITY_EDITOR
		return 0;
#else
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
			currentAcc = -currentAcc;
    	float rotationAngle = Mathf.Rad2Deg * Mathf.Atan2(currentAcc.y, Mathf.Abs(currentAcc.x));
    	return rotationAngle;
#endif
	}
}
