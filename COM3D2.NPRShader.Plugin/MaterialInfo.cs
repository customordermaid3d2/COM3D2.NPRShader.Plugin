using System;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class MaterialInfo
	{
		public MaterialInfo(string catName, string slotName, int materialNo, string filename)
		{
			this.catName = catName;
			this.slotName = slotName;
			this.materialNo = materialNo;
			this.filename = filename;
		}

		public string catName
		{
			[CompilerGenerated]
			get
			{
				return this.<catName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<catName>k__BackingField = value;
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

		public int materialNo
		{
			[CompilerGenerated]
			get
			{
				return this.<materialNo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<materialNo>k__BackingField = value;
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
		private string <catName>k__BackingField;

		[CompilerGenerated]
		private string <slotName>k__BackingField;

		[CompilerGenerated]
		private int <materialNo>k__BackingField;

		[CompilerGenerated]
		private string <filename>k__BackingField;
	}
}
