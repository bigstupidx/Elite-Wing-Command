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

	[SerializeField] float allyAirWeaponUpgradeCost;
	int allyAirWeaponLevel;
	public int AllyAirWeaponLevel { get { return allyAirWeaponLevel; }}
	public float AllyAirWeaponUpgradeCost { get { if (AllyAirWeaponLevel > 0) return allyAirWeaponUpgradeCost * (1.5f * AllyAirWeaponLevel); 
			else return allyAirWeaponUpgradeCost; } set { allyAirWeaponUpgradeCost = value; }}

	[SerializeField] float allyAirSpeedUpgradeCost;
	int allyAirSpeedLevel;
	public int AllyAirSpeedLevel { get { return allyAirSpeedLevel; }}
	public float AllyAirSpeedUpgradeCost { get { if (AllyAirSpeedLevel > 0) return allyAirSpeedUpgradeCost * (1.5f * AllyAirSpeedLevel); 
			else return allyAirSpeedUpgradeCost; } set { allyAirSpeedUpgradeCost = value; }}

	[SerializeField] float allyGroundWeaponUpgradeCost;
	int allyGroundWeaponLevel;
	public int AllyGroundWeaponLevel { get { return allyGroundWeaponLevel; }}
	public float AllyGroundWeaponUpgradeCost { get { if (AllyGroundWeaponLevel > 0) return allyGroundWeaponUpgradeCost * (1.5f * AllyGroundWeaponLevel); 
			else return allyGroundWeaponUpgradeCost; } set { allyGroundWeaponUpgradeCost = value; }}
	
	[SerializeField] float allyGroundSpeedUpgradeCost;
	int allyGroundSpeedLevel;
	public int AllyGroundSpeedLevel { get { return allyGroundSpeedLevel; }}
	public float AllyGroundSpeedUpgradeCost { get { if (AllyGroundSpeedLevel > 0) return allyGroundSpeedUpgradeCost * (1.5f * AllyGroundSpeedLevel); 
			else return allyGroundSpeedUpgradeCost; } set { allyGroundSpeedUpgradeCost = value; }}

	void Start()
	{
		playerWeaponLevel = EncryptedPlayerPrefs.GetInt("Player Weapon Level", 0);
		playerSpeedLevel = EncryptedPlayerPrefs.GetInt("Player Speed Level", 0);
		playerHealthLevel = EncryptedPlayerPrefs.GetInt("Player Health Level", 0);
		playerRecoveryLevel = EncryptedPlayerPrefs.GetInt("Player Recovery Level", 0);
		allyAirWeaponLevel = EncryptedPlayerPrefs.GetInt("Ally Air Weapon Level", 0);
		allyAirSpeedLevel = EncryptedPlayerPrefs.GetInt("Ally Air Speed Level", 0);
		allyGroundWeaponLevel = EncryptedPlayerPrefs.GetInt("Ally Ground Weapon Level", 0);
		allyGroundSpeedLevel = EncryptedPlayerPrefs.GetInt("Ally Ground Speed Level", 0);
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
		playerRecoveryLevel += 1;
	}

	public void UpgradeAllyAirWeaponLevel()
	{
		allyAirWeaponLevel += 1;
	}
	
	public void UpgradeAllyAirSpeedLevel()
	{
		allyAirSpeedLevel += 1;
	}

	public void UpgradeAllyGroundWeaponLevel()
	{
		allyGroundWeaponLevel += 1;
	}
	
	public void UpgradeAllyGroundSpeedLevel()
	{
		allyGroundSpeedLevel += 1;
	}
}
