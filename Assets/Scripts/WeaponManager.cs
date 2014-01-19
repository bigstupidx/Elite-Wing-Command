using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
	[SerializeField] Weapon[] weapons;
	Weapon equipped;
	bool firing = false;
	int fingersInArea;

	void OnEnable()
	{
		EasyButton.On_ButtonDown += On_ButtonDown;
		EasyButton.On_ButtonUp += On_ButtonUp;
	}

	void OnDisable()
	{
		EasyButton.On_ButtonDown -= On_ButtonDown;
		EasyButton.On_ButtonUp -= On_ButtonUp;
	}
	
	void OnDestroy()
	{
		EasyButton.On_ButtonDown -= On_ButtonDown;
		EasyButton.On_ButtonUp -= On_ButtonUp;
	}

	void  On_ButtonDown(string buttonName)
	{
		if (buttonName == "Player Fire Weapon")
		{
			firing = true;
			FireWeapon();
		}
		else if (buttonName == "Player Drop Bomb")
		{
			firing = true;
			StartCoroutine(DropBomb());
		}
	}
	
	void  On_ButtonUp(string buttonName)
	{
		if (firing)
		{
			StopWeapon();
			firing = false;
		}
	}

	void Awake()
	{
		Equip(0);
	}
	
	void Update()
	{
		FireControl();
		string text = Input.inputString;
		if (text.Length == 0)
			return;
		char first = text[0];
		if (char.IsNumber(first))
		{
			Equip((int)char.GetNumericValue(first) - 1);
		}
	}

	public void Equip(int id)
	{
		if (id < 0 || id >= weapons.Length)
			return;
		
		for(int i = 0; i < weapons.Length; ++i)
		{
			if (i == id)
			{
				equipped = weapons[id];
				equipped.Equip();
			}
			else
				weapons[id].Unequip();
		}
	}

	public void FireControl()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			FireWeapon();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			StopWeapon();
		}
		else if (Input.GetKeyDown(KeyCode.X))
		{
			StartCoroutine(DropBomb());
		}
	}

	void FireWeapon()
	{
		equipped.Fire();
	}

	void StopWeapon()
	{
		equipped.Stop();
	}

	IEnumerator DropBomb()
	{
		var lastEquipped = equipped;
		Equip(weapons.Length - 1);
		equipped.Fire();
		yield return new WaitForSeconds(0.01f);
		equipped.Stop();
		equipped = lastEquipped;
	}
}
