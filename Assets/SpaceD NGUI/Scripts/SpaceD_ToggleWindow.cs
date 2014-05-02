using UnityEngine;
using System.Collections;

public class SpaceD_ToggleWindow : MonoBehaviour {

	[SerializeField] private UIToggle toggle;
	[SerializeField] private int[] windowIds;

	void Start()
	{
		if (this.toggle == null)
			this.toggle = this.GetComponent<UIToggle>();

		if (this.toggle != null)
			this.toggle.onChange.Add(new EventDelegate(OnChange));
	}

	void OnChange()
	{
		bool check = this.toggle.value;

		foreach (int id in this.windowIds)
		{
			SpaceDUIWindow window = SpaceDUIWindow.GetWindow(id);

			if (window != null)
			{
				if (check)
					window.FadeIn(0.5f);
				else
					window.FadeOut(0.5f);
			}
		}
	}
}
