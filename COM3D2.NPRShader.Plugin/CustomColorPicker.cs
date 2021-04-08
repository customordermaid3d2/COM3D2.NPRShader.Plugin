using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomColorPicker : ControlBase
	{
		public CustomColorPicker(Color color)
		{
			try
			{
				this.BackgroundColor = Color.clear;
				this._value = color;
				this._valuebackup = color;
				this.IsRGBA = true;
				this._colorTex = new Texture2D(1, 1);
				this._colorTex.SetPixel(0, 0, color);
				this._colorTex.Apply();
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
				Rect position = new Rect(this.Left, this.Top, this.Height, this.Height);
				Rect position2 = new Rect(this.Left + this.Height, this.Top, this.Width - this.Height, this.Height);
				GUIStyle guistyle = new GUIStyle("label");
				guistyle.alignment = TextAnchor.MiddleLeft;
				guistyle.normal.textColor = this.TextColor;
				guistyle.fontSize = this.FixedFontSize;
				GUI.DrawTexture(position, this._colorTex);
				GUI.Label(position2, this.Text, guistyle);
				if (GUI.Button(position, string.Empty, guistyle))
				{
					GlobalColorPicker.Set(new Vector2(this.Left + base.ScreenPos.x, this.Top + base.ScreenPos.y), (float)(this.FontSize * 15), this.FontSize, this._value, this.IsRGBA ? ColorPickerType.RGBA : ColorPickerType.RGB, delegate(Color32 x, MaidParts.PartsColor y)
					{
						this.Value = x;
						this.ColorChanged(this, new EventArgs());
					});
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void Reset()
		{
			this.Value = this.ValueBackup;
			if (this.Enabled)
			{
				this.ColorChanged(this, new EventArgs());
			}
		}

		public virtual Color Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				this._colorTex.SetPixel(0, 0, value);
				this._colorTex.Apply();
			}
		}

		public virtual Color ValueBackup
		{
			get
			{
				return this._valuebackup;
			}
			set
			{
				this._valuebackup = value;
			}
		}

		public bool IsRGBA
		{
			[CompilerGenerated]
			get
			{
				return this.<IsRGBA>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<IsRGBA>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private void <OnGUI>b__1_0(Color32 x, MaidParts.PartsColor y)
		{
			this.Value = x;
			this.ColorChanged(this, new EventArgs());
		}

		private Color _value;

		private Color _valuebackup;

		[CompilerGenerated]
		private bool <IsRGBA>k__BackingField;

		private Texture2D _colorTex;

		public EventHandler ColorChanged = delegate(object <p0>, EventArgs <p1>)
		{
		};

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			public <>c()
			{
			}

			internal void <.ctor>b__0_0(object <p0>, EventArgs <p1>)
			{
			}

			public static readonly CustomColorPicker.<>c <>9 = new CustomColorPicker.<>c();

			public static EventHandler <>9__0_0;
		}
	}
}
