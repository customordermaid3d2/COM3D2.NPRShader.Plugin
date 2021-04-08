using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace COM3D2.NPRShader.Plugin
{
	public class MenuInfo
	{
		public MenuInfo()
		{
			this.modelType = ModelType.MaidEquip;
			this.models = new List<ModelInfo>();
			this.textureChanges = new List<TextureChangeInfo>();
			this.materialChanges = new List<MaterialChangeInfo>();
			this.itemAnimations = new List<ItemAnimationInfo>();
			this.materialAnimators = new List<MaterialAnimatorInfo>();
		}

		public static MenuInfo MakeBGObjectMenu(long id)
		{
			return new MenuInfo
			{
				modelType = ModelType.BGObject,
				bGObjectId = id
			};
		}

		public static MenuInfo MakeBackgroundMenu(string prefabName)
		{
			return new MenuInfo
			{
				modelType = ModelType.Background,
				modelName = prefabName
			};
		}

		public static MenuInfo MakeMyRoomMenu(string guid)
		{
			return new MenuInfo
			{
				modelType = ModelType.MyRoom,
				modelId = guid
			};
		}

		public static MenuInfo MakeMyRoomObjectMenu(int id)
		{
			return new MenuInfo
			{
				modelType = ModelType.MyRoomObject,
				myRoomObjectId = id
			};
		}

		public MenuInfo(MenuInfo other) : this()
		{
			this.modelType = other.modelType;
			this.menuFileName = other.menuFileName;
			this.menuName = other.menuName;
			this.menuInfo = other.menuInfo;
			this.modelName = other.modelName;
			this.objLayer = other.objLayer;
			this.bundleName = other.bundleName;
			this.modelId = other.modelId;
			this.myRoomObjectId = other.myRoomObjectId;
			this.delOnly = other.delOnly;
			this.iconTextureName = other.iconTextureName;
			this.partCategory = other.partCategory;
			foreach (ModelInfo item in other.models)
			{
				this.models.Add(item);
			}
			foreach (TextureChangeInfo item2 in other.textureChanges)
			{
				this.textureChanges.Add(item2);
			}
			foreach (MaterialChangeInfo item3 in other.materialChanges)
			{
				this.materialChanges.Add(item3);
			}
			foreach (ItemAnimationInfo item4 in other.itemAnimations)
			{
				this.itemAnimations.Add(item4);
			}
			foreach (MaterialAnimatorInfo item5 in other.materialAnimators)
			{
				this.materialAnimators.Add(item5);
			}
		}

		public ModelType modelType
		{
			[CompilerGenerated]
			get
			{
				return this.ModelType;
			}
			[CompilerGenerated]
			set
			{
				this.ModelType = value;
			}
		}

		public string menuFileName
		{
			[CompilerGenerated]
			get
			{
				return this.MenuFileName;
			}
			[CompilerGenerated]
			set
			{
				this.MenuFileName = value;
			}
		}

		public string menuName
		{
			[CompilerGenerated]
			get
			{
				return this.MenuName;
			}
			[CompilerGenerated]
			set
			{
				this.MenuName = value;
			}
		}
		/*
		public string menuInfo
		{
			[CompilerGenerated]
			get
			{
				return this.menuInfo;
			}
			[CompilerGenerated]
			set
			{
				this.menuInfo = value;
			}
		}
		*/
		public string modelName
		{
			[CompilerGenerated]
			get
			{
				return this.ModelName;
			}
			[CompilerGenerated]
			set
			{
				this.ModelName = value;
			}
		}

		public int objLayer
		{
			[CompilerGenerated]
			get
			{
				return this.ObjLayer;
			}
			[CompilerGenerated]
			set
			{
				this.ObjLayer = value;
			}
		}

		public string bundleName
		{
			[CompilerGenerated]
			get
			{
				return this.BundleName;
			}
			[CompilerGenerated]
			set
			{
				this.BundleName = value;
			}
		}

		public string modelId
		{
			[CompilerGenerated]
			get
			{
				return this.ModelId;
			}
			[CompilerGenerated]
			set
			{
				this.ModelId = value;
			}
		}

		public int myRoomObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.MyRoomObjectId;
			}
			[CompilerGenerated]
			set
			{
				this.MyRoomObjectId = value;
			}
		}

		public long BGObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.bGObjectId;
			}
			[CompilerGenerated]
			set
			{
				this.bGObjectId = value;
			}
		}

		public bool delOnly
		{
			[CompilerGenerated]
			get
			{
				return this.DelOnly;
			}
			[CompilerGenerated]
			set
			{
				this.DelOnly = value;
			}
		}

		public string iconTextureName
		{
			[CompilerGenerated]
			get
			{
				return this.IconTextureName;
			}
			[CompilerGenerated]
			set
			{
				this.IconTextureName = value;
			}
		}

		public MPN partCategory
		{
			[CompilerGenerated]
			get
			{
				return this.PartCategory;
			}
			[CompilerGenerated]
			set
			{
				this.PartCategory = value;
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

		public string modFileName
		{
			[CompilerGenerated]
			get
			{
				return this.ModFileName;
			}
			[CompilerGenerated]
			set
			{
				this.ModFileName = value;
			}
		}

		[CompilerGenerated]
		private ModelType ModelType;

		[CompilerGenerated]
		private string MenuFileName;

		[CompilerGenerated]
		private string MenuName;

		[CompilerGenerated]
		private string menuInfo;

		[CompilerGenerated]
		private string ModelName;

		[CompilerGenerated]
		private int ObjLayer;

		[CompilerGenerated]
		private string BundleName;

		[CompilerGenerated]
		private string ModelId;

		[CompilerGenerated]
		private int MyRoomObjectId;

		[CompilerGenerated]
		private long bGObjectId;

		[CompilerGenerated]
		private bool DelOnly;

		[CompilerGenerated]
		private string IconTextureName;

		[CompilerGenerated]
		private MPN PartCategory;

		[CompilerGenerated]
		private MaidParts.PARTS_COLOR PartsColor;

		public List<ModelInfo> models;

		public List<TextureChangeInfo> textureChanges;

		public List<MaterialChangeInfo> materialChanges;

		public List<ItemAnimationInfo> itemAnimations;

		public List<MaterialAnimatorInfo> materialAnimators;

		[CompilerGenerated]
		private string ModFileName;
	}
}
