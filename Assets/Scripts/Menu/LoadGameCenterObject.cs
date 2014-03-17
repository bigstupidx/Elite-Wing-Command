using UnityEngine;
using System.Collections;

public class LoadGameCenterObject : MonoBehaviour
{
	[SerializeField] GameObject gameCenterPrefab;
	GameObject gameCenterObject;

	void Awake()
	{
		gameCenterObject = GameObject.FindGameObjectWithTag("GameCenter");

		if (gameCenterObject == null)
		{
			gameCenterObject = (GameObject)Instantiate(gameCenterPrefab);
			DontDestroyOnLoad(gameCenterObject);
		}
	}
}