using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class ModeSelectWindow : ControlBase
	{
		public ModeSelectWindow(int fontSize)
		{
			try
			{
				this.FontSize = fontSize;
				this.Awake();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void Awake()
		{
			try
			{
				this.windowStyle = new GUIStyle("box");
				this.windowStyle.alignment = TextAnchor.UpperLeft;
				this.windowStyle.normal.textColor = (this.windowStyle.onNormal.textColor = (this.windowStyle.hover.textColor = (this.windowStyle.onHover.textColor = (this.windowStyle.active.textColor = (this.windowStyle.onActive.textColor = (this.windowStyle.focused.textColor = (this.windowStyle.onFocused.textColor = Color.white)))))));
				this.maidSettingButton = new CustomToggleButton(false);
				this.maidSettingButton.FontSize = this.FontSize;
				this.maidSettingButton.Text = "メイド";
				CustomToggleButton customToggleButton = this.maidSettingButton;
				customToggleButton.CheckedChanged = (EventHandler)Delegate.Combine(customToggleButton.CheckedChanged, new EventHandler(this.MaidSettingButton_CheckedChanged));
				this.objSettingButton = new CustomToggleButton(false);
				this.objSettingButton.FontSize = this.FontSize;
				this.objSettingButton.Text = "オブジェクト";
				CustomToggleButton customToggleButton2 = this.objSettingButton;
				customToggleButton2.CheckedChanged = (EventHandler)Delegate.Combine(customToggleButton2.CheckedChanged, new EventHandler(this.ObjSettingButton_CheckedChanged));
				this.envSettingButton = new CustomToggleButton(false);
				this.envSettingButton.FontSize = this.FontSize;
				this.envSettingButton.Text = "環境";
				CustomToggleButton customToggleButton3 = this.envSettingButton;
				customToggleButton3.CheckedChanged = (EventHandler)Delegate.Combine(customToggleButton3.CheckedChanged, new EventHandler(this.EnvSettingButton_CheckedChanged));
				CustomToggleButton.SetPairButton(this.maidSettingButton, this.objSettingButton);
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
				this.Height = this.ControlHeight * 2f + (float)(ControlBase.FixedMargin * 3);
				this.windowRect = new Rect(this.Left, this.Top, this.Width, this.Height);
				this.windowStyle.fontSize = this.FixedFontSize;
				GUI.Box(this.windowRect, this.Text, this.windowStyle);
				this.maidSettingButton.Left = this.Left + (float)ControlBase.FixedMargin;
				this.maidSettingButton.Top = this.Top + this.ControlHeight + (float)(ControlBase.FixedMargin * 2);
				this.maidSettingButton.Width = this.Width / 3f - (float)(ControlBase.FixedMargin * 2);
				this.maidSettingButton.Height = this.ControlHeight;
				if (this.IsEnableMaidSettingButton)
				{
					this.maidSettingButton.OnGUI();
				}
				this.objSettingButton.Left = this.maidSettingButton.Left + this.maidSettingButton.Width + (float)(ControlBase.FixedMargin * 2);
				this.objSettingButton.Top = this.Top + this.ControlHeight + (float)(ControlBase.FixedMargin * 2);
				this.objSettingButton.Width = this.Width / 3f - (float)(ControlBase.FixedMargin * 2);
				this.objSettingButton.Height = this.ControlHeight;
				if (this.IsEnableObjSettingButton)
				{
					this.objSettingButton.OnGUI();
				}
				this.envSettingButton.Left = this.objSettingButton.Left + this.objSettingButton.Width + (float)(ControlBase.FixedMargin * 2);
				this.envSettingButton.Top = this.Top + this.ControlHeight + (float)(ControlBase.FixedMargin * 2);
				this.envSettingButton.Width = this.Width / 3f - (float)(ControlBase.FixedMargin * 2);
				this.envSettingButton.Height = this.ControlHeight;
				if (this.IsEnableEnvSettingButton)
				{
					this.envSettingButton.OnGUI();
				}
				Vector2 vector = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
				bool flag = Input.GetAxis("Mouse ScrollWheel") != 0f;
				for (int i = 0; i < 3; i++)
				{
					flag |= Input.GetMouseButtonDown(i);
				}
				if (this.WindowRect.Contains(vector) && flag)
				{
					GameMain.Instance.MainCamera.SetControl(false);
					UICamera.InputEnable = false;
				}
				if (this.WindowRect.Contains(vector) && Input.GetMouseButtonDown(0))
				{
					this.dragging = true;
					this.dragStartPos = vector;
					this.dragStartWindowPos = new Vector2(this.Left, this.Top);
				}
				if (this.WindowRect.Contains(vector) && Input.GetAxis("Mouse ScrollWheel") != 0f)
				{
					Input.ResetInputAxes();
				}
				if (this.WindowRect.Contains(vector) && Input.GetMouseButtonUp(0))
				{
					this.dragging = false;
					GameMain.Instance.MainCamera.SetControl(true);
					UICamera.InputEnable = true;
				}
				if (this.dragging)
				{
					Vector2 vector2 = this.dragStartWindowPos - (this.dragStartPos - vector);
					this.Left = vector2.x;
					this.Top = vector2.y;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void MaidSettingButton_CheckedChanged(object sender, EventArgs args)
		{
			try
			{
				CustomComboBox.CloseAllDropDownList();
				if (this.IsEnableMaidSettingButton && this.maidSettingButton.Value)
				{
					this.SelectedMode = ConstantValues.EditMode.Maid;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void ObjSettingButton_CheckedChanged(object sender, EventArgs args)
		{
			try
			{
				CustomComboBox.CloseAllDropDownList();
				if (this.IsEnableObjSettingButton && this.objSettingButton.Value)
				{
					this.SelectedMode = ConstantValues.EditMode.Object;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void EnvSettingButton_CheckedChanged(object sender, EventArgs args)
		{
			try
			{
				CustomComboBox.CloseAllDropDownList();
				if (this.IsEnableEnvSettingButton && this.envSettingButton.Value)
				{
					this.SelectedMode = ConstantValues.EditMode.Environment;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public ConstantValues.EditMode SelectedMode
		{
			get
			{
				return this._selectedMode;
			}
			set
			{
				if (this._selectedMode != value)
				{
					this._selectedMode = value;
					switch (this._selectedMode)
					{
					case ConstantValues.EditMode.Maid:
						this.maidSettingButton.Value = true;
						this.objSettingButton.Value = false;
						this.envSettingButton.Value = false;
						return;
					case ConstantValues.EditMode.Object:
						this.maidSettingButton.Value = false;
						this.objSettingButton.Value = true;
						this.envSettingButton.Value = false;
						return;
					case ConstantValues.EditMode.Environment:
						this.maidSettingButton.Value = false;
						this.objSettingButton.Value = false;
						this.envSettingButton.Value = true;
						return;
					}
					this.maidSettingButton.Value = false;
					this.objSettingButton.Value = false;
					this.envSettingButton.Value = false;
				}
			}
		}

		public bool IsEnableMaidSettingButton
		{
			get
			{
				return this._isEnableMaidSettingButton;
			}
			set
			{
				this._isEnableMaidSettingButton = value;
			}
		}

		public bool IsEnableObjSettingButton
		{
			get
			{
				return this._isEnableObjSettingButton;
			}
			set
			{
				this._isEnableObjSettingButton = value;
			}
		}

		public bool IsEnableEnvSettingButton
		{
			get
			{
				return this._isEnableEnvSettingButton;
			}
			set
			{
				this._isEnableEnvSettingButton = value;
			}
		}

		public bool IsEnableDataSettingButton
		{
			get
			{
				return this._isEnableDataSettingButton;
			}
			set
			{
				this._isEnableDataSettingButton = value;
			}
		}

		public float ControlHeight
		{
			get
			{
				return (float)(this.FixedFontSize + ControlBase.FixedMargin * 2);
			}
		}

		private ConstantValues.EditMode _selectedMode;

		private bool _isEnableMaidSettingButton = true;

		private bool _isEnableObjSettingButton = true;

		private bool _isEnableEnvSettingButton = true;

		private bool _isEnableDataSettingButton = true;

		private GUIStyle windowStyle;

		private CustomToggleButton maidSettingButton;

		private CustomToggleButton objSettingButton;

		private CustomToggleButton envSettingButton;

		private bool dragging;

		private Vector2 dragStartPos = Vector2.zero;

		private Vector2 dragStartWindowPos = Vector2.zero;

		public Rect windowRect;
	}
}
