using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	public static class AssetLoader
	{
		public static Texture2D LoadTexture(string textureName)
		{
			Texture2D result = null;
			if (textureName != null && textureName != string.Empty)
			{
				try
				{
					result = ImportCM.CreateTexture(textureName);
				}
				catch
				{
					try
					{
						textureName = textureName.Replace("tex\\", "");
						result = ImportCM.CreateTexture(textureName);
					}
					catch (Exception ex)
					{
						Debug.LogError("Error: " + ex.ToString());
					}
				}
			}
			return result;
		}

		private static string readCom(BinaryReader binaryReader)
		{
			for (;;)
			{
				int num = (int)binaryReader.ReadByte();
				string text = string.Empty;
				if (num == 0)
				{
					break;
				}
				for (int i = 0; i < num; i++)
				{
					text = text + "\"" + binaryReader.ReadString() + "\" ";
				}
				if (!(text == string.Empty))
				{
					return text;
				}
			}
			return null;
		}

		public static void LoadPriorityMaterials()
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
								NDebug.Assert(afileBase.IsValid(), text + "を開けませんでした");
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
								}
							}
						}
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
									NDebug.Assert(afileBase2.IsValid(), text2 + "を開けませんでした");
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
											Debug.LogWarning(".pmatファイル名とpmat内のマテリアル名の不一致を検出");
											Debug.Log(text2);
											Debug.Log("pmatファイル名: " + Path.GetFileNameWithoutExtension(text2));
											Debug.Log("マテリアル名  : " + text3);
										}
										if (num3 != hashCode)
										{
											Debug.LogWarning("不正なHash値を検出、修正処理を実行: " + num3.ToString());
											Debug.Log(text2);
											num3 = hashCode;
										}
										AssetLoader.m_hashPriorityMaterials.ContainsKey(num3);
										AssetLoader.m_hashPriorityMaterials[num3] = new KeyValuePair<string, float>(text3, value2);
									}
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
		}

		public static Material LoadMaterial(string materialName, Material existmat = null)
		{
			byte[] array = null;
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
			string text = binaryReader.ReadString();
			if (text != "CM3D2_MATERIAL")
			{
				NDebug.Assert("ProcScriptBin 例外 : ヘッダーファイルが不正です。" + text, false);
			}
			int num = binaryReader.ReadInt32();
			string text2 = binaryReader.ReadString();
			Material result = AssetLoader.ReadMaterial(binaryReader, existmat);
			binaryReader.Close();
			return result;
		}

		public static Material ReadMaterial(BinaryReader r, Material existmat)
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
			string str = r.ReadString();
			Material material2;
			if (existmat == null)
			{
				Material material = Resources.Load("DefMaterial/" + str, typeof(Material)) as Material;
				if (material == null)
				{
					NDebug.Assert("DefMaterialが見つかりません。DefMaterial/" + str, false);
				}
				material2 = UnityEngine.Object.Instantiate<Material>(material);
			}
			else
			{
				material2 = existmat;
				NDebug.Assert(material2.shader.name == text4, "マテリアル入れ替えエラー。違うシェーダーに入れようとしました。 " + text4 + " -> " + material2.shader.name);
			}
			material2.name = name;
			int hashCode2 = material2.name.GetHashCode();
			if (AssetLoader.m_hashPriorityMaterials != null && AssetLoader.m_hashPriorityMaterials.ContainsKey(hashCode2))
			{
				KeyValuePair<string, float> keyValuePair = AssetLoader.m_hashPriorityMaterials[hashCode2];
				if (keyValuePair.Key == material2.name)
				{
					material2.SetFloat("_SetManualRenderQueue", keyValuePair.Value);
					material2.renderQueue = (int)keyValuePair.Value;
				}
			}
			for (;;)
			{
				string text5 = r.ReadString();
				if (text5 == "end")
				{
					break;
				}
				string name2 = r.ReadString();
				if (text5 == "tex")
				{
					string a3 = r.ReadString();
					if (a3 == "null")
					{
						material2.SetTexture(name2, null);
					}
					else if (a3 == "tex2d")
					{
						string text6 = r.ReadString();
						r.ReadString();
						Texture2D texture2D = ImportCM.CreateTexture(text6 + ".tex");
						texture2D.name = text6;
						texture2D.wrapMode = TextureWrapMode.Clamp;
						material2.SetTexture(name2, texture2D);
						Vector2 value3;
						value3.x = r.ReadSingle();
						value3.y = r.ReadSingle();
						material2.SetTextureOffset(name2, value3);
						Vector2 value4;
						value4.x = r.ReadSingle();
						value4.y = r.ReadSingle();
						material2.SetTextureScale(name2, value4);
					}
					if (a3 == "texRT")
					{
						r.ReadString();
						r.ReadString();
					}
				}
				else if (text5 == "col")
				{
					Color value5;
					value5.r = r.ReadSingle();
					value5.g = r.ReadSingle();
					value5.b = r.ReadSingle();
					value5.a = r.ReadSingle();
					material2.SetColor(name2, value5);
				}
				else if (text5 == "vec")
				{
					Vector4 value6;
					value6.x = r.ReadSingle();
					value6.y = r.ReadSingle();
					value6.z = r.ReadSingle();
					value6.w = r.ReadSingle();
					material2.SetVector(name2, value6);
				}
				else if (text5 == "f")
				{
					float value7 = r.ReadSingle();
					material2.SetFloat(name2, value7);
				}
				else
				{
					Debug.LogError("マテリアルが読み込めません。不正なマテリアルプロパティ型です " + text5);
				}
			}
			return material2;
		}

		public static Material LoadMaterialWithSetShader(string materialName, string shaderName, Material existmat = null)
		{
			byte[] array = null;
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
			string text = binaryReader.ReadString();
			if (text != "CM3D2_MATERIAL")
			{
				NDebug.Assert("ProcScriptBin 例外 : ヘッダーファイルが不正です。" + text, false);
			}
			int num = binaryReader.ReadInt32();
			string text2 = binaryReader.ReadString();
			Material result = AssetLoader.ReadMaterialWithSetShader(binaryReader, shaderName);
			binaryReader.Close();
			return result;
		}

		public static Material ReadMaterialWithSetShader(BinaryReader r, string shaderMatName)
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
								NDebug.Assert(afileBase.IsValid(), text + "を開けませんでした");
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
								}
							}
						}
					}
				}
				string[] fileListAtExtension = GameUty.FileSystemMod.GetFileListAtExtension(".pmat");
				if (fileListAtExtension != null && fileListAtExtension.Length != 0)
				{
					for (int j = 0; j < fileListAtExtension.Length; j++)
					{
						if (Path.GetExtension(fileListAtExtension[j]) == ".pmat")
						{
							string text2 = fileListAtExtension[j];
							using (AFileBase afileBase2 = GameUty.FileOpen(text2, null))
							{
								NDebug.Assert(afileBase2.IsValid(), text2 + "を開けませんでした");
								byte[] buffer2 = afileBase2.ReadAll();
								using (BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(buffer2), Encoding.UTF8))
								{
									string a2 = binaryReader2.ReadString();
									if (a2 != "CM3D2_PMATERIAL")
									{
										NDebug.Assert("ヘッダーエラー\n" + text2, false);
									}
									int num2 = binaryReader2.ReadInt32();
									int key3 = binaryReader2.ReadInt32();
									string key4 = binaryReader2.ReadString();
									float value2 = binaryReader2.ReadSingle();
									AssetLoader.m_hashPriorityMaterials[key3] = new KeyValuePair<string, float>(key4, value2);
								}
							}
						}
					}
				}
			}
			string name = r.ReadString();
			string str = r.ReadString();
			string text3 = r.ReadString();
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
					if (!material2.shader.ToString().ToLower().Contains("com3d2mod"))
					{
						material2 = (Resources.Load("DefMaterial/" + str, typeof(Material)) as Material);
					}
					if (material2 == null)
					{
						Debug.Log("DefMaterialが見つかりません。");
					}
					AssetLoader.m_dicCacheMaterial[shaderMatName] = material2;
				}
				material = UnityEngine.Object.Instantiate<Material>(material2);
			}
			material.name = name;
			int hashCode = material.name.GetHashCode();
			if (AssetLoader.m_hashPriorityMaterials != null && AssetLoader.m_hashPriorityMaterials.ContainsKey(hashCode))
			{
				KeyValuePair<string, float> keyValuePair = AssetLoader.m_hashPriorityMaterials[hashCode];
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
				string text4 = r.ReadString();
				if (text4 == "end")
				{
					break;
				}
				string text5 = r.ReadString();
				if (text4 == "tex")
				{
					string a3 = r.ReadString();
					if (a3 == "null")
					{
						material.SetTexture(text5, null);
					}
					else if (a3 == "tex2d")
					{
						string text6 = r.ReadString();
						r.ReadString();
						Texture2D texture2D = ImportCM.CreateTexture(text6 + ".tex");
						texture2D.name = text6;
						if (!text5.Contains("_EmissionToon") && text5.ToLower().Contains("toon"))
						{
							texture2D.wrapMode = TextureWrapMode.Clamp;
						}
						else
						{
							texture2D.wrapMode = TextureWrapMode.Repeat;
						}
						material.SetTexture(text5, texture2D);
						Vector2 value3;
						value3.x = r.ReadSingle();
						value3.y = r.ReadSingle();
						material.SetTextureOffset(text5, value3);
						Vector2 value4;
						value4.x = r.ReadSingle();
						value4.y = r.ReadSingle();
						material.SetTextureScale(text5, value4);
					}
					if (a3 == "texRT")
					{
						r.ReadString();
						r.ReadString();
					}
				}
				else if (text4 == "col")
				{
					Color value5;
					value5.r = r.ReadSingle();
					value5.g = r.ReadSingle();
					value5.b = r.ReadSingle();
					value5.a = r.ReadSingle();
					material.SetColor(text5, value5);
				}
				else if (text4 == "vec")
				{
					Vector4 value6;
					value6.x = r.ReadSingle();
					value6.y = r.ReadSingle();
					value6.z = r.ReadSingle();
					value6.w = r.ReadSingle();
					material.SetVector(text5, value6);
				}
				else if (text4 == "f")
				{
					float num3 = r.ReadSingle();
					material.SetFloat(text5, num3);
					if (text5.Contains("Toggle"))
					{
						if (num3 == 1f)
						{
							material.EnableKeyword(text5.ToUpper().ToString() + "_ON");
						}
						else
						{
							material.DisableKeyword(text5.ToUpper().ToString() + "_ON");
						}
					}
					else
					{
						material.SetFloat(text5, num3);
					}
				}
				else
				{
					Debug.LogError("マテリアルが読み込めません。不正なマテリアルプロパティ型です " + text4);
				}
			}
			return material;
		}

		public static PBRModelInfo LoadMenuPBR(string menuFileName)
		{
			AFileBase afileBase = null;
			try
			{
				afileBase = GameUty.FileOpen(menuFileName, null);
			}
			catch
			{
				throw;
			}
			PBRModelInfo pbrmodelInfo = new PBRModelInfo();
			pbrmodelInfo.menuFileName = menuFileName;
			byte[] buffer = afileBase.ReadAll();
			afileBase.Dispose();
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(buffer), Encoding.UTF8);
			string text = binaryReader.ReadString();
			NDebug.Assert(text == "CM3D2_MENU", "ProcScriptBin 例外 : ヘッダーファイルが不正です。" + text);
			binaryReader.ReadInt32();
			string text2 = binaryReader.ReadString();
			binaryReader.ReadString();
			binaryReader.ReadString();
			binaryReader.ReadString();
			int num = binaryReader.ReadInt32();
			string text3 = string.Empty;
			string empty = string.Empty;
			string empty2 = string.Empty;
			try
			{
				string stringCom;
				do
				{
					text3 = AssetLoader.readCom(binaryReader);
					if (text3 == null)
					{
						break;
					}
					stringCom = UTY.GetStringCom(text3);
					string[] stringList = UTY.GetStringList(text3);
					if (stringCom == "name")
					{
						string text4 = stringList[1];
						string text5 = string.Empty;
						string str = string.Empty;
						int i = 0;
						while (i < text4.Length && text4[i] != '\u3000')
						{
							if (text4[i] == ' ')
							{
								break;
							}
							text5 += text4[i].ToString();
							i++;
						}
						while (i < text4.Length)
						{
							str += text4[i].ToString();
							i++;
						}
						pbrmodelInfo.menuName = text5;
					}
					else
					{
						if (stringCom == "category")
						{
							string text6 = stringList[1].ToLower();
							try
							{
								pbrmodelInfo.partCategory = (MPN)((int)Enum.Parse(typeof(MPN), text6));
								goto IL_313;
							}
							catch
							{
								if (text6 != "def")
								{
									Debug.LogWarning("カテゴリがありません。" + text6);
								}
								pbrmodelInfo.partCategory = MPN.null_mpn;
								goto IL_313;
							}
						}
						if (stringCom == "additem")
						{
							string slotName = pbrmodelInfo.partCategory.ToString().ToLower();
							string text7 = stringList[1];
							if (stringList.Length > 1)
							{
								slotName = stringList[2];
							}
							pbrmodelInfo.modelName = text7;
							pbrmodelInfo.slotName = slotName;
							pbrmodelInfo.models.Add(new ModelInfo(slotName, text7));
						}
						else if (stringCom == "tex" || stringCom == "テクスチャ変更")
						{
							string slotName2 = stringList[1];
							int materialNo = int.Parse(stringList[2]);
							string propName = stringList[3];
							string filename = stringList[4];
							MaidParts.PARTS_COLOR partsColor = MaidParts.PARTS_COLOR.NONE;
							if (stringList.Length == 6)
							{
								string text8 = stringList[5];
								try
								{
									partsColor = (MaidParts.PARTS_COLOR)((int)Enum.Parse(typeof(MaidParts.PARTS_COLOR), text8.ToUpper()));
								}
								catch
								{
									Debug.LogWarning("無限色IDがありません :" + text8);
									partsColor = MaidParts.PARTS_COLOR.NONE;
								}
							}
							pbrmodelInfo.textureChanges.Add(new TextureChangeInfo(slotName2, materialNo, propName, filename, partsColor));
						}
						else if (stringCom == "マテリアル変更")
						{
							string slotName3 = stringList[1];
							int materialNo2 = int.Parse(stringList[2]);
							string filename2 = stringList[3];
							pbrmodelInfo.materialChanges.Add(new MaterialChangeInfo(slotName3, materialNo2, filename2));
						}
					}
					IL_313:;
				}
				while (!(stringCom == "end"));
			}
			catch
			{
				Debug.LogError("menuファイル読み込み失敗 " + menuFileName);
				Debug.LogError("sss : " + text3);
				throw;
			}
			binaryReader.Close();
			return pbrmodelInfo;
		}

		// Note: this type is marked as 'beforefieldinit'.
		static AssetLoader()
		{
		}

		public static Dictionary<int, KeyValuePair<string, float>> m_hashPriorityMaterials = null;

		public static Dictionary<string, Material> m_dicCacheMaterial = new Dictionary<string, Material>();
	}
}
