using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class ObjectWindow : ScrollablePane
	{
		public ObjectWindow(int fontSize) : base(fontSize)
		{
		}

		public override void Awake()
		{
			try
			{
				this.materialSlotInfos = new Dictionary<string, MaterialInfo>();
				this.objDic = new Dictionary<int, string>();
				this.objectPanes = new List<ObjectPane>();
				this.categoryButtons = new List<CustomToggleButton>();
				this.bgObjectButtons = new List<CustomToggleButton>();
				this.reloadMaidsButton = new CustomButton();
				this.reloadMaidsButton.Text = "再読込";
				CustomButton customButton = this.reloadMaidsButton;
				customButton.Click = (EventHandler)Delegate.Combine(customButton.Click, new EventHandler(delegate(object o, EventArgs e)
				{
					foreach (ObjectPane item in this.objectPanes)
					{
						this.ChildControls.Remove(item);
					}
					foreach (CustomToggleButton item2 in this.categoryButtons)
					{
						this.ChildControls.Remove(item2);
					}
					foreach (CustomToggleButton item3 in this.bgObjectButtons)
					{
						this.ChildControls.Remove(item3);
					}
					this.objectPanes.Clear();
					this.categoryButtons.Clear();
					this.bgObjectButtons.Clear();
					this.materialSlotInfos.Clear();
					this.objDic.Clear();
					this.objDic[0] = "- - -";
					this.UpdaateMaterial();
				}));
				this.ChildControls.Add(this.reloadMaidsButton);
				this.AutoUpdateButton = new CustomToggleButton(false, "toggle");
				this.AutoUpdateButton.FontSize = this.FontSize;
				this.AutoUpdateButton.Text = "自動更新有効";
				CustomToggleButton autoUpdateButton = this.AutoUpdateButton;
				autoUpdateButton.CheckedChanged = (EventHandler)Delegate.Combine(autoUpdateButton.CheckedChanged, new EventHandler(this.AutoUpdateButton_CheckedChanged));
				this.ChildControls.Add(this.AutoUpdateButton);
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
				if (this.bUpdateRequest)
				{
					foreach (ObjectPane item in this.objectPanes)
					{
						this.ChildControls.Remove(item);
					}
					foreach (CustomToggleButton item2 in this.categoryButtons)
					{
						this.ChildControls.Remove(item2);
					}
					foreach (CustomToggleButton item3 in this.bgObjectButtons)
					{
						this.ChildControls.Remove(item3);
					}
					this.objectPanes.Clear();
					this.categoryButtons.Clear();
					this.bgObjectButtons.Clear();
					this.materialSlotInfos.Clear();
					this.objDic.Clear();
					this.objDic[0] = "- - -";
					this.bUpdateRequest = false;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public override void ShowPane()
		{
			this.reloadMaidsButton.Left = this.Left + (float)ControlBase.FixedMargin;
			this.reloadMaidsButton.Top = this.Top + (float)ControlBase.FixedMargin;
			this.reloadMaidsButton.Width = (float)(this.FontSize * 6);
			this.reloadMaidsButton.Height = base.ControlHeight;
			this.reloadMaidsButton.OnGUI();
			this.reloadMaidsButton.Visible = true;
			GUIUtil.AddGUICheckbox(this, this.AutoUpdateButton, this.reloadMaidsButton);
			ControlBase reference = this.AutoUpdateButton;
			foreach (CustomToggleButton customToggleButton in this.categoryButtons)
			{
				GUIUtil.AddGUICheckbox(this, customToggleButton, reference);
				reference = customToggleButton;
				foreach (MaterialInfo materialInfo in this.materialSlotInfos.Values)
				{
					if (customToggleButton.Text == materialInfo.catName)
					{
						foreach (ObjectPane objectPane in this.objectPanes)
						{
							if (objectPane.Name == materialInfo.catName + materialInfo.slotName + materialInfo.materialNo.ToString())
							{
								if (customToggleButton.Value)
								{
									GUIUtil.AddGUICheckbox(this, objectPane, reference);
									reference = objectPane;
								}
								else
								{
									objectPane.Visible = false;
								}
							}
						}
					}
				}
			}
			foreach (CustomToggleButton customToggleButton2 in from a in this.bgObjectButtons
			orderby a.Text
			select a)
			{
				GUIUtil.AddGUICheckbox(this, customToggleButton2, reference);
				reference = customToggleButton2;
			}
			this.Height = GUIUtil.GetHeightForParent(this);
		}

		public void reloadMaterial()
		{
			if (this.reloadMaterialRequest && this.AutoUpdateButton.Value)
			{
				foreach (ObjectPane item in this.objectPanes)
				{
					this.ChildControls.Remove(item);
				}
				foreach (CustomToggleButton item2 in this.categoryButtons)
				{
					this.ChildControls.Remove(item2);
				}
				foreach (CustomToggleButton item3 in this.bgObjectButtons)
				{
					this.ChildControls.Remove(item3);
				}
				this.objectPanes.Clear();
				this.categoryButtons.Clear();
				this.bgObjectButtons.Clear();
				this.materialSlotInfos.Clear();
				this.objDic.Clear();
				this.objDic[0] = "- - -";
				this.UpdaateMaterial();
				this.reloadMaterialRequest = false;
				NPRShader.reloadMaterialRequest = false;
			}
		}

		public void UpdaateMaterial()
		{
			this.isReflection = false;
			string text = "_NPRMAT_";
			Regex regex = new Regex(text.ToLower());
			int num = 0;
			foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				if (gameObject.name.Length >= 6 && gameObject.name.ToLower().EndsWith(".menu"))
				{
					string menuFileName = gameObject.name.Replace("[SC] ", "");
					PBRModelInfo pbrmodelInfo = AssetLoader.LoadMenuPBR(menuFileName);
					if (!string.IsNullOrEmpty(pbrmodelInfo.modelName))
					{
						this.objDic[num] = pbrmodelInfo.menuName;
						num++;
						CustomToggleButton customToggleButton = new CustomToggleButton(false, "toggle");
						customToggleButton.Text = "OBJ" + num.ToString() + ":" + pbrmodelInfo.menuName.ToString();
						customToggleButton.FontSize = this.FontSize;
						this.categoryButtons.Add(customToggleButton);
						this.ChildControls.Add(customToggleButton);
						foreach (ModelInfo modelInfo in pbrmodelInfo.models)
						{
							int num2 = (int)TBody.hashSlotName[modelInfo.slotName];
							try
							{
								foreach (SkinnedMeshRenderer skinnedMeshRenderer in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
								{
									if (skinnedMeshRenderer != null)
									{
										Material[] array2 = new Material[skinnedMeshRenderer.materials.Length];
										for (int k = 0; k < array2.Length; k++)
										{
											array2[k] = new Material(skinnedMeshRenderer.materials[k]);
										}
										foreach (MaterialChangeInfo materialChangeInfo in pbrmodelInfo.materialChanges)
										{
											if (materialChangeInfo.filename.ToLower().Contains(text.ToLower()))
											{
												string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(materialChangeInfo.filename);
												string[] source = regex.Split(fileNameWithoutExtension.ToLower());
												string shaderName = source.Last<string>();
												try
												{
													using (AFileBase afileBase = GameUty.FileOpen(materialChangeInfo.filename, null))
													{
														if (afileBase.IsValid())
														{
															Debug.Log("NPRShader: シェーダー変更" + pbrmodelInfo.menuFileName);
															array2[materialChangeInfo.materialNo] = AssetLoader.LoadMaterialWithSetShader(materialChangeInfo.filename, shaderName, null);
															materialChangeInfo.filename.ToLower().Contains("_reflection_");
														}
													}
													continue;
												}
												catch (Exception ex)
												{
													Debug.LogError(ex.ToString());
													array2[materialChangeInfo.materialNo] = AssetLoader.LoadMaterial(materialChangeInfo.filename, null);
													continue;
												}
											}
											array2[materialChangeInfo.materialNo] = AssetLoader.LoadMaterial(materialChangeInfo.filename, null);
										}
										foreach (TextureChangeInfo textureChangeInfo in pbrmodelInfo.textureChanges)
										{
											array2[textureChangeInfo.materialNo].SetTexture(textureChangeInfo.propName, AssetLoader.LoadTexture(textureChangeInfo.filename));
										}
										skinnedMeshRenderer.materials = array2;
										skinnedMeshRenderer.sharedMesh.RecalculateTangents();
									}
								}
							}
							catch (Exception ex2)
							{
								Debug.LogError(ex2.ToString());
							}
						}
						if (pbrmodelInfo.materialChanges != null)
						{
							foreach (MaterialChangeInfo materialChangeInfo2 in pbrmodelInfo.materialChanges)
							{
								MaterialInfo materialInfo = new MaterialInfo("OBJ" + num.ToString() + ":" + pbrmodelInfo.menuName.ToString(), materialChangeInfo2.slotName, materialChangeInfo2.materialNo, materialChangeInfo2.filename);
								this.materialSlotInfos[string.Concat(new string[]
								{
									"OBJ",
									num.ToString(),
									":",
									pbrmodelInfo.menuName.ToString(),
									materialChangeInfo2.slotName,
									materialChangeInfo2.materialNo.ToString()
								})] = materialInfo;
								ObjectPane item = new ObjectPane(this.FontSize, materialInfo.catName, materialInfo.slotName, materialInfo.materialNo, materialInfo.filename, gameObject);
								this.objectPanes.Add(item);
								this.ChildControls.Add(item);
							}
						}
					}
				}
			}
		}

		private void AutoUpdateButton_CheckedChanged(object sender, EventArgs args)
		{
			this.configObjectMaterialsAutoUpdate = this.AutoUpdateButton.Value;
		}

		public void init()
		{
			this.AutoUpdateButton.Value = this.configObjectMaterialsAutoUpdate;
		}
		/*
		[CompilerGenerated]
		private void <Awake>b__1_0(object o, EventArgs e)
		{
			foreach (ObjectPane item in this.objectPanes)
			{
				this.ChildControls.Remove(item);
			}
			foreach (CustomToggleButton item2 in this.categoryButtons)
			{
				this.ChildControls.Remove(item2);
			}
			foreach (CustomToggleButton item3 in this.bgObjectButtons)
			{
				this.ChildControls.Remove(item3);
			}
			this.objectPanes.Clear();
			this.categoryButtons.Clear();
			this.bgObjectButtons.Clear();
			this.materialSlotInfos.Clear();
			this.objDic.Clear();
			this.objDic[0] = "- - -";
			this.UpdaateMaterial();
		}
		*/
		public bool bUpdateRequest = true;

		public bool reloadMaterialRequest;

		public bool configObjectMaterialsAutoUpdate;

		public bool isReflection;

		private CustomButton reloadMaidsButton;

		private CustomToggleButton AutoUpdateButton;

		public Dictionary<int, string> objDic;

		private List<CustomToggleButton> categoryButtons;

		private List<CustomToggleButton> bgObjectButtons;

		private List<ObjectPane> objectPanes;

		private Dictionary<string, MaterialInfo> materialSlotInfos;
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

			internal string <ShowPane>b__3_0(CustomToggleButton a)
			{
				return a.Text;
			}

			public static readonly ObjectWindow.<>c <>9 = new ObjectWindow.<>c();

			public static Func<CustomToggleButton, string> <>9__3_0;
		}
		*/
	}
}
