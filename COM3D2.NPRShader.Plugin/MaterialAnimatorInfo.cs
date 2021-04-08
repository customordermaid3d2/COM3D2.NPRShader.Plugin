using System;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class MaterialAnimatorInfo
	{
		public MaterialAnimatorInfo(string slotName, int materialNo)
		{
			this.slotName = slotName;
			this.materialNo = materialNo;
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

		[CompilerGenerated]
		private string <slotName>k__BackingField;

		[CompilerGenerated]
		private int <materialNo>k__BackingField;
	}
}
