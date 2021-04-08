using System;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class ModelInfo
	{
		public ModelInfo(string slotName, string filename)
		{
			this.slotName = slotName;
			this.filename = filename;
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

		public string filename
		{
			[CompilerGenerated]
			get
			{
				return this.<filename>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<filename>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private string <slotName>k__BackingField;

		[CompilerGenerated]
		private string <filename>k__BackingField;
	}
}
