using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(UIImageThumb))]
public class UIImageThumbInspector : Editor
{
	UIImageThumb mButton;
	
	/// <summary>
	/// Atlas selection callback.
	/// </summary>
	
	void OnSelectAtlas (Object obj)
	{
		if (mButton.target != null)
		{
			NGUIEditorTools.RegisterUndo("Atlas Selection", mButton.target);
			mButton.target.atlas = obj as UIAtlas;
			mButton.target.MakePixelPerfect();
		}
	}
	
	public override void OnInspectorGUI()
	{
		EditorGUIUtility.labelWidth = 80f;
		mButton = target as UIImageThumb;
		
		UISprite sprite = EditorGUILayout.ObjectField("Sprite", mButton.target, typeof(UISprite), true) as UISprite;
		
		if (mButton.target != sprite)
		{
			NGUIEditorTools.RegisterUndo("Image Thumb Change", mButton);
			mButton.target = sprite;
			if (sprite != null) sprite.spriteName = mButton.normalSprite;
		}
		
		if (mButton.target != null)
		{
			ComponentSelector.Draw<UIAtlas>(sprite.atlas as UIAtlas, OnSelectAtlas, true);
			
			if (sprite.atlas != null)
			{
				NGUIEditorTools.DrawSpriteField("Normal", sprite.atlas as UIAtlas, mButton.normalSprite, OnNormalSpriteChange);
				NGUIEditorTools.DrawSpriteField("Hover", sprite.atlas as UIAtlas, mButton.hoverSprite, OnHoverSpriteChange);
				NGUIEditorTools.DrawSpriteField("Pressed", sprite.atlas as UIAtlas, mButton.pressedSprite, OnPressedSpriteChange);
			}
		}
	}
	
	private void OnNormalSpriteChange(string newSprite)
	{
		mButton.normalSprite = newSprite;
	}
	
	private void OnHoverSpriteChange(string newSprite)
	{
		mButton.hoverSprite = newSprite;
	}

	private void OnPressedSpriteChange(string newSprite)
	{
		mButton.pressedSprite = newSprite;
	}
}