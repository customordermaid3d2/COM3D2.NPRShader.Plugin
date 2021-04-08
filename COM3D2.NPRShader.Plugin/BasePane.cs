using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal abstract class BasePane : ControlBase
	{
		public abstract void SetupPane();

		public abstract void ShowPane();

		public BasePane(int fontSize, string title)
		{
			try
			{
				this.FontSize = fontSize;
				this.Text = title;
				this.Awake();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void Awake()
		{
			this.enableCheckbox = new CustomToggleButton(false, "toggle");
			this.enableCheckbox.FontSize = this.FontSize;
			this.enableCheckbox.Text = this.Text;
			this.ChildControls.Add(this.enableCheckbox);
			this.resetButton = new CustomButton();
			this.resetButton.FontSize = this.FontSize;
			this.resetButton.Text = "|";
			CustomButton customButton = this.resetButton;
			customButton.Click = (EventHandler)Delegate.Combine(customButton.Click, new EventHandler(delegate(object o, EventArgs e)
			{
				this.Reset();
			}));
			this.ChildControls.Add(this.resetButton);
			try
			{
				this.SetupPane();
			}
			catch (Exception ex)
			{
				Debug.LogError("Error during SetupPane():\n" + ex.ToString());
			}
		}

		public override void OnGUI()
		{
			try
			{
				base.SetAllVisible(false, 0);
				this.enableCheckbox.Left = this.Left + (float)ControlBase.FixedMargin;
				this.enableCheckbox.Top = this.Top + (float)ControlBase.FixedMargin;
				this.enableCheckbox.Width = this.Width - (float)(ControlBase.FixedMargin * 2);
				this.enableCheckbox.Height = this.ControlHeight;
				this.Update();
				if (this.enableCheckbox.Value)
				{
					this.ShowPane();
				}
				this.Height = GUIUtil.GetHeightForParent(this);
				this.OnGUIChildControls();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public float ControlHeight
		{
			get
			{
				return (float)(this.FixedFontSize + ControlBase.FixedMargin);
			}
		}

		public bool IsEnabled
		{
			get
			{
				return this.enableCheckbox.Value;
			}
			set
			{
				this.enableCheckbox.Value = value;
			}
		}

		[CompilerGenerated]
		private void <Awake>b__3_0(object o, EventArgs e)
		{
			this.Reset();
		}

		private CustomToggleButton enableCheckbox;

		private CustomButton resetButton;
	}
}
