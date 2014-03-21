using UnityEngine;
using System.Collections;

public class UpgradesContainer : MonoBehaviour
{
	[SerializeField] float playerWeaponUpgradeCost;
	int playerWeaponLevel;
	public int PlayerWeaponLevel { get { return playerWeaponLevel; }}
	public float PlayerWeaponUpgradeCost { get { if (PlayerWeaponLevel > 0) return playerWeaponUpgradeCost * (1.5f * PlayerWeaponLevel); 
			else return playerWeaponUpgradeCost; }}

	void Start()
	{
		playerWeaponLevel = PlayerPrefs.GetInt("Player Weapon Level", 1);
	}

	public void UpgradePlayerWeaponLevel()
	{
		playerWeaponLevel += 1;
	}
}
