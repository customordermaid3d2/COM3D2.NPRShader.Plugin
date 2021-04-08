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
				return this.CatName;
			}
			[CompilerGenerated]
			set
			{
				this.CatName = value;
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
		private string CatName;

		[CompilerGenerated]
		private string SlotName;

		[CompilerGenerated]
		private int MaterialNo;

		[CompilerGenerated]
		private string Filename;
	}
}
