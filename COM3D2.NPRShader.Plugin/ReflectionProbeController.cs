using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace COM3D2.NPRShader.Plugin
{
	public class ReflectionProbeController : MonoBehaviour
	{
		private void Awake()
		{
			this.obj = new GameObject("CubemapCamera", new Type[]
			{
				typeof(GameObject)
			});
			this.obj.hideFlags = HideFlags.DontSave;
			this.probe = this.obj.AddComponent<ReflectionProbe>();
			this.probe.mode = ReflectionProbeMode.Realtime;
			this.probe.resolution = 16;
			this.probe.size = new Vector3(500f, 500f, 500f);
			this.probe.backgroundColor = new Color32(0, 0, 0, 0);
			this.probe.clearFlags = ReflectionProbeClearFlags.Skybox;
			this.probe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
			this.probe.boxProjection = true;
			this.probe.hdr = true;
			this.probe.cullingMask = -1025;
			this.probe.transform.position = new Vector3(0f, 0f, 0f);
		}

		public void probCamera()
		{
		}

		public void LateUpdate()
		{
			this.probe.transform.rotation = Quaternion.identity;
			if (GameMain.Instance.VRDummyMode)
			{
				this.probe.transform.position = GameMain.Instance.OvrMgr.OvrCamera.GetRealHeadTransform().position;
				this.probe.center = GameMain.Instance.OvrMgr.OvrCamera.GetRealHeadTransform().position;
			}
			else if (NPRShader.isDance)
			{
				this.probe.transform.position = new Vector3(GameMain.Instance.MainCamera.GetPos().x, 0.95894f, GameMain.Instance.MainCamera.GetPos().z);
				this.probe.center = new Vector3(GameMain.Instance.MainCamera.GetPos().x, 0.95894f, GameMain.Instance.MainCamera.GetPos().z);
			}
			else
			{
				this.probe.transform.position = GameMain.Instance.MainCamera.GetPos();
				this.probe.center = GameMain.Instance.MainCamera.GetPos();
			}
			this.RenderProbe();
		}

		public void RenderProbe()
		{
			if (this.probe != null)
			{
				this.probe.RenderProbe();
				return;
			}
			Debug.LogError("null probe.RenderProbe()");
		}

		public ReflectionProbeController()
		{
		}

		public ReflectionProbe probe;

		public Material skybox;

		private GameObject obj;
	}
}
