
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections
import System.Drawing
import System.Windows.Forms
import System.Runtime.InteropServices
import System.Security.Permissions
import System.Diagnostics.CodeAnalysis

public class FloatWindow(Form, INestedPanesContainer, IDockDragSource):

	private m_nestedPanes as NestedPaneCollection

	internal static final WM_CHECKDISPOSE = cast(int, (Win32.Msgs.WM_USER + 1))

	
	protected def constructor(dockPanel as DockPanel, pane as DockPane):
		InternalConstruct(dockPanel, pane, false, Rectangle.Empty)

	
	protected def constructor(dockPanel as DockPanel, pane as DockPane, bounds as Rectangle):
		InternalConstruct(dockPanel, pane, true, bounds)

	
	private def InternalConstruct(dockPanel as DockPanel, pane as DockPane, boundsSpecified as bool, bounds as Rectangle):
		if dockPanel is null:
			raise ArgumentNullException(Strings.FloatWindow_Constructor_NullDockPanel)
		
		m_nestedPanes = NestedPaneCollection(self)
		
		FormBorderStyle = FormBorderStyle.SizableToolWindow
		ShowInTaskbar = false
		if dockPanel.RightToLeft != RightToLeft:
			RightToLeft = dockPanel.RightToLeft
		if RightToLeftLayout != dockPanel.RightToLeftLayout:
			RightToLeftLayout = dockPanel.RightToLeftLayout
		
		SuspendLayout()
		if boundsSpecified:
			Bounds = bounds
			StartPosition = FormStartPosition.Manual
		else:
			StartPosition = FormStartPosition.WindowsDefaultLocation
			Size = dockPanel.DefaultFloatWindowSize
		
		m_dockPanel = dockPanel
		Owner = DockPanel.FindForm()
		DockPanel.AddFloatWindow(self)
		if pane is not null:
			pane.FloatWindow = self
		
		ResumeLayout()

	
	protected override def Dispose(disposing as bool):
		if disposing:
			if DockPanel is not null:
				DockPanel.RemoveFloatWindow(self)
			m_dockPanel = null
		super.Dispose(disposing)

	
	private m_allowEndUserDocking = true

	public AllowEndUserDocking as bool:
		get:
			return m_allowEndUserDocking
		set:
			m_allowEndUserDocking = value

	
	public NestedPanes as NestedPaneCollection:
		get:
			return m_nestedPanes

	
	public VisibleNestedPanes as VisibleNestedPaneCollection:
		get:
			return NestedPanes.VisibleNestedPanes

	
	private m_dockPanel as DockPanel

	public DockPanel as DockPanel:
		get:
			return m_dockPanel

	
	public DockState as DockState:
		get:
			return DockState.Float

	
	public IsFloat as bool:
		get:
			return (DockState == DockState.Float)

	
	internal def IsDockStateValid(dockState as DockState) as bool:
		for pane as DockPane in NestedPanes:
			for content as IDockContent in pane.Contents:
				if not DockHelper.IsDockStateValid(dockState, content.DockHandler.DockAreas):
					return false
		
		return true

	
	protected override def OnActivated(e as EventArgs):
		DockPanel.FloatWindows.BringWindowToFront(self)
		super.OnActivated(e)

	
	protected override def OnLayout(levent as LayoutEventArgs):
		VisibleNestedPanes.Refresh()
		RefreshChanges()
		Visible = (VisibleNestedPanes.Count > 0)
		SetText()
		
		super.OnLayout(levent)

	
	
	[SuppressMessage('Microsoft.Globalization', 'CA1303:DoNotPassLiteralsAsLocalizedParameters', MessageId: 'System.Windows.Forms.Control.set_Text(System.String)')]
	internal def SetText():
		theOnlyPane as DockPane = (VisibleNestedPanes[0] if (VisibleNestedPanes.Count == 1) else null)
		
		if theOnlyPane is null:
			Text = ' '
		elif theOnlyPane.ActiveContent is null:
		// use " " instead of string.Empty because the whole title bar will disappear when ControlBox is set to false.
			Text = ' '
		else:
			Text = theOnlyPane.ActiveContent.DockHandler.TabText

	
	protected override def SetBoundsCore(x as int, y as int, width as int, height as int, specified as BoundsSpecified):
		rectWorkArea as Rectangle = SystemInformation.VirtualScreen
		
		if (y + height) > rectWorkArea.Bottom:
			y -= ((y + height) - rectWorkArea.Bottom)
		
		if y < rectWorkArea.Top:
			y += (rectWorkArea.Top - y)
		
		super.SetBoundsCore(x, y, width, height, specified)

	
	[SecurityPermission(SecurityAction.LinkDemand, Flags: SecurityPermissionFlag.UnmanagedCode)]
	protected override def WndProc(ref m as Message):
		
		result as uint
		if m.Msg == cast(int, Win32.Msgs.WM_NCLBUTTONDOWN):
			if IsDisposed:
				return 
			result = NativeMethods.SendMessage(self.Handle, cast(int, Win32.Msgs.WM_NCHITTEST), 0, cast(uint, m.LParam))
			if ((result == 2) and DockPanel.AllowEndUserDocking) and self.AllowEndUserDocking:
				// HITTEST_CAPTION
				Activate()
				m_dockPanel.BeginDrag(self)
			else:
				super.WndProc(m)
			
			return 
		elif m.Msg == cast(int, Win32.Msgs.WM_NCRBUTTONDOWN):
			result = NativeMethods.SendMessage(self.Handle, cast(int, Win32.Msgs.WM_NCHITTEST), 0, cast(uint, m.LParam))
			if result == 2:
				// HITTEST_CAPTION
				theOnlyPane as DockPane = (VisibleNestedPanes[0] if (VisibleNestedPanes.Count == 1) else null)
				if (theOnlyPane is not null) and (theOnlyPane.ActiveContent is not null):
					theOnlyPane.ShowTabPageContextMenu(self, PointToClient(Control.MousePosition))
					return 
			
			super.WndProc(m)
			return 
		elif m.Msg == cast(int, Win32.Msgs.WM_CLOSE):
			if NestedPanes.Count == 0:
				super.WndProc(m)
				return 
			for i in range((NestedPanes.Count - 1), -1, -1):
			
				contents as DockContentCollection = NestedPanes[i].Contents
				for j in range((contents.Count - 1), -1, -1):
					content as IDockContent = contents[j]
					if content.DockHandler.DockState != DockState.Float:
						continue 
					
					if not content.DockHandler.CloseButton:
						continue 
					
					if content.DockHandler.HideOnClose:
						content.DockHandler.Hide()
					else:
						content.DockHandler.Close()
			
			return 
		elif m.Msg == cast(int, Win32.Msgs.WM_NCLBUTTONDBLCLK):
			result = NativeMethods.SendMessage(self.Handle, cast(int, Win32.Msgs.WM_NCHITTEST), 0, cast(uint, m.LParam))
			if result != 2:
				// HITTEST_CAPTION
				super.WndProc(m)
				return 
			
			DockPanel.SuspendLayout(true)
			
			// Restore to panel
			for pane as DockPane in NestedPanes:
				if pane.DockState != DockState.Float:
					continue 
				pane.RestoreToPanel()
			
			
			DockPanel.ResumeLayout(true, true)
			return 
		elif m.Msg == WM_CHECKDISPOSE:
			if NestedPanes.Count == 0:
				Dispose()
			
			return 
		
		super.WndProc(m)

	
	internal def RefreshChanges():
		if IsDisposed:
			return 
		
		if VisibleNestedPanes.Count == 0:
			ControlBox = true
			return 
		for i in range((VisibleNestedPanes.Count - 1), -1, -1):
		
			contents as DockContentCollection = VisibleNestedPanes[i].Contents
			for j in range((contents.Count - 1), -1, -1):
				content as IDockContent = contents[j]
				if content.DockHandler.DockState != DockState.Float:
					continue 
				
				if content.DockHandler.CloseButton:
					ControlBox = true
					return 
		ControlBox = false

	
	public virtual DisplayingRectangle as Rectangle:
		get:
			return ClientRectangle

	
	internal def TestDrop(dragSource as IDockDragSource, dockOutline as DockOutlineBase):
		if VisibleNestedPanes.Count == 1:
			pane as DockPane = VisibleNestedPanes[0]
			if not dragSource.CanDockTo(pane):
				return 
			
			ptMouse as Point = Control.MousePosition
			lParam as uint = Win32Helper.MakeLong(ptMouse.X, ptMouse.Y)
			if NativeMethods.SendMessage(Handle, cast(int, Win32.Msgs.WM_NCHITTEST), 0, lParam) == cast(uint, Win32.HitTest.HTCAPTION):
				dockOutline.Show(VisibleNestedPanes[0], -1)

	
	#region IDockDragSource Members
	
	#region IDragSource Members
	
	IDragSource.DragControl as Control:
		get:
			return self

	
	#endregion
	
	def IDockDragSource.IsDockStateValid(dockState as DockState) as bool:
		return IsDockStateValid(dockState)

	
	def IDockDragSource.CanDockTo(pane as DockPane) as bool:
		if not IsDockStateValid(pane.DockState):
			return false
		
		if pane.FloatWindow == self:
			return false
		
		return true

	
	def IDockDragSource.BeginDrag(ptMouse as Point) as Rectangle:
		return Bounds

	
	public def FloatAt(floatWindowBounds as Rectangle):
		Bounds = floatWindowBounds

	
	public def DockTo(pane as DockPane, dockStyle as DockStyle, contentIndex as int):
		if dockStyle == DockStyle.Fill:
			for i in range((NestedPanes.Count - 1), -1, -1):
				paneFrom as DockPane = NestedPanes[i]
				for j in range((paneFrom.Contents.Count - 1), -1, -1):
					c as IDockContent = paneFrom.Contents[j]
					c.DockHandler.Pane = pane
					if contentIndex != (-1):
						pane.SetContentIndex(c, contentIndex)
		else:
			alignment as DockAlignment = DockAlignment.Left
			if dockStyle == DockStyle.Left:
				alignment = DockAlignment.Left
			elif dockStyle == DockStyle.Right:
				alignment = DockAlignment.Right
			elif dockStyle == DockStyle.Top:
				alignment = DockAlignment.Top
			elif dockStyle == DockStyle.Bottom:
				alignment = DockAlignment.Bottom
			
			MergeNestedPanes(VisibleNestedPanes, pane.NestedPanesContainer.NestedPanes, pane, alignment, 0.5)

	
	public def DockTo(panel as DockPanel, dockStyle as DockStyle):
		if panel != DockPanel:
			raise ArgumentException(Strings.IDockDragSource_DockTo_InvalidPanel, 'panel')
		
		nestedPanesTo as NestedPaneCollection = null
		
		if dockStyle == DockStyle.Top:
			nestedPanesTo = DockPanel.DockWindows[DockState.DockTop].NestedPanes
		elif dockStyle == DockStyle.Bottom:
			nestedPanesTo = DockPanel.DockWindows[DockState.DockBottom].NestedPanes
		elif dockStyle == DockStyle.Left:
			nestedPanesTo = DockPanel.DockWindows[DockState.DockLeft].NestedPanes
		elif dockStyle == DockStyle.Right:
			nestedPanesTo = DockPanel.DockWindows[DockState.DockRight].NestedPanes
		elif dockStyle == DockStyle.Fill:
			nestedPanesTo = DockPanel.DockWindows[DockState.Document].NestedPanes
		
		prevPane as DockPane = null
		for i in range((nestedPanesTo.Count - 1), -1, -1):
			if nestedPanesTo[i] != VisibleNestedPanes[0]:
				prevPane = nestedPanesTo[i]
		MergeNestedPanes(VisibleNestedPanes, nestedPanesTo, prevPane, DockAlignment.Left, 0.5)

	
	private static def MergeNestedPanes(nestedPanesFrom as VisibleNestedPaneCollection, nestedPanesTo as NestedPaneCollection, prevPane as DockPane, alignment as DockAlignment, proportion as double):
		if nestedPanesFrom.Count == 0:
			return 
		
		count as int = nestedPanesFrom.Count
		panes as (DockPane) = array(DockPane, count)
		prevPanes as (DockPane) = array(DockPane, count)
		alignments as (DockAlignment) = array(DockAlignment, count)
		proportions as (double) = array(double, count)
		for i in range(0, count):
		
			panes[i] = nestedPanesFrom[i]
			prevPanes[i] = nestedPanesFrom[i].NestedDockingStatus.PreviousPane
			alignments[i] = nestedPanesFrom[i].NestedDockingStatus.Alignment
			proportions[i] = nestedPanesFrom[i].NestedDockingStatus.Proportion
		
		pane as DockPane = panes[0].DockTo(nestedPanesTo.Container, prevPane, alignment, proportion)
		panes[0].DockState = nestedPanesTo.DockState
		for i in range(1, count):
		
			for j in range(i, count):
				if prevPanes[j] == panes[(i - 1)]:
					prevPanes[j] = pane
			pane = panes[i].DockTo(nestedPanesTo.Container, prevPanes[i], alignments[i], proportions[i])
			panes[i].DockState = nestedPanesTo.DockState
	
	#endregion

