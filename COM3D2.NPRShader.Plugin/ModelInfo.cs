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
				return this.SlotName;
			}
			[CompilerGenerated]
			set
			{
				this.SlotName = value;
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
		private string Filename;
	}
}
