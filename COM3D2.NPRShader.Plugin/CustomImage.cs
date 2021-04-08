using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomImage : ControlBase
	{
		public CustomImage(Texture2D tex)
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

		public override void OnGUI()
		{
			Rect position = new Rect(this.Left, this.Top, this.Width, this.Height);
			if (this._texture != null)
			{
				GUI.DrawTexture(position, this._texture);
			}
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
	}
}
