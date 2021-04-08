using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace COM3D2.NPRShader.Plugin
{
	public class CubemapGenerate : MonoBehaviour
	{
		public static Cubemap Generate(int res)
		{
			Cubemap cubemap = new Cubemap(res, TextureFormat.RGBA32, true);
			GameObject gameObject = new GameObject("CubemapCamera");
			Camera camera = gameObject.AddComponent<Camera>();
			gameObject.transform.position = GameMain.Instance.MainCamera.GetPos();
			gameObject.transform.rotation = Quaternion.identity;
			camera.enabled = false;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			camera.depth = 8f;
			int cullingMask = -1025;
			camera.clearFlags = CameraClearFlags.Skybox;
			camera.cullingMask = cullingMask;
			camera.allowHDR = true;
			camera.allowMSAA = true;
			camera.RenderToCubemap(cubemap);
			UnityEngine.Object.DestroyImmediate(gameObject);
			RenderSettings.defaultReflectionMode = DefaultReflectionMode.Custom;
			RenderSettings.customReflection = cubemap;
			RenderSettings.ambientMode = AmbientMode.Skybox;
			return cubemap;
		}

		public CubemapGenerate()
		{
		}
	}
}
