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
				return this.<menuFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<menuFileName>k__BackingField = value;
			}
		}

		public string menuName
		{
			[CompilerGenerated]
			get
			{
				return this.<menuName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<menuName>k__BackingField = value;
			}
		}

		public string modelName
		{
			[CompilerGenerated]
			get
			{
				return this.<modelName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<modelName>k__BackingField = value;
			}
		}

		public MPN partCategory
		{
			[CompilerGenerated]
			get
			{
				return this.<partCategory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<partCategory>k__BackingField = value;
			}
		}

		public string slotName
		{
			[CompilerGenerated]
			get
			{
				return this.<slotName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<slotName>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private string <menuFileName>k__BackingField;

		[CompilerGenerated]
		private string <menuName>k__BackingField;

		[CompilerGenerated]
		private string <modelName>k__BackingField;

		[CompilerGenerated]
		private MPN <partCategory>k__BackingField;

		[CompilerGenerated]
		private string <slotName>k__BackingField;

		public List<ModelInfo> models;

		public List<TextureChangeInfo> textureChanges;

		public List<MaterialChangeInfo> materialChanges;
	}
}
