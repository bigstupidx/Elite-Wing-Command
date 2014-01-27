using UnityEngine;
using System.Collections;

public class InitWeaponSlots : MonoBehaviour
{
	[SerializeField] UIPopupList popupList;
	string popupListText;

	void OnEnable()
	{
		switch(PlayerPrefs.GetInt("Weapon Slots"))
		{
		case 2:
			popupListText = "[99FF66]2 Weapons";
			break;
		case 3:
			popupListText = "3 Weapons";
			break;
		case 4:
			popupListText = "[FF6633]4 Weapons";
			break;
		case 5:
			popupListText = "[FF0066]5 Weapons";
			break;
		default:
			Debug.LogError("Stored Weapon Slots Not Valid: " + transform.name);
			break;
		}

		popupList.value = popupListText;
	}
}
