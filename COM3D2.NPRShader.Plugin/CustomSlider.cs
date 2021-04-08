using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomSlider : ControlBase
	{
		public CustomSlider()
		{
			this.BackgroundColor = Color.white;
		}

		public CustomSlider(float value, float min, float max, int dec)
		{
			this._value = value;
			this._defaultValue = value;
			this.fieldValue = value.ToString();
			this._min = min;
			this._max = max;
			this._decimal = dec;
			this.BackgroundColor = Color.white;
		}

		public override void OnGUI()
		{
			try
			{
				GUIStyle guistyle = new GUIStyle("label");
				guistyle.alignment = TextAnchor.MiddleLeft;
				guistyle.normal.textColor = this.TextColor;
				guistyle.fontSize = this.FixedFontSize;
				Rect position2;
				if (this.Text != string.Empty)
				{
					Rect position = new Rect(this.Left, this.Top, this.Width, this.Height - (float)this.FontSize - (float)ControlBase.FixedMargin);
					position2 = new Rect(this.Left, this.Top + this.Height - (float)this.FontSize - (float)ControlBase.FixedMargin, this.Width - (float)(this.FontSize * 6), (float)(this.FontSize + ControlBase.FixedMargin));
					GUI.Label(position, this.Text, guistyle);
				}
				else
				{
					position2 = new Rect(this.Left, this.Top, this.Width, this.Height);
				}
				float num = 0f;
				using (new GUIColor(this.BackgroundColor, this.TextColor))
				{
					num = GUI.HorizontalSlider(position2, this._value, this._min, this._max);
				}
				if (Convert.ToInt32((double)num * Math.Pow(10.0, (double)this._decimal)) != Convert.ToInt32((double)this._value * Math.Pow(10.0, (double)this._decimal)))
				{
					string format = "{0:f" + this._decimal.ToString() + "}";
					this.Value = (float)Convert.ToDouble(string.Format(format, num));
					this.fieldValue = this._value.ToString();
				}
				GUIStyle guistyle2 = new GUIStyle("textarea");
				guistyle2.alignment = TextAnchor.MiddleLeft;
				guistyle2.fontSize = this.FixedFontSize + 1;
				Rect rect = new Rect(position2.x + position2.width + (float)ControlBase.FixedMargin, position2.y, (float)(this.FontSize * 8), position2.height);
				string text = Util.DrawTextFieldF(rect, this.fieldValue, guistyle2);
				if (text != this.fieldValue)
				{
					this.fieldValue = text;
					float value;
					if (float.TryParse(text, out value))
					{
						this.Value = value;
					}
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
				this.ValueChanged(this, new EventArgs());
			}
		}

		public float Value
		{
			get
			{
				return this._value;
			}
			set
			{
				if (this._min <= value && value <= this._max)
				{
					this._value = value;
					string format = "f" + this._decimal.ToString();
					if (this._decimal == 0)
					{
						format = "G";
					}
					this.fieldValue = value.ToString(format);
					if (this.Enabled)
					{
						this.ValueChanged(this, new EventArgs());
					}
				}
			}
		}

		public float DefaultValue
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

		public float Min
		{
			get
			{
				return this._min;
			}
			set
			{
				this._min = value;
			}
		}

		public float Max
		{
			get
			{
				return this._max;
			}
			set
			{
				this._max = value;
			}
		}

		public int Decimal
		{
			get
			{
				return this._decimal;
			}
			set
			{
				this._decimal = value;
			}
		}

		private float _value;

		private float _defaultValue;

		private float _min;

		private float _max;

		private int _decimal;

		private string fieldValue;

		public EventHandler ValueChanged;/*= delegate(object <p0>, EventArgs <p1>)
		{
		};*/
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

			internal void <.ctor>b__1_0(object <p0>, EventArgs <p1>)
			{
			}

			public static readonly CustomSlider.<>c <>9 = new CustomSlider.<>c();

			public static EventHandler <>9__0_0;

			public static EventHandler <>9__1_0;
		}
		*/
	}
}
