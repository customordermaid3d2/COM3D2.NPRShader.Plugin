using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class CustomComboBox : ControlBase
	{
		public CustomComboBox()
		{
			try
			{
				this._id = CustomComboBox.NewWindowID;
				CustomComboBox.NewWindowID++;
				CustomComboBox.allComboBoxes[this._id] = this;
				this._items = new List<GUIContent>();
				this.Awake();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public CustomComboBox(List<GUIContent> items)
		{
			try
			{
				this._id = CustomComboBox.NewWindowID;
				CustomComboBox.NewWindowID++;
				CustomComboBox.allComboBoxes[this._id] = this;
				this._items = items;
				this.Awake();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public CustomComboBox(string[] items)
		{
			try
			{
				this._id = CustomComboBox.NewWindowID;
				CustomComboBox.NewWindowID++;
				CustomComboBox.allComboBoxes[this._id] = this;
				List<string> source = new List<string>(items);
				this._items = (from x in source
				select new GUIContent(x)).ToList<GUIContent>();
				this.Awake();
			}
			catch (Exception ex)
			{
				Debug.LogError("CustomComboBox()"+ex.ToString());
			}
		}

		static bool isSetup=false;

		public void Setup()
		{
            if (isSetup)
            {
				isSetup =true;
				return;
            }
			try
			{
				this.comboButtonStyle = new GUIStyle("button");
				this.comboButtonStyle.alignment = TextAnchor.MiddleLeft;
				this.comboButtonStyle.normal.textColor = (this.comboButtonStyle.onNormal.textColor = (this.comboButtonStyle.hover.textColor = (this.comboButtonStyle.onHover.textColor = (this.comboButtonStyle.active.textColor = (this.comboButtonStyle.onActive.textColor = (this.comboButtonStyle.focused.textColor = (this.comboButtonStyle.onFocused.textColor = Color.white)))))));
				this.comboBoxStyle = new GUIStyle("box");
				this.comboBoxStyle.normal.textColor = (this.comboBoxStyle.onNormal.textColor = (this.comboBoxStyle.hover.textColor = (this.comboBoxStyle.onHover.textColor = (this.comboBoxStyle.active.textColor = (this.comboBoxStyle.onActive.textColor = (this.comboBoxStyle.focused.textColor = (this.comboBoxStyle.onFocused.textColor = Color.white)))))));
				this.comboListStyle = new GUIStyle("box");
				this.comboListStyle.onHover.background = (this.comboListStyle.hover.background = new Texture2D(2, 2));
				this.comboListStyle.padding.left = (this.comboListStyle.padding.right = (this.comboListStyle.padding.top = (this.comboListStyle.padding.bottom = 4)));
				this.comboListStyle.normal.textColor = (this.comboListStyle.onNormal.textColor = (this.comboListStyle.hover.textColor = (this.comboListStyle.onHover.textColor = (this.comboListStyle.active.textColor = (this.comboListStyle.onActive.textColor = (this.comboListStyle.focused.textColor = (this.comboListStyle.onFocused.textColor = Color.white)))))));
				this.labelStyle = new GUIStyle("label");
				this.labelStyle.alignment = TextAnchor.MiddleLeft;
				this.labelStyle.fontSize = this.FixedFontSize;
			}
			catch (Exception ex)
			{
				Debug.LogError("CustomComboBox.Awake() : " + ex.ToString());
			}
		}

		public override void OnGUI()
		{
			Setup();
			try
			{
				if (this._isShowDropDownList && !GlobalComboBox.Visible)
				{
					this._isShowDropDownList = false;
				}
				this.comboButtonStyle.fontSize = this.FixedFontSize;
				this.comboBoxStyle.fontSize = this.FixedFontSize;
				this.comboListStyle.fontSize = this.FixedFontSize;
				this.labelStyle.fontSize = this.FixedFontSize;
				this.comboBoxButton = null;
				if (this._items != null && 0 <= this._selectedIndex && this._selectedIndex < this.Count)
				{
					this.comboBoxButton = this._items[this._selectedIndex];
				}
				else
				{
					this.comboBoxButton = new GUIContent(string.Empty);
				}
				Rect position = new Rect(this.Left, this.Top, this.Width / 3f, this.Height);
				GUI.Label(position, this.Text, this.labelStyle);
				Rect position2 = new Rect(this.Left + this.Width / 3f, this.Top, this.Width / 3f * 2f, this.Height);
				if (GUI.Button(position2, this.comboBoxButton, this.comboButtonStyle) && this._items != null)
				{
					this._isShowDropDownList = !this._isShowDropDownList;
					CustomComboBox.CloseAllDropDownList(this._id);
				}
				if (this._items != null && this._isShowDropDownList)
				{
					float num = (float)(Screen.height - Screen.height / 4) - base.ScreenPos.y;
					float num2 = this.comboListStyle.CalcHeight(this.comboBoxButton, 1f) * (float)this._items.Count;
					float num3 = this.comboListStyle.CalcHeight(this.comboBoxButton, 1f);
					float height = (num < num2) ? num : num2;
					this.dropDownListRect = new Rect(position2.x + base.ScreenPos.x, this.Top + base.ScreenPos.y + base.ScreenPos.height, position2.width, height);
					GUIStyle guistyle = new GUIStyle("box");
					Rect rect = new Rect(this.dropDownListRect.x, this.Top + base.ScreenPos.y + 36f, this.dropDownListRect.width, num2);
					GUIUtil.MouseClickOnGUIRect(rect);
					GUIContent[] array = new GUIContent[this._items.Count];
					this._items.CopyTo(array);
					GlobalComboBox.Set(rect, array, this.FontSize, new Action<int>(this.pick));
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void pick(int newSelectedItemIndex)
		{
			if (newSelectedItemIndex != this._selectedIndex)
			{
				this.SelectedIndex = newSelectedItemIndex;
				this._isShowDropDownList = false;
			}
		}

		public void SetItems(List<string> itemList)
		{
			try
			{
				this._items = (from x in itemList
				select new GUIContent(x)).ToList<GUIContent>();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void Reset()
		{
			this.SelectedIndex = this.DefaultIndex;
			if (this.Enabled)
			{
				this.SelectedIndexChanged(this, new EventArgs());
			}
		}

		private void OnGUI_DropDownList(int windowID)
		{
			try
			{
				float num = this.comboListStyle.CalcHeight(this.comboBoxButton, 1f) * (float)this._items.Count;
				Rect position = new Rect(base.ScreenPos.x, base.ScreenPos.y, this.dropDownListRect.width, this.dropDownListRect.height);
				GUIContent[] array = new GUIContent[this._items.Count];
				this._items.CopyTo(array);
				int num2 = GUI.SelectionGrid(position, this._selectedIndex, array, 1, this.comboListStyle);
				if (num2 != this._selectedIndex)
				{
					this.SelectedIndex = num2;
					this._isShowDropDownList = false;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private bool GetAnyMouseButtonDown()
		{
			return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
		}

		public int Next()
		{
			try
			{
				if (this._items != null && 0 < this._items.Count)
				{
					if (this._selectedIndex + 1 < this._items.Count)
					{
						int selectedIndex = this.SelectedIndex;
						this.SelectedIndex = selectedIndex + 1;
					}
					else
					{
						this.SelectedIndex = 0;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
			return this._selectedIndex;
		}

		public int Prev()
		{
			try
			{
				if (this._items != null && 0 < this._items.Count)
				{
					if (0 <= this._selectedIndex - 1)
					{
						int selectedIndex = this.SelectedIndex;
						this.SelectedIndex = selectedIndex - 1;
					}
					else
					{
						this.SelectedIndex = this._items.Count - 1;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
			return this._selectedIndex;
		}

		public static void CloseAllDropDownList()
		{
			try
			{
				CustomComboBox.CloseAllDropDownList(-1);
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public static void CloseAllDropDownList(int ignoreID)
		{
			try
			{
				foreach (CustomComboBox customComboBox in CustomComboBox.allComboBoxes.Values)
				{
					if (customComboBox.ID != ignoreID)
					{
						customComboBox.IsShowDropDownList = false;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public int ID
		{
			get
			{
				return this._id;
			}
		}

		public List<GUIContent> Items
		{
			get
			{
				return this._items;
			}
			set
			{
				this._items = value;
			}
		}

		public int SelectedIndex
		{
			get
			{
				return this._selectedIndex;
			}
			set
			{
				this._selectedIndex = value;
				if (this.Enabled)
				{
					this.SelectedIndexChanged(this, new EventArgs());
				}
			}
		}

		public int DefaultIndex
		{
			get
			{
				return this._defaultIndex;
			}
			set
			{
				this._defaultIndex = value;
			}
		}

		public string SelectedItem
		{
			get
			{
				string result = string.Empty;
				if (0 <= this._selectedIndex && this._selectedIndex < this._items.Count)
				{
					result = this._items[this._selectedIndex].text;
				}
				return result;
			}
			set
			{
				int num = this._items.FindIndex((GUIContent item) => item.text == value);
				if (0 <= num)
				{
					this.SelectedIndex = num;
				}
			}
		}

		public int Count
		{
			get
			{
				if (this._items != null)
				{
					return this._items.Count;
				}
				return 0;
			}
		}

		public bool IsShowDropDownList
		{
			get
			{
				return this._isShowDropDownList;
			}
			set
			{
				this._isShowDropDownList = value;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static CustomComboBox()
		{
		}

		private int _id = 32;

		private List<GUIContent> _items;

		private int _selectedIndex = -1;

		private int _defaultIndex = -1;

		private bool _isShowDropDownList;

		private static Dictionary<int, CustomComboBox> allComboBoxes = new Dictionary<int, CustomComboBox>();

		private static int NewWindowID = 75;

		private Rect dropDownListRect;

		private GUIContent comboBoxButton;

		private GUIStyle comboButtonStyle;

		private GUIStyle comboBoxStyle;

		private GUIStyle comboListStyle;

		private GUIStyle labelStyle;

		private Vector2 scrollViewVector = Vector2.zero;

		public EventHandler SelectedIndexChanged;/*= delegate(object <p0>, EventArgs <p1>)
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

			internal void <.ctor>b__1_0(object <p0>, EventArgs <p1>)
			{
			}

			internal GUIContent <.ctor>b__2_0(string x)
			{
				return new GUIContent(x);
			}

			internal void <.ctor>b__2_1(object <p0>, EventArgs <p1>)
			{
			}

			internal GUIContent <SetItems>b__6_0(string x)
			{
				return new GUIContent(x);
			}

			public static readonly CustomComboBox.<>c <>9 = new CustomComboBox.<>c();

			public static EventHandler <>9__0_0;

			public static EventHandler <>9__1_0;

			public static Func<string, GUIContent> <>9__2_0;

			public static EventHandler <>9__2_1;

			public static Func<string, GUIContent> <>9__6_0;
		}
		*/
		/*
		[CompilerGenerated]
		private sealed class <>c__DisplayClass31_0
		{
			public <>c__DisplayClass31_0()
			{
			}

			internal bool <set_SelectedItem>b__0(GUIContent item)
			{
				return item.text == this.value;
			}

			public string value;
		}
		*/
	}
}
