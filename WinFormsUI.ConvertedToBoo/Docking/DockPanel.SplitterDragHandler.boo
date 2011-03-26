
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Text
import System.Windows.Forms
import System.Drawing
import System.Drawing.Drawing2D
import System.ComponentModel

partial internal class DockPanel:

	private final class SplitterDragHandler(DragHandler):

		private class SplitterOutline:

			public def constructor():
				m_dragForm = DragForm()
				SetDragForm(Rectangle.Empty)
				DragForm.BackColor = Color.Black
				DragForm.Opacity = 0.7
				DragForm.Show(false)

			
			private m_dragForm as DragForm

			private DragForm as DragForm:
				get:
					return m_dragForm

			
			public def Show(rect as Rectangle):
				SetDragForm(rect)

			
			public def Close():
				DragForm.Close()

			
			private def SetDragForm(rect as Rectangle):
				DragForm.Bounds = rect
				if rect == Rectangle.Empty:
					DragForm.Region = Region(Rectangle.Empty)
				elif DragForm.Region is not null:
					DragForm.Region = null

		
		public def constructor(dockPanel as DockPanel):
			super(dockPanel)

		
		public DragSource as ISplitterDragSource:
			get:
				return (super.DragSource as ISplitterDragSource)
			private set:
				super.DragSource = value

		
		private m_outline as SplitterOutline

		private Outline as SplitterOutline:
			get:
				return m_outline
			set:
				m_outline = value

		
		private m_rectSplitter as Rectangle

		private RectSplitter as Rectangle:
			get:
				return m_rectSplitter
			set:
				m_rectSplitter = value

		
		public def BeginDrag(dragSource as ISplitterDragSource, rectSplitter as Rectangle):
			DragSource = dragSource
			RectSplitter = rectSplitter
			
			if not BeginDrag():
				DragSource = null
				return 
			
			Outline = SplitterOutline()
			Outline.Show(rectSplitter)
			DragSource.BeginDrag(rectSplitter)

		
		protected override def OnDragging():
			Outline.Show(GetSplitterOutlineBounds(Control.MousePosition))

		
		protected override def OnEndDrag(abort as bool):
			DockPanel.SuspendLayout(true)
			
			Outline.Close()
			
			if not abort:
				DragSource.MoveSplitter(GetMovingOffset(Control.MousePosition))
			
			DragSource.EndDrag()
			DockPanel.ResumeLayout(true, true)

		
		private def GetMovingOffset(ptMouse as Point) as int:
			rect as Rectangle = GetSplitterOutlineBounds(ptMouse)
			if DragSource.IsVertical:
				return (rect.X - RectSplitter.X)
			else:
				return (rect.Y - RectSplitter.Y)

		
		private def GetSplitterOutlineBounds(ptMouse as Point) as Rectangle:
			rectLimit as Rectangle = DragSource.DragLimitBounds
			
			rect as Rectangle = RectSplitter
			if (rectLimit.Width <= 0) or (rectLimit.Height <= 0):
				return rect
			
			if DragSource.IsVertical:
				rect.X += (ptMouse.X - StartMousePosition.X)
				rect.Height = rectLimit.Height
			else:
				rect.Y += (ptMouse.Y - StartMousePosition.Y)
				rect.Width = rectLimit.Width
			
			if rect.Left < rectLimit.Left:
				rect.X = rectLimit.X
			if rect.Top < rectLimit.Top:
				rect.Y = rectLimit.Y
			if rect.Right > rectLimit.Right:
				rect.X -= (rect.Right - rectLimit.Right)
			if rect.Bottom > rectLimit.Bottom:
				rect.Y -= (rect.Bottom - rectLimit.Bottom)
			
			return rect

	
	private m_splitterDragHandler as SplitterDragHandler = null

	private def GetSplitterDragHandler() as SplitterDragHandler:
		if m_splitterDragHandler is null:
			m_splitterDragHandler = SplitterDragHandler(self)
		return m_splitterDragHandler

	
	internal def BeginDrag(dragSource as ISplitterDragSource, rectSplitter as Rectangle):
		GetSplitterDragHandler().BeginDrag(dragSource, rectSplitter)

