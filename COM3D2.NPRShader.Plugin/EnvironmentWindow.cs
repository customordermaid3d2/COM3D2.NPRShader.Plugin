using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace COM3D2.NPRShader.Plugin
{
	internal class EnvironmentWindow : ScrollablePane
	{
		public EnvironmentWindow(int fontSize) : base(fontSize)
		{
		}

		public override void Awake()
		{
			try
			{
				this.light = new GameObject("NPR_Emissiv_Light", new Type[]
				{
					typeof(Light)
				})
				{
					hideFlags = HideFlags.HideAndDontSave
				}.GetComponent<Light>();
				this.light.color = Color.black;
				this.light.type = LightType.Directional;
				this.light.intensity = 0.001f;
				this.light.shadows = LightShadows.None;
				this.probe = GameMain.Instance.MainCamera.gameObject.GetOrAddComponent<ReflectionProbeController>();
				this.reloadMaidsButton = new CustomButton();
				this.reloadMaidsButton.Text = "更新";
				CustomButton customButton = this.reloadMaidsButton;
				customButton.Click = (EventHandler)Delegate.Combine(customButton.Click, new EventHandler(delegate(object o, EventArgs e)
				{
					this.bUpdateCubeMapRequest = true;
					this.UpdateCubeMap();
					this.bUpdateCubeMapRequest = false;
					this.probe.RenderProbe();
				}));
				this.ChildControls.Add(this.reloadMaidsButton);
				this.reflectionProbeButton = new CustomToggleButton(false, "toggle");
				this.reflectionProbeButton.FontSize = this.FontSize;
				this.reflectionProbeButton.Text = "リフレクション自動更新有効";
				CustomToggleButton customToggleButton = this.reflectionProbeButton;
				customToggleButton.CheckedChanged = (EventHandler)Delegate.Combine(customToggleButton.CheckedChanged, new EventHandler(this.ReflectionProbeButton_CheckedChanged));
				this.ChildControls.Add(this.reflectionProbeButton);
				this.environmentMapButton = new CustomToggleButton(false, "toggle");
				this.environmentMapButton.FontSize = this.FontSize;
				this.environmentMapButton.Text = "環境マップ自動更新有効";
				CustomToggleButton customToggleButton2 = this.environmentMapButton;
				customToggleButton2.CheckedChanged = (EventHandler)Delegate.Combine(customToggleButton2.CheckedChanged, new EventHandler(this.EnvironmentMapButtonButton_CheckedChanged));
				this.ChildControls.Add(this.environmentMapButton);
				this.m_ShadowmapResBox = new CustomComboBox(EnvironmentWindow.RESOLUTIONS);
				this.m_ShadowmapResBox.Text = "環境マップ解像度";
				this.m_ShadowmapResBox.SelectedIndex = 1;
				CustomComboBox shadowmapResBox = this.m_ShadowmapResBox;
				shadowmapResBox.SelectedIndexChanged = (EventHandler)Delegate.Combine(shadowmapResBox.SelectedIndexChanged, new EventHandler(this.ChangeShadowmapRes));
				this.ChildControls.Add(this.m_ShadowmapResBox);
				this.showSkyboxButton = new CustomToggleButton(false, "toggle");
				this.showSkyboxButton.FontSize = this.FontSize;
				this.showSkyboxButton.Text = "環境マップ表示";
				CustomToggleButton customToggleButton3 = this.showSkyboxButton;
				customToggleButton3.CheckedChanged = (EventHandler)Delegate.Combine(customToggleButton3.CheckedChanged, new EventHandler(this.showSkyboxButton_CheckedChanged));
				this.ChildControls.Add(this.showSkyboxButton);
				this.CubemapTexturePicker = new CustomCubemapPicker(this.cubemap, null, ConstantValues.CubemapImageDir);
				this.CubemapTexturePicker.Text = "Cubemap Images";
				CustomCubemapPicker cubemapTexturePicker = this.CubemapTexturePicker;
				cubemapTexturePicker.TextureChanged = (EventHandler)Delegate.Combine(cubemapTexturePicker.TextureChanged, new EventHandler(this.CubemapTexturePicker_CheckedChanged));
				this.ChildControls.Add(this.CubemapTexturePicker);
				this.ChangePointLightDirButton = new CustomToggleButton(false, "toggle");
				this.ChangePointLightDirButton.FontSize = this.FontSize;
				this.ChangePointLightDirButton.Text = "ダンス用ポイントライト補正有効";
				CustomToggleButton changePointLightDirButton = this.ChangePointLightDirButton;
				changePointLightDirButton.CheckedChanged = (EventHandler)Delegate.Combine(changePointLightDirButton.CheckedChanged, new EventHandler(this.ChangeLightDirButton_CheckedChanged));
				this.ChildControls.Add(this.ChangePointLightDirButton);
				this.ChangeSpotLightDirButton = new CustomToggleButton(false, "toggle");
				this.ChangeSpotLightDirButton.FontSize = this.FontSize;
				this.ChangeSpotLightDirButton.Text = "ダンス用スポットライト補正有効";
				CustomToggleButton changeSpotLightDirButton = this.ChangeSpotLightDirButton;
				changeSpotLightDirButton.CheckedChanged = (EventHandler)Delegate.Combine(changeSpotLightDirButton.CheckedChanged, new EventHandler(this.ChangeLightDirButton_CheckedChanged));
				this.ChildControls.Add(this.ChangeSpotLightDirButton);
			}
			catch (Exception ex)
			{
				Debug.LogError("EnvironmentWindow.Awake(): " + ex.ToString());
			}
		}

		public override void Update()
		{
		}

		public void LateUpdate()
		{
			try
			{
				if (this.reflectionProbeButton.Value)
				{
					this.probe.LateUpdate();
				}
				if (this.bUpdateCubeMapRequest && this.environmentMapButton.Value)
				{
					if (!this.OVRMode)
					{
						this.probe.probe.mode = ReflectionProbeMode.Realtime;
					}
					else
					{
						this.probe.probe.mode = ReflectionProbeMode.Realtime;
					}
					NPRShader.bUpdateCubeMapRequest = false;
					this.UpdateCubeMap();
					this.bUpdateCubeMapRequest = false;
				}
				if (this.bRestorSkyboxRequest)
				{
					this.RestoreSkybox();
					this.bRestorSkyboxRequest = false;
					NPRShader.bRestorSkyboxRequest = false;
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
			GUIUtil.AddGUICheckbox(this, this.reflectionProbeButton, this.reloadMaidsButton);
			GUIUtil.AddGUICheckbox(this, this.environmentMapButton, this.reflectionProbeButton);
			GUIUtil.AddGUICheckbox(this, this.m_ShadowmapResBox, this.environmentMapButton);
			GUIUtil.AddGUICheckbox(this, this.showSkyboxButton, this.m_ShadowmapResBox);
			GUIUtil.AddGUICheckbox(this, this.ChangePointLightDirButton, this.showSkyboxButton);
			GUIUtil.AddGUICheckbox(this, this.ChangeSpotLightDirButton, this.ChangePointLightDirButton);
			this.Height = GUIUtil.GetHeightForParent(this);
		}

		private void ReflectionProbeButton_CheckedChanged(object sender, EventArgs args)
		{
			this.probe.enabled = this.reflectionProbeButton.Value;
		}

		private void EnvironmentMapButtonButton_CheckedChanged(object sender, EventArgs args)
		{
		}

		public void UpdateCubeMap()
		{
			foreach (AssetBundle assetBundle in Util.ALLShaders)
			{
				if (this.skybox == null)
				{
					try
					{
						this.skybox = (assetBundle.LoadAsset("NPRSkybox", typeof(Material)) as Material);
						this.skyboxBackup = RenderSettings.skybox;
					}
					catch
					{
					}
				}
			}
			if (this.environmentMapButton.Value || this.bUpdateCubeMapRequest)
			{
				this.skybox.SetTexture("_Tex", CubemapGenerate.Generate(this.probe.probe.resolution));
				RenderSettings.ambientMode = AmbientMode.Skybox;
				RenderSettings.skybox = this.skybox;
				return;
			}
			RenderSettings.skybox = this.skyboxBackup;
		}

		public void RestoreSkybox()
		{
			Debug.Log("RestoreSkybox");
			RenderSettings.skybox = this.skyboxBackup;
		}

		public void ChangeShadowmapRes(object sender, EventArgs args)
		{
			this.m_lightShaftsChangeShadowmapRes();
			this.wasChanged = true;
		}

		public void m_lightShaftsChangeShadowmapRes()
		{
			string selectedItem = this.m_ShadowmapResBox.SelectedItem;
			if (selectedItem == "VeryLow")
			{
				this.probe.probe.resolution = 128;
				return;
			}
			if (selectedItem == "Low")
			{
				this.probe.probe.resolution = 256;
				return;
			}
			if (selectedItem == "Medium")
			{
				this.probe.probe.resolution = 512;
				return;
			}
			if (selectedItem == "High")
			{
				this.probe.probe.resolution = 1024;
				return;
			}
			if (!(selectedItem == "VeryHigh"))
			{
				return;
			}
			this.probe.probe.resolution = 2048;
		}

		public void showSkyboxButton_CheckedChanged(object sender, EventArgs args)
		{
			Camera camera = GameMain.Instance.MainCamera.camera;
			if (this.showSkyboxButton.Value)
			{
				camera.clearFlags = CameraClearFlags.Skybox;
				return;
			}
			camera.clearFlags = CameraClearFlags.Color;
		}

		public void CubemapTexturePicker_CheckedChanged(object sender, EventArgs args)
		{
			if (this.cubemap != null && this.skybox != null)
			{
				this.skybox.SetTexture("_Tex", this.CubemapTexturePicker.Value);
			}
		}

		private void ChangeLightDirButton_CheckedChanged(object sender, EventArgs args)
		{
			CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
			List<Maid> stockMaidList = characterMgr.GetStockMaidList();
			foreach (Maid maid in stockMaidList)
			{
				bool flag = maid != null && maid.body0.trsHead != null && maid.Visible;
				if (flag)
				{
					bool flag2 = maid != null && maid.body0.trsHead != null;
					if (flag2)
					{
						bool visible = maid.Visible;
					}
					try
					{
						bool flag3 = maid != null && maid.body0.trsHead != null && maid.Visible;
						if (flag3)
						{
							foreach (MPN mpn in ConstantValues.PropParts_PBR.Values)
							{
								try
								{
									MaidProp prop = maid.GetProp(mpn);
									bool flag4 = !string.IsNullOrEmpty(prop.strFileName) && !prop.strFileName.ToLower().EndsWith(".mod");
									if (flag4)
									{
										PBRModelInfo pbrmodelInfo = AssetLoader.LoadMenuPBR(prop.strFileName);
										bool flag5 = !string.IsNullOrEmpty(pbrmodelInfo.modelName) && !string.IsNullOrEmpty(pbrmodelInfo.modelName);
										if (flag5)
										{
											foreach (ModelInfo modelInfo in pbrmodelInfo.models)
											{
												int i = (int)TBody.hashSlotName[modelInfo.slotName];
												GameObject obj = maid.body0.goSlot[i].obj;
												foreach (SkinnedMeshRenderer skinnedMeshRenderer in obj.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
												{
													bool flag6 = skinnedMeshRenderer != null && skinnedMeshRenderer.material != null;
													if (flag6)
													{
														Material[] array = new Material[skinnedMeshRenderer.materials.Length];
														array = new Material[skinnedMeshRenderer.materials.Length];
														for (int k = 0; k < array.Length; k++)
														{
															array[k] = new Material(skinnedMeshRenderer.materials[k]);
															bool flag7 = array[k].shader.name.Contains("com3d2");
															if (flag7)
															{
																bool value = this.ChangePointLightDirButton.Value;
																if (value)
																{
																	array[k].SetFloat("_CUSTOMPOINTLIGHTDIR", 1f);
																}
																else
																{
																	array[k].SetFloat("_CUSTOMPOINTLIGHTDIR", 0f);
																}
																bool value2 = this.ChangeSpotLightDirButton.Value;
																if (value2)
																{
																	array[k].SetFloat("_CUSTOMSPOTLIGHTDIR", 1f);
																}
																else
																{
																	array[k].SetFloat("_CUSTOMSPOTLIGHTDIR", 0f);
																}
															}
														}
														for (int l = 0; l < skinnedMeshRenderer.materials.Length; l++)
														{
															UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer.materials[l]);
														}
														skinnedMeshRenderer.materials = array;
													}
												}
											}
										}
									}
								}
								catch
								{
								}
							}
						}
					}
					catch
					{
					}
				}
			}
			foreach (GameObject gameObject in UnityEngine.Object.FindObjectsOfType<GameObject>())
			{
				bool flag8 = gameObject.name.Length >= 6 && gameObject.name.ToLower().EndsWith(".menu");
				if (flag8)
				{
					string menuFileName = gameObject.name.Replace("[SC] ", "");
					PBRModelInfo pbrmodelInfo2 = AssetLoader.LoadMenuPBR(menuFileName);
					bool flag9 = !string.IsNullOrEmpty(pbrmodelInfo2.modelName);
					if (flag9)
					{
						foreach (ModelInfo modelInfo2 in pbrmodelInfo2.models)
						{
							int num = (int)TBody.hashSlotName[modelInfo2.slotName];
							try
							{
								foreach (SkinnedMeshRenderer skinnedMeshRenderer2 in gameObject.transform.GetComponentsInChildren<SkinnedMeshRenderer>(true))
								{
									bool flag10 = skinnedMeshRenderer2 != null;
									if (flag10)
									{
										Material[] array3 = new Material[skinnedMeshRenderer2.materials.Length];
										array3 = new Material[skinnedMeshRenderer2.materials.Length];
										for (int num2 = 0; num2 < array3.Length; num2++)
										{
											array3[num2] = new Material(skinnedMeshRenderer2.materials[num2]);
											bool flag11 = array3[num2].shader.name.Contains("com3d2");
											if (flag11)
											{
												bool value3 = this.ChangePointLightDirButton.Value;
												if (value3)
												{
													array3[num2].SetFloat("_CUSTOMPOINTLIGHTDIR", 1f);
												}
												else
												{
													array3[num2].SetFloat("_CUSTOMPOINTLIGHTDIR", 0f);
												}
												bool value4 = this.ChangeSpotLightDirButton.Value;
												if (value4)
												{
													array3[num2].SetFloat("_CUSTOMSPOTLIGHTDIR", 1f);
												}
												else
												{
													array3[num2].SetFloat("_CUSTOMSPOTLIGHTDIR", 0f);
												}
											}
										}
										for (int num3 = 0; num3 < skinnedMeshRenderer2.materials.Length; num3++)
										{
											UnityEngine.Object.DestroyImmediate(skinnedMeshRenderer2.materials[num3]);
										}
										skinnedMeshRenderer2.materials = array3;
									}
								}
							}
							catch (Exception ex)
							{
								Debug.LogError(ex.ToString());
							}
						}
					}
				}
			}
		}

		public bool WasChanged
		{
			get
			{
				return this.wasChanged;
			}
			set
			{
				this.wasChanged = value;
			}
		}

		public void init()
		{
			int num = 0;
			bool flag = false;
			foreach (string b in EnvironmentWindow.RESOLUTIONS)
			{
				if (this.configEnvMapResolution == b)
				{
					flag = true;
					break;
				}
				num++;
			}
			if (!flag)
			{
				num = 1;
			}
			this.m_ShadowmapResBox.SelectedIndex = num;
			this.reflectionProbeButton.Value = this.configReflectionProbeAutoUpdate;
			this.environmentMapButton.Value = this.configEnvMapAutoUpdate;
		}

		public bool isDance
		{
			get
			{
				return this._isDance;
			}
			set
			{
				this._isDance = value;
				this.ChangePointLightDirButton.Value = value;
				this.ChangeSpotLightDirButton.Value = value;
			}
		}

		static EnvironmentWindow()
		{
		}
		/*
		[CompilerGenerated]
		private void <Awake>b__1_0(object o, EventArgs e)
		{
			this.bUpdateCubeMapRequest = true;
			this.UpdateCubeMap();
			this.bUpdateCubeMapRequest = false;
			this.probe.RenderProbe();
		}
		*/
		public Material skybox;

		public Material skyboxBackup;

		private Cubemap cubemap = new Cubemap(16, TextureFormat.RGBA32, true);

		private static readonly string[] RESOLUTIONS = new string[]
		{
			"VeryLow",
			"Low",
			"Medium",
			"High",
			"VeryHigh"
		};

		private bool wasChanged;

		private CustomComboBox m_ShadowmapResBox;

		private Light light;

		public bool bUpdateRequest = true;

		public bool bUpdateCubeMapRequest;

		public bool bRestorSkyboxRequest;

		private ReflectionProbeController probe;

		private ReflectionProbeController vrprobe;

		private CustomButton reloadMaidsButton;

		private CustomToggleButton reflectionProbeButton;

		private CustomToggleButton environmentMapButton;

		private CustomToggleButton showSkyboxButton;

		private CustomToggleButton ChangePointLightDirButton;

		private CustomToggleButton ChangeSpotLightDirButton;

		private CustomCubemapPicker CubemapTexturePicker;

		public bool _isDance;

		public bool isReflection;

		public bool OVRMode;

		public bool bClearProbe;

		public string configEnvMapResolution = string.Empty;

		public bool configEnvMapAutoUpdate;

		public bool configReflectionProbeAutoUpdate;
	}
}
