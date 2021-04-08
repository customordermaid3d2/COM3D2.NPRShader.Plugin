using System;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class ColorChangeInfo
	{
		public ColorChangeInfo(string slotName, int materialNo, string filename)
		{
			this.slotName = slotName;
			this.materialNo = materialNo;
			this.filename = filename;
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

		public string filename
		{
			[CompilerGenerated]
			get
			{
				return this.Filename;
			}
			[CompilerGenerated]
			set
			{
				this.Filename = value;
			}
		}

		[CompilerGenerated]
		private string SlotName;

		[CompilerGenerated]
		private int MaterialNo;

		[CompilerGenerated]
		private string Filename;
	}
}
