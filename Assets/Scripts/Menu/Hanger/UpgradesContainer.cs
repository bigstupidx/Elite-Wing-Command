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

	[SerializeField] float playerHealthUpgradeCost;
	int playerHealthLevel;
	public int PlayerHealthLevel { get { return playerHealthLevel; }}
	public float PlayerHealthUpgradeCost { get { if (PlayerHealthLevel > 0) return playerHealthUpgradeCost * (1.5f * PlayerHealthLevel); 
			else return playerHealthUpgradeCost; } set { playerHealthUpgradeCost = value; }}

	[SerializeField] float playerRecoveryUpgradeCost;
	int playerRecoveryLevel;
	public int PlayerRecoveryLevel { get { return playerRecoveryLevel; }}
	public float PlayerRecoveryUpgradeCost { get { if (PlayerRecoveryLevel > 0) return playerRecoveryUpgradeCost * (1.5f * PlayerRecoveryLevel); 
			else return playerRecoveryUpgradeCost; } set { playerRecoveryUpgradeCost = value; }}

	void Start()
	{
		playerWeaponLevel = PlayerPrefs.GetInt("Player Weapon Level", 0);
		playerSpeedLevel = PlayerPrefs.GetInt("Player Speed Level", 0);
		playerHealthLevel = PlayerPrefs.GetInt("Player Health Level", 0);
		playerRecoveryLevel = PlayerPrefs.GetInt("Player Recovery Level", 0);
	}

	public void UpgradePlayerWeaponLevel()
	{
		playerWeaponLevel += 1;
	}

	public void UpgradePlayerSpeedLevel()
	{
		playerSpeedLevel += 1;
	}

	public void UpgradePlayerHealthLevel()
	{
		playerHealthLevel += 1;
	}

	public void UpgradePlayerRecoveryLevel()
	{
		playerHealthLevel += 1;
	}
}
