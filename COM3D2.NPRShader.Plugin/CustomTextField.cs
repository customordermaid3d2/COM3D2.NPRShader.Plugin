using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomTextField : ControlBase
	{
		public CustomTextField()
		{
			try
			{
				this.BackgroundColor = Color.clear;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public CustomTextField(string text)
		{
			try
			{
				this.BackgroundColor = Color.clear;
				this._value = text;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void OnGUI()
		{
			Rect position = new Rect(this.Left, this.Top, this.Width / 5f, this.Height);
			Rect position2;
			if (this.Text != string.Empty)
			{
				position2 = new Rect(position.x + this.Width / 5f, this.Top, this.Width / 5f * 4f, this.Height);
			}
			else
			{
				position2 = new Rect(this.Left, this.Top, this.Width, this.Height);
			}
			GUIStyle guistyle = new GUIStyle("label");
			guistyle.alignment = TextAnchor.MiddleLeft;
			guistyle.normal.textColor = this.TextColor;
			guistyle.fontSize = this.FixedFontSize;
			GUIStyle guistyle2 = new GUIStyle("textarea");
			guistyle2.alignment = TextAnchor.UpperLeft;
			guistyle2.fontSize = this.FixedFontSize;
			if (this.Text != string.Empty)
			{
				GUI.Label(position, this.Text, guistyle);
			}
			string text = GUI.TextField(position2, this.Value, guistyle2);
			if (text != this.Value)
			{
				this.ValueChanged(this, new EventArgs());
				this.Value = text;
			}
		}

		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
				if (this.Enabled)
				{
					this.ValueChanged(this, new EventArgs());
				}
			}
		}

		private string _value = "";

		public EventHandler ValueChanged;/* = delegate(object <p0>, EventArgs <p1>)
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

			internal void <.ctor>b__1_0(object <p0>, EventArgs <p1>)
			{
			}

			public static readonly CustomTextField.<>c <>9 = new CustomTextField.<>c();

			public static EventHandler <>9__0_0;

			public static EventHandler <>9__1_0;
		}
			*/
	}
}
