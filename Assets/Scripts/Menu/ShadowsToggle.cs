using UnityEngine;
using System.Collections;

public class ShadowsToggle : MonoBehaviour
{
	[SerializeField] GameManager gameManager;
	UIToggle toggle;
	GameObject target;

	void OnEnable()
	{
		toggle = GetComponent<UIToggle>();

		if (EncryptedPlayerPrefs.GetInt("Shadows", 1) == 1)
			toggle.startsActive = true;
		else
			toggle.startsActive = false;
	}

	public void ToggleShadows()
	{
		if (UIToggle.current.value == true)
			EncryptedPlayerPrefs.SetInt("Shadows", 1);
		else
			EncryptedPlayerPrefs.SetInt("Shadows", 0);

		PlayerPrefs.Save();
		gameManager.UpdateShadowSettings();
	}
}
