
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections
import System.Windows.Forms
import System.Drawing
import System.Drawing.Drawing2D
import System.Collections.Generic
import System.Diagnostics.CodeAnalysis

partial public abstract class AutoHideStripBase(Control):

	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	protected class Tab(IDisposable):

		private m_content as IDockContent

		
		protected def constructor(content as IDockContent):
			m_content = content

		
		def destructor():
			Dispose(false)

		
		public Content as IDockContent:
			get:
				return m_content

		
		public def Dispose():
			Dispose(true)
			GC.SuppressFinalize(self)

		
		protected virtual def Dispose(disposing as bool):
			pass

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	protected final class TabCollection(IEnumerable[of Tab]):

		#region IEnumerable Members
		def IEnumerable[of Tab].GetEnumerator() as IEnumerator[of Tab]:
			for i in range(0, Count):
				yield self[i]

		
		def IEnumerable.GetEnumerator() as IEnumerator:
			for i in range(0, Count):
				yield self[i]

		#endregion
		
		internal def constructor(pane as DockPane):
			m_dockPane = pane

		
		private m_dockPane as DockPane = null

		public DockPane as DockPane:
			get:
				return m_dockPane

		
		public DockPanel as DockPanel:
			get:
				return DockPane.DockPanel

		
		public Count as int:
			get:
				return DockPane.DisplayingContents.Count

		
		public self[index as int] as Tab:
			get:
				content as IDockContent = DockPane.DisplayingContents[index]
				if content is null:
					raise ArgumentOutOfRangeException('index')
				if content.DockHandler.AutoHideTab is null:
					content.DockHandler.AutoHideTab = DockPanel.AutoHideStripControl.CreateTab(content)
				return (content.DockHandler.AutoHideTab as Tab)

		
		public def Contains(tab as Tab) as bool:
			return (IndexOf(tab) != (-1))

		
		public def Contains(content as IDockContent) as bool:
			return (IndexOf(content) != (-1))

		
		public def IndexOf(tab as Tab) as int:
			if tab is null:
				return (-1)
			
			return IndexOf(tab.Content)

		
		public def IndexOf(content as IDockContent) as int:
			return DockPane.DisplayingContents.IndexOf(content)

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	protected class Pane(IDisposable):

		private m_dockPane as DockPane

		
		protected def constructor(dockPane as DockPane):
			m_dockPane = dockPane

		
		def destructor():
			Dispose(false)

		
		public DockPane as DockPane:
			get:
				return m_dockPane

		
		public AutoHideTabs as TabCollection:
			get:
				if DockPane.AutoHideTabs is null:
					DockPane.AutoHideTabs = TabCollection(DockPane)
				return (DockPane.AutoHideTabs as TabCollection)

		
		public def Dispose():
			Dispose(true)
			GC.SuppressFinalize(self)

		
		protected virtual def Dispose(disposing as bool):
			pass

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	protected final class PaneCollection(IEnumerable[of Pane]):

		private class AutoHideState:

			public m_dockState as DockState

			public m_selected = false

			
			public def constructor(dockState as DockState):
				m_dockState = dockState

			
			public DockState as DockState:
				get:
					return m_dockState

			
			public Selected as bool:
				get:
					return m_selected
				set:
					m_selected = value

		
		private class AutoHideStateCollection:

			private m_states as (AutoHideState)

			
			public def constructor():
				m_states = (of AutoHideState: AutoHideState(DockState.DockTopAutoHide), AutoHideState(DockState.DockBottomAutoHide), AutoHideState(DockState.DockLeftAutoHide), AutoHideState(DockState.DockRightAutoHide))

			
			public self[dockState as DockState] as AutoHideState:
				get:
					for i in range(0, m_states.Length):
						if m_states[i].DockState == dockState:
							return m_states[i]
					raise ArgumentOutOfRangeException('dockState')

			
			public def ContainsPane(pane as DockPane) as bool:
				if pane.IsHidden:
					return false
				for i in range(0, m_states.Length):
				
					if (m_states[i].DockState == pane.DockState) and m_states[i].Selected:
						return true
				return false

		
		internal def constructor(panel as DockPanel, dockState as DockState):
			m_dockPanel = panel
			m_states = AutoHideStateCollection()
			States[DockState.DockTopAutoHide].Selected = (dockState == DockState.DockTopAutoHide)
			States[DockState.DockBottomAutoHide].Selected = (dockState == DockState.DockBottomAutoHide)
			States[DockState.DockLeftAutoHide].Selected = (dockState == DockState.DockLeftAutoHide)
			States[DockState.DockRightAutoHide].Selected = (dockState == DockState.DockRightAutoHide)

		
		private m_dockPanel as DockPanel

		public DockPanel as DockPanel:
			get:
				return m_dockPanel

		
		private m_states as AutoHideStateCollection

		private States as AutoHideStateCollection:
			get:
				return m_states

		
		public Count as int:
			get:
				count = 0
				for pane as DockPane in DockPanel.Panes:
					if States.ContainsPane(pane):
						count += 1
				
				return count

		
		public self[index as int] as Pane:
			get:
				count = 0
				for pane as DockPane in DockPanel.Panes:
					if not States.ContainsPane(pane):
						continue 
					
					if count == index:
						if pane.AutoHidePane is null:
							pane.AutoHidePane = DockPanel.AutoHideStripControl.CreatePane(pane)
						return (pane.AutoHidePane as Pane)
					
					count += 1
				raise ArgumentOutOfRangeException('index')

		
		public def Contains(pane as Pane) as bool:
			return (IndexOf(pane) != (-1))

		
		public def IndexOf(pane as Pane) as int:
			if pane is null:
				return (-1)
			
			index = 0
			for dockPane as DockPane in DockPanel.Panes:
				if not States.ContainsPane(pane.DockPane):
					continue 
				
				if pane == dockPane.AutoHidePane:
					return index
				
				index += 1
			return (-1)

		
		#region IEnumerable Members
		
		def IEnumerable[of Pane].GetEnumerator() as IEnumerator[of Pane]:
			for i in range(0, Count):
				yield self[i]

		
		def IEnumerable.GetEnumerator() as IEnumerator:
			for i in range(0, Count):
				yield self[i]
		
		#endregion

	
	protected def constructor(panel as DockPanel):
		m_dockPanel = panel
		m_panesTop = PaneCollection(panel, DockState.DockTopAutoHide)
		m_panesBottom = PaneCollection(panel, DockState.DockBottomAutoHide)
		m_panesLeft = PaneCollection(panel, DockState.DockLeftAutoHide)
		m_panesRight = PaneCollection(panel, DockState.DockRightAutoHide)
		
		SetStyle(ControlStyles.OptimizedDoubleBuffer, true)
		SetStyle(ControlStyles.Selectable, false)

	
	private m_dockPanel as DockPanel

	protected DockPanel as DockPanel:
		get:
			return m_dockPanel

	
	private m_panesTop as PaneCollection

	protected PanesTop as PaneCollection:
		get:
			return m_panesTop

	
	private m_panesBottom as PaneCollection

	protected PanesBottom as PaneCollection:
		get:
			return m_panesBottom

	
	private m_panesLeft as PaneCollection

	protected PanesLeft as PaneCollection:
		get:
			return m_panesLeft

	
	private m_panesRight as PaneCollection

	protected PanesRight as PaneCollection:
		get:
			return m_panesRight

	
	protected def GetPanes(dockState as DockState) as PaneCollection:
		if dockState == DockState.DockTopAutoHide:
			return PanesTop
		elif dockState == DockState.DockBottomAutoHide:
			return PanesBottom
		elif dockState == DockState.DockLeftAutoHide:
			return PanesLeft
		elif dockState == DockState.DockRightAutoHide:
			return PanesRight
		else:
			raise ArgumentOutOfRangeException('dockState')

	
	internal def GetNumberOfPanes(dockState as DockState) as int:
		return GetPanes(dockState).Count

	
	protected RectangleTopLeft as Rectangle:
		get:
			height as int = MeasureHeight()
			return (Rectangle(0, 0, height, height) if ((PanesTop.Count > 0) and (PanesLeft.Count > 0)) else Rectangle.Empty)

	
	protected RectangleTopRight as Rectangle:
		get:
			height as int = MeasureHeight()
			return (Rectangle((Width - height), 0, height, height) if ((PanesTop.Count > 0) and (PanesRight.Count > 0)) else Rectangle.Empty)

	
	protected RectangleBottomLeft as Rectangle:
		get:
			height as int = MeasureHeight()
			return (Rectangle(0, (Height - height), height, height) if ((PanesBottom.Count > 0) and (PanesLeft.Count > 0)) else Rectangle.Empty)

	
	protected RectangleBottomRight as Rectangle:
		get:
			height as int = MeasureHeight()
			return (Rectangle((Width - height), (Height - height), height, height) if ((PanesBottom.Count > 0) and (PanesRight.Count > 0)) else Rectangle.Empty)

	
	protected def GetTabStripRectangle(dockState as DockState) as Rectangle:
		height as int = MeasureHeight()
		if (dockState == DockState.DockTopAutoHide) and (PanesTop.Count > 0):
			return Rectangle(RectangleTopLeft.Width, 0, ((Width - RectangleTopLeft.Width) - RectangleTopRight.Width), height)
		elif (dockState == DockState.DockBottomAutoHide) and (PanesBottom.Count > 0):
			return Rectangle(RectangleBottomLeft.Width, (Height - height), ((Width - RectangleBottomLeft.Width) - RectangleBottomRight.Width), height)
		elif (dockState == DockState.DockLeftAutoHide) and (PanesLeft.Count > 0):
			return Rectangle(0, RectangleTopLeft.Width, height, ((Height - RectangleTopLeft.Height) - RectangleBottomLeft.Height))
		elif (dockState == DockState.DockRightAutoHide) and (PanesRight.Count > 0):
			return Rectangle((Width - height), RectangleTopRight.Width, height, ((Height - RectangleTopRight.Height) - RectangleBottomRight.Height))
		else:
			return Rectangle.Empty

	
	private m_displayingArea as GraphicsPath = null

	private DisplayingArea as GraphicsPath:
		get:
			if m_displayingArea is null:
				m_displayingArea = GraphicsPath()
			
			return m_displayingArea

	
	private def SetRegion():
		DisplayingArea.Reset()
		DisplayingArea.AddRectangle(RectangleTopLeft)
		DisplayingArea.AddRectangle(RectangleTopRight)
		DisplayingArea.AddRectangle(RectangleBottomLeft)
		DisplayingArea.AddRectangle(RectangleBottomRight)
		DisplayingArea.AddRectangle(GetTabStripRectangle(DockState.DockTopAutoHide))
		DisplayingArea.AddRectangle(GetTabStripRectangle(DockState.DockBottomAutoHide))
		DisplayingArea.AddRectangle(GetTabStripRectangle(DockState.DockLeftAutoHide))
		DisplayingArea.AddRectangle(GetTabStripRectangle(DockState.DockRightAutoHide))
		Region = Region(DisplayingArea)

	
	protected override def OnMouseDown(e as MouseEventArgs):
		super.OnMouseDown(e)
		
		if e.Button != MouseButtons.Left:
			return 
		
		content as IDockContent = HitTest()
		if content is null:
			return 
		
		content.DockHandler.Activate()

	
	protected override def OnMouseHover(e as EventArgs):
		super.OnMouseHover(e)
		
		content as IDockContent = HitTest()
		if (content is not null) and (DockPanel.ActiveAutoHideContent != content):
			DockPanel.ActiveAutoHideContent = content
		
		// requires further tracking of mouse hover behavior,
		ResetMouseEventArgs()

	
	protected override def OnLayout(levent as LayoutEventArgs):
		RefreshChanges()
		super.OnLayout(levent)

	
	internal def RefreshChanges():
		if IsDisposed:
			return 
		
		SetRegion()
		OnRefreshChanges()

	
	protected virtual def OnRefreshChanges():
		pass

	
	protected abstract def MeasureHeight() as int:
		pass

	
	private def HitTest() as IDockContent:
		ptMouse as Point = PointToClient(Control.MousePosition)
		return HitTest(ptMouse)

	
	protected virtual def CreateTab(content as IDockContent) as Tab:
		return Tab(content)

	
	protected virtual def CreatePane(dockPane as DockPane) as Pane:
		return Pane(dockPane)

	
	protected abstract def HitTest(point as Point) as IDockContent:
		pass

