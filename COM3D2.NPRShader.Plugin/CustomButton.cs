using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomButton : ControlBase
	{
		public CustomButton()
		{
			this.TextColor = Color.white;
		}

		public override void OnGUI()
		{
			try
			{
				Rect position = new Rect(this.Left, this.Top, this.Width, this.Height);
				GUIStyle guistyle = new GUIStyle("button");
				guistyle.alignment = TextAnchor.MiddleCenter;
				guistyle.fontSize = this.FixedFontSize;
				if (!this.Enabled)
				{
					//guistyle.onHover.background = (guistyle.hover.background = new Texture2D(2, 2));
					guistyle.normal.background = (guistyle.onNormal.background = (guistyle.hover.background = (guistyle.onHover.background = (guistyle.active.background = (guistyle.onActive.background = (guistyle.focused.background = (guistyle.onFocused.background = new Texture2D(2, 2))))))));
					guistyle.normal.textColor = (guistyle.onNormal.textColor = (guistyle.hover.textColor = (guistyle.onHover.textColor = (guistyle.active.textColor = (guistyle.onActive.textColor = (guistyle.focused.textColor = (guistyle.onFocused.textColor = Color.gray)))))));
				}
				if (GUI.Button(position, this.Text, guistyle) && this.Enabled)
				{
					this.Click(this, new EventArgs());
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public EventHandler Click;/* = delegate(object <p0>, EventArgs <p1>)
		{
		};
		*/
		/*
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

			public static readonly CustomButton.<>c <>9 = new CustomButton.<>c();

			public static EventHandler <>9__0_0;
		}
		*/
	}
}
