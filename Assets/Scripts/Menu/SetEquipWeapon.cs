using UnityEngine;
using System.Collections;
using System;

public class SetEquipWeapon : MonoBehaviour
{
	[SerializeField] UILabel uiLabel;
	[SerializeField] int weaponEquipSlot;

	public void SetCurrentSelection()
	{
		uiLabel.text = UIPopupList.current.value;

		switch(UIPopupList.current.value)
		{
		case "[FF0066]Red":
			PlayerPrefs.SetInt("Weapon Equip " + weaponEquipSlot.ToString(), 1);
			break;
		case "[0000FF]Blue":
			PlayerPrefs.SetInt("Weapon Equip " + weaponEquipSlot.ToString(), 2);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}
	}
}