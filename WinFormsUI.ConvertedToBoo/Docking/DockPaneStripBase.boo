
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms
import System.Drawing
import System.Drawing.Drawing2D
import System.Collections
import System.Collections.Generic
import System.Security.Permissions
import System.Diagnostics.CodeAnalysis

public abstract class DockPaneStripBase(Control):

	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	protected class Tab(IDisposable):

		private m_content as IDockContent

		
		public def constructor(content as IDockContent):
			m_content = content

		
		def destructor():
			Dispose(false)

		
		public Content as IDockContent:
			get:
				return m_content

		
		public ContentForm as Form:
			get:
				return (m_content as Form)

		
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

		
		private m_dockPane as DockPane

		public DockPane as DockPane:
			get:
				return m_dockPane

		
		public Count as int:
			get:
				return DockPane.DisplayingContents.Count

		
		public self[index as int] as Tab:
			get:
				content as IDockContent = DockPane.DisplayingContents[index]
				if content is null:
					raise ArgumentOutOfRangeException('index')
				return content.DockHandler.GetTab(DockPane.TabStripControl)

		
		public def Contains(tab as Tab) as bool:
			return (IndexOf(tab) != (-1))

		
		public def Contains(content as IDockContent) as bool:
			return (IndexOf(content) != (-1))

		
		public def IndexOf(tab as Tab) as int:
			if tab is null:
				return (-1)
			
			return DockPane.DisplayingContents.IndexOf(tab.Content)

		
		public def IndexOf(content as IDockContent) as int:
			return DockPane.DisplayingContents.IndexOf(content)

	
	protected def constructor(pane as DockPane):
		m_dockPane = pane
		
		SetStyle(ControlStyles.OptimizedDoubleBuffer, true)
		SetStyle(ControlStyles.Selectable, false)
		AllowDrop = true

	
	private m_dockPane as DockPane

	protected DockPane as DockPane:
		get:
			return m_dockPane

	
	protected Appearance as DockPane.AppearanceStyle:
		get:
			return DockPane.Appearance

	
	private m_tabs as TabCollection = null

	protected Tabs as TabCollection:
		get:
			if m_tabs is null:
				m_tabs = TabCollection(DockPane)
			
			return m_tabs

	
	internal def RefreshChanges():
		if IsDisposed:
			return 
		
		OnRefreshChanges()

	
	protected virtual def OnRefreshChanges():
		pass

	
	protected abstract def MeasureHeight() as int:
		pass

	
	protected abstract def EnsureTabVisible(content as IDockContent):
		pass

	
	protected def HitTest() as int:
		return HitTest(PointToClient(Control.MousePosition))

	
	protected abstract def HitTest(point as Point) as int:
		pass

	
	protected abstract def GetOutline(index as int) as GraphicsPath:
		pass

	
	protected virtual def CreateTab(content as IDockContent) as Tab:
		return Tab(content)

	
	protected override def OnMouseDown(e as MouseEventArgs):
		super.OnMouseDown(e)
		
		index as int = HitTest()
		if index != (-1):
			content as IDockContent = Tabs[index].Content
			if DockPane.ActiveContent != content:
				DockPane.ActiveContent = content
		
		if e.Button == MouseButtons.Left:
			if (DockPane.DockPanel.AllowEndUserDocking and DockPane.AllowDockDragAndDrop) and DockPane.ActiveContent.DockHandler.AllowEndUserDocking:
				DockPane.DockPanel.BeginDrag(DockPane.ActiveContent.DockHandler)

	
	protected HasTabPageContextMenu as bool:
		get:
			return DockPane.HasTabPageContextMenu

	
	protected def ShowTabPageContextMenu(position as Point):
		DockPane.ShowTabPageContextMenu(self, position)

	
	protected override def OnMouseUp(e as MouseEventArgs):
		super.OnMouseUp(e)
		
		if e.Button == MouseButtons.Right:
			ShowTabPageContextMenu(Point(e.X, e.Y))

	
	[SecurityPermission(SecurityAction.LinkDemand, Flags: SecurityPermissionFlag.UnmanagedCode)]
	protected override def WndProc(ref m as Message):
		if m.Msg == cast(int, Win32.Msgs.WM_LBUTTONDBLCLK):
			super.WndProc(m)
			
			index as int = HitTest()
			if DockPane.DockPanel.AllowEndUserDocking and (index != (-1)):
				content as IDockContent = Tabs[index].Content
				if content.DockHandler.CheckDockState(not content.DockHandler.IsFloat) != DockState.Unknown:
					content.DockHandler.IsFloat = (not content.DockHandler.IsFloat)
			
			return 
		
		super.WndProc(m)
		return 

	
	protected override def OnDragOver(drgevent as DragEventArgs):
		super.OnDragOver(drgevent)
		
		index as int = HitTest()
		if index != (-1):
			content as IDockContent = Tabs[index].Content
			if DockPane.ActiveContent != content:
				DockPane.ActiveContent = content

