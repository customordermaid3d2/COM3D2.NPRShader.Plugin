using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using BepInEx;
using BepInEx.Configuration;
using GearMenu;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
//using UnityInjector;
//using UnityInjector.Attributes;

namespace COM3D2.NPRShader.Plugin
{
	[BepInProcess("COM3D2x64")]
	[BepInProcess("COM3D2OHx64")]
	[BepInPlugin("COM3D2.NPRShader.Plugin", "NPRShader by Lilly", "0.9.2.2")]
	//[PluginName("COM3D2.NPRShader.Plugin")]
	//[PluginVersion("0.9.2.2")]
	public class NPRShader : BaseUnityPlugin
	{

		public static NPRShader nPRShader;

		public void Awake()
		{
			nPRShader = this;
			try
			{
				UnityEngine.Object.DontDestroyOnLoad(this);
				this.ReadPluginPreferences();
				Util.LoadALLShaders();
				NPRShader.sID = new Dictionary<string, int>();
				this.InitGearMenu();
				SceneManager.sceneLoaded += this.OnSceneLoaded;
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
		{
			NPRShader.isDance = scene.name.Contains("SceneDance_");
			this.danceMain = UnityEngine.Object.FindObjectOfType<DanceMain>();
			NPRShader.isKaraoke = false;
			NPRShader.sID.Clear();
			this.maidView.bUpdateRequest = true;
			this.maidView.Update();
			this.objView.bUpdateRequest = true;
			this.objView.Update();
			this.envView.isDance = NPRShader.isDance;
			NPRShader.reloadMaterialRequest = true;
			NPRShader.bUpdateCubeMapRequest = true;
			this.envView.bClearProbe = true;
			RenderSettings.ambientMode = AmbientMode.Skybox;
			RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
			RenderSettings.reflectionBounces = 2;
			this.isOVRDance = false;
			Util.OVRMode = false;
			NPRShader.OVRMode = false;
			this.envView.Update();
		}

		private void gearButtonPressed(GameObject goButton)
		{
			if (this.modeSelectView.SelectedMode == ConstantValues.EditMode.Maid)
			{
				this.modeSelectView.SelectedMode = ConstantValues.EditMode.Disable;
				return;
			}
			this.modeSelectView.SelectedMode = ConstantValues.EditMode.Maid;
		}

		public void Update()
		{
			try
			{
				if (!this.initialized)
				{
					this.Initialize();
					this.initialized = true;
					this.maidView.Update();
					this.objView.Update();
					this.envView.Update();
				}
				if (this.modeSelectView.SelectedMode == ConstantValues.EditMode.Maid)
				{
					this.maidView.Update();
				}
				if (this.modeSelectView.SelectedMode == ConstantValues.EditMode.Environment)
				{
					this.envView.Update();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void LateUpdate()
		{
			try
			{
				if (NPRShader.reloadMaidViewRequest)
				{
					this.maidView.bUpdateRequest = NPRShader.reloadMaidViewRequest;
					this.maidView.Update();
					NPRShader.reloadMaidViewRequest = false;
				}
				if (NPRShader.reloadMaterialRequest)
				{
					this.objView.reloadMaterialRequest = NPRShader.reloadMaterialRequest;
					this.objView.reloadMaterial();
				}
				if (NPRShader.bRestorSkyboxRequest)
				{
					this.envView.bRestorSkyboxRequest = NPRShader.bRestorSkyboxRequest;
				}
				if (NPRShader.bUpdateCubeMapRequest)
				{
					this.envView.bUpdateCubeMapRequest = NPRShader.bUpdateCubeMapRequest;
				}
				this.envView.LateUpdate();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void OnGUI()
		{
			try
			{
				if (this.modeSelectView.SelectedMode != ConstantValues.EditMode.Disable)
				{
					float num = (float)(Screen.width / 4 - ControlBase.FixedMargin * 2);
					Rect rect = new Rect((float)Screen.width - num, (float)(Screen.height / 15 + ControlBase.FixedMargin), (float)(Screen.width / 5 - Screen.width / 65), (float)(Screen.height - Screen.height / 5));
					this.modeSelectView.OnGUI();
					if (this.modeSelectView.SelectedMode == ConstantValues.EditMode.Maid)
					{
						this.maidView.rectGui.x = this.modeSelectView.Left;
						this.maidView.rectGui.y = this.modeSelectView.Top + this.modeSelectView.Height + (float)ControlBase.FixedMargin;
						this.maidView.Left = 0f;
						this.maidView.Top = 0f;
						this.maidView.Width = this.modeSelectView.Width;
						this.maidView.rectGui.width = this.maidView.Width;
						this.maidView.OnGUI();
					}
					else if (this.modeSelectView.SelectedMode == ConstantValues.EditMode.Object)
					{
						this.objView.rectGui.x = this.modeSelectView.Left;
						this.objView.rectGui.y = this.modeSelectView.Top + this.modeSelectView.Height + (float)ControlBase.FixedMargin;
						this.objView.Left = 0f;
						this.objView.Top = 0f;
						this.objView.Width = this.modeSelectView.Width;
						this.objView.rectGui.width = this.objView.Width;
						this.objView.OnGUI();
					}
					else if (this.modeSelectView.SelectedMode == ConstantValues.EditMode.Environment)
					{
						this.envView.rectGui.x = this.modeSelectView.Left;
						this.envView.rectGui.y = this.modeSelectView.Top + this.modeSelectView.Height + (float)ControlBase.FixedMargin;
						this.envView.Left = 0f;
						this.envView.Top = 0f;
						this.envView.Width = this.modeSelectView.Width;
						this.envView.rectGui.width = this.envView.Width;
						this.envView.OnGUI();
					}
					GlobalColorPicker.Update();
					GlobalComboBox.Update();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void ResetOBJView()
		{
			this.objView.bUpdateRequest = true;
			this.objView.Update();
		}

		private void Initialize()
		{
			try
			{
				int fontSize;
				if (Screen.width < 1366)
				{
					fontSize = 10;
				}
				else
				{
					fontSize = 11;
				}
				float num = (float)(Screen.width / 4 - ControlBase.FixedMargin * 2);
				this.modeSelectView = new ModeSelectWindow(fontSize);
				this.modeSelectView.Text = string.Format("{0} ver.{1}", NPRShader.GetPluginName(), NPRShader.GetPluginVersion());
				this.modeSelectView.Left = (float)Screen.width - num - (float)ControlBase.FixedMargin;
				this.modeSelectView.Top = (float)(ControlBase.FixedMargin * 15);
				this.modeSelectView.Width = num;
				this.maidView = new MaidWindow(fontSize);
				this.objView = new ObjectWindow(fontSize);
				this.envView = new EnvironmentWindow(fontSize);
				this.objView.configObjectMaterialsAutoUpdate = this.configObjectMaterialsAutoUpdate.Value;
				this.objView.init();
				this.envView.configEnvMapResolution = this.configEnvMapResolution.Value;
				this.envView.configEnvMapAutoUpdate = this.configEnvMapAutoUpdate.Value;
				this.envView.configReflectionProbeAutoUpdate = this.configReflectionProbeAutoUpdate.Value;
				this.envView.init();
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		private void InitGearMenu()
		{
			this.gear = Buttons.Add("NPRShader", "NPRShader", ConstantValues.byteIconPng, new Action<GameObject>(this.gearButtonPressed));
			Buttons.SetText(this.gear, "NPRShader");
		}

		public static string GetPluginName()
		{
			string result = string.Empty;
			try
			{
				BepInPlugin pluginNameAttribute = Attribute.GetCustomAttribute(typeof(NPRShader), typeof(BepInPlugin)) as BepInPlugin;
				if (pluginNameAttribute != null)
				{
					result = pluginNameAttribute.Name;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
			return result;
		}

		public static string GetPluginVersion()
		{
			string result = string.Empty;
			try
			{
				BepInPlugin pluginVersionAttribute = Attribute.GetCustomAttribute(typeof(NPRShader), typeof(BepInPlugin)) as BepInPlugin;
				if (pluginVersionAttribute != null)
				{
					result = pluginVersionAttribute.Version.ToString();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
			return result;
		}

		private void ReadPluginPreferences()
		{
			this.configEnvMapResolution = Config.Bind("Config", "EnvironmentMapResolution", "VeryLow");
			this.configReflectionProbeAutoUpdate = Config.Bind("Config", "ReflectionProbeAutoUpdate", true);
			this.configEnvMapAutoUpdate = Config.Bind("Config", "EnvironmentMapAutoUpdate", false);
			this.configObjectMaterialsAutoUpdate = Config.Bind("Config", "ObjectMaterialsAutoUpdate", false);
		}
		/*
		private string GetPreferences(string section, string key, string defaultValue)
		{
			if (!base.Preferences.HasSection(section) || !base.Preferences[section].HasKey(key) || string.IsNullOrEmpty(base.Preferences[section][key].Value))
			{
				base.Preferences[section][key].Value = defaultValue;
				base.SaveConfig();
			}
			return base.Preferences[section][key].Value;
		}

		private bool GetPreferences(string section, string key, bool defaultValue)
		{
			if (!base.Preferences.HasSection(section) || !base.Preferences[section].HasKey(key) || string.IsNullOrEmpty(base.Preferences[section][key].Value))
			{
				base.Preferences[section][key].Value = defaultValue.ToString();
				base.SaveConfig();
			}
			bool result = defaultValue;
			bool.TryParse(base.Preferences[section][key].Value, out result);
			return result;
		}

		private int GetPreferences(string section, string key, int defaultValue)
		{
			if (!base.Preferences.HasSection(section) || !base.Preferences[section].HasKey(key) || string.IsNullOrEmpty(base.Preferences[section][key].Value))
			{
				base.Preferences[section][key].Value = defaultValue.ToString();
				base.SaveConfig();
			}
			int result = defaultValue;
			int.TryParse(base.Preferences[section][key].Value, out result);
			return result;
		}

		private float GetPreferences(string section, string key, float defaultValue)
		{
			if (!base.Preferences.HasSection(section) || !base.Preferences[section].HasKey(key) || string.IsNullOrEmpty(base.Preferences[section][key].Value))
			{
				base.Preferences[section][key].Value = defaultValue.ToString();
				base.SaveConfig();
			}
			float result = defaultValue;
			float.TryParse(base.Preferences[section][key].Value, out result);
			return result;
		}
		*/
		public static bool IsValid(Maid m)
		{
			return m != null && m.body0 != null && m.Visible;
		}

		public static Material LoadMaterial(string f_strFileName, TBodySkin bodyskin, Material existmat = null)
		{
			byte[] array = null;
			try
			{
				using (AFileBase afileBase = GameUty.FileOpen(f_strFileName, null))
				{
					NDebug.Assert(afileBase.IsValid(), "LoadMaterial マテリアルコンテナが読めません。 :" + f_strFileName);
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
					f_strFileName,
					" : ",
					ex.Message,
					" : StackTrace ：\n",
					ex.StackTrace
				}));
				throw ex;
			}
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(array), Encoding.UTF8);
			string text = binaryReader.ReadString();
			if (text != "CM3D2_MATERIAL")
			{
				NDebug.Assert("ProcScriptBin 例外 : ヘッダーファイルが不正です。" + text, false);
			}
			int num = binaryReader.ReadInt32();
			string text2 = binaryReader.ReadString();
			string text3 = "_NPRMAT_";
			Regex regex = new Regex(text3.ToLower());
			regex = new Regex(text3.ToLower());
			string[] source = regex.Split(f_strFileName.ToLower());
			Material result = NPRShader.ReadMaterial(binaryReader, source.Last<string>().Replace(".mate", ""), bodyskin, existmat);
			binaryReader.Close();
			return result;
		}

		private static Material ReadMaterial(BinaryReader r, string shaderMatName, TBodySkin bodyskin = null, Material existmat = null)
		{
			if (AssetLoader.m_hashPriorityMaterials == null)
			{
				AssetLoader.m_hashPriorityMaterials = new Dictionary<int, KeyValuePair<string, float>>();
				string[] list = GameUty.FileSystem.GetList("prioritymaterial", AFileSystemBase.ListType.AllFile);
				if (list != null && list.Length != 0)
				{
					for (int i = 0; i < list.Length; i++)
					{
						if (Path.GetExtension(list[i]) == ".pmat")
						{
							string text = list[i];
							using (AFileBase afileBase = GameUty.FileOpen(text, null))
							{
								if (afileBase.IsValid())
								{
									byte[] buffer = afileBase.ReadAll();
									using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer), Encoding.UTF8))
									{
										string a = binaryReader.ReadString();
										if (a != "CM3D2_PMATERIAL")
										{
											NDebug.Assert("ヘッダーエラー\n" + text, false);
										}
										int num = binaryReader.ReadInt32();
										int key = binaryReader.ReadInt32();
										string key2 = binaryReader.ReadString();
										float value = binaryReader.ReadSingle();
										NDebug.Assert(!AssetLoader.m_hashPriorityMaterials.ContainsKey(key), "すでにハッシュが登録されています");
										AssetLoader.m_hashPriorityMaterials.Add(key, new KeyValuePair<string, float>(key2, value));
										goto IL_137;
									}
								}
								Debug.LogError(text + "を開けませんでした");
							}
						}
						IL_137:;
					}
				}
				string[] fileListAtExtension = GameUty.FileSystemMod.GetFileListAtExtension(".pmat");
				if (fileListAtExtension != null && fileListAtExtension.Length != 0)
				{
					for (int j = 0; j < fileListAtExtension.Length; j++)
					{
						try
						{
							if (Path.GetExtension(fileListAtExtension[j]) == ".pmat")
							{
								string text2 = fileListAtExtension[j];
								using (AFileBase afileBase2 = GameUty.FileOpen(text2, null))
								{
									if (afileBase2.IsValid())
									{
										byte[] buffer2 = afileBase2.ReadAll();
										using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(buffer2), Encoding.UTF8))
										{
											string a2 = binaryReader2.ReadString();
											if (a2 != "CM3D2_PMATERIAL")
											{
												NDebug.Assert("ヘッダーエラー\n" + text2, false);
											}
											int num2 = binaryReader2.ReadInt32();
											int num3 = binaryReader2.ReadInt32();
											string text3 = binaryReader2.ReadString();
											float value2 = binaryReader2.ReadSingle();
											int hashCode = text3.GetHashCode();
											if (Path.GetFileNameWithoutExtension(text2).ToLower() != text3.ToLower())
											{
												Debug.LogError(".pmatファイルとpmat内のマテリアル名の不一致を検出");
												Debug.Log("pmatファイル: " + text2);
												Debug.Log("マテリアル名: " + text3);
											}
											if (num3 != hashCode)
											{
												Debug.LogWarning("不正なHash値を検出、修正処理を実行: " + num3.ToString());
												Debug.Log(text2);
												num3 = hashCode;
											}
											if (AssetLoader.m_hashPriorityMaterials.ContainsKey(num3))
											{
												Debug.LogWarning("pmatの重複を検出、上書き処理を実行: " + num3.ToString());
												Debug.Log(text2);
											}
											AssetLoader.m_hashPriorityMaterials[num3] = new KeyValuePair<string, float>(text3, value2);
											goto IL_2E5;
										}
									}
									Debug.LogError(text2 + "を開けませんでした");
									IL_2E5:;
								}
							}
						}
						catch (Exception ex)
						{
							Debug.LogError(" Error: " + ex.ToString());
							Debug.LogError(" Error: " + fileListAtExtension[j]);
						}
					}
				}
			}
			string name = r.ReadString();
			string text4 = r.ReadString();
			string text5 = r.ReadString();
			Material material = null;
			if (shaderMatName != null)
			{
				Material material2 = null;
				if (!AssetLoader.m_dicCacheMaterial.TryGetValue(shaderMatName, out material2))
				{
					foreach (AssetBundle assetBundle in Util.ALLShaders)
					{
						if (material2 == null)
						{
							try
							{
								material2 = (assetBundle.LoadAsset("com3d2mod_" + shaderMatName, typeof(Material)) as Material);
							}
							catch
							{
							}
						}
					}
					if (material2 == null)
					{
						Debug.LogError("DefMaterialが見つかりません。");
					}
					AssetLoader.m_dicCacheMaterial[shaderMatName] = material2;
				}
				material = UnityEngine.Object.Instantiate<Material>(material2);
				if (bodyskin != null)
				{
					material.name = name;
					bodyskin.listDEL.Add(material);
				}
			}
			material.name = name;
			int hashCode2 = material.name.GetHashCode();
			if (AssetLoader.m_hashPriorityMaterials != null && AssetLoader.m_hashPriorityMaterials.ContainsKey(hashCode2))
			{
				KeyValuePair<string, float> keyValuePair = AssetLoader.m_hashPriorityMaterials[hashCode2];
				if (keyValuePair.Key == material.name)
				{
					material.SetFloat("_SetManualRenderQueue", keyValuePair.Value);
					material.renderQueue = (int)keyValuePair.Value;
				}
			}
			if (NPRShader.isDance)
			{
				material.SetFloat("_CUSTOMSPOTLIGHTDIR", 1f);
				material.SetFloat("_CUSTOMPOINTLIGHTDIR", 1f);
			}
			else
			{
				material.SetFloat("_CUSTOMSPOTLIGHTDIR", 0f);
				material.SetFloat("_CUSTOMPOINTLIGHTDIR", 0f);
			}
			for (;;)
			{
				string text6 = r.ReadString();
				if (text6 == "end")
				{
					break;
				}
				string text7 = r.ReadString();
				if (text6 == "tex")
				{
					string a3 = r.ReadString();
					if (a3 == "null")
					{
						material.SetTexture(text7, null);
					}
					else if (a3 == "tex2d")
					{
						string text8 = r.ReadString();
						string text9 = r.ReadString();
						Texture2D texture2D = ImportCM.CreateTexture(text8 + ".tex");
						texture2D.name = text8;
						if (!text7.Contains("_EmissionToon") && text7.ToLower().Contains("toon"))
						{
							texture2D.wrapMode = TextureWrapMode.Clamp;
						}
						else
						{
							texture2D.wrapMode = TextureWrapMode.Repeat;
						}
						material.SetTexture(text7, texture2D);
						if (bodyskin != null)
						{
							bodyskin.listDEL.Add(texture2D);
						}
						Vector2 value3;
						value3.x = r.ReadSingle();
						value3.y = r.ReadSingle();
						material.SetTextureOffset(text7, value3);
						Vector2 value4;
						value4.x = r.ReadSingle();
						value4.y = r.ReadSingle();
						material.SetTextureScale(text7, value4);
					}
					else if (a3 == "texRT")
					{
						string text10 = r.ReadString();
						string text11 = r.ReadString();
					}
				}
				else if (text6 == "col")
				{
					Color value5;
					value5.r = r.ReadSingle();
					value5.g = r.ReadSingle();
					value5.b = r.ReadSingle();
					value5.a = r.ReadSingle();
					material.SetColor(text7, value5);
				}
				else if (text6 == "vec")
				{
					Vector4 value6;
					value6.x = r.ReadSingle();
					value6.y = r.ReadSingle();
					value6.z = r.ReadSingle();
					value6.w = r.ReadSingle();
					material.SetVector(text7, value6);
				}
				else if (text6 == "f")
				{
					float num4 = r.ReadSingle();
					if ((text7 == "_StencilID" || text7 == "_StencilID2") && (bodyskin.SlotId.ToString() == "head" || bodyskin.SlotId.ToString() == "hairF" || bodyskin.SlotId.ToString() == "hairS"))
					{
						int num5 = 0;
						if (text7 == "_StencilID2")
						{
							num5 = 1;
						}
						CharacterMgr characterMgr = GameMain.Instance.CharacterMgr;
						string guid = bodyskin.body.maid.status.guid;
						if (!NPRShader.sID.ContainsKey(guid) && NPRShader.IsValid(bodyskin.body.maid) && characterMgr.GetStockMaid(guid) && NPRShader.sID.Count<KeyValuePair<string, int>>() < 32)
						{
							for (int k = 0; k < characterMgr.GetStockMaidCount(); k++)
							{
								if (characterMgr.GetStockMaid(k) != null && characterMgr.GetStockMaid(k).status.guid == guid)
								{
									NPRShader.sID[guid] = 32 + NPRShader.sID.Count * 2;
								}
							}
						}
						else if (!NPRShader.sID.ContainsKey("def"))
						{
							NPRShader.sID["def"] = 30 + NPRShader.sID.Count * 2;
						}
						if (NPRShader.sID.ContainsKey(guid))
						{
							material.SetFloat(text7, (float)(NPRShader.sID[guid] + num5));
						}
						else
						{
							material.SetFloat(text7, (float)(NPRShader.sID["def"] + num5));
						}
					}
					else if (text7.Contains("Toggle"))
					{
						if (num4 == 1f)
						{
							material.EnableKeyword(text7.ToUpper().ToString() + "_ON");
						}
						else
						{
							material.DisableKeyword(text7.ToUpper().ToString() + "_ON");
						}
					}
					else
					{
						material.SetFloat(text7, num4);
					}
				}
				else
				{
					Debug.LogError("マテリアルが読み込めません。不正なマテリアルプロパティ型です " + text6);
				}
			}
			return material;
		}

		public NPRShader()
		{
		}

		private bool initialized;

		private bool configUseKeyboad = true;

		public ConfigEntry<string> configEnvMapResolution ;

		public ConfigEntry<bool> configEnvMapAutoUpdate;

		public ConfigEntry<bool> configReflectionProbeAutoUpdate;

		public ConfigEntry<bool> configObjectMaterialsAutoUpdate;

		private ModeSelectWindow modeSelectView;

		private MaidWindow maidView;

		private ObjectWindow objView;

		private EnvironmentWindow envView;

		public static bool isDance;

		public static bool isKaraoke;

		public static bool isReflection;

		public static bool bUpdateCubeMapRequest;

		public static bool reloadMaterialRequest;

		public static bool bRestorSkyboxRequest;

		public static bool reloadMaidViewRequest;

		private DanceMain danceMain;

		public static bool OVRMode;

		public bool isOVRDance;

		private GameObject gear;

		public static Dictionary<string, int> sID;
	}
}
