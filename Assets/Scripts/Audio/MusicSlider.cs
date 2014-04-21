using UnityEngine;
using System.Collections;

public class MusicSlider : MonoBehaviour
{
	Fabric.GroupComponent musicChannel;

	void Awake()
	{
		Debug.Log("Music Slider HERE");
		UISlider musicSlider = GetComponent<UISlider>();
		musicSlider.value = EncryptedPlayerPrefs.GetFloat("Music Volume", 1f);

		GameObject musicChannelObject = GameObject.FindGameObjectWithTag("Music");
		musicChannel = musicChannelObject.GetComponent<Fabric.GroupComponent>();
	}

	public void UpdateMusicVolume()
	{
		if (musicChannel != null)
		{
			musicChannel.SetVolume(UISlider.current.value);
			EncryptedPlayerPrefs.SetFloat("Music Volume", UISlider.current.value);
		}
	}
}
