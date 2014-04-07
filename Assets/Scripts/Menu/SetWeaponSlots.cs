using UnityEngine;
using System.Collections;
using System;

public class SetWeaponSlots : MonoBehaviour
{
	[SerializeField] UILabel uiLabel;

	public void SetCurrentSelection()
	{
		uiLabel.text = UIPopupList.current.value;

		switch(UIPopupList.current.value)
		{
		case "[99FF66]2 Weapons":
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 2);
			break;
		case "3 Weapons":
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 3);
			break;
		case "[FF6633]4 Weapons":
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 4);
			break;
		case "[FF0066]5 Weapons":
			EncryptedPlayerPrefs.SetInt("Weapon Slots", 5);
			break;
		default:
			Debug.LogError("Selection Not Valid: " + transform.name);
			break;
		}

		PlayerPrefs.Save();
	}
}
