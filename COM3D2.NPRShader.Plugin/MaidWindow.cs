using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal class MaidWindow : ScrollablePane
	{
		public MaidWindow(int fontSize) : base(fontSize)
		{
		}

		public override void Awake()
		{
			try
			{
				this.maidDic = new Dictionary<string, string>();
				this.materialSlotInfos = new Dictionary<string, MaterialInfo>();
				this.maidManager = new MaidManager();
				this.maidManager.Find();
				this.maidDic[""] = "- - -";
				for (int i = 0; i < this.maidManager.listMaid.Count; i++)
				{
					this.maidDic[this.maidManager.listID[i]] = this.maidManager.listName[i];
				}
				this.maidListBox = new CustomComboBox(this.maidDic.Values.ToArray<string>());
				this.maidListBox.FontSize = this.FontSize;
				this.maidListBox.Text = "Maid";
				this.maidListBox.SelectedIndex = 0;
				CustomComboBox customComboBox = this.maidListBox;
				customComboBox.SelectedIndexChanged = (EventHandler)Delegate.Combine(customComboBox.SelectedIndexChanged, new EventHandler(this.ChangeMaid));
				this.ChildControls.Add(this.maidListBox);
				this.reloadMaidsButton = new CustomButton();
				this.reloadMaidsButton.Text = "再読込";
				CustomButton customButton = this.reloadMaidsButton;
				customButton.Click = (EventHandler)Delegate.Combine(customButton.Click, new EventHandler(delegate(object o, EventArgs e)
				{
					this.bUpdateRequest = true;
				}));
				this.ChildControls.Add(this.reloadMaidsButton);
				this.selectedMaidLabel = new CustomLabel();
				this.ChildControls.Add(this.selectedMaidLabel);
				this.categoryButtons = new List<CustomToggleButton>();
				this.materialPanes = new List<MaterialPane>();
			}
			catch (Exception ex)
			{
				Debug.LogError("MaidWindow.Awake() : " + ex.ToString());
			}
		}

		public override void Update()
		{
			try
			{
				if (this.bUpdateRequest)
				{
					this.maidManager.bUpdateRequest = true;
					this.materialPanes.Clear();
					this.categoryButtons.Clear();
					this.materialSlotInfos.Clear();
					this.maidDic.Clear();
					this.ChildControls.Clear();
					this.Awake();
					this.maidManager.bUpdateRequest = false;
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
			this.maidManager.Update();
			this.reloadMaidsButton.Left = this.Left + (float)ControlBase.FixedMargin;
			this.reloadMaidsButton.Top = this.Top + (float)ControlBase.FixedMargin;
			this.reloadMaidsButton.Width = (float)(this.FontSize * 6);
			this.reloadMaidsButton.Height = base.ControlHeight;
			this.reloadMaidsButton.OnGUI();
			this.reloadMaidsButton.Visible = true;
			GUIUtil.AddGUICheckbox(this, this.maidListBox, this.reloadMaidsButton);
			ControlBase reference = this.maidListBox;
			foreach (CustomToggleButton customToggleButton in this.categoryButtons)
			{
				bool flag = customToggleButton.Text.Contains(this.maidListBox.SelectedItem);
				GUIUtil.AddGUICheckbox(this, customToggleButton, reference);
				reference = customToggleButton;
				foreach (MaterialInfo materialInfo in this.materialSlotInfos.Values)
				{
					if (customToggleButton.Text == materialInfo.catName)
					{
						foreach (MaterialPane materialPane in this.materialPanes)
						{
							if (materialPane.Name == materialInfo.catName + materialInfo.slotName + materialInfo.materialNo.ToString())
							{
								if (customToggleButton.Value)
								{
									GUIUtil.AddGUICheckbox(this, materialPane, reference);
									reference = materialPane;
								}
								else
								{
									materialPane.Visible = false;
								}
							}
						}
					}
				}
			}
			this.Height = GUIUtil.GetHeightForParent(this);
		}

		private bool MatePaneVisibleToggle(bool b)
		{
			return b;
		}

		private void ChangeMaid(object sender, EventArgs args)
		{
			string guid = null;
			foreach (string text in this.maidDic.Keys)
			{
				bool flag = this.maidListBox.SelectedItem == this.maidDic[text];
				if (flag)
				{
					guid = text;
				}
			}
			foreach (MaterialPane item in this.materialPanes)
			{
				this.ChildControls.Remove(item);
			}
			foreach (CustomToggleButton item2 in this.categoryButtons)
			{
				this.ChildControls.Remove(item2);
			}
			this.materialPanes.Clear();
			this.categoryButtons.Clear();
			this.materialSlotInfos.Clear();
			bool flag2 = this.maidListBox.SelectedIndex == 0;
			if (!flag2)
			{
				CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
				Maid stockMaid = characterMgr.GetStockMaid(guid);
				bool flag3 = stockMaid != null && stockMaid.body0.trsHead != null && stockMaid.Visible;
				if (flag3)
				{
					bool flag4 = stockMaid != null && stockMaid.body0.trsHead != null;
					if (flag4)
					{
						bool visible = stockMaid.Visible;
					}
					try
					{
						bool flag5 = stockMaid != null && stockMaid.body0.trsHead != null && stockMaid.Visible;
						if (flag5)
						{
							foreach (MPN mpn in ConstantValues.PropParts_PBR.Values)
							{
								try
								{
									MaidProp prop = stockMaid.GetProp(mpn);
									bool flag6 = !string.IsNullOrEmpty(prop.strFileName) && !prop.strFileName.ToLower().EndsWith(".mod");
									if (flag6)
									{
										PBRModelInfo pbrmodelInfo = AssetLoader.LoadMenuPBR(prop.strFileName);
										bool flag7 = !string.IsNullOrEmpty(pbrmodelInfo.modelName);
										if (flag7)
										{
											CustomToggleButton customToggleButton = new CustomToggleButton(false, "toggle");
											customToggleButton.Text = pbrmodelInfo.partCategory.ToString();
											customToggleButton.FontSize = this.FontSize;
											this.categoryButtons.Add(customToggleButton);
											this.ChildControls.Add(customToggleButton);
											bool flag8 = !string.IsNullOrEmpty(pbrmodelInfo.modelName);
											if (flag8)
											{
												foreach (ModelInfo modelInfo in pbrmodelInfo.models)
												{
													int i = (int)TBody.hashSlotName[modelInfo.slotName];
													GameObject obj = stockMaid.body0.goSlot[i].obj;
													foreach (SkinnedMeshRenderer skinnedMeshRenderer in obj.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
													{
														bool flag9 = skinnedMeshRenderer != null && skinnedMeshRenderer.material != null;
														if (flag9)
														{
															Material[] array = new Material[skinnedMeshRenderer.materials.Length];
															array = new Material[skinnedMeshRenderer.materials.Length];
															for (int k = 0; k < array.Length; k++)
															{
																MaterialInfo value = new MaterialInfo(pbrmodelInfo.partCategory.ToString(), pbrmodelInfo.slotName, k, null);
																this.materialSlotInfos[pbrmodelInfo.partCategory.ToString() + pbrmodelInfo.slotName + k.ToString()] = value;
															}
														}
													}
												}
											}
										}
										bool flag10 = pbrmodelInfo.materialChanges == null;
										if (flag10)
										{
											continue;
										}
										using (List<MaterialChangeInfo>.Enumerator enumerator6 = pbrmodelInfo.materialChanges.GetEnumerator())
										{
											while (enumerator6.MoveNext())
											{
												MaterialChangeInfo materialChangeInfo = enumerator6.Current;
												MaterialInfo value2 = new MaterialInfo(pbrmodelInfo.partCategory.ToString(), materialChangeInfo.slotName, materialChangeInfo.materialNo, materialChangeInfo.filename);
												this.materialSlotInfos[pbrmodelInfo.partCategory.ToString() + materialChangeInfo.slotName + materialChangeInfo.materialNo.ToString()] = value2;
											}
											continue;
										}
									}
									Debug.Log("NPRShader: NPR非対応の形式のため処理をスキップ");
									Debug.Log(prop.name + ":" + prop.strFileName);
								}
								catch
								{
									Debug.LogError("foreach (MPN prop in ConstantValues.PropParts_PBR.Values)");
								}
							}
							foreach (MaterialInfo materialInfo in this.materialSlotInfos.Values)
							{
								MaterialPane item3 = new MaterialPane(this.FontSize, materialInfo.catName, materialInfo.slotName, materialInfo.materialNo, materialInfo.filename, stockMaid);
								this.materialPanes.Add(item3);
								this.ChildControls.Add(item3);
							}
						}
					}
					catch
					{
					}
				}
			}
		}
		/*
		[CompilerGenerated]
		private void <Awake>b__1_0(object o, EventArgs e)
		{
			this.bUpdateRequest = true;
		}
		*/
		public Dictionary<string, string> maidDic;

		public bool bUpdateRequest = true;

		public bool bUpdateRequestWithoutMaidManager;

		private CustomComboBox maidListBox;

		private CustomButton reloadMaidsButton;

		private CustomLabel selectedMaidLabel;

		private MaidManager maidManager;

		private List<MaterialPane> materialPanes;

		private List<CustomToggleButton> categoryButtons;

		private Dictionary<string, MaterialInfo> materialSlotInfos;
	}
}
