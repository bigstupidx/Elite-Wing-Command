using UnityEngine;
using System.Collections;

public class InitEquipWeapon : MonoBehaviour
{
	[SerializeField] UIPopupList popupList;
	[SerializeField] UILabel listTitle;
	[SerializeField] int weaponEquipSlot;
	int weaponSlots;
	string popupListText;
	
	void OnEnable()
	{
		weaponSlots = PlayerPrefs.GetInt("Weapon Slots");

		if (weaponEquipSlot > weaponSlots)
		{
			popupList.gameObject.SetActive(false);
			listTitle.gameObject.SetActive(false);
		}
		else
		{
			popupList.gameObject.SetActive(true);
			listTitle.gameObject.SetActive(true);
		}

		switch(PlayerPrefs.GetInt("Weapon Equip " + weaponEquipSlot.ToString()))
		{
		case 1:
			popupListText = "[99FF66]Green";
			break;
		case 2:
			popupListText = "[0000FF]Blue";
			break;
		default:
			Debug.LogError("Equip Weapon Not Valid: " + transform.name);
			break;
		}
		
		popupList.value = popupListText;
	}
}
