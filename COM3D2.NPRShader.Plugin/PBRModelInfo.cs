using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class PBRModelInfo
	{
		public PBRModelInfo()
		{
			this.models = new List<ModelInfo>();
			this.textureChanges = new List<TextureChangeInfo>();
			this.materialChanges = new List<MaterialChangeInfo>();
		}

		public string menuFileName
		{
			[CompilerGenerated]
			get
			{
				return this.MenuFileName;
			}
			[CompilerGenerated]
			set
			{
				this.MenuFileName = value;
			}
		}

		public string menuName
		{
			[CompilerGenerated]
			get
			{
				return this.MenuName;
			}
			[CompilerGenerated]
			set
			{
				this.MenuName = value;
			}
		}

		public string modelName
		{
			[CompilerGenerated]
			get
			{
				return this.ModelName;
			}
			[CompilerGenerated]
			set
			{
				this.ModelName = value;
			}
		}

		public MPN partCategory
		{
			[CompilerGenerated]
			get
			{
				return this.PartCategory;
			}
			[CompilerGenerated]
			set
			{
				this.PartCategory = value;
			}
		}

		public string slotName
		{
			[CompilerGenerated]
			get
			{
				return this.SlotName;
			}
			[CompilerGenerated]
			set
			{
				this.SlotName = value;
			}
		}

		[CompilerGenerated]
		private string MenuFileName;

		[CompilerGenerated]
		private string MenuName;

		[CompilerGenerated]
		private string ModelName;

		[CompilerGenerated]
		private MPN PartCategory;

		[CompilerGenerated]
		private string SlotName;

		public List<ModelInfo> models;

		public List<TextureChangeInfo> textureChanges;

		public List<MaterialChangeInfo> materialChanges;
	}
}
