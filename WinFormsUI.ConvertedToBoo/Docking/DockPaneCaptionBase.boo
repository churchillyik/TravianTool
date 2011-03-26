
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms
import System.Drawing
import System.Runtime.InteropServices
import System.Security.Permissions

public abstract class DockPaneCaptionBase(Control):

	protected def constructor(pane as DockPane):
		m_dockPane = pane
		
		SetStyle((((ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw) | ControlStyles.UserPaint) | ControlStyles.AllPaintingInWmPaint), true)
		SetStyle(ControlStyles.Selectable, false)

	
	private m_dockPane as DockPane

	protected DockPane as DockPane:
		get:
			return m_dockPane

	
	protected Appearance as DockPane.AppearanceStyle:
		get:
			return DockPane.Appearance

	
	protected HasTabPageContextMenu as bool:
		get:
			return DockPane.HasTabPageContextMenu

	
	protected def ShowTabPageContextMenu(position as Point):
		DockPane.ShowTabPageContextMenu(self, position)

	
	protected override def OnMouseUp(e as MouseEventArgs):
		super.OnMouseUp(e)
		
		if e.Button == MouseButtons.Right:
			ShowTabPageContextMenu(Point(e.X, e.Y))

	
	protected override def OnMouseDown(e as MouseEventArgs):
		super.OnMouseDown(e)
		
		if ((((e.Button == MouseButtons.Left) and DockPane.DockPanel.AllowEndUserDocking) and DockPane.AllowDockDragAndDrop) and (not DockHelper.IsDockStateAutoHide(DockPane.DockState))) and (DockPane.ActiveContent is not null):
			DockPane.DockPanel.BeginDrag(DockPane)

	
	[SecurityPermission(SecurityAction.LinkDemand, Flags: SecurityPermissionFlag.UnmanagedCode)]
	protected override def WndProc(ref m as Message):
		if m.Msg == cast(int, Win32.Msgs.WM_LBUTTONDBLCLK):
			if DockHelper.IsDockStateAutoHide(DockPane.DockState):
				DockPane.DockPanel.ActiveAutoHideContent = null
				return 
			
			if DockPane.IsFloat:
				DockPane.RestoreToPanel()
			else:
				DockPane.Float()
		super.WndProc(m)

	
	internal def RefreshChanges():
		if IsDisposed:
			return 
		
		OnRefreshChanges()

	
	protected virtual def OnRightToLeftLayoutChanged():
		pass

	
	protected virtual def OnRefreshChanges():
		pass

	
	protected abstract def MeasureHeight() as int:
		pass

