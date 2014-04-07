using UnityEngine;
using System.Collections;

public class MenuScreenSet : MonoBehaviour
{
	public enum MenuScreen
	{
		Main,
		Campaign,
		Hanger,
		Options
	}

	public MenuScreen menuScreen;

	void OnClick()
	{
		EncryptedPlayerPrefs.SetString("Menu Screen", menuScreen.ToString());
	}
}
