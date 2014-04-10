using UnityEngine;
using System.Collections;

public class StartAudioAtStart : MonoBehaviour
{
	[SerializeField] FabricObjectManager fabricObjectManager;
	[SerializeField] string eventName;

	void Start ()
	{
		if (!Fabric.EventManager.Instance.IsEventActive(eventName, fabricObjectManager.FabricPrefab))
		{
			Fabric.EventManager.Instance.PostEvent(eventName, Fabric.EventAction.PlaySound);

			GameObject musicChannelObject = GameObject.FindGameObjectWithTag("Music");
			Fabric.GroupComponent musicChannel = musicChannelObject.GetComponent<Fabric.GroupComponent>();
			musicChannel.SetVolume(EncryptedPlayerPrefs.GetFloat("Music Volume", 1f));

			GameObject sfxChannelObject = GameObject.FindGameObjectWithTag("SFX");
			Fabric.GroupComponent sfxChannel = sfxChannelObject.GetComponent<Fabric.GroupComponent>();
			sfxChannel.SetVolume(EncryptedPlayerPrefs.GetFloat("SFX Volume", 1f));
		}
	}
}
