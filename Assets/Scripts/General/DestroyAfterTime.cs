using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
	[SerializeField] float destroyTime = 5f;
	
	void Start()
	{
		Destroy(gameObject, destroyTime);
	}
}
