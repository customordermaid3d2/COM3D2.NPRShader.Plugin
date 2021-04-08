using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	public static class GlobalCubemapPicker
	{
		static GlobalCubemapPicker()
		{
			GlobalCubemapPicker.gsWin = new GUIStyle("box");
			GlobalCubemapPicker.gsWin.fontSize = Util.GetPix(12);
			GlobalCubemapPicker.gsWin.alignment = TextAnchor.UpperRight;
		}

		public static void Update()
		{
			if (GlobalCubemapPicker.texturePicker.show)
			{
				GlobalCubemapPicker.texturePicker.rect = GUI.Window(GlobalCubemapPicker.texturePicker.WINDOW_ID, GlobalCubemapPicker.texturePicker.rect, new GUI.WindowFunction(GlobalCubemapPicker.texturePicker.GuiFunc), string.Empty, GlobalCubemapPicker.gsWin);
			}
		}

		public static bool Visible
		{
			get
			{
				return GlobalCubemapPicker.texturePicker.show;
			}
		}

		public static void Set(Vector2 p, float fWidth, int iFontSize, List<string> imageDirectories, Action<Cubemap, string> f)
		{
			GlobalCubemapPicker.texturePicker.Set(p, fWidth, iFontSize, imageDirectories, f);
		}

		private static GUIStyle gsWin;

		private static GlobalCubemapPicker.TexturePickerWindow texturePicker = new GlobalCubemapPicker.TexturePickerWindow(300);

		internal class TexturePickerWindow
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

			private float fWidth
			{
				[CompilerGenerated]
				get
				{
					return this.<fWidth>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<fWidth>k__BackingField = value;
				}
			}

			private float fMargin
			{
				[CompilerGenerated]
				get
				{
					return this.<fMargin>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<fMargin>k__BackingField = value;
				}
			}

			private float fRightPos
			{
				[CompilerGenerated]
				get
				{
					return this.<fRightPos>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<fRightPos>k__BackingField = value;
				}
			}

			private float fUpPos
			{
				[CompilerGenerated]
				get
				{
					return this.<fUpPos>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<fUpPos>k__BackingField = value;
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

			public Action<Cubemap, string> func
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

			private GUIStyle gsLabel
			{
				[CompilerGenerated]
				get
				{
					return this.<gsLabel>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<gsLabel>k__BackingField = value;
				}
			}

			private GUIStyle gsButton
			{
				[CompilerGenerated]
				get
				{
					return this.<gsButton>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<gsButton>k__BackingField = value;
				}
			}

			private Vector2 scrollPosition
			{
				[CompilerGenerated]
				get
				{
					return this.<scrollPosition>k__BackingField;
				}
				[CompilerGenerated]
				set
				{
					this.<scrollPosition>k__BackingField = value;
				}
			}

			public TexturePickerWindow(int iWIndowID)
			{
				this.WINDOW_ID = iWIndowID;
				this.imageFiles = new List<GlobalCubemapPicker.TexturePickerWindow.ImageInfo>();
			}

			public void ChangeImage(GlobalCubemapPicker.TexturePickerWindow.ImageInfo info)
			{
				if (!this.imageFiles.Any<GlobalCubemapPicker.TexturePickerWindow.ImageInfo>())
				{
					return;
				}
				this.currentTexture = info.tex;
				this.func(this.currentTexture, info.abbrevPath);
			}

			private static Cubemap ReadTexture(string fullPath)
			{
				byte[] array = File.ReadAllBytes(fullPath);
				return new Cubemap(16, TextureFormat.RGBA32, true)
				{
					name = fullPath
				};
			}

			private void UpdateImages(List<string> imageDirectories)
			{
				this.imageFiles = new List<GlobalCubemapPicker.TexturePickerWindow.ImageInfo>();
				foreach (string str in imageDirectories)
				{
					string path = ConstantValues.BaseConfigDir + "\\" + str;
					DirectoryInfo directoryInfo = new DirectoryInfo(path);
					FileInfo[] array = (from p in directoryInfo.GetFiles("*.png", SearchOption.AllDirectories)
					orderby p.FullName
					select p).ToArray<FileInfo>();
					foreach (FileInfo fileInfo in array)
					{
						string abbrevPath = fileInfo.FullName.Replace(ConstantValues.BaseConfigDir + "\\", "");
						GlobalCubemapPicker.TexturePickerWindow.ImageInfo item = new GlobalCubemapPicker.TexturePickerWindow.ImageInfo
						{
							fullPath = fileInfo.FullName,
							abbrevPath = abbrevPath,
							tex = GlobalCubemapPicker.TexturePickerWindow.ReadTexture(fileInfo.FullName)
						};
						this.imageFiles.Add(item);
					}
				}
			}

			public void Set(Vector2 p, float fWidth, int iFontSize, List<string> imageDirectories, Action<Cubemap, string> f)
			{
				this.rect = new Rect(p.x - fWidth, p.y, fWidth, 0f);
				this.fRightPos = p.x + fWidth;
				this.fUpPos = p.y;
				this.fWidth = fWidth;
				this.gsLabel = new GUIStyle("label");
				this.gsLabel.fontSize = iFontSize;
				this.gsLabel.alignment = TextAnchor.MiddleLeft;
				this.gsButton = new GUIStyle("button");
				this.gsButton.fontSize = iFontSize;
				this.gsButton.alignment = TextAnchor.MiddleLeft;
				this.fMargin = (float)iFontSize * 0.3f;
				this.scrollPosition = new Vector2(0f, 0f);
				this.func = f;
				this.show = true;
				this.UpdateImages(imageDirectories);
				if (this.firstSet && this.imageFiles.Count > 0)
				{
					this.ChangeImage(this.imageFiles[0]);
					this.firstSet = false;
				}
			}

			private void fileButton(GlobalCubemapPicker.TexturePickerWindow.ImageInfo info, ref Rect rectItem)
			{
				rectItem.width = (float)this.gsButton.fontSize * 2f;
				rectItem.height = rectItem.width;
				rectItem.x = (float)this.gsButton.fontSize * 0.5f;
				GUI.DrawTexture(rectItem, info.tex);
				rectItem.x += rectItem.width + this.fMargin;
				rectItem.width = this.rect.width - rectItem.width - this.fMargin * 9f;
				if (GUI.Button(rectItem, info.abbrevPath, this.gsButton))
				{
					this.ChangeImage(info);
				}
				rectItem.y += rectItem.height;
			}

			public void GuiFunc(int winId)
			{
				float num = this.rect.height;
				if (this.rect.height > (float)Screen.height * 0.7f)
				{
					num = (float)Screen.height * 0.7f;
				}
				int fontSize = this.gsLabel.fontSize;
				Rect position = new Rect(0f, this.fMargin * 2f, this.rect.width - this.fMargin, num - this.fMargin * 4f);
				Rect viewRect = new Rect(0f, 0f, this.rect.width - 7f * this.fMargin, this.guiScrollHeight);
				this.scrollPosition = GUI.BeginScrollView(position, this.scrollPosition, viewRect, false, true);
				Rect position2 = new Rect((float)fontSize * 0.5f, (float)fontSize * 0.5f, (float)fontSize * 1.5f, (float)fontSize * 1.5f);
				if (this.currentTexture != null)
				{
					GUI.DrawTexture(position2, this.currentTexture);
				}
				int num2 = 0;
				foreach (GlobalCubemapPicker.TexturePickerWindow.ImageInfo info in this.imageFiles)
				{
					this.fileButton(info, ref position2);
					num2++;
				}
				GUI.EndScrollView();
				float num3 = position2.y + position2.height + this.fMargin;
				if (num3 > (float)Screen.height * 0.7f)
				{
					num3 = (float)Screen.height * 0.7f;
				}
				if (this.rect.height != num3)
				{
					Rect rect = new Rect(this.rect.x, this.rect.y - num3, this.rect.width, num3);
					this.rect = rect;
				}
				else if (this.rect.x < 0f)
				{
					Rect rect2 = new Rect(this.fRightPos, this.rect.y, this.rect.width, this.rect.height);
					this.rect = rect2;
				}
				else if (this.rect.y < 0f)
				{
					Rect rect3 = new Rect(this.rect.x, this.fUpPos, this.rect.width, this.rect.height);
					this.rect = rect3;
				}
				this.guiScrollHeight = position2.y + position2.height;
				GUIUtil.MouseClickOnGUIRect(this.rect);
				GUI.DragWindow();
				if (this.GetAnyMouseButtonDown())
				{
					Vector2 point = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
					if (!this.rect.Contains(point))
					{
						this.show = false;
					}
				}
			}

			private bool GetAnyMouseButtonDown()
			{
				return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
			}

			public readonly int WINDOW_ID;

			[CompilerGenerated]
			private Rect <rect>k__BackingField;

			[CompilerGenerated]
			private float <fWidth>k__BackingField;

			[CompilerGenerated]
			private float <fMargin>k__BackingField;

			[CompilerGenerated]
			private float <fRightPos>k__BackingField;

			[CompilerGenerated]
			private float <fUpPos>k__BackingField;

			[CompilerGenerated]
			private float GuiScrollHeight;

			[CompilerGenerated]
			private bool <show>k__BackingField;

			[CompilerGenerated]
			private Action<Cubemap, string> Func;

			[CompilerGenerated]
			private GUIStyle <gsLabel>k__BackingField;

			[CompilerGenerated]
			private GUIStyle <gsButton>k__BackingField;

			[CompilerGenerated]
			private Vector2 <scrollPosition>k__BackingField;

			private bool firstSet;

			private List<GlobalCubemapPicker.TexturePickerWindow.ImageInfo> imageFiles;

			private Cubemap currentTexture;

			internal class ImageInfo
			{
				public ImageInfo()
				{
				}

				public string fullPath;

				public string abbrevPath;

				public Cubemap tex;
			}

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

				internal string <UpdateImages>b__51_0(FileInfo p)
				{
					return p.FullName;
				}

				public static readonly GlobalCubemapPicker.TexturePickerWindow.<>c <>9 = new GlobalCubemapPicker.TexturePickerWindow.<>c();

				public static Func<FileInfo, string> <>9__51_0;
			}
		}
	}
}
