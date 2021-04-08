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
				return this.SlotName;
			}
			[CompilerGenerated]
			set
			{
				this.SlotName = value;
			}
		}

		public int materialNo
		{
			[CompilerGenerated]
			get
			{
				return this.MaterialNo;
			}
			[CompilerGenerated]
			set
			{
				this.MaterialNo = value;
			}
		}

		[CompilerGenerated]
		private string SlotName;

		[CompilerGenerated]
		private int MaterialNo;
	}
}
