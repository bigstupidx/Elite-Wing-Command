using UnityEngine;
using System.Collections;

public class SFXSlider : MonoBehaviour
{
	Fabric.GroupComponent sfxChannel;
	
	void Start()
	{
		UISlider sfxSlider = GetComponent<UISlider>();
		sfxSlider.value = EncryptedPlayerPrefs.GetFloat("SFX Volume", 1f);
		
		GameObject sfxChannelObject = GameObject.FindGameObjectWithTag("SFX");
		sfxChannel = sfxChannelObject.GetComponent<Fabric.GroupComponent>();
	}
	
	public void UpdateSFXVolume()
	{
		if (sfxChannel != null)
		{
			sfxChannel.SetVolume(UISlider.current.value);
			EncryptedPlayerPrefs.SetFloat("SFX Volume", UISlider.current.value);
		}
	}
}
