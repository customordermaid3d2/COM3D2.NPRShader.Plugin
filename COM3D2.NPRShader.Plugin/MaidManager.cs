using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	internal class MaidManager
	{
		public List<Maid> listMaid
		{
			[CompilerGenerated]
			get
			{
				return this.<listMaid>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<listMaid>k__BackingField = value;
			}
		}

		public List<string> listID
		{
			[CompilerGenerated]
			get
			{
				return this.<listID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<listID>k__BackingField = value;
			}
		}

		public List<string> listName
		{
			[CompilerGenerated]
			get
			{
				return this.<listName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<listName>k__BackingField = value;
			}
		}

		public int iCurrent
		{
			[CompilerGenerated]
			get
			{
				return this.<iCurrent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<iCurrent>k__BackingField = value;
			}
		}

		public string sCurrent
		{
			[CompilerGenerated]
			get
			{
				return this.<sCurrent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<sCurrent>k__BackingField = value;
			}
		}

		public string uCurrent
		{
			[CompilerGenerated]
			get
			{
				return this.<uCurrent>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<uCurrent>k__BackingField = value;
			}
		}

		public bool bUpdateRequest
		{
			[CompilerGenerated]
			get
			{
				return this.<bUpdateRequest>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<bUpdateRequest>k__BackingField = value;
			}
		}

		public bool bFade
		{
			[CompilerGenerated]
			get
			{
				return this.<bFade>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<bFade>k__BackingField = value;
			}
		}

		private CharacterMgr cm
		{
			[CompilerGenerated]
			get
			{
				return this.<cm>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<cm>k__BackingField = value;
			}
		}

		public MaidManager()
		{
			this.listMaid = new List<Maid>();
			this.listID = new List<string>();
			this.listName = new List<string>();
			this.iCurrent = -1;
			this.sCurrent = "- - -";
			this.cm = GameMain.Instance.CharacterMgr;
			this.bUpdateRequest = true;
		}

		public void Find()
		{
			this.bUpdateRequest = false;
			int count = this.listMaid.Count;
			this.listMaid.Clear();
			this.listID.Clear();
			this.listName.Clear();
			List<Maid> stockMaidList = this.cm.GetStockMaidList();
			for (int i = 0; i < stockMaidList.Count; i++)
			{
				if (MaidManager.IsValid(stockMaidList[i]) && !this.listMaid.Contains(stockMaidList[i]))
				{
					this.listMaid.Add(stockMaidList[i]);
				}
			}
			if (this.listMaid.Count == 0)
			{
				this.iCurrent = -1;
				this.sCurrent = "- - -";
				return;
			}
			if (this.iCurrent >= this.listMaid.Count)
			{
				this.iCurrent = 0;
			}
			else if (this.iCurrent < 0)
			{
				this.iCurrent = this.listMaid.Count - 1;
			}
			for (int j = 0; j < this.listMaid.Count; j++)
			{
				this.listName.Add(this.listMaid[j].status.fullNameJpStyle);
				this.listID.Add(this.listMaid[j].status.guid);
			}
			this.sCurrent = this.listName[this.iCurrent];
		}

		public void Prev()
		{
			if (this.listMaid.Count == 0)
			{
				return;
			}
			int num = this.iCurrent - 1;
			this.iCurrent = num;
			if (num < 0)
			{
				this.iCurrent = this.listMaid.Count - 1;
			}
			this.sCurrent = this.listName[this.iCurrent];
		}

		public void Next()
		{
			if (this.listMaid.Count == 0)
			{
				return;
			}
			int num = this.iCurrent + 1;
			this.iCurrent = num;
			if (num >= this.listMaid.Count)
			{
				this.iCurrent = 0;
			}
			this.sCurrent = this.listName[this.iCurrent];
		}

		public bool Select(int iNum)
		{
			if (this.listMaid.Count == 0 || iNum < 0)
			{
				return false;
			}
			while (iNum >= this.listMaid.Count)
			{
				iNum--;
			}
			this.iCurrent = iNum;
			this.sCurrent = this.listName[this.iCurrent];
			this.uCurrent = this.listID[this.iCurrent];
			return true;
		}

		public void Clear()
		{
			this.listMaid.Clear();
			this.listName.Clear();
			this.listID.Clear();
			this.iCurrent = -1;
			this.sCurrent = string.Empty;
		}

		public void Update()
		{
			if (this.bUpdateRequest)
			{
				this.Find();
			}
		}

		public static bool IsValid(Maid m)
		{
			return m != null && m.body0 != null && m.Visible;
		}

		[CompilerGenerated]
		private List<Maid> <listMaid>k__BackingField;

		[CompilerGenerated]
		private List<string> <listID>k__BackingField;

		[CompilerGenerated]
		private List<string> <listName>k__BackingField;

		[CompilerGenerated]
		private int <iCurrent>k__BackingField;

		[CompilerGenerated]
		private string <sCurrent>k__BackingField;

		[CompilerGenerated]
		private string <uCurrent>k__BackingField;

		[CompilerGenerated]
		private bool <bUpdateRequest>k__BackingField;

		[CompilerGenerated]
		private bool <bFade>k__BackingField;

		[CompilerGenerated]
		private CharacterMgr <cm>k__BackingField;
	}
}
