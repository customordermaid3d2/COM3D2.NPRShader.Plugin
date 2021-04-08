using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace COM3D2.NPRShader.Plugin
{
	internal class ReflectionProbeObj : MonoBehaviour
	{
		private void Awake(GameObject pobj)
		{
			GameObject gameObject = new GameObject("ReflectionProbe", new Type[]
			{
				typeof(ReflectionProbe)
			});
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			gameObject.transform.SetParent(pobj.transform);
			this.probe = gameObject.GetComponent<ReflectionProbe>();
			this.probe.mode = ReflectionProbeMode.Realtime;
			this.probe.resolution = 2048;
			this.probe.size = new Vector3(50f, 50f, 50f);
			this.probe.backgroundColor = Color.white;
			this.probe.clearFlags = ReflectionProbeClearFlags.Skybox;
			this.probe.refreshMode = ReflectionProbeRefreshMode.ViaScripting;
			this.probe.boxProjection = true;
			this.probe.hdr = false;
			this.probe.cullingMask = -3073;
		}

		public void Update()
		{
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

		public ReflectionProbeObj()
		{
		}

		private ReflectionProbe probe;

		public Material skybox;
	}
}
