using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/Image Checkbox")]
public class UIImageCheckbox : MonoBehaviour
{
	public UISprite target;
	public string mNormalSprite;
	public string normalSprite {
		get { return this.mNormalSprite; }
		set { this.mNormalSprite = value; UpdateImage(); }
	}
	public string hoverSprite;
	
	void Start ()
	{
		if (target == null) target = GetComponentInChildren<UISprite>();
	}
	
	void OnHover (bool isOver)
	{
		if (target != null && !string.IsNullOrEmpty(normalSprite) && !string.IsNullOrEmpty(hoverSprite))
		{
			target.spriteName = isOver ? hoverSprite : normalSprite;
			target.MakePixelPerfect();
		}
	}

	void UpdateImage()
	{
		if (target != null && !string.IsNullOrEmpty(normalSprite))
		{
			target.spriteName = normalSprite;
			target.MakePixelPerfect();
		}
	}
}