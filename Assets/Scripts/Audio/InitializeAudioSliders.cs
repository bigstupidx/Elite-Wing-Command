using UnityEngine;
using System.Collections;

public class InitializeAudioSliders : MonoBehaviour
{

	void Start()
	{
		GameObject musicChannelObject = GameObject.FindGameObjectWithTag("Music");
		Fabric.GroupComponent musicChannel = musicChannelObject.GetComponent<Fabric.GroupComponent>();
		musicChannel.SetVolume(EncryptedPlayerPrefs.GetFloat("Music Volume", 1f));
		
		GameObject sfxChannelObject = GameObject.FindGameObjectWithTag("SFX");
		Fabric.GroupComponent sfxChannel = sfxChannelObject.GetComponent<Fabric.GroupComponent>();
		sfxChannel.SetVolume(EncryptedPlayerPrefs.GetFloat("SFX Volume", 1f));
	}
}