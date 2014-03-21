using UnityEngine;
using System.Collections;

public class UpgradesContainer : MonoBehaviour
{
	[SerializeField] float playerWeaponUpgradeCost;
	int playerWeaponLevel;
	public int PlayerWeaponLevel { get { return playerWeaponLevel; }}
	public float PlayerWeaponUpgradeCost { get { if (PlayerWeaponLevel > 0) return playerWeaponUpgradeCost * (1.5f * PlayerWeaponLevel); 
			else return playerWeaponUpgradeCost; } set { playerWeaponUpgradeCost = value; }}

	[SerializeField] float playerSpeedUpgradeCost;
	int playerSpeedLevel;
	public int PlayerSpeedLevel { get { return playerSpeedLevel; }}
	public float PlayerSpeedUpgradeCost { get { if (PlayerSpeedLevel > 0) return playerSpeedUpgradeCost * (1.5f * PlayerSpeedLevel); 
			else return playerSpeedUpgradeCost; } set { playerSpeedUpgradeCost = value; }}

	void Start()
	{
		playerWeaponLevel = PlayerPrefs.GetInt("Player Weapon Level", 0);
		playerSpeedLevel = PlayerPrefs.GetInt("Player Speed Level", 0);
	}

	public void UpgradePlayerWeaponLevel()
	{
		playerWeaponLevel += 1;
	}

	public void UpgradePlayerSpeedLevel()
	{
		playerSpeedLevel += 1;
	}
}
