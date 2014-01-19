using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
	[SerializeField] Weapon[] weapons;
	Weapon equipped;
	
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
			equipped.Fire();
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			equipped.Stop();
		}
		else if (Input.GetKeyDown(KeyCode.X))
		{
			StartCoroutine(DropBomb());
		}
	}

	IEnumerator DropBomb()
	{
		var lastEquipped = equipped;
		Equip(weapons.Length - 1);
		equipped.Fire();
		yield return new WaitForSeconds(0.1f);
		equipped.Stop();
		equipped = lastEquipped;
	}
}
