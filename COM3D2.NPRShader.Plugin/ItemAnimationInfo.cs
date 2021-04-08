using System;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class ItemAnimationInfo
	{
		public ItemAnimationInfo(string slotName, string filename, bool loop)
		{
			this.slotName = slotName;
			this.filename = filename;
			this.loop = loop;
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

		public bool loop
		{
			[CompilerGenerated]
			get
			{
				return this.<loop>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<loop>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private string <slotName>k__BackingField;

		[CompilerGenerated]
		private string <filename>k__BackingField;

		[CompilerGenerated]
		private bool <loop>k__BackingField;
	}
}
