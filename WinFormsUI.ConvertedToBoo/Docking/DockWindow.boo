
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms
import System.Drawing
import System.Runtime.InteropServices
import System.ComponentModel

[ToolboxItem(false)]
partial public class DockWindow(Panel, INestedPanesContainer, ISplitterDragSource):

	private m_dockPanel as DockPanel

	private m_dockState as DockState

	private m_splitter as SplitterControl

	private m_nestedPanes as NestedPaneCollection

	
	internal def constructor(dockPanel as DockPanel, dockState as DockState):
		m_nestedPanes = NestedPaneCollection(self)
		m_dockPanel = dockPanel
		m_dockState = dockState
		Visible = false
		
		SuspendLayout()
		
		if (((DockState == DockState.DockLeft) or (DockState == DockState.DockRight)) or (DockState == DockState.DockTop)) or (DockState == DockState.DockBottom):
			m_splitter = SplitterControl()
			Controls.Add(m_splitter)
		
		if DockState == DockState.DockLeft:
			Dock = DockStyle.Left
			m_splitter.Dock = DockStyle.Right
		elif DockState == DockState.DockRight:
			Dock = DockStyle.Right
			m_splitter.Dock = DockStyle.Left
		elif DockState == DockState.DockTop:
			Dock = DockStyle.Top
			m_splitter.Dock = DockStyle.Bottom
		elif DockState == DockState.DockBottom:
			Dock = DockStyle.Bottom
			m_splitter.Dock = DockStyle.Top
		elif DockState == DockState.Document:
			Dock = DockStyle.Fill
		
		ResumeLayout()

	
	public VisibleNestedPanes as VisibleNestedPaneCollection:
		get:
			return NestedPanes.VisibleNestedPanes

	
	public NestedPanes as NestedPaneCollection:
		get:
			return m_nestedPanes

	
	public DockPanel as DockPanel:
		get:
			return m_dockPanel

	
	public DockState as DockState:
		get:
			return m_dockState

	
	public IsFloat as bool:
		get:
			return (DockState == DockState.Float)

	
	internal DefaultPane as DockPane:
		get:
			return (null if (VisibleNestedPanes.Count == 0) else VisibleNestedPanes[0])

	
	public virtual DisplayingRectangle as Rectangle:
		get:
			rect as Rectangle = ClientRectangle
			// if DockWindow is document, exclude the border
			if DockState == DockState.Document:
				rect.X += 1
				rect.Y += 1
				rect.Width -= 2
				rect.Height -= 2
			elif DockState == DockState.DockLeft:
			// exclude the splitter
				rect.Width -= Measures.SplitterSize
			elif DockState == DockState.DockRight:
				rect.X += Measures.SplitterSize
				rect.Width -= Measures.SplitterSize
			elif DockState == DockState.DockTop:
				rect.Height -= Measures.SplitterSize
			elif DockState == DockState.DockBottom:
				rect.Y += Measures.SplitterSize
				rect.Height -= Measures.SplitterSize
			
			return rect

	
	protected override def OnPaint(e as PaintEventArgs):
		// if DockWindow is document, draw the border
		if DockState == DockState.Document:
			e.Graphics.DrawRectangle(SystemPens.ControlDark, ClientRectangle.X, ClientRectangle.Y, (ClientRectangle.Width - 1), (ClientRectangle.Height - 1))
		
		super.OnPaint(e)

	
	protected override def OnLayout(levent as LayoutEventArgs):
		VisibleNestedPanes.Refresh()
		if VisibleNestedPanes.Count == 0:
			if Visible:
				Visible = false
		elif not Visible:
			Visible = true
			VisibleNestedPanes.Refresh()
		
		super.OnLayout(levent)

	
	#region ISplitterDragSource Members
	
	def ISplitterDragSource.BeginDrag(rectSplitter as Rectangle):
		pass

	
	def ISplitterDragSource.EndDrag():
		pass

	
	ISplitterDragSource.IsVertical as bool:
		get:
			return ((DockState == DockState.DockLeft) or (DockState == DockState.DockRight))

	
	ISplitterDragSource.DragLimitBounds as Rectangle:
		get:
			rectLimit as Rectangle = DockPanel.DockArea
			location as Point
			if (Control.ModifierKeys & Keys.Shift) == 0:
				location = Location
			else:
				location = DockPanel.DockArea.Location
			
			if cast(ISplitterDragSource, self).IsVertical:
				rectLimit.X += MeasurePane.MinSize
				rectLimit.Width -= (2 * MeasurePane.MinSize)
				rectLimit.Y = location.Y
				if (Control.ModifierKeys & Keys.Shift) == 0:
					rectLimit.Height = Height
			else:
				rectLimit.Y += MeasurePane.MinSize
				rectLimit.Height -= (2 * MeasurePane.MinSize)
				rectLimit.X = location.X
				if (Control.ModifierKeys & Keys.Shift) == 0:
					rectLimit.Width = Width
			
			return DockPanel.RectangleToScreen(rectLimit)

	
	def ISplitterDragSource.MoveSplitter(offset as int):
		if (Control.ModifierKeys & Keys.Shift) != 0:
			SendToBack()
		
		rectDockArea as Rectangle = DockPanel.DockArea
		if (DockState == DockState.DockLeft) and (rectDockArea.Width > 0):
			if DockPanel.DockLeftPortion > 1:
				DockPanel.DockLeftPortion = (Width + offset)
			else:
				DockPanel.DockLeftPortion += (cast(double, offset) / cast(double, rectDockArea.Width))
		elif (DockState == DockState.DockRight) and (rectDockArea.Width > 0):
			if DockPanel.DockRightPortion > 1:
				DockPanel.DockRightPortion = (Width - offset)
			else:
				DockPanel.DockRightPortion -= (cast(double, offset) / cast(double, rectDockArea.Width))
		elif (DockState == DockState.DockBottom) and (rectDockArea.Height > 0):
			if DockPanel.DockBottomPortion > 1:
				DockPanel.DockBottomPortion = (Height - offset)
			else:
				DockPanel.DockBottomPortion -= (cast(double, offset) / cast(double, rectDockArea.Height))
		elif (DockState == DockState.DockTop) and (rectDockArea.Height > 0):
			if DockPanel.DockTopPortion > 1:
				DockPanel.DockTopPortion = (Height + offset)
			else:
				DockPanel.DockTopPortion += (cast(double, offset) / cast(double, rectDockArea.Height))

	
	#region IDragSource Members
	
	IDragSource.DragControl as Control:
		get:
			return self
	
	#endregion
	#endregion

