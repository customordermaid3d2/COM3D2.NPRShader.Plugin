using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	public static class GlobalColorPicker
	{
		static GlobalColorPicker()
		{
			GlobalColorPicker.gsWin = new GUIStyle("box");
			GlobalColorPicker.gsWin.fontSize = Util.GetPix(12);
			GlobalColorPicker.gsWin.alignment = TextAnchor.UpperRight;
		}

		public static void Update()
		{
			if (GlobalColorPicker.color.show)
			{
				GlobalColorPicker.color.rect = GUI.Window(GlobalColorPicker.color.WINDOW_ID, GlobalColorPicker.color.rect, new GUI.WindowFunction(GlobalColorPicker.color.GuiFunc), string.Empty, GlobalColorPicker.gsWin);
			}
		}

		public static bool Visible
		{
			get
			{
				return GlobalColorPicker.color.show;
			}
		}

		public static void Set(Vector2 p, float fWidth, int iFontSize, Color32 c, ColorPickerType type, Action<Color32, MaidParts.PartsColor> f)
		{
			GlobalColorPicker.color.Set(p, fWidth, iFontSize, c, type, f);
		}

		private static GUIStyle gsWin;

		private static GlobalColorPicker.ColorWindow color = new GlobalColorPicker.ColorWindow(211);

		internal class ColorWindow
		{
			public Rect rect
			{
				[CompilerGenerated]
				get
				{
					return this.Rect;
				}
				[CompilerGenerated]
				set
				{
					this.Rect = value;
				}
			}

			private float fMargin
			{
				[CompilerGenerated]
				get
				{
					return this.FMargin;
				}
				[CompilerGenerated]
				set
				{
					this.FMargin = value;
				}
			}

			private float fRightPos
			{
				[CompilerGenerated]
				get
				{
					return this.FRightPos;
				}
				[CompilerGenerated]
				set
				{
					this.FRightPos = value;
				}
			}

			private float fUpPos
			{
				[CompilerGenerated]
				get
				{
					return this.FUpPos;
				}
				[CompilerGenerated]
				set
				{
					this.FUpPos = value;
				}
			}

			public bool show
			{
				[CompilerGenerated]
				get
				{
					return this.Show;
				}
				[CompilerGenerated]
				private set
				{
					this.Show = value;
				}
			}

			public Action<Color32, MaidParts.PartsColor> func
			{
				[CompilerGenerated]
				get
				{
					return this.Func;
				}
				[CompilerGenerated]
				private set
				{
					this.Func = value;
				}
			}

			private GUIStyle gsLabel
			{
				[CompilerGenerated]
				get
				{
					return this.GsLabel;
				}
				[CompilerGenerated]
				set
				{
					this.GsLabel = value;
				}
			}

			private GUIStyle gsButton
			{
				[CompilerGenerated]
				get
				{
					return this.GsButton;
				}
				[CompilerGenerated]
				set
				{
					this.GsButton = value;
				}
			}

			private Texture2D texture
			{
				[CompilerGenerated]
				get
				{
					return this.Texture;
				}
				[CompilerGenerated]
				set
				{
					this.Texture = value;
				}
			}

			private byte r
			{
				[CompilerGenerated]
				get
				{
					return this.R;
				}
				[CompilerGenerated]
				set
				{
					this.R = value;
				}
			}

			private byte g
			{
				[CompilerGenerated]
				get
				{
					return this.G;
				}
				[CompilerGenerated]
				set
				{
					this.G = value;
				}
			}

			private byte b
			{
				[CompilerGenerated]
				get
				{
					return this.B;
				}
				[CompilerGenerated]
				set
				{
					this.B = value;
				}
			}

			private byte a
			{
				[CompilerGenerated]
				get
				{
					return this.A;
				}
				[CompilerGenerated]
				set
				{
					this.A = value;
				}
			}

			private ColorPickerType type
			{
				[CompilerGenerated]
				get
				{
					return this.Type;
				}
				[CompilerGenerated]
				set
				{
					this.Type = value;
				}
			}

			public ColorWindow(int iWIndowID)
			{
				this.WINDOW_ID = iWIndowID;
				this.r = (this.g = (this.b = (this.a = byte.MaxValue)));
				this.texture = new Texture2D(1, 1);
				this.texture.SetPixel(0, 0, new Color32(this.r, this.g, this.b, this.a));
				this.texture.Apply();
			}

			public void Set(Vector2 p, float fWidth, int iFontSize, Color32 c, ColorPickerType type, Action<Color32, MaidParts.PartsColor> f)
			{
				this.rect = new Rect(p.x - fWidth, p.y, fWidth, 0f);
				this.fRightPos = p.x + fWidth;
				this.fUpPos = p.y;
				this.gsLabel = new GUIStyle("label");
				this.gsLabel.fontSize = iFontSize;
				this.gsLabel.alignment = TextAnchor.MiddleLeft;
				this.gsButton = new GUIStyle("button");
				this.gsButton.fontSize = iFontSize;
				this.gsButton.alignment = TextAnchor.MiddleCenter;
				this.fMargin = (float)iFontSize * 0.3f;
				this.func = f;
				this.r = c.r;
				this.g = c.g;
				this.b = c.b;
				this.a = c.a;
				this.partsColor = default(MaidParts.PartsColor);
				this.type = type;
				this.texture.SetPixel(0, 0, c);
				this.texture.Apply();
				this.show = true;
			}

			private float slider(float val, float min, float max, string name, ref Rect rectItem)
			{
				rectItem.width = this.rect.width - (float)this.gsLabel.fontSize;
				rectItem.y += rectItem.height + this.fMargin;
				GUI.Label(rectItem, name + ": " + val.ToString(), this.gsLabel);
				rectItem.x = this.rect.width - (float)this.gsLabel.fontSize * 4.5f;
				rectItem.width = (float)(this.gsLabel.fontSize * 2);
				if (GUI.Button(rectItem, "-1", this.gsButton))
				{
					val = ((val == min) ? val : (val - 1f));
				}
				rectItem.x += rectItem.width;
				if (GUI.Button(rectItem, "+1", this.gsButton))
				{
					val = ((val == max) ? val : (val + 1f));
				}
				rectItem.x = (float)this.gsLabel.fontSize * 0.5f;
				rectItem.width = this.rect.width - (float)this.gsLabel.fontSize;
				rectItem.y += rectItem.height;
				return GUI.HorizontalSlider(rectItem, val, 0f, 255f);
			}

			public void GuiFunc(int winId)
			{
				int fontSize = this.gsLabel.fontSize;
				Rect position = new Rect((float)fontSize * 0.5f, (float)fontSize * 0.5f, (float)fontSize * 1.5f, (float)fontSize * 1.5f);
				GUI.DrawTexture(position, this.texture);
				if (this.type == ColorPickerType.RGB || this.type == ColorPickerType.RGBA)
				{
					this.r = (byte)this.slider((float)this.r, 0f, 255f, "R", ref position);
					this.g = (byte)this.slider((float)this.g, 0f, 255f, "G", ref position);
					this.b = (byte)this.slider((float)this.b, 0f, 255f, "B", ref position);
					if (this.type == ColorPickerType.RGBA)
					{
						this.a = (byte)this.slider((float)this.a, 0f, 255f, "A", ref position);
					}
				}
				float num = position.y + position.height + this.fMargin;
				if (this.rect.height != num)
				{
					Rect rect = new Rect(this.rect.x, this.rect.y - num, this.rect.width, num);
					this.rect = rect;
				}
				else if (this.rect.x < 0f)
				{
					Rect rect2 = new Rect(this.fRightPos, this.rect.y, this.rect.width, this.rect.height);
					this.rect = rect2;
				}
				else if (this.rect.y < 0f)
				{
					Rect rect3 = new Rect(this.rect.x, this.fUpPos, this.rect.width, this.rect.height);
					this.rect = rect3;
				}
				if (GUI.changed)
				{
					this.texture.SetPixel(0, 0, new Color32(this.r, this.g, this.b, this.a));
					this.texture.Apply();
					this.func(new Color32(this.r, this.g, this.b, this.a), this.partsColor);
				}
				GUIUtil.MouseClickOnGUIRect(this.rect);
				GUI.DragWindow();
				if (this.GetAnyMouseButtonDown())
				{
					Vector2 point = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
					if (!this.rect.Contains(point))
					{
						this.func(new Color32(this.r, this.g, this.b, this.a), this.partsColor);
						this.show = false;
					}
				}
			}

			private bool GetAnyMouseButtonDown()
			{
				return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
			}

			public readonly int WINDOW_ID;

			[CompilerGenerated]
			private Rect Rect;

			[CompilerGenerated]
			private float FMargin;

			[CompilerGenerated]
			private float FRightPos;

			[CompilerGenerated]
			private float FUpPos;

			[CompilerGenerated]
			private bool Show;

			[CompilerGenerated]
			private Action<Color32, MaidParts.PartsColor> Func;

			[CompilerGenerated]
			private GUIStyle GsLabel;

			[CompilerGenerated]
			private GUIStyle GsButton;

			[CompilerGenerated]
			private Texture2D Texture;

			[CompilerGenerated]
			private byte R;

			[CompilerGenerated]
			private byte G;

			[CompilerGenerated]
			private byte B;

			[CompilerGenerated]
			private byte A;

			private MaidParts.PartsColor partsColor;

			[CompilerGenerated]
			private ColorPickerType Type;
		}
	}
}
