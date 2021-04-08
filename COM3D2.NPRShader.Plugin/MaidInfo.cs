using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	public class MaidInfo
	{
		public MaidInfo()
		{
		}

		public MaidInfo(MaidInfo info)
		{
			this.modelName = info.modelName;
			this.objLayer = info.objLayer;
			this.modelId = info.modelId;
			this.modelIconName = info.modelIconName;
			this.menuFileName = info.menuFileName;
			this.bundleName = info.bundleName;
			this.modelType = info.modelType;
			this.myRoomObjectId = info.myRoomObjectId;
			this.BGObjectId = info.BGObjectId;
		}

		public ModelType modelType;

		public string menuFileName;

		public string modelName;

		public int objLayer;

		public string bundleName;

		public string modelId;

		public string modelIconName;

		public int myRoomObjectId;

		public long BGObjectId;

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 localScale;
	}
}
