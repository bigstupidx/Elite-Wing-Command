using UnityEngine;
using System.Collections;

public class GameObjectSetState : MonoBehaviour
{
	[SerializeField] bool setActive = false;

	void Start()
	{
		this.gameObject.SetActive(setActive);
	}
}
