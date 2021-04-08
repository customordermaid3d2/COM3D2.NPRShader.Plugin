using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace COM3D2.NPRShader.Plugin
{
	internal class ControlBase
	{
		private static int FixPx(int px)
		{
			return (int)((1f + ((float)Screen.width / 1280f - 1f) * 0.3f) * (float)px);
		}

		protected virtual void AwakeChildControls()
		{
			try
			{
				foreach (ControlBase controlBase in this._childControls)
				{
					controlBase.Awake();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		protected virtual void OnSceneLoadedChildControls(Scene scene, LoadSceneMode sceneMode)
		{
			try
			{
				foreach (ControlBase controlBase in this._childControls)
				{
					controlBase.OnSceneLoaded(scene, sceneMode);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		protected virtual void UpdateChildControls()
		{
			try
			{
				foreach (ControlBase controlBase in this._childControls)
				{
					controlBase.Update();
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		protected virtual void OnGUIChildControls()
		{
			try
			{
				foreach (ControlBase controlBase in this._childControls)
				{
					if (controlBase.Visible)
					{
						controlBase.ScreenPos = this.ScreenPos;
						controlBase.OnGUI();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		protected virtual void OnGUIChildControls(Action<ControlBase, ControlBase> action)
		{
			try
			{
				foreach (ControlBase controlBase in this._childControls)
				{
					if (controlBase.Visible)
					{
						controlBase.ScreenPos = this.ScreenPos;
						controlBase.OnGUI();
						action(this, controlBase);
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void SetAllVisible(bool value, int ignoreId)
		{
			try
			{
				for (int i = 0; i < this.ChildControls.Count; i++)
				{
					if (ignoreId != i)
					{
						ControlBase controlBase = this.ChildControls[i];
						controlBase.Visible = value;
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError(ex.ToString());
			}
		}

		public void SetFromRect(Rect rect)
		{
			this.Left = rect.x;
			this.Top = rect.y;
			this.Width = rect.width;
			this.Height = rect.height;
		}

		public virtual void Awake()
		{
		}

		public virtual void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
		{
		}

		public virtual void Update()
		{
		}

		public virtual void OnGUI()
		{
		}

		public virtual void Reset()
		{
		}

		protected virtual List<ControlBase> ChildControls
		{
			get
			{
				return this._childControls;
			}
			set
			{
				this._childControls = value;
			}
		}

		public virtual bool Enabled
		{
			get
			{
				return this._enabled;
			}
			set
			{
				this._enabled = value;
			}
		}

		public virtual bool Visible
		{
			get
			{
				return this._visible;
			}
			set
			{
				this._visible = value;
			}
		}

		public virtual float LastElementSize
		{
			get
			{
				List<ControlBase> list = (from x in this._childControls
				where x.Visible
				select x).ToList<ControlBase>();
				if (list.Count == 0)
				{
					return this.Top + this.Height;
				}
				ControlBase controlBase = (from c in list
				orderby c.Top + c.Height descending
				select c).First<ControlBase>();
				return controlBase.Top + controlBase.Height;
			}
		}

		public virtual void OrderChildControls()
		{
			this._childControls = (from c in this._childControls
			orderby c.Top
			select c).ToList<ControlBase>();
		}

		public virtual Rect WindowRect
		{
			get
			{
				return new Rect(this._left, this._top, this._width, this._height);
			}
		}

		public Rect ScreenPos
		{
			[CompilerGenerated]
			get
			{
				return this.screenPos;
			}
			[CompilerGenerated]
			set
			{
				this.screenPos = value;
			}
		}

		public virtual string Text
		{
			get
			{
				return this._text;
			}
			set
			{
				this._text = value;
			}
		}

		public virtual Color TextColor
		{
			get
			{
				return this._textColor;
			}
			set
			{
				this._textColor = value;
			}
		}

		public virtual Color BackgroundColor
		{
			get
			{
				return this._backgroundColor;
			}
			set
			{
				this._backgroundColor = value;
			}
		}

		public virtual float Left
		{
			get
			{
				return this._left;
			}
			set
			{
				this._left = value;
			}
		}

		public virtual float Top
		{
			get
			{
				return this._top;
			}
			set
			{
				this._top = value;
			}
		}

		public virtual float Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this._width = value;
			}
		}

		public virtual float Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this._height = value;
			}
		}

		public virtual int FontSize
		{
			get
			{
				return this._fontSize;
			}
			set
			{
				this._fontSize = value;
			}
		}

		public virtual int FixedFontSize
		{
			get
			{
				return ControlBase.FixPx(this._fontSize);
			}
		}

		public static int FixedMargin
		{
			get
			{
				return ControlBase.FixPx(5);
			}
		}

		public ControlBase()
		{
		}

		private List<ControlBase> _childControls = new List<ControlBase>();

		private bool _enabled = true;

		private bool _visible = true;

		[CompilerGenerated]
		private Rect screenPos;

		private string _text = string.Empty;

		private Color _textColor = Color.white;

		private Color _backgroundColor = Color.black;

		private float _left;

		private float _top;

		private float _width;

		private float _height;

		private int _fontSize;

		public const int Margin = 5;

		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			public <>c()
			{
			}

			internal bool <get_LastElementSize>b__26_0(ControlBase x)
			{
				return x.Visible;
			}

			internal float <get_LastElementSize>b__26_1(ControlBase c)
			{
				return c.Top + c.Height;
			}

			internal float <OrderChildControls>b__27_0(ControlBase c)
			{
				return c.Top;
			}

			public static readonly ControlBase.<>c <>9 = new ControlBase.<>c();

			public static Func<ControlBase, bool> <>9__26_0;

			public static Func<ControlBase, float> <>9__26_1;

			public static Func<ControlBase, float> <>9__27_0;
		}
	}
}
