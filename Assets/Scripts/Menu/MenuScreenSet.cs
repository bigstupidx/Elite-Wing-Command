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
		PlayerPrefs.SetString("Menu Screen", menuScreen.ToString());
	}
}
