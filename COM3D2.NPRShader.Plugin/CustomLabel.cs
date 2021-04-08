using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomLabel : ControlBase
	{
		public CustomLabel()
		{
			try
			{
				this.TextColor = new Color(255f, 255f, 255f, 255f);
				this.BackgroundColor = Color.clear;
				this.labelStyle = new GUIStyle("label");
				this.labelStyle.alignment = TextAnchor.MiddleLeft;
				this.labelStyle.normal.textColor = this.TextColor;
				this.labelStyle.fontSize = this.FixedFontSize;
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
				Rect position = new Rect(this.Left, this.Top, this.Width, this.Height);
				this.labelStyle.normal.textColor = this.TextColor;
				this.labelStyle.fontSize = this.FixedFontSize;
				GUI.Label(position, this.Text, this.labelStyle);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private GUIStyle labelStyle;
	}
}
