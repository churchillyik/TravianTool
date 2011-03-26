
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections
import System.ComponentModel
import System.Drawing
import System.Windows.Forms

partial internal class DockPane:

	private class SplitterControl(Control, ISplitterDragSource):

		private m_pane as DockPane

		
		public def constructor(pane as DockPane):
			SetStyle(ControlStyles.Selectable, false)
			m_pane = pane

		
		public DockPane as DockPane:
			get:
				return m_pane

		
		private m_alignment as DockAlignment

		public Alignment as DockAlignment:
			get:
				return m_alignment
			set:
				m_alignment = value
				if (m_alignment == DockAlignment.Left) or (m_alignment == DockAlignment.Right):
					Cursor = Cursors.VSplit
				elif (m_alignment == DockAlignment.Top) or (m_alignment == DockAlignment.Bottom):
					Cursor = Cursors.HSplit
				else:
					Cursor = Cursors.Default
				
				if DockPane.DockState == DockState.Document:
					Invalidate()

		
		protected override def OnPaint(e as PaintEventArgs):
			super.OnPaint(e)
			
			if DockPane.DockState != DockState.Document:
				return 
			
			g as Graphics = e.Graphics
			rect as Rectangle = ClientRectangle
			if (Alignment == DockAlignment.Top) or (Alignment == DockAlignment.Bottom):
				g.DrawLine(SystemPens.ControlDark, rect.Left, (rect.Bottom - 1), rect.Right, (rect.Bottom - 1))
			elif (Alignment == DockAlignment.Left) or (Alignment == DockAlignment.Right):
				g.DrawLine(SystemPens.ControlDarkDark, (rect.Right - 1), rect.Top, (rect.Right - 1), rect.Bottom)

		
		protected override def OnMouseDown(e as MouseEventArgs):
			super.OnMouseDown(e)
			
			if e.Button != MouseButtons.Left:
				return 
			
			DockPane.DockPanel.BeginDrag(self, Parent.RectangleToScreen(Bounds))

		
		#region ISplitterDragSource Members
		
		def ISplitterDragSource.BeginDrag(rectSplitter as Rectangle):
			pass

		
		def ISplitterDragSource.EndDrag():
			pass

		
		ISplitterDragSource.IsVertical as bool:
			get:
				status as NestedDockingStatus = DockPane.NestedDockingStatus
				return ((status.DisplayingAlignment == DockAlignment.Left) or (status.DisplayingAlignment == DockAlignment.Right))

		
		ISplitterDragSource.DragLimitBounds as Rectangle:
			get:
				status as NestedDockingStatus = DockPane.NestedDockingStatus
				rectLimit as Rectangle = Parent.RectangleToScreen(status.LogicalBounds)
				if cast(ISplitterDragSource, self).IsVertical:
					rectLimit.X += MeasurePane.MinSize
					rectLimit.Width -= (2 * MeasurePane.MinSize)
				else:
					rectLimit.Y += MeasurePane.MinSize
					rectLimit.Height -= (2 * MeasurePane.MinSize)
				
				return rectLimit

		
		def ISplitterDragSource.MoveSplitter(offset as int):
			status as NestedDockingStatus = DockPane.NestedDockingStatus
			proportion as double = status.Proportion
			if (status.LogicalBounds.Width <= 0) or (status.LogicalBounds.Height <= 0):
				return 
			elif status.DisplayingAlignment == DockAlignment.Left:
				proportion += (cast(double, offset) / cast(double, status.LogicalBounds.Width))
			elif status.DisplayingAlignment == DockAlignment.Right:
				proportion -= (cast(double, offset) / cast(double, status.LogicalBounds.Width))
			elif status.DisplayingAlignment == DockAlignment.Top:
				proportion += (cast(double, offset) / cast(double, status.LogicalBounds.Height))
			else:
				proportion -= (cast(double, offset) / cast(double, status.LogicalBounds.Height))
			
			DockPane.SetNestedDockingProportion(proportion)

		
		#region IDragSource Members
		
		IDragSource.DragControl as Control:
			get:
				return self
		
		#endregion
		
		#endregion

	
	private m_splitter as SplitterControl

	private Splitter as SplitterControl:
		get:
			return m_splitter

	
	internal SplitterBounds as Rectangle:
		set:
			Splitter.Bounds = value

	
	internal SplitterAlignment as DockAlignment:
		set:
			Splitter.Alignment = value

