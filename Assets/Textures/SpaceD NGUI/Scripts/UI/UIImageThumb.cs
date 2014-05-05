using UnityEngine;

[AddComponentMenu("NGUI/UI/Image Thumb")]
public class UIImageThumb : MonoBehaviour
{
	public UISprite target;

	public string mNormalSprite;
	public string normalSprite {
		get { return this.mNormalSprite; } 
		set { this.mNormalSprite = value; UpdateImage(); }
	}

	public string hoverSprite;
	public string pressedSprite;
	
	public bool isEnabled
	{
		get
		{
			Collider col = collider;
			return col && col.enabled;
		}
		set
		{
			Collider col = collider;
			if (!col) return;
			
			if (col.enabled != value)
			{
				col.enabled = value;
				UpdateImage();
			}
		}
	}
	
	void OnEnable ()
	{
		if (this.target == null) this.target = GetComponentInChildren<UISprite>();
		UpdateImage();
	}
	
	void UpdateImage()
	{
		if (this.isEnabled && this.target != null)
		{
			if (!string.IsNullOrEmpty(this.normalSprite) && !string.IsNullOrEmpty(this.hoverSprite))
				this.target.spriteName = UICamera.IsHighlighted(gameObject) ? this.hoverSprite : this.normalSprite;
		}
	}
	
	void OnHover (bool isOver)
	{
		if (this.isEnabled && this.target != null)
		{
			if (!string.IsNullOrEmpty(this.normalSprite) && !string.IsNullOrEmpty(this.hoverSprite))
			{
				this.target.spriteName = isOver ? this.hoverSprite : this.normalSprite;
				this.target.Update();
			}
		}
	}
	
	void OnPress (bool pressed)
	{
		if (pressed && !string.IsNullOrEmpty(this.pressedSprite))
		{
			this.target.spriteName = this.pressedSprite;
			this.target.Update();
		}
		else this.UpdateImage();
	}
}
