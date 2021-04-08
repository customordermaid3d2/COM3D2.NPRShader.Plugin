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

		public string propName
		{
			[CompilerGenerated]
			get
			{
				return this.<propName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<propName>k__BackingField = value;
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

		public MaidParts.PARTS_COLOR partsColor
		{
			[CompilerGenerated]
			get
			{
				return this.<partsColor>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<partsColor>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private string <slotName>k__BackingField;

		[CompilerGenerated]
		private int <materialNo>k__BackingField;

		[CompilerGenerated]
		private string <propName>k__BackingField;

		[CompilerGenerated]
		private string <filename>k__BackingField;

		[CompilerGenerated]
		private MaidParts.PARTS_COLOR <partsColor>k__BackingField;
	}
}
