using UnityEngine;
using System.Collections;

public class TutorialPanelTransition : MonoBehaviour
{
	[SerializeField] GameObject panelToOpen;
	[SerializeField] GameObject panelToClose;
	[SerializeField] bool hideGUI = false;
	[SerializeField] GameObject guiObject;
	[SerializeField] Camera radarCamera;
	[SerializeField] KGFMapSystem mapScript;

	void OnClick()
	{
		panelToClose.SetActive(false);

		if (hideGUI)
		{
			guiObject.SetActive(false);
			radarCamera.enabled = false;
			mapScript.SetGlobalHideGui(true);
		}
		else
		{
			guiObject.SetActive(true);
			radarCamera.enabled = true;
			mapScript.SetGlobalHideGui(false);
		}

		panelToOpen.SetActive(true);
	}
}
