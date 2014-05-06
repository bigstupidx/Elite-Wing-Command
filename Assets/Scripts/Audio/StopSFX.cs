using UnityEngine;
using System.Collections;

public class StopSFX : MonoBehaviour
{
	
	void Start()
	{
		Fabric.EventManager.Instance.PostEvent("SFX", Fabric.EventAction.StopSound);
		Fabric.EventManager.Instance.PostEvent("Music_Mission", Fabric.EventAction.StopSound);

		GameObject fireWeapon = GameObject.Find("Player Fire Weapon");
		GameObject dropBomb = GameObject.Find("Player Drop Bomb");

		fireWeapon.SetActive(false);
		dropBomb.SetActive(false);
	}
}
