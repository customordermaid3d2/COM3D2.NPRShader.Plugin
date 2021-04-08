using System;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class TextureChangeInfo
	{
		public TextureChangeInfo(string slotName, int materialNo, string propName, string filename, MaidParts.PARTS_COLOR partsColor)
		{
			this.slotName = slotName;
			this.materialNo = materialNo;
			this.propName = propName;
			this.filename = filename;
			this.partsColor = partsColor;
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

		public string propName
		{
			[CompilerGenerated]
			get
			{
				return this.PropName;
			}
			[CompilerGenerated]
			set
			{
				this.PropName = value;
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

		public MaidParts.PARTS_COLOR partsColor
		{
			[CompilerGenerated]
			get
			{
				return this.PartsColor;
			}
			[CompilerGenerated]
			set
			{
				this.PartsColor = value;
			}
		}

		[CompilerGenerated]
		private string SlotName;

		[CompilerGenerated]
		private int MaterialNo;

		[CompilerGenerated]
		private string PropName;

		[CompilerGenerated]
		private string Filename;

		[CompilerGenerated]
		private MaidParts.PARTS_COLOR PartsColor;
	}
}
