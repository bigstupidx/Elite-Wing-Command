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
		weaponSlots = EncryptedPlayerPrefs.GetInt("Weapon Slots");

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

		switch(EncryptedPlayerPrefs.GetInt("Weapon Equip " + weaponEquipSlot.ToString()))
		{
		case 1:
			popupListText = "[FF0066]Red";
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
