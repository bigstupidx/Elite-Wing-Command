using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Input Focus")]
public class UIInputFocus : MonoBehaviour
{
	public UISprite target;
	public string normalSprite;
	public string selectedSprite;
	
	void Start ()
	{
		if (target == null) target = GetComponentInChildren<UISprite>();
	}

	public void OnSelect(bool isSelected)
	{
		if (target != null)
		{
			target.spriteName = isSelected ? selectedSprite : normalSprite;
			target.MakePixelPerfect();
		}
	}
}
