using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomToggleButton : ControlBase
	{
		public CustomToggleButton(bool value)
		{
			this._value = value;
			this._style = "button";
			this.BackgroundColor = Color.gray;
			this.TextColor = new Color(192f, 192f, 192f);
		}

		public CustomToggleButton(bool value, string style)
		{
			this._value = value;
			this._defaultValue = value;
			this._style = style;
			this.BackgroundColor = Color.gray;
			this.TextColor = Color.white;
		}

		public override void OnGUI()
		{
			try
			{
				Rect position = new Rect(this.Left, this.Top, this.Width, this.Height);
				GUIStyle guistyle = new GUIStyle("label");
				guistyle.normal.textColor = this.TextColor;
				if (this._style == "button")
				{
					guistyle.alignment = TextAnchor.MiddleCenter;
				}
				else
				{
					guistyle.alignment = TextAnchor.MiddleLeft;
				}
				guistyle.fontSize = this.FixedFontSize;
				bool flag = false;
				using (new GUIColor(this.CurrentBackgroundColor, GUI.contentColor))
				{
					flag = GUI.Toggle(position, this._value, "", new GUIStyle(this._style));
				}
				if (this._style == "toggle")
				{
					position.x += 15f;
				}
				using (new GUIColor(GUI.backgroundColor, this.CurrentTextColor))
				{
					GUI.Label(position, this.Text, guistyle);
				}
				if (flag != this._value)
				{
					if (this.pair != null)
					{
						if (!flag && !this.pair.Value)
						{
							flag = true;
						}
						else if (flag && this.pair.Value)
						{
							this.pair.Value = false;
						}
					}
					this.Value = flag;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void Reset()
		{
			this.Value = this.DefaultValue;
			if (this.Enabled)
			{
				this.CheckedChanged(this, new EventArgs());
			}
		}

		public static void SetPairButton(CustomToggleButton button1, CustomToggleButton button2)
		{
			button1.pair = button2;
			button2.pair = button1;
		}

		public bool Value
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
					this.CheckedChanged(this, new EventArgs());
				}
			}
		}

		public bool DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
			set
			{
				this._defaultValue = value;
			}
		}

		public Color CurrentBackgroundColor
		{
			get
			{
				if (!this._value)
				{
					return this.BackgroundColor;
				}
				return this._selectBackgroundColor;
			}
		}

		public Color CurrentTextColor
		{
			get
			{
				if (!this._value)
				{
					return this.TextColor;
				}
				return this._selectTextColor;
			}
		}

		public Color SelectBackgroundColor
		{
			get
			{
				return this._selectBackgroundColor;
			}
			set
			{
				this._selectBackgroundColor = value;
			}
		}

		public Color SelectTextColor
		{
			get
			{
				return this._selectTextColor;
			}
			set
			{
				this._selectTextColor = value;
			}
		}

		private string _style = "button";

		private bool _value;

		private bool _defaultValue;

		private Color _selectBackgroundColor = Color.green;

		private Color _selectTextColor = Color.white;

		private CustomToggleButton pair;

		public EventHandler CheckedChanged = delegate(object <p0>, EventArgs <p1>)
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

			public static readonly CustomToggleButton.<>c <>9 = new CustomToggleButton.<>c();

			public static EventHandler <>9__0_0;

			public static EventHandler <>9__1_0;
		}
	}
}
