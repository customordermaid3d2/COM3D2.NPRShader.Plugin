using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	public static class GlobalComboBox
	{
		static GlobalComboBox()
		{
			GlobalComboBox.gsWin = new GUIStyle("box");
			GlobalComboBox.gsWin.fontSize = Util.GetPix(12);
			GlobalComboBox.gsWin.alignment = TextAnchor.UpperRight;
		}

		public static void Update()
		{
			if (GlobalComboBox.combo.show)
			{
				GlobalComboBox.combo.rect = GUI.Window(GlobalComboBox.combo.WINDOW_ID, GlobalComboBox.combo.rect, new GUI.WindowFunction(GlobalComboBox.combo.GuiFunc), string.Empty, GlobalComboBox.gsWin);
			}
		}

		public static bool Visible
		{
			get
			{
				return GlobalComboBox.combo.show;
			}
		}

		public static void Set(Rect r, GUIContent[] s, int i, Action<int> f)
		{
			GlobalComboBox.combo.Set(r, s, i, f);
		}

		private static GUIStyle gsWin;

		private static GlobalComboBox.ComboBox combo = new GlobalComboBox.ComboBox(200);

		internal class ComboBox
		{
			public Rect rect
			{
				[CompilerGenerated]
				get
				{
					return this.<rect>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<rect>k__BackingField = value;
				}
			}

			private Rect rectItem
			{
				[CompilerGenerated]
				get
				{
					return this.<rectItem>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<rectItem>k__BackingField = value;
				}
			}

			public bool show
			{
				[CompilerGenerated]
				get
				{
					return this.<show>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<show>k__BackingField = value;
				}
			}

			private GUIContent[] sItems
			{
				[CompilerGenerated]
				get
				{
					return this.<sItems>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<sItems>k__BackingField = value;
				}
			}

			private GUIStyle gsSelectionGrid
			{
				[CompilerGenerated]
				get
				{
					return this.<gsSelectionGrid>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<gsSelectionGrid>k__BackingField = value;
				}
			}

			private GUIStyleState gssBlack
			{
				[CompilerGenerated]
				get
				{
					return this.<gssBlack>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<gssBlack>k__BackingField = value;
				}
			}

			private GUIStyleState gssWhite
			{
				[CompilerGenerated]
				get
				{
					return this.<gssWhite>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<gssWhite>k__BackingField = value;
				}
			}

			public Action<int> func
			{
				[CompilerGenerated]
				get
				{
					return this.Func;
				}
				[CompilerGenerated]
				private set
				{
					this.Func = value;
				}
			}

			public Rect ScreenPos
			{
				[CompilerGenerated]
				get
				{
					return this.screenPos;
				}
				[CompilerGenerated]
				set
				{
					this.screenPos = value;
				}
			}

			private float guiScrollHeight
			{
				[CompilerGenerated]
				get
				{
					return this.GuiScrollHeight;
				}
				[CompilerGenerated]
				set
				{
					this.GuiScrollHeight = value;
				}
			}

			public ComboBox(int iWIndowID)
			{
				this.WINDOW_ID = iWIndowID;
			}

			public void Set(Rect r, GUIContent[] s, int i, Action<int> f)
			{
				this.rect = r;
				this.sItems = s;
				this.gsSelectionGrid = new GUIStyle();
				this.gsSelectionGrid.fontSize = i;
				this.gssBlack = new GUIStyleState();
				this.gssBlack.textColor = Color.white;
				this.gssBlack.background = Texture2D.blackTexture;
				this.gssWhite = new GUIStyleState();
				this.gssWhite.textColor = Color.black;
				this.gssWhite.background = Texture2D.whiteTexture;
				this.gsSelectionGrid.normal = this.gssBlack;
				this.gsSelectionGrid.hover = this.gssWhite;
				this.rectItem = new Rect(0f, 0f, this.rect.width, this.rect.height);
				this.func = f;
				this.show = true;
			}

			public void GuiFunc(int winId)
			{
				float num = (float)(Screen.height - Screen.height / 4) - this.ScreenPos.y - 36f;
				float height = (num < this.rect.height) ? num : this.rect.height;
				Rect position = new Rect(0f, 0f, this.rectItem.width, height);
				Rect viewRect = new Rect(0f, 0f, this.rectItem.width - 20f, this.guiScrollHeight);
				this.scrollViewVector = GUI.BeginScrollView(position, this.scrollViewVector, viewRect);
				int num2 = GUI.SelectionGrid(this.rectItem, -1, this.sItems, 1, this.gsSelectionGrid);
				if (num2 >= 0)
				{
					this.func(num2);
					this.show = false;
				}
				GUI.EndScrollView();
				this.guiScrollHeight = this.rect.height;
				if (this.GetAnyMouseButtonDown())
				{
					Vector2 point = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
					if (!this.rect.Contains(point))
					{
						this.show = false;
					}
				}
				GUI.DragWindow();
			}

			private bool GetAnyMouseButtonDown()
			{
				return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
			}

			public readonly int WINDOW_ID;

			[CompilerGenerated]
			private Rect <rect>k__BackingField;

			[CompilerGenerated]
			private Rect <rectItem>k__BackingField;

			[CompilerGenerated]
			private bool <show>k__BackingField;

			[CompilerGenerated]
			private GUIContent[] <sItems>k__BackingField;

			[CompilerGenerated]
			private GUIStyle <gsSelectionGrid>k__BackingField;

			[CompilerGenerated]
			private GUIStyleState <gssBlack>k__BackingField;

			[CompilerGenerated]
			private GUIStyleState <gssWhite>k__BackingField;

			[CompilerGenerated]
			private Action<int> Func;

			private Vector2 scrollViewVector = Vector2.zero;

			[CompilerGenerated]
			private Rect screenPos;

			[CompilerGenerated]
			private float GuiScrollHeight;
		}
	}
}
