using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomCubemapPicker : ControlBase
	{
		public CustomCubemapPicker(Cubemap texture, string filename, List<string> imageDirectories)
		{
			try
			{
				this.imageDirectories = imageDirectories;
				this._value = texture;
				if (this._value == null)
				{
					this._value = new Cubemap(4, TextureFormat.RGBA32, true);
				}
				this._filename = filename;
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
				GUI.DrawTexture(position, this._value);
				GUI.Label(position2, this.Text, guistyle);
				if (GUI.Button(position, string.Empty, guistyle))
				{
					GlobalCubemapPicker.Set(new Vector2(this.Left + base.ScreenPos.x, this.Top + base.ScreenPos.y), (float)(this.FontSize * 40), this.FontSize, this.imageDirectories, delegate(Cubemap x, string y)
					{
						this._value = x;
						this._filename = y;
						this.TextureChanged(this, new EventArgs());
					});
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public virtual Cubemap Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		public virtual string Filename
		{
			get
			{
				return this._filename;
			}
			set
			{
				this._filename = value;
			}
		}

		[CompilerGenerated]
		private void <OnGUI>b__1_0(Cubemap x, string y)
		{
			this._value = x;
			this._filename = y;
			this.TextureChanged(this, new EventArgs());
		}

		private Cubemap _value;

		private string _filename;

		private List<string> imageDirectories;

		public EventHandler TextureChanged = delegate(object <p0>, EventArgs <p1>)
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

			public static readonly CustomCubemapPicker.<>c <>9 = new CustomCubemapPicker.<>c();

			public static EventHandler <>9__0_0;
		}
	}
}
