using System;
using UnityEngine;

namespace COM3D2.NPRShader.Plugin
{
	internal static class GUIUtil
	{
		static GUIUtil()
		{
			GUIUtil.gsLabel.fontSize = 11;
			GUIUtil.gsLabel.alignment = TextAnchor.MiddleLeft;
		}

		public static void AddGUIButton(ControlBase parent, ControlBase elem, int rowButtonCount)
		{
			float lastElementSize = parent.LastElementSize;
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = lastElementSize + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width / (float)rowButtonCount - (float)(ControlBase.FixedMargin / 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUIButton(ControlBase parent, ControlBase elem, ControlBase reference, int rowButtonCount)
		{
			elem.Left = reference.Left + reference.Width;
			elem.Top = reference.Top;
			elem.Width = parent.Width / (float)rowButtonCount - (float)(ControlBase.FixedMargin / 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUIButtonNoRender(ControlBase parent, ControlBase elem, ControlBase reference, int rowButtonCount)
		{
			elem.Left = reference.Left + reference.Width;
			elem.Top = reference.Top;
			elem.Width = parent.Width / (float)rowButtonCount - (float)(ControlBase.FixedMargin / 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.Visible = true;
		}

		public static void AddGUIButtonAfter(ControlBase parent, ControlBase elem, ControlBase reference, int rowButtonCount)
		{
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = reference.Top + reference.Height + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width / (float)rowButtonCount - (float)(ControlBase.FixedMargin / 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUIButtonAfter(ControlBase parent, ControlBase elem, int rowButtonCount)
		{
			float lastElementSize = parent.LastElementSize;
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = lastElementSize + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width / (float)rowButtonCount - (float)(ControlBase.FixedMargin / 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUICheckbox(ControlBase parent, ControlBase elem)
		{
			float lastElementSize = parent.LastElementSize;
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = lastElementSize + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width - (float)(ControlBase.FixedMargin * 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUICheckbox(ControlBase parent, ControlBase elem, ControlBase reference)
		{
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = reference.Top + reference.Height + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width - (float)(ControlBase.FixedMargin * 4);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUICurve(ControlBase parent, ControlBase elem)
		{
			float lastElementSize = parent.LastElementSize;
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = lastElementSize + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width - (float)(ControlBase.FixedMargin * 4);
			elem.Height = GUIUtil.ControlHeight(parent) * 4f;
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUISlider(ControlBase parent, ControlBase elem)
		{
			float lastElementSize = parent.LastElementSize;
			elem.Left = parent.Left + (float)ControlBase.FixedMargin;
			elem.Top = lastElementSize + (float)ControlBase.FixedMargin;
			elem.Width = parent.Width - (float)(ControlBase.FixedMargin * 4);
			elem.Height = GUIUtil.ControlHeight(parent) * 2f;
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUISlider(ControlBase parent, ControlBase elem, string label)
		{
			float lastElementSize = parent.LastElementSize;
			Rect position = new Rect(parent.Left + (float)ControlBase.FixedMargin, lastElementSize + (float)ControlBase.FixedMargin, parent.Width - (float)(ControlBase.FixedMargin * 4), GUIUtil.ControlHeight(parent));
			GUI.Label(position, label, GUIUtil.gsLabel);
			position.y += position.height;
			elem.Left = position.x;
			elem.Top = position.y;
			elem.Width = position.width;
			elem.Height = position.height;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUISlider(ControlBase parent, ControlBase elem, ControlBase reference)
		{
			Rect rect = new Rect(parent.Left + (float)ControlBase.FixedMargin, reference.Top + reference.Height + (float)ControlBase.FixedMargin, parent.Width - (float)(ControlBase.FixedMargin * 4), GUIUtil.ControlHeight(parent) * 2f);
			elem.Left = rect.x;
			elem.Top = rect.y;
			elem.Width = rect.width;
			elem.Height = rect.height;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUISlider(ControlBase parent, ControlBase elem, ControlBase reference, string label)
		{
			Rect position = new Rect(parent.Left + (float)ControlBase.FixedMargin, reference.Top + reference.Height + (float)ControlBase.FixedMargin, parent.Width - (float)(ControlBase.FixedMargin * 4), GUIUtil.ControlHeight(parent));
			GUI.Label(position, label, GUIUtil.gsLabel);
			position.y += position.height;
			elem.Left = position.x;
			elem.Top = position.y;
			elem.Width = position.width;
			elem.Height = position.height;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddGUISliderNoRender(ControlBase parent, ControlBase elem, ControlBase reference)
		{
			Rect rect = new Rect(parent.Left + (float)ControlBase.FixedMargin, reference.Top + reference.Height + (float)ControlBase.FixedMargin, parent.Width - (float)(ControlBase.FixedMargin * 4), GUIUtil.ControlHeight(parent) * 2f);
			elem.Left = rect.x;
			elem.Top = rect.y;
			elem.Width = rect.width;
			elem.Height = rect.height;
			elem.Visible = true;
		}

		public static void AddResetButton(ControlBase parent, ControlBase elem)
		{
			elem.Left = parent.Left + parent.Width - elem.Width - (float)ControlBase.FixedMargin;
			elem.Top = parent.Top + (float)ControlBase.FixedMargin;
			elem.Width = (float)(parent.FontSize * 2);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.OnGUI();
			elem.Visible = true;
		}

		public static void AddResetButtonNoRender(ControlBase parent, ControlBase elem)
		{
			elem.Left = parent.Left + parent.Width - elem.Width - (float)ControlBase.FixedMargin;
			elem.Top = parent.Top + (float)ControlBase.FixedMargin;
			elem.Width = (float)(parent.FontSize * 2);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.Visible = true;
		}

		public static void AddChildResetButton(ControlBase parent, ControlBase elem)
		{
			float lastElementSize = parent.LastElementSize;
			elem.Left = parent.Left + parent.Width - elem.Width - (float)ControlBase.FixedMargin;
			elem.Top = lastElementSize + (float)ControlBase.FixedMargin;
			elem.Width = (float)(parent.FontSize * 2);
			elem.Height = GUIUtil.ControlHeight(parent);
			elem.FontSize = parent.FontSize;
			elem.OnGUI();
			elem.Visible = true;
		}

		public static float GetHeightForParent(ControlBase parent, ControlBase lastElem)
		{
			return lastElem.Top + lastElem.Height + (float)ControlBase.FixedMargin - parent.Top;
		}

		public static float GetHeightForParent(ControlBase parent)
		{
			float lastElementSize = parent.LastElementSize;
			return lastElementSize + (float)ControlBase.FixedMargin - parent.Top;
		}

		public static float ControlHeight(ControlBase parent)
		{
			return (float)(parent.FixedFontSize + ControlBase.FixedMargin);
		}

		internal static bool IsMouseOnRect(Rect rect)
		{
			return rect.Contains(new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y));
		}

		internal static bool GetAnyMouseDown()
		{
			return Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2);
		}

		internal static bool GetMouseWheelUse()
		{
			return Input.GetAxis("Mouse ScrollWheel") != 0f;
		}

		internal static void MouseClickOnGUIRect(Rect rect, float left, float top)
		{
			Vector2 vector = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
			bool flag = false;
			Vector2 a = Vector2.zero;
			Vector2 zero = Vector2.zero;
			if (rect.Contains(vector))
			{
				if (Input.GetAxis("Mouse ScrollWheel") != 0f)
				{
					Input.ResetInputAxes();
				}
				if (Input.GetMouseButtonDown(0))
				{
					flag = true;
					a = vector;
					zero = new Vector2(left, top);
					Input.ResetInputAxes();
				}
				if (Input.GetMouseButtonUp(0))
				{
					flag = false;
					Input.ResetInputAxes();
				}
				if (flag)
				{
					Vector2 vector2 = zero - (a - vector);
					if (vector2.x == 0f)
					{
						float y = vector2.y;
					}
				}
			}
		}

		internal static void MouseClickOnGUIRect(Rect rect)
		{
			Vector2 point = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
			if (rect.Contains(point) && (Input.GetAxis("Mouse ScrollWheel") != 0f || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)))
			{
				Input.ResetInputAxes();
			}
		}

		internal static void MouseClickAndDragOnGUIRect(Rect rect, float left, float top)
		{
			Vector2 vector = new Vector2(Input.mousePosition.x, (float)Screen.height - Input.mousePosition.y);
			bool flag = false;
			Vector2 a = Vector2.zero;
			Vector2 zero = Vector2.zero;
			if (rect.Contains(vector))
			{
				if (Input.GetAxis("Mouse ScrollWheel") != 0f)
				{
					Input.ResetInputAxes();
				}
				if (Input.GetMouseButtonDown(0))
				{
					flag = true;
					a = vector;
					zero = new Vector2(left, top);
				}
				if (flag)
				{
					Vector2 vector2 = zero - (a - vector);
					left = vector2.x;
					top = vector2.y;
				}
				if (Input.GetMouseButtonUp(0))
				{
					Input.ResetInputAxes();
				}
			}
		}

		private static GUIStyle gsLabel = new GUIStyle("label");
	}
}
