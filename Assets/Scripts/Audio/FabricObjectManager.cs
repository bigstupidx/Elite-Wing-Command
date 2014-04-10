using UnityEngine;
using System.Collections;

public class FabricObjectManager : MonoBehaviour
{
	[SerializeField] GameObject fabricPrefab;
	public GameObject FabricPrefab { get { return fabricPrefab; }}

	void Awake()
	{
		GameObject fabricPrefabObject = GameObject.FindGameObjectWithTag("Fabric");

		if (fabricPrefabObject == null)
			Instantiate(fabricPrefab);
	}
}
