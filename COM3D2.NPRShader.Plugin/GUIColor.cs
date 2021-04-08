using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class GUIColor : IDisposable
	{
		public GUIColor(Color backgroundColor, Color contentColor)
		{
			try
			{
				this.oldBackgroundColor = GUI.backgroundColor;
				this.oldcontentColor = GUI.contentColor;
				GUI.backgroundColor = backgroundColor;
				GUI.contentColor = contentColor;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void Dispose()
		{
			try
			{
				GUI.backgroundColor = this.oldBackgroundColor;
				GUI.contentColor = this.oldcontentColor;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private Color oldBackgroundColor;

		private Color oldcontentColor;
	}
}
