using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal static class Util
	{
		internal static AssetBundle LoadAssetBundle(string name)
		{
			string path = Util.SHADER_DIR + "\\" + name;
			return AssetBundle.LoadFromFile(path);
		}

		internal static AssetBundle LoadALLAssetBundle(string name)
		{
			return AssetBundle.LoadFromFile(name);
		}

		internal static void LoadShaders()
		{
			Util.NPRToon_Shaders = Util.LoadAssetBundle("nprshaders");
		}

		internal static void LoadALLShaders()
		{
			string[] files = Directory.GetFiles(Util.SHADER_DIR);
			foreach (string name in files)
			{
				try
				{
					AssetBundle assetBundle = Util.LoadALLAssetBundle(name);
					foreach (string name2 in assetBundle.GetAllAssetNames())
					{
						Material material = assetBundle.LoadAsset(name2, typeof(Material)) as Material;
						Util.shaderList[material.shader.name] = material.shader;
					}
					Util.ALLShaders.Add(assetBundle);
				}
				catch(Exception e)
				{
					Debug.LogWarning("LoadALLShaders() : " + e.ToString());
				}
			}
            if (Util.ALLShaders.Count==0)
            {
				Debug.LogWarning("ALLShaders.Count 0");
			}
		}

		internal static int GetPix(int i)
		{
			float num = 1f + ((float)Screen.width / 1280f - 1f) * 0.6f;
			return (int)(num * (float)i);
		}

		internal static string DrawTextFieldF(Rect rect, string sField, GUIStyle style)
		{
			string text = GUI.TextField(rect, sField, style);
			if (text != sField)
			{
				string text2 = string.Copy(text);
				if (text2[text2.Length - 1] == '.')
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				float num;
				if (float.TryParse(text2, out num))
				{
					return text;
				}
			}
			return sField;
		}

		public static string ConvertVector3ToString(Vector3 v3)
		{
			return string.Concat(new string[]
			{
				v3[0].ToString(),
				",",
				v3[1].ToString(),
				",",
				v3[2].ToString()
			});
		}

		public static Vector3 ConvertStringToVector3(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			if (array.Length == 3)
			{
				float[] array2 = new float[3];
				bool flag = false;
				for (int i = 0; i < 3; i++)
				{
					if (!float.TryParse(array[i], out array2[i]))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return new Vector3(array2[0], array2[1], array2[2]);
				}
			}
			return Vector3.zero;
		}

		public static string ConvertQuaternionToString(Quaternion q)
		{
			return string.Concat(new string[]
			{
				q[0].ToString(),
				",",
				q[1].ToString(),
				",",
				q[2].ToString(),
				",",
				q[3].ToString()
			});
		}

		public static Quaternion ConvertStringToQuaternion(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			if (array.Length == 4)
			{
				float[] array2 = new float[4];
				bool flag = false;
				for (int i = 0; i < 4; i++)
				{
					if (!float.TryParse(array[i], out array2[i]))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return new Quaternion(array2[0], array2[1], array2[2], array2[3]);
				}
			}
			return Quaternion.identity;
		}

		public static string ConvertColor32ToString(Color32 color)
		{
			return string.Concat(new string[]
			{
				color.r.ToString(),
				",",
				color.g.ToString(),
				",",
				color.b.ToString(),
				",",
				color.a.ToString()
			});
		}

		public static Color32 ConvertStringToColor32(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			if (array.Length == 4)
			{
				byte[] array2 = new byte[4];
				bool flag = false;
				for (int i = 0; i < 4; i++)
				{
					if (!byte.TryParse(array[i], out array2[i]))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return new Color32(array2[0], array2[1], array2[2], array2[3]);
				}
			}
			return Color.white;
		}

		public static string ConvertAnimationCurveToString(AnimationCurve curve)
		{
			float outTangent = curve.keys[0].outTangent;
			float value = curve.keys[0].value;
			float inTangent = curve.keys[1].inTangent;
			float value2 = curve.keys[1].value;
			return string.Concat(new string[]
			{
				outTangent.ToString(),
				",",
				value.ToString(),
				",",
				inTangent.ToString(),
				",",
				value2.ToString()
			});
		}

		public static AnimationCurve ConvertStringToAnimationCurve(string s)
		{
			string[] array = s.Split(new char[]
			{
				','
			});
			if (array.Length == 4)
			{
				float[] array2 = new float[4];
				bool flag = false;
				for (int i = 0; i < 4; i++)
				{
					if (!float.TryParse(array[i], out array2[i]))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					Keyframe[] array3 = new Keyframe[2];
					array3[0] = new Keyframe(0f, 0f, 0f, 1f);
					array3[0].outTangent = array2[0];
					array3[0].value = array2[1];
					array3[1] = new Keyframe(1f, 1f, 1f, 0f);
					array3[1].inTangent = array2[2];
					array3[1].value = array2[3];
					return new AnimationCurve(array3);
				}
			}
			return new AnimationCurve();
		}

		internal static FieldInfo[] GetFields<T>()
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return typeof(T).GetFields(bindingAttr);
		}

		internal static FieldInfo[] GetFields(Type t)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return t.GetFields(bindingAttr);
		}

		internal static string[] GetFieldNames<T>()
		{
			FieldInfo[] fields = Util.GetFields<T>();
			if (fields.Length == 0)
			{
				return new string[0];
			}
			string[] array = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array[i] = fields[i].Name;
			}
			return array;
		}

		internal static string[] GetFieldNamesSpecifyType<T, T2>()
		{
			FieldInfo[] fields = Util.GetFields<T>();
			if (fields.Length == 0)
			{
				return new string[0];
			}
			Type typeFromHandle = typeof(T2);
			List<string> list = new List<string>();
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i].FieldType == typeFromHandle)
				{
					list.Add(fields[i].Name);
				}
			}
			return list.ToArray();
		}

		internal static string[] GetFieldNamesSpecifyType(Type t, Type t2)
		{
			FieldInfo[] fields = Util.GetFields(t);
			if (fields.Length == 0)
			{
				return new string[0];
			}
			List<string> list = new List<string>();
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i].FieldType == t2)
				{
					list.Add(fields[i].Name);
				}
			}
			return list.ToArray();
		}

		internal static FieldInfo[] GetFieldsSpecifyType(Type t, Type t2)
		{
			FieldInfo[] fields = Util.GetFields(t);
			if (fields.Length == 0)
			{
				return new FieldInfo[0];
			}
			List<FieldInfo> list = new List<FieldInfo>();
			for (int i = 0; i < fields.Length; i++)
			{
				if (fields[i].FieldType == t2)
				{
					list.Add(fields[i]);
				}
			}
			return list.ToArray();
		}

		internal static FieldInfo GetFieldInfo<T>(string name)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return typeof(T).GetField(name, bindingAttr);
		}

		internal static FieldInfo GetFieldInfo(Type t, string name)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return t.GetField(name, bindingAttr);
		}

		internal static TResult GetFieldValue<T, TResult>(T inst, string name)
		{
			if (inst == null)
			{
				return default(TResult);
			}
			FieldInfo fieldInfo = Util.GetFieldInfo<T>(name);
			if (fieldInfo == null)
			{
				return default(TResult);
			}
			return (TResult)((object)fieldInfo.GetValue(inst));
		}

		internal static void SetFieldValue<T>(object inst, string name, object val)
		{
			if (inst == null)
			{
				return;
			}
			FieldInfo fieldInfo = Util.GetFieldInfo<T>(name);
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(inst, val);
			}
		}

		internal static void SetFieldValue(Type t, object inst, string name, object val)
		{
			if (inst == null)
			{
				return;
			}
			FieldInfo fieldInfo = Util.GetFieldInfo(t, name);
			if (fieldInfo != null)
			{
				fieldInfo.SetValue(inst, val);
			}
		}

		internal static PropertyInfo GetPropertyInfo<T>(string name)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return typeof(T).GetProperty(name, bindingAttr);
		}

		internal static PropertyInfo GetPropertyInfo(Type t, string name)
		{
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			return t.GetProperty(name, bindingAttr);
		}

		internal static TResult GetPropertyValue<T, TResult>(T inst, string name)
		{
			if (inst == null)
			{
				return default(TResult);
			}
			PropertyInfo propertyInfo = Util.GetPropertyInfo<T>(name);
			if (propertyInfo == null)
			{
				return default(TResult);
			}
			return (TResult)((object)propertyInfo.GetValue(inst, null));
		}

		internal static void SetPropertyValue<T>(object inst, string name, object val)
		{
			if (inst == null)
			{
				return;
			}
			PropertyInfo propertyInfo = Util.GetPropertyInfo<T>(name);
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(inst, val, null);
			}
		}

		internal static void SetPropertyValue(Type t, object inst, string name, object val)
		{
			if (inst == null)
			{
				return;
			}
			PropertyInfo propertyInfo = Util.GetPropertyInfo(t, name);
			if (propertyInfo != null)
			{
				propertyInfo.SetValue(inst, val, null);
			}
		}

		static Util()
		{
		}

		public static AssetBundle NPRToon_Shaders;

		public static List<AssetBundle> ALLShaders = new List<AssetBundle>();

		public static bool OVRMode;

		public static Dictionary<string, Shader> shaderList = new Dictionary<string, Shader>();

		private static readonly string SHADER_DIR = ConstantValues.ConfigDir + "\\Shaders";
	}
}
