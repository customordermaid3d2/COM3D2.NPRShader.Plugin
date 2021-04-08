using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal abstract class ScrollablePane : ControlBase
	{
		public abstract void ShowPane();

		public ScrollablePane()
		{
		}

		public ScrollablePane(int fontSize)
		{
			try
			{
				this.FontSize = fontSize;
				this.guiScroll = Vector2.zero;
				this.screenSize = new Vector2((float)Screen.width, (float)Screen.height);
				this.Awake();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void OnGUI()
		{
			try
			{
				GUIStyle guistyle = new GUIStyle("box");
				guistyle.fontSize = Util.GetPix(12);
				guistyle.alignment = TextAnchor.UpperRight;
				float num = (float)(guistyle.fontSize * (this.isGuiScroll ? 19 : 18));
				if (this.rectGui.width > num)
				{
					num = this.rectGui.width;
				}
				if (this.rectGui.width < 1f)
				{
					this.rectGui.Set((float)Screen.width - num - (float)(ControlBase.FixedMargin * 4), (float)(ControlBase.FixedMargin * 4), num, this.guiHeight);
				}
				if (this.guiHeight != this.rectGui.height || num != this.rectGui.width)
				{
					this.rectGui.Set(this.rectGui.x, this.rectGui.y, num, this.guiHeight);
				}
				if (this.screenSize != new Vector2((float)Screen.width, (float)Screen.height))
				{
					this.rectGui.Set(this.rectGui.x, this.rectGui.y, num, this.guiHeight);
					this.screenSize = new Vector2((float)Screen.width, (float)Screen.height);
				}
				if (this.rectGui.x < 0f - this.rectGui.width * 0.9f)
				{
					this.rectGui.x = 0f;
				}
				else if (this.rectGui.x > this.screenSize.x - this.rectGui.width * 0.1f)
				{
					this.rectGui.x = this.screenSize.x - this.rectGui.width;
				}
				else if (this.rectGui.y < 0f - this.rectGui.height * 0.9f)
				{
					this.rectGui.y = 0f;
				}
				else if (this.rectGui.y > this.screenSize.y - this.rectGui.height * 0.1f)
				{
					this.rectGui.y = this.screenSize.y - this.rectGui.height;
				}
				this.rectGui = GUI.Window(253, this.rectGui, new GUI.WindowFunction(this.GuiFunc), "", guistyle);
				base.ScreenPos = new Rect(this.rectGui.x + this.guiScroll.x, this.rectGui.y - this.guiScroll.y, this.rectGui.width, this.rectGui.height);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void GuiFunc(int winId)
		{
			float height = (float)this.FontSize * 1.5f;
			float num = (float)this.FontSize * 0.3f;
			Rect rect = new Rect((float)(this.FontSize / 2), (float)this.FontSize, (float)this.FontSize * ScrollablePane.ITEM_SIZE - (float)this.FontSize, this.rectGui.height - (float)(this.FontSize * 2));
			Rect rect2 = new Rect(0f, 0f, (float)(Util.GetPix(8) * 2), (float)(Util.GetPix(8) * 2));
			if (this.guiScrollHeight > this.screenSize.y * 0.7f)
			{
				this.guiHeight = this.screenSize.y * 0.7f;
				this.isGuiScroll = true;
			}
			else
			{
				this.guiHeight = this.guiScrollHeight;
				this.isGuiScroll = false;
			}
			float num2 = (float)(this.FontSize * (this.isGuiScroll ? 19 : 18));
			Rect position = new Rect(0f, rect.y + num * 2f, this.rectGui.width, this.rectGui.height);
			Rect viewRect = new Rect(0f, 0f, this.rectGui.width, this.guiScrollHeight);
			this.guiScroll = GUI.BeginScrollView(position, this.guiScroll, viewRect);
			rect2.Set(rect.x, 0f, (float)(this.FontSize * 15), height);
			rect2.width = (float)this.FontSize * 1.2f + (float)(this.FontSize * 18) * 0.6f;
			this.ShowPane();
			GUIStyle guistyle = new GUIStyle("button");
			guistyle.fontSize = this.FontSize;
			guistyle.alignment = TextAnchor.MiddleCenter;
			this.OnGUIChildControls();
			GUI.EndScrollView();
			this.guiScrollHeight = this.LastElementSize + rect.y + num * 3f;
			GUIUtil.MouseClickOnGUIRect(this.rectGui);
			GUI.DragWindow();
		}

		public float ControlHeight
		{
			get
			{
				return (float)(this.FixedFontSize + ControlBase.FixedMargin * 2);
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ScrollablePane()
		{
		}

		public static readonly float ITEM_SIZE = 18f;

		private float guiHeight;

		private float guiScrollHeight;

		private Vector2 screenSize;

		private Vector2 guiScroll;

		private bool isGuiScroll;

		public Rect rectGui;
	}
}
