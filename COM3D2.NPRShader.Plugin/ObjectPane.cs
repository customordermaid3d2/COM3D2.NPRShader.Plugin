using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class ObjectPane : BasePane
	{
		public ObjectPane(int fontSize, string catname, string slotname, int slotnum, string matefilename, GameObject go) : base(fontSize, string.Concat(new string[]
		{
			catname,
			":",
			slotname,
			":",
			slotnum.ToString()
		}))
		{
			this.FontSize = fontSize;
			this.catname = catname;
			this.slotname = slotname;
			this.slotnum = slotnum;
			this.name = catname + slotname + slotnum.ToString();
			this.go = go;
			this.mateFileName = matefilename;
		}

		public override void SetupPane()
		{
			this.deleteRequested = false;
			this.bLoadMateFileRequested = true;
			this.bResetMaterial = false;
			this.shaderList = new Dictionary<string, Shader>();
			this.colProps = new Dictionary<string, Color>();
			this.fProps = new Dictionary<string, float>();
			this.sKeywords = new Dictionary<string, bool>();
			this.propColorPickers = new List<CustomColorPicker>();
			this.porpFSliders = new List<CustomSlider>();
			this.propToggleButtons = new List<CustomToggleButton>();
			this.propListBoxes = new List<CustomComboBox>();
			this.shaderToggleButtons = new List<CustomToggleButton>();
			this.slotLabel = new CustomLabel();
			this.slotLabel.Text = this.name;
			this.ChildControls.Add(this.slotLabel);
			this.reloadMatesButton = new CustomButton();
			this.reloadMatesButton.Text = "×";
			CustomButton customButton = this.reloadMatesButton;
			customButton.Click = (EventHandler)Delegate.Combine(customButton.Click, new EventHandler(delegate(object o, EventArgs e)
			{
				this.ReSetMateFile = true;
			}));
			this.ChildControls.Add(this.reloadMatesButton);
		}

		public override void ShowPane()
		{
			try
			{
				this.getMaterial();
				GUIUtil.AddResetButton(this, this.reloadMatesButton);
				if (this.bLoadMateFileRequested)
				{
					this.colProps.Clear();
					this.fProps.Clear();
					this.sKeywords.Clear();
					this.propColorPickers.Clear();
					this.porpFSliders.Clear();
					this.propToggleButtons.Clear();
					this.propListBoxes.Clear();
					this.shaderToggleButtons.Clear();
					if (this.bResetMaterial)
					{
						this.Material = this.BackUpMaterial;
						this.resetMaterial();
						this.RenderQueueSlider.Value = (float)this.BackUpMaterial.renderQueue;
						this.bResetMaterial = false;
					}
					this.parseMateFile(this.FileName, out this.colProps, out this.fProps, out this.sKeywords);
					if (!this.bInitShaderList)
					{
						this.shaderList[this.backUpShader.name + "(defalt)"] = this.backUpShader;
						foreach (string key in Util.shaderList.Keys)
						{
							this.shaderList[key] = Util.shaderList[key];
						}
						this.shaderBox = new CustomComboBox(this.shaderList.Keys.ToArray<string>());
						this.shaderBox.FontSize = this.FontSize;
						this.shaderBox.Text = "shader";
						this.shaderBox.DefaultIndex = 0;
						this.shaderBox.SelectedIndex = 0;
						CustomComboBox customComboBox = this.shaderBox;
						customComboBox.SelectedIndexChanged = (EventHandler)Delegate.Combine(customComboBox.SelectedIndexChanged, new EventHandler(delegate(object o, EventArgs e)
						{
							if (this.shaderBox.SelectedIndex != 0)
							{
								this.Material.shader = this.shaderList[this.shaderBox.SelectedItem];
							}
							else
							{
								this.Material.shader = this.shaderList[this.shaderBox.SelectedItem];
							}
							this.setMaterial();
							foreach (CustomColorPicker item in this.propColorPickers)
							{
								this.ChildControls.Remove(item);
							}
							foreach (CustomSlider item2 in this.porpFSliders)
							{
								this.ChildControls.Remove(item2);
							}
							foreach (CustomToggleButton item3 in this.propToggleButtons)
							{
								this.ChildControls.Remove(item3);
							}
							foreach (CustomComboBox item4 in this.propListBoxes)
							{
								this.ChildControls.Remove(item4);
							}
							foreach (CustomToggleButton item5 in this.shaderToggleButtons)
							{
								this.ChildControls.Remove(item5);
							}
							this.bLoadMateFileRequested = true;
						}));
						this.ChildControls.Add(this.shaderBox);
						this.bInitShaderList = true;
					}
					this.renderQueue = this.Material.renderQueue;
					this.RenderQueueSlider = new CustomSlider((float)this.renderQueue, 0f, 5000f, 0);
					this.RenderQueueSlider.Text = "RenderQueue";
					CustomSlider renderQueueSlider = this.RenderQueueSlider;
					renderQueueSlider.ValueChanged = (EventHandler)Delegate.Combine(renderQueueSlider.ValueChanged, new EventHandler(delegate(object o, EventArgs e)
					{
						this.Material.renderQueue = (int)this.RenderQueueSlider.Value;
						this.setMaterial();
					}));
					this.ChildControls.Add(this.RenderQueueSlider);
					using (Dictionary<string, Color>.KeyCollection.Enumerator enumerator2 = this.colProps.Keys.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							string s = enumerator2.Current;
							CustomColorPicker propColorPicker = new CustomColorPicker(this.colProps[s]);
							propColorPicker.Text = s;
							CustomColorPicker propColorPicker2 = propColorPicker;
							propColorPicker2.ColorChanged = (EventHandler)Delegate.Combine(propColorPicker2.ColorChanged, new EventHandler(delegate(object o, EventArgs e)
							{
								if (this.Material.HasProperty(s))
								{
									this.Material.SetColor(s, propColorPicker.Value);
									this.setMaterial();
								}
							}));
							this.propColorPickers.Add(propColorPicker);
							this.ChildControls.Add(propColorPicker);
						}
					}
					using (Dictionary<string, float>.KeyCollection.Enumerator enumerator3 = this.fProps.Keys.GetEnumerator())
					{
						while (enumerator3.MoveNext())
						{
							string s = enumerator3.Current;
							int dec = 4;
							float min = 0f;
							float max = 1f;
							ObjectPane.ButtonType buttonType = ObjectPane.ButtonType.Slider;
							float num = this.fProps[s];
							if (s.ToLower().Contains("value") || s.ToLower().Contains("ztest2alpha"))
							{
								min = 0f;
								max = 1f;
							}
							else if (s.ToLower().Contains("rimpower"))
							{
								min = -30f;
								max = 30f;
							}
							else if (s.ToLower().Contains("angelringshift"))
							{
								min = -1f;
								max = 1f;
								dec = 4;
							}
							else if (s.ToLower().Contains("angelring"))
							{
								min = 0f;
								max = 2f;
								dec = 4;
							}
							else if (s.ToLower().Contains("power") || s.ToLower().Contains("power"))
							{
								min = -10f;
								max = 10f;
							}
							else if (s.ToLower().Contains("stencilid"))
							{
								min = 0f;
								max = 255f;
								dec = 0;
							}
							else if (s.ToLower().Contains("exposure"))
							{
								min = 0f;
								max = 3f;
								dec = 4;
							}
							if (!s.ToLower().Contains("ztest2alpha"))
							{
								if (s.ToLower().Contains("customblend") || s.ToLower().Contains("_brightnesstoalpha") || s.ToLower().Contains("alphatomask") || s.ToLower().Contains("fixatc") || s.ToLower().Contains("zwrite") || s.ToLower().Contains("ztest2") || s.ToLower().Contains("_cutouttoopaque"))
								{
									buttonType = ObjectPane.ButtonType.Toggle;
								}
								else if (s.ToLower().Contains("culling") || s.ToLower().Contains("blendop") || s.ToLower().Contains("srcblend") || s.ToLower().Contains("dstblend") || s.ToLower().Contains("ztest"))
								{
									buttonType = ObjectPane.ButtonType.List;
								}
							}
							switch (buttonType)
							{
							case ObjectPane.ButtonType.Slider:
							{
								CustomSlider porpFSlider = new CustomSlider(num, min, max, dec);
								porpFSlider.Text = s;
								CustomSlider porpFSlider2 = porpFSlider;
								porpFSlider2.ValueChanged = (EventHandler)Delegate.Combine(porpFSlider2.ValueChanged, new EventHandler(delegate(object o, EventArgs e)
								{
									if (this.Material.HasProperty(s))
									{
										this.Material.SetFloat(s, porpFSlider.Value);
										this.setMaterial();
									}
								}));
								this.porpFSliders.Add(porpFSlider);
								this.ChildControls.Add(porpFSlider);
								break;
							}
							case ObjectPane.ButtonType.Toggle:
							{
								bool value = num > 0f;
								CustomToggleButton propToggleButton = new CustomToggleButton(value, "toggle");
								propToggleButton.Text = s;
								CustomToggleButton propToggleButton2 = propToggleButton;
								propToggleButton2.CheckedChanged = (EventHandler)Delegate.Combine(propToggleButton2.CheckedChanged, new EventHandler(delegate(object o, EventArgs e)
								{
									if (propToggleButton.Value)
									{
										if (this.Material.HasProperty(s))
										{
											this.Material.SetFloat(s, 1f);
										}
									}
									else if (this.Material.HasProperty(s))
									{
										this.Material.SetFloat(s, 0f);
									}
									this.setMaterial();
								}));
								this.propToggleButtons.Add(propToggleButton);
								this.ChildControls.Add(propToggleButton);
								break;
							}
							case ObjectPane.ButtonType.List:
							{
								string[] items = null;
								if (s.ToLower().Contains("culling"))
								{
									items = ObjectPane.CULLMODE;
								}
								else if (s.ToLower().Contains("ztest"))
								{
									items = ObjectPane.ZTESTOP;
								}
								else if (s.ToLower().Contains("blendop"))
								{
									items = ObjectPane.BLENDOP;
								}
								else if (s.ToLower().Contains("srcblend") || s.ToLower().Contains("dstblend"))
								{
									items = ObjectPane.BLENDMODE;
								}
								CustomComboBox propListBox = new CustomComboBox(items);
								propListBox.FontSize = this.FontSize;
								propListBox.Text = s;
								propListBox.DefaultIndex = (int)num;
								propListBox.SelectedIndex = (int)num;
								CustomComboBox propListBox2 = propListBox;
								propListBox2.SelectedIndexChanged = (EventHandler)Delegate.Combine(propListBox2.SelectedIndexChanged, new EventHandler(delegate(object o, EventArgs e)
								{
									if (this.Material.HasProperty(s))
									{
										this.Material.SetFloat(s, (float)propListBox.SelectedIndex);
										this.setMaterial();
									}
								}));
								this.propListBoxes.Add(propListBox);
								this.ChildControls.Add(propListBox);
								break;
							}
							}
						}
					}
					using (Dictionary<string, bool>.KeyCollection.Enumerator enumerator4 = this.sKeywords.Keys.GetEnumerator())
					{
						while (enumerator4.MoveNext())
						{
							string s = enumerator4.Current;
							bool value2 = this.sKeywords[s];
							CustomToggleButton shaderToggleButton = new CustomToggleButton(value2, "toggle");
							shaderToggleButton.Text = s;
							CustomToggleButton shaderToggleButton2 = shaderToggleButton;
							shaderToggleButton2.CheckedChanged = (EventHandler)Delegate.Combine(shaderToggleButton2.CheckedChanged, new EventHandler(delegate(object o, EventArgs e)
							{
								if (shaderToggleButton.Value)
								{
									if (this.Material.HasProperty(s))
									{
										this.Material.EnableKeyword(s.ToUpper().ToString() + "_ON");
									}
								}
								else if (this.Material.HasProperty(s))
								{
									this.Material.DisableKeyword(s.ToUpper().ToString() + "_ON");
								}
								this.setMaterial();
							}));
							this.shaderToggleButtons.Add(shaderToggleButton);
							this.ChildControls.Add(shaderToggleButton);
						}
					}
					this.bLoadMateFileRequested = false;
				}
				GUIUtil.AddGUICheckbox(this, this.shaderBox);
				GUIUtil.AddGUISlider(this, this.RenderQueueSlider);
				foreach (CustomColorPicker elem in this.propColorPickers)
				{
					GUIUtil.AddGUICheckbox(this, elem);
				}
				foreach (CustomSlider elem2 in this.porpFSliders)
				{
					GUIUtil.AddGUISlider(this, elem2);
				}
				foreach (CustomToggleButton elem3 in this.propToggleButtons)
				{
					GUIUtil.AddGUICheckbox(this, elem3);
				}
				foreach (CustomComboBox elem4 in this.propListBoxes)
				{
					GUIUtil.AddGUICheckbox(this, elem4);
				}
				foreach (CustomToggleButton elem5 in this.shaderToggleButtons)
				{
					GUIUtil.AddGUICheckbox(this, elem5);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void Update()
		{
			try
			{
				if (this.bResetMaterial)
				{
					this.shaderBox.Reset();
					this.RenderQueueSlider.Reset();
					foreach (CustomColorPicker customColorPicker in this.propColorPickers)
					{
						customColorPicker.Reset();
					}
					foreach (CustomSlider customSlider in this.porpFSliders)
					{
						customSlider.Reset();
					}
					foreach (CustomToggleButton customToggleButton in this.propToggleButtons)
					{
						customToggleButton.Reset();
					}
					foreach (CustomComboBox customComboBox in this.propListBoxes)
					{
						customComboBox.Reset();
					}
					foreach (CustomToggleButton customToggleButton2 in this.shaderToggleButtons)
					{
						customToggleButton2.Reset();
					}
					this.bResetMaterial = false;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void parseMateFile(string materialName, out Dictionary<string, Color> colProps, out Dictionary<string, float> fProps, out Dictionary<string, bool> sKeywords)
		{
			byte[] array = null;
			Dictionary<string, Color> dictionary = new Dictionary<string, Color>();
			Dictionary<string, float> dictionary2 = new Dictionary<string, float>();
			Dictionary<string, bool> dictionary3 = new Dictionary<string, bool>();
			if (materialName == null)
			{
				foreach (string text in ObjectPane.BASECOLORPROP)
				{
					if (this.Material.HasProperty(text))
					{
						dictionary[text] = this.Material.GetColor(text);
					}
				}
				foreach (string text2 in ObjectPane.BASEFRPROP)
				{
					if (this.Material.HasProperty(text2))
					{
						dictionary2[text2] = this.Material.GetFloat(text2);
					}
				}
				foreach (string text3 in ObjectPane.SHADERKEYWORD)
				{
					if (this.Material.HasProperty(text3))
					{
						dictionary3[text3] = this.Material.IsKeywordEnabled(text3);
					}
				}
				colProps = dictionary;
				fProps = dictionary2;
				sKeywords = dictionary3;
				return;
			}
			try
			{
				using (AFileBase afileBase = GameUty.FileOpen(materialName, null))
				{
					NDebug.Assert(afileBase.IsValid(), "LoadMaterial マテリアルコンテナが読めません。 :" + materialName);
					if (array == null)
					{
						array = new byte[Math.Max(10000, afileBase.GetSize())];
					}
					else if (array.Length < afileBase.GetSize())
					{
						array = new byte[afileBase.GetSize()];
					}
					afileBase.Read(ref array, afileBase.GetSize());
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"LoadMaterial マテリアルコンテナが読み込めませんでした。 : ",
					materialName,
					" : ",
					ex.Message,
					" : StackTrace ：\n",
					ex.StackTrace
				}));
				throw ex;
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(array), Encoding.UTF8);
			string text4 = binaryReader.ReadString();
			if (text4 != "CM3D2_MATERIAL")
			{
				NDebug.Assert("ProcScriptBin 例外 : ヘッダーファイルが不正です。" + text4, false);
			}
			int num = binaryReader.ReadInt32();
			string text5 = binaryReader.ReadString();
			string text6 = binaryReader.ReadString();
			string text7 = binaryReader.ReadString();
			string text8 = binaryReader.ReadString();
			foreach (string text9 in ObjectPane.BASECOLORPROP)
			{
				if (this.Material.HasProperty(text9))
				{
					dictionary[text9] = this.Material.GetColor(text9);
				}
			}
			foreach (string text10 in ObjectPane.BASEFRPROP)
			{
				if (this.Material.HasProperty(text10))
				{
					dictionary2[text10] = this.Material.GetFloat(text10);
				}
			}
			foreach (string text11 in ObjectPane.SHADERKEYWORD)
			{
				if (this.Material.HasProperty(text11))
				{
					dictionary3[text11] = this.Material.IsKeywordEnabled(text11);
				}
			}
			for (;;)
			{
				string text12 = binaryReader.ReadString();
				if (text12 == "end")
				{
					break;
				}
				string text13 = binaryReader.ReadString();
				if (text12 == "tex")
				{
					string a = binaryReader.ReadString();
					if (!(a == "null") && a == "tex2d")
					{
						string text14 = binaryReader.ReadString();
						binaryReader.ReadString();
						Vector2 vector;
						vector.x = binaryReader.ReadSingle();
						vector.y = binaryReader.ReadSingle();
						Vector2 vector2;
						vector2.x = binaryReader.ReadSingle();
						vector2.y = binaryReader.ReadSingle();
					}
					if (a == "texRT")
					{
						binaryReader.ReadString();
						binaryReader.ReadString();
					}
				}
				else if (text12 == "col")
				{
					Color color;
					color.r = binaryReader.ReadSingle();
					color.g = binaryReader.ReadSingle();
					color.b = binaryReader.ReadSingle();
					color.a = binaryReader.ReadSingle();
					if (this.Material.HasProperty(text13) && !dictionary.ContainsKey(text13))
					{
						dictionary[text13] = this.Material.GetColor(text13);
					}
				}
				else if (text12 == "vec")
				{
					Vector4 vector3;
					vector3.x = binaryReader.ReadSingle();
					vector3.y = binaryReader.ReadSingle();
					vector3.z = binaryReader.ReadSingle();
					vector3.w = binaryReader.ReadSingle();
				}
				else if (text12 == "f")
				{
					float num2 = binaryReader.ReadSingle();
					if (text13.Contains("Toggle"))
					{
						if (num2 == 1f)
						{
							if (this.Material.HasProperty(text13))
							{
								dictionary3[text13] = true;
							}
						}
						else if (this.Material.HasProperty(text13))
						{
							dictionary3[text13] = false;
						}
					}
					else if (this.Material.HasProperty(text13) && !dictionary2.ContainsKey(text13))
					{
						dictionary2[text13] = this.Material.GetFloat(text13);
					}
				}
				else
				{
					Debug.LogError("マテリアルが読み込めません。不正なマテリアルプロパティ型です " + text12);
				}
			}
			binaryReader.Close();
			colProps = dictionary;
			fProps = dictionary2;
			sKeywords = dictionary3;
		}

		private void getMaterial()
		{
			try
			{
				int num = (int)TBody.hashSlotName[this.SlotName];
				GameObject gameObject = this.Go;
				foreach (SkinnedMeshRenderer skinnedMeshRenderer in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
				{
					if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null && this.SlotNum < skinnedMeshRenderer.materials.Length)
					{
						Material[] array = new Material[skinnedMeshRenderer.materials.Length];
						array = new Material[skinnedMeshRenderer.materials.Length];
						for (int j = 0; j < array.Length; j++)
						{
							array[j] = new Material(skinnedMeshRenderer.materials[j]);
						}
						UnityEngine.Object.DestroyImmediate(this.Material);
						this.Material = array[this.SlotNum];
						if (!this.backedUp)
						{
							UnityEngine.Object.DestroyImmediate(this.BackUpMaterial);
							this.BackUpMaterial = array[this.SlotNum];
							this.BackUpShader = array[this.SlotNum].shader;
							this.backedUp = true;
						}
						break;
					}
				}
			}
			catch (Exception ex)
			{
				this.deleteRequested = true;
				Debug.LogError(ex.ToString());
			}
		}

		private void setMaterial()
		{
			int num = (int)TBody.hashSlotName[this.SlotName];
			GameObject gameObject = this.Go;
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
			{
				if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null && this.SlotNum < skinnedMeshRenderer.materials.Length)
				{
					Material[] array = new Material[skinnedMeshRenderer.materials.Length];
					array = new Material[skinnedMeshRenderer.materials.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = new Material(skinnedMeshRenderer.materials[j]);
					}
					array[this.SlotNum] = this.Material;
					skinnedMeshRenderer.materials = array;
					if (!this.backUpShader.name.Contains("com3d2"))
					{
						skinnedMeshRenderer.sharedMesh.RecalculateTangents();
					}
					return;
				}
			}
		}

		private void resetMaterial()
		{
			int num = (int)TBody.hashSlotName[this.SlotName];
			GameObject gameObject = this.Go;
			foreach (SkinnedMeshRenderer skinnedMeshRenderer in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
			{
				if (skinnedMeshRenderer != null && skinnedMeshRenderer.material != null && this.SlotNum < skinnedMeshRenderer.materials.Length)
				{
					Material[] array = new Material[skinnedMeshRenderer.materials.Length];
					array = new Material[skinnedMeshRenderer.materials.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array[j] = new Material(skinnedMeshRenderer.materials[j]);
					}
					array[this.SlotNum] = this.BackUpMaterial;
					array[this.SlotNum].shader = this.BackUpShader;
					skinnedMeshRenderer.materials = array;
					return;
				}
			}
		}

		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		public string CategoryName
		{
			get
			{
				return this.catname;
			}
			set
			{
				this.catname = value;
			}
		}

		public string SlotName
		{
			get
			{
				return this.slotname;
			}
			set
			{
				this.slotname = value;
			}
		}

		public int SlotNum
		{
			get
			{
				return this.slotnum;
			}
			set
			{
				this.slotnum = value;
			}
		}

		public string FileName
		{
			get
			{
				return this.mateFileName;
			}
			set
			{
				this.mateFileName = value;
			}
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

		public bool ReloadMateFile
		{
			get
			{
				return this.bLoadMateFileRequested;
			}
			set
			{
				this.bLoadMateFileRequested = value;
			}
		}

		public bool ReSetMateFile
		{
			get
			{
				return this.bResetMaterial;
			}
			set
			{
				this.bResetMaterial = value;
			}
		}

		public Material Material
		{
			get
			{
				return this.material;
			}
			set
			{
				this.material = value;
			}
		}

		public Material BackUpMaterial
		{
			get
			{
				return this.backUpMaterial;
			}
			set
			{
				this.backUpMaterial = value;
			}
		}

		public Shader Shader
		{
			get
			{
				return this.shader;
			}
			set
			{
				this.shader = value;
			}
		}

		public Shader BackUpShader
		{
			get
			{
				return this.backUpShader;
			}
			set
			{
				this.backUpShader = value;
			}
		}

		public GameObject Go
		{
			get
			{
				return this.go;
			}
			set
			{
				this.go = value;
			}
		}

		public bool wasChanged
		{
			[CompilerGenerated]
			get
			{
				return this.WasChanged;
			}
			[CompilerGenerated]
			set
			{
				this.WasChanged = value;
			}
		}

		public bool bLoadMateFileRequested
		{
			[CompilerGenerated]
			get
			{
				return this.BLoadMateFileRequested;
			}
			[CompilerGenerated]
			set
			{
				this.BLoadMateFileRequested = value;
			}
		}

		public bool bResetMaterial
		{
			[CompilerGenerated]
			get
			{
				return this.BResetMaterial;
			}
			[CompilerGenerated]
			set
			{
				this.BResetMaterial = value;
			}
		}

		public ObjectPane.ButtonType buttonType
		{
			get
			{
				return this._buttonType;
			}
			set
			{
				this._buttonType = value;
			}
		}

		// Note: this type is marked as 'beforefieldinit'.
		static ObjectPane()
		{
		}
		/*
		[CompilerGenerated]
		private void <SetupPane>b__1_0(object o, EventArgs e)
		{
			this.ReSetMateFile = true;
		}

		[CompilerGenerated]
		private void <ShowPane>b__2_0(object o, EventArgs e)
		{
			if (this.shaderBox.SelectedIndex != 0)
			{
				this.Material.shader = this.shaderList[this.shaderBox.SelectedItem];
			}
			else
			{
				this.Material.shader = this.shaderList[this.shaderBox.SelectedItem];
			}
			this.setMaterial();
			foreach (CustomColorPicker item in this.propColorPickers)
			{
				this.ChildControls.Remove(item);
			}
			foreach (CustomSlider item2 in this.porpFSliders)
			{
				this.ChildControls.Remove(item2);
			}
			foreach (CustomToggleButton item3 in this.propToggleButtons)
			{
				this.ChildControls.Remove(item3);
			}
			foreach (CustomComboBox item4 in this.propListBoxes)
			{
				this.ChildControls.Remove(item4);
			}
			foreach (CustomToggleButton item5 in this.shaderToggleButtons)
			{
				this.ChildControls.Remove(item5);
			}
			this.bLoadMateFileRequested = true;
		}
		*/
		/*
		[CompilerGenerated]
		private void <ShowPane>b__2_1(object o, EventArgs e)
		{
			this.Material.renderQueue = (int)this.RenderQueueSlider.Value;
			this.setMaterial();
		}
		*/
		private bool _value;

		public EventHandler CheckedChanged;/* = delegate(object <p0>, EventArgs <p1>)
		{
		};*/

		private static readonly string[] CULLMODE = new string[]
		{
			"Back",
			"Front",
			"Off"
		};

		private static readonly string[] MODETOGGLE = new string[]
		{
			"OFF",
			"ON"
		};

		private static readonly string[] ZTESTOP = new string[]
		{
			"Less",
			"Greater",
			"LEqual",
			"GEqual",
			"Equal",
			"NotEqual",
			"Always"
		};

		private static readonly string[] BLENDOP = new string[]
		{
			"Add",
			"Subtract",
			"ReverseSubtract",
			"Min",
			"Max"
		};

		private static readonly string[] BLENDMODE = new string[]
		{
			"Zero",
			"One",
			"DstColor",
			"SrcColor",
			"OneMinusDstColor",
			"SrcAlpha",
			"OneMinusSrcColor",
			"DstAlpha",
			"OneMinusDstAlpha",
			"SrcAlphaSaturate",
			"OneMinusSrcAlpha"
		};

		private static readonly string[] BASECOLORPROP = new string[]
		{
			"_Color",
			"_ShadowColor",
			"_RimColor",
			"_OutlineColor",
			"_MatcapColor",
			"_MatcapMaskColor",
			"_RimLightColor",
			"_EmissionColor"
		};

		private static readonly string[] BASEFRPROP = new string[]
		{
			"_OutlineWidth",
			"_Shininess",
			"_RimPower",
			"_RimShift",
			"_HiPow",
			"_ParallaxValue",
			"_NormalValue",
			"_ToonToDiffuseRateValue",
			"_MetallicValue",
			"_SmoothnessValue",
			"_OcclusionValue",
			"_ReflectionDiffuseValue",
			"_FabricDiffuseValue",
			"_HighLightValue",
			"_AngelRingX",
			"_AngelRingY",
			"_AngelRingPower",
			"_AngelRingShiftTangent",
			"_AngelRingShiftBinormal",
			"_HiRate",
			"_HiRateForMatcap",
			"_MatcapValue",
			"_MatcapMaskValue",
			"_MatcapCustomBlend",
			"_RimLightValue",
			"_RimLightPower",
			"_RimLightCustomBlend",
			"_EmissionValue",
			"_EmissionPower",
			"_EmissionHDRExposure",
			"_EmissionCustomBlend",
			"_EmissionToonScrollSpeed",
			"_EmissionUVScrollSpeed_X",
			"_EmissionUVScrollSpeed_Y",
			"_BrightnessToAlpha",
			"_AlphaToMask",
			"_FixAtC",
			"_Cutoff",
			"_CutoutToOpaque",
			"_ShadowCutoff",
			"_Culling",
			"_ZWrite",
			"_ZTest",
			"_BlendOp",
			"_SrcBlend",
			"_DstBlend",
			"_BlendOp2",
			"_SrcBlend2",
			"_DstBlend2",
			"_ZTest2",
			"_ZTest2Alpha",
			"_FloatValue1",
			"_FloatValue2",
			"_FloatValue3"
		};

		private static readonly string[] SHADERKEYWORD = new string[]
		{
			"_ParallaxShaderToggle",
			"_ParallaxLoopShaderToggle",
			"_EmissionToonShaderToggle",
			"_EmissionToonViewModeShaderToggle",
			"_EmissionUVScrollShaderToggle"
		};

		private string name;

		private string catname;

		private string slotname;

		private int slotnum;

		private string mateFileName;

		public bool deleteRequested;

		[CompilerGenerated]
		private bool WasChanged;

		[CompilerGenerated]
		private bool BLoadMateFileRequested;

		[CompilerGenerated]
		private bool BResetMaterial;

		private int renderQueue;

		private Material material;

		private Material backUpMaterial;

		private bool backedUp;

		private Shader shader;

		private Shader backUpShader;

		private List<string> cats;

		private GameObject go;

		private Dictionary<string, Color> colProps;

		private Dictionary<string, float> fProps;

		private Dictionary<string, bool> sKeywords;

		private CustomLabel slotLabel;

		private MaterialInfo mateInfo;

		private static Dictionary<int, KeyValuePair<string, float>> hashPriorityMaterials = null;

		private List<CustomToggleButton> categoryButtons;

		private int propId;

		private List<CustomColorPicker> propColorPickers;

		private List<CustomSlider> porpFSliders;

		private List<CustomToggleButton> propToggleButtons;

		private List<CustomComboBox> propListBoxes;

		private List<CustomToggleButton> shaderToggleButtons;

		public Dictionary<string, Shader> shaderList;

		private CustomComboBox shaderBox;

		private bool bUpDateShaderRequested;

		private bool bInitShaderList;

		private CustomSlider RenderQueueSlider;

		private CustomButton reloadMatesButton;

		public ObjectPane.ButtonType _buttonType;

		public enum ButtonType
		{
			Slider,
			Toggle,
			List
		}
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

			public static readonly ObjectPane.<>c <>9 = new ObjectPane.<>c();

			public static EventHandler <>9__0_0;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_0
		{
			public <>c__DisplayClass2_0()
			{
			}

			internal void <ShowPane>b__2(object o, EventArgs e)
			{
				if (this.<>4__this.Material.HasProperty(this.s))
				{
					this.<>4__this.Material.SetColor(this.s, this.propColorPicker.Value);
					this.<>4__this.setMaterial();
				}
			}

			public string s;

			public CustomColorPicker propColorPicker;

			public ObjectPane <>4__this;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_1
		{
			public <>c__DisplayClass2_1()
			{
			}

			public string s;

			public ObjectPane <>4__this;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_2
		{
			public <>c__DisplayClass2_2()
			{
			}

			internal void <ShowPane>b__3(object o, EventArgs e)
			{
				if (this.CS$<>8__locals1.<>4__this.Material.HasProperty(this.CS$<>8__locals1.s))
				{
					this.CS$<>8__locals1.<>4__this.Material.SetFloat(this.CS$<>8__locals1.s, this.porpFSlider.Value);
					this.CS$<>8__locals1.<>4__this.setMaterial();
				}
			}

			public CustomSlider porpFSlider;

			public ObjectPane.<>c__DisplayClass2_1 CS$<>8__locals1;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_3
		{
			public <>c__DisplayClass2_3()
			{
			}

			internal void <ShowPane>b__4(object o, EventArgs e)
			{
				if (this.propToggleButton.Value)
				{
					if (this.CS$<>8__locals2.<>4__this.Material.HasProperty(this.CS$<>8__locals2.s))
					{
						this.CS$<>8__locals2.<>4__this.Material.SetFloat(this.CS$<>8__locals2.s, 1f);
					}
				}
				else if (this.CS$<>8__locals2.<>4__this.Material.HasProperty(this.CS$<>8__locals2.s))
				{
					this.CS$<>8__locals2.<>4__this.Material.SetFloat(this.CS$<>8__locals2.s, 0f);
				}
				this.CS$<>8__locals2.<>4__this.setMaterial();
			}

			public CustomToggleButton propToggleButton;

			public ObjectPane.<>c__DisplayClass2_1 CS$<>8__locals2;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_4
		{
			public <>c__DisplayClass2_4()
			{
			}

			internal void <ShowPane>b__5(object o, EventArgs e)
			{
				if (this.CS$<>8__locals3.<>4__this.Material.HasProperty(this.CS$<>8__locals3.s))
				{
					this.CS$<>8__locals3.<>4__this.Material.SetFloat(this.CS$<>8__locals3.s, (float)this.propListBox.SelectedIndex);
					this.CS$<>8__locals3.<>4__this.setMaterial();
				}
			}

			public CustomComboBox propListBox;

			public ObjectPane.<>c__DisplayClass2_1 CS$<>8__locals3;
		}

		[CompilerGenerated]
		private sealed class <>c__DisplayClass2_5
		{
			public <>c__DisplayClass2_5()
			{
			}

			internal void <ShowPane>b__6(object o, EventArgs e)
			{
				if (this.shaderToggleButton.Value)
				{
					if (this.<>4__this.Material.HasProperty(this.s))
					{
						this.<>4__this.Material.EnableKeyword(this.s.ToUpper().ToString() + "_ON");
					}
				}
				else if (this.<>4__this.Material.HasProperty(this.s))
				{
					this.<>4__this.Material.DisableKeyword(this.s.ToUpper().ToString() + "_ON");
				}
				this.<>4__this.setMaterial();
			}

			public string s;

			public CustomToggleButton shaderToggleButton;

			public ObjectPane <>4__this;
		}*/
	}
}
