using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomTextureButton : ControlBase
	{
		public CustomTextureButton(Texture2D tex)
		{
			try
			{
				this.Texture = tex;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public CustomTextureButton(Texture2D tex, bool center)
		{
			try
			{
				this.Texture = tex;
				this.isAlignmentCenter = center;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void OnGUI()
		{
			Rect position = new Rect(this.Left, this.Top, this.Width, this.Height);
			GUIStyle guistyle = new GUIStyle("button");
			guistyle.alignment = (this.isAlignmentCenter ? TextAnchor.MiddleCenter : TextAnchor.MiddleLeft);
			guistyle.fontSize = this.FixedFontSize;
			if (!this.Enabled)
			{
				guistyle.normal.background = (guistyle.onNormal.background = (guistyle.hover.background = (guistyle.onHover.background = (guistyle.active.background = (guistyle.onActive.background = (guistyle.focused.background = (guistyle.onFocused.background = new Texture2D(2, 2))))))));
				guistyle.normal.textColor = (guistyle.onNormal.textColor = (guistyle.hover.textColor = (guistyle.onHover.textColor = (guistyle.active.textColor = (guistyle.onActive.textColor = (guistyle.focused.textColor = (guistyle.onFocused.textColor = Color.gray)))))));
			}
			if (GUI.Button(position, this.Text, guistyle) && this.Enabled)
			{
				this.Click(this, new EventArgs());
			}
			if (this._texture != null)
			{
				GUI.DrawTexture(position, this._texture);
				return;
			}
			GUI.Label(position, this.Text);
		}

		public Texture2D Texture
		{
			get
			{
				return this._texture;
			}
			set
			{
				this._texture = value;
			}
		}

		private Texture2D _texture;

		public EventHandler Click = delegate(object <p0>, EventArgs <p1>)
		{
		};

		private bool isAlignmentCenter = true;

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

			internal void <.ctor>b__1_0(object <p0>, EventArgs <p1>)
			{
			}

			public static readonly CustomTextureButton.<>c <>9 = new CustomTextureButton.<>c();

			public static EventHandler <>9__0_0;

			public static EventHandler <>9__1_0;
		}
	}
}
