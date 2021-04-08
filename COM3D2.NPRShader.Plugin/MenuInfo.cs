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
				BGObjectId = id
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
				return this.<modelType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<modelType>k__BackingField = value;
			}
		}

		public string menuFileName
		{
			[CompilerGenerated]
			get
			{
				return this.<menuFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<menuFileName>k__BackingField = value;
			}
		}

		public string menuName
		{
			[CompilerGenerated]
			get
			{
				return this.<menuName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<menuName>k__BackingField = value;
			}
		}

		public string menuInfo
		{
			[CompilerGenerated]
			get
			{
				return this.<menuInfo>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<menuInfo>k__BackingField = value;
			}
		}

		public string modelName
		{
			[CompilerGenerated]
			get
			{
				return this.<modelName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<modelName>k__BackingField = value;
			}
		}

		public int objLayer
		{
			[CompilerGenerated]
			get
			{
				return this.<objLayer>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<objLayer>k__BackingField = value;
			}
		}

		public string bundleName
		{
			[CompilerGenerated]
			get
			{
				return this.<bundleName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<bundleName>k__BackingField = value;
			}
		}

		public string modelId
		{
			[CompilerGenerated]
			get
			{
				return this.<modelId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<modelId>k__BackingField = value;
			}
		}

		public int myRoomObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.<myRoomObjectId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<myRoomObjectId>k__BackingField = value;
			}
		}

		public long BGObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.<BGObjectId>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<BGObjectId>k__BackingField = value;
			}
		}

		public bool delOnly
		{
			[CompilerGenerated]
			get
			{
				return this.<delOnly>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<delOnly>k__BackingField = value;
			}
		}

		public string iconTextureName
		{
			[CompilerGenerated]
			get
			{
				return this.<iconTextureName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<iconTextureName>k__BackingField = value;
			}
		}

		public MPN partCategory
		{
			[CompilerGenerated]
			get
			{
				return this.<partCategory>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<partCategory>k__BackingField = value;
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

		public string modFileName
		{
			[CompilerGenerated]
			get
			{
				return this.<modFileName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<modFileName>k__BackingField = value;
			}
		}

		[CompilerGenerated]
		private ModelType <modelType>k__BackingField;

		[CompilerGenerated]
		private string <menuFileName>k__BackingField;

		[CompilerGenerated]
		private string <menuName>k__BackingField;

		[CompilerGenerated]
		private string <menuInfo>k__BackingField;

		[CompilerGenerated]
		private string <modelName>k__BackingField;

		[CompilerGenerated]
		private int <objLayer>k__BackingField;

		[CompilerGenerated]
		private string <bundleName>k__BackingField;

		[CompilerGenerated]
		private string <modelId>k__BackingField;

		[CompilerGenerated]
		private int <myRoomObjectId>k__BackingField;

		[CompilerGenerated]
		private long <BGObjectId>k__BackingField;

		[CompilerGenerated]
		private bool <delOnly>k__BackingField;

		[CompilerGenerated]
		private string <iconTextureName>k__BackingField;

		[CompilerGenerated]
		private MPN <partCategory>k__BackingField;

		[CompilerGenerated]
		private MaidParts.PARTS_COLOR <partsColor>k__BackingField;

		public List<ModelInfo> models;

		public List<TextureChangeInfo> textureChanges;

		public List<MaterialChangeInfo> materialChanges;

		public List<ItemAnimationInfo> itemAnimations;

		public List<MaterialAnimatorInfo> materialAnimators;

		[CompilerGenerated]
		private string <modFileName>k__BackingField;
	}
}
