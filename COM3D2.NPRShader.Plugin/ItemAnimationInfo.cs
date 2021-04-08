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

		public bool loop
		{
			[CompilerGenerated]
			get
			{
				return this.Loop;
			}
			[CompilerGenerated]
			set
			{
				this.Loop = value;
			}
		}

		[CompilerGenerated]
		private string SlotName;

		[CompilerGenerated]
		private string Filename;

		[CompilerGenerated]
		private bool Loop;
	}
}
