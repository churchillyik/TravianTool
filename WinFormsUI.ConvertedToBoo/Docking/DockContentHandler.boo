
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms
import System.Drawing
import System.ComponentModel
import System.Diagnostics.CodeAnalysis

public callable GetPersistStringCallback() as string

public class DockContentHandler(IDisposable, IDockDragSource):

	public def constructor(form as Form):
		self(form, null)

	
	public def constructor(form as Form, getPersistStringCallback as GetPersistStringCallback):
		if not (form isa IDockContent):
			raise ArgumentException(Strings.DockContent_Constructor_InvalidForm, 'form')
		
		m_form = form
		m_getPersistStringCallback = getPersistStringCallback
		
		m_events = EventHandlerList()
		Form.Disposed += Form_Disposed
		Form.TextChanged += Form_TextChanged

	
	public def Dispose():
		Dispose(true)
		GC.SuppressFinalize(self)

	
	protected virtual def Dispose(disposing as bool):
		if disposing:
			lock self:
				DockPanel = null
				if m_autoHideTab is not null:
					m_autoHideTab.Dispose()
				if m_tab is not null:
					m_tab.Dispose()
				
				Form.Disposed -= Form_Disposed
				Form.TextChanged -= Form_TextChanged
				m_events.Dispose()

	
	private m_form as Form

	public Form as Form:
		get:
			return m_form

	
	public Content as IDockContent:
		get:
			return (Form as IDockContent)

	
	private m_previousActive as IDockContent = null

	public PreviousActive as IDockContent:
		get:
			return m_previousActive
		internal set:
			m_previousActive = value

	
	private m_nextActive as IDockContent = null

	public NextActive as IDockContent:
		get:
			return m_nextActive
		internal set:
			m_nextActive = value

	
	private m_events as EventHandlerList

	private Events as EventHandlerList:
		get:
			return m_events

	
	private m_allowEndUserDocking = true

	public AllowEndUserDocking as bool:
		get:
			return m_allowEndUserDocking
		set:
			m_allowEndUserDocking = value

	
	private m_autoHidePortion = 0.25

	public AutoHidePortion as double:
		get:
			return m_autoHidePortion
		set:
			if value <= 0:
				raise ArgumentOutOfRangeException(Strings.DockContentHandler_AutoHidePortion_OutOfRange)
			
			if m_autoHidePortion == value:
				return 
			
			m_autoHidePortion = value
			
			if DockPanel is null:
				return 
			
			if DockPanel.ActiveAutoHideContent == Content:
				DockPanel.PerformLayout()

	
	private m_closeButton = true

	public CloseButton as bool:
		get:
			return m_closeButton
		set:
			if m_closeButton == value:
				return 
			
			m_closeButton = value
			if Pane is not null:
				if Pane.ActiveContent.DockHandler == self:
					Pane.RefreshChanges()

	
	private DefaultDockState as DockState:
		get:
			if (ShowHint != DockState.Unknown) and (ShowHint != DockState.Hidden):
				return ShowHint
			
			if (DockAreas & DockAreas.Document) != 0:
				return DockState.Document
			if (DockAreas & DockAreas.DockRight) != 0:
				return DockState.DockRight
			if (DockAreas & DockAreas.DockLeft) != 0:
				return DockState.DockLeft
			if (DockAreas & DockAreas.DockBottom) != 0:
				return DockState.DockBottom
			if (DockAreas & DockAreas.DockTop) != 0:
				return DockState.DockTop
			
			return DockState.Unknown

	
	private DefaultShowState as DockState:
		get:
			if ShowHint != DockState.Unknown:
				return ShowHint
			
			if (DockAreas & DockAreas.Document) != 0:
				return DockState.Document
			if (DockAreas & DockAreas.DockRight) != 0:
				return DockState.DockRight
			if (DockAreas & DockAreas.DockLeft) != 0:
				return DockState.DockLeft
			if (DockAreas & DockAreas.DockBottom) != 0:
				return DockState.DockBottom
			if (DockAreas & DockAreas.DockTop) != 0:
				return DockState.DockTop
			if (DockAreas & DockAreas.Float) != 0:
				return DockState.Float
			
			return DockState.Unknown

	
	private m_allowedAreas as DockAreas = (((((DockAreas.DockLeft | DockAreas.DockRight) | DockAreas.DockTop) | DockAreas.DockBottom) | DockAreas.Document) | DockAreas.Float)

	public DockAreas as DockAreas:
		get:
			return m_allowedAreas
		set:
			if m_allowedAreas == value:
				return 
			
			if not DockHelper.IsDockStateValid(DockState, value):
				raise InvalidOperationException(Strings.DockContentHandler_DockAreas_InvalidValue)
			
			m_allowedAreas = value
			
			if not DockHelper.IsDockStateValid(ShowHint, m_allowedAreas):
				ShowHint = DockState.Unknown

	
	private m_dockState as DockState = DockState.Unknown

	public DockState as DockState:
		get:
			return m_dockState
		set:
			if m_dockState == value:
				return 
			
			DockPanel.SuspendLayout(true)
			
			if value == DockState.Hidden:
				IsHidden = true
			else:
				SetDockState(false, value, Pane)
			
			DockPanel.ResumeLayout(true, true)

	
	private m_dockPanel as DockPanel = null

	public DockPanel as DockPanel:
		get:
			return m_dockPanel
		set:
			if m_dockPanel == value:
				return 
			
			Pane = null
			
			if m_dockPanel is not null:
				m_dockPanel.RemoveContent(Content)
			
			if m_tab is not null:
				m_tab.Dispose()
				m_tab = null
			
			if m_autoHideTab is not null:
				m_autoHideTab.Dispose()
				m_autoHideTab = null
			
			m_dockPanel = value
			
			if m_dockPanel is not null:
				m_dockPanel.AddContent(Content)
				Form.TopLevel = false
				Form.FormBorderStyle = FormBorderStyle.None
				Form.ShowInTaskbar = false
				Form.WindowState = FormWindowState.Normal
				NativeMethods.SetWindowPos(Form.Handle, IntPtr.Zero, 0, 0, 0, 0, (((((Win32.FlagsSetWindowPos.SWP_NOACTIVATE | Win32.FlagsSetWindowPos.SWP_NOMOVE) | Win32.FlagsSetWindowPos.SWP_NOSIZE) | Win32.FlagsSetWindowPos.SWP_NOZORDER) | Win32.FlagsSetWindowPos.SWP_NOOWNERZORDER) | Win32.FlagsSetWindowPos.SWP_FRAMECHANGED))

	
	public Icon as Icon:
		get:
			return Form.Icon

	
	public Pane as DockPane:
		get:
			return (FloatPane if IsFloat else PanelPane)
		set:
			if Pane == value:
				return 
			
			DockPanel.SuspendLayout(true)
			
			oldPane as DockPane = Pane
			
			SuspendSetDockState()
			FloatPane = (null if (value is null) else (value if value.IsFloat else FloatPane))
			PanelPane = (null if (value is null) else (PanelPane if value.IsFloat else value))
			ResumeSetDockState(IsHidden, (value.DockState if (value is not null) else DockState.Unknown), oldPane)
			
			DockPanel.ResumeLayout(true, true)

	
	private m_isHidden = true

	public IsHidden as bool:
		get:
			return m_isHidden
		set:
			if m_isHidden == value:
				return 
			
			SetDockState(value, VisibleState, Pane)

	
	private m_tabText as string = null

	public TabText as string:
		get:
			return (Form.Text if (m_tabText is null) else m_tabText)
		set:
			if m_tabText == value:
				return 
			
			m_tabText = value
			if Pane is not null:
				Pane.RefreshChanges()

	
	private m_visibleState as DockState = DockState.Unknown

	public VisibleState as DockState:
		get:
			return m_visibleState
		set:
			if m_visibleState == value:
				return 
			
			SetDockState(IsHidden, value, Pane)

	
	private m_isFloat = false

	public IsFloat as bool:
		get:
			return m_isFloat
		set:
			if m_isFloat == value:
				return 
			
			visibleState as DockState = CheckDockState(value)
			
			if visibleState == DockState.Unknown:
				raise InvalidOperationException(Strings.DockContentHandler_IsFloat_InvalidValue)
			
			SetDockState(IsHidden, visibleState, Pane)

	
	[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters')]
	public def CheckDockState(isFloat as bool) as DockState:
		dockState as DockState
		
		if isFloat:
			if not IsDockStateValid(DockState.Float):
				dockState = DockState.Unknown
			else:
				dockState = DockState.Float
		else:
			dockState = (PanelPane.DockState if (PanelPane is not null) else DefaultDockState)
			if (dockState != DockState.Unknown) and (not IsDockStateValid(dockState)):
				dockState = DockState.Unknown
		
		return dockState

	
	private m_panelPane as DockPane = null

	public PanelPane as DockPane:
		get:
			return m_panelPane
		set:
			if m_panelPane == value:
				return 
			
			if value is not null:
				if value.IsFloat or (value.DockPanel != DockPanel):
					raise InvalidOperationException(Strings.DockContentHandler_DockPane_InvalidValue)
			
			oldPane as DockPane = Pane
			
			if m_panelPane is not null:
				RemoveFromPane(m_panelPane)
			m_panelPane = value
			if m_panelPane is not null:
				m_panelPane.AddContent(Content)
				SetDockState(IsHidden, (DockState.Float if IsFloat else m_panelPane.DockState), oldPane)
			else:
				SetDockState(IsHidden, DockState.Unknown, oldPane)

	
	private def RemoveFromPane(pane as DockPane):
		pane.RemoveContent(Content)
		SetPane(null)
		if pane.Contents.Count == 0:
			pane.Dispose()

	
	private m_floatPane as DockPane = null

	public FloatPane as DockPane:
		get:
			return m_floatPane
		set:
			if m_floatPane == value:
				return 
			
			if value is not null:
				if (not value.IsFloat) or (value.DockPanel != DockPanel):
					raise InvalidOperationException(Strings.DockContentHandler_FloatPane_InvalidValue)
			
			oldPane as DockPane = Pane
			
			if m_floatPane is not null:
				RemoveFromPane(m_floatPane)
			m_floatPane = value
			if m_floatPane is not null:
				m_floatPane.AddContent(Content)
				SetDockState(IsHidden, (DockState.Float if IsFloat else VisibleState), oldPane)
			else:
				SetDockState(IsHidden, DockState.Unknown, oldPane)

	
	private m_countSetDockState = 0

	private def SuspendSetDockState():
		m_countSetDockState += 1

	
	private def ResumeSetDockState():
		m_countSetDockState -= 1
		if m_countSetDockState < 0:
			m_countSetDockState = 0

	
	internal IsSuspendSetDockState as bool:
		get:
			return (m_countSetDockState != 0)

	
	private def ResumeSetDockState(isHidden as bool, visibleState as DockState, oldPane as DockPane):
		ResumeSetDockState()
		SetDockState(isHidden, visibleState, oldPane)

	
	internal def SetDockState(isHidden as bool, visibleState as DockState, oldPane as DockPane):
		if IsSuspendSetDockState:
			return 
		
		if (DockPanel is null) and (visibleState != DockState.Unknown):
			raise InvalidOperationException(Strings.DockContentHandler_SetDockState_NullPanel)
		
		if (visibleState == DockState.Hidden) or ((visibleState != DockState.Unknown) and (not IsDockStateValid(visibleState))):
			raise InvalidOperationException(Strings.DockContentHandler_SetDockState_InvalidState)
		
		dockPanel as DockPanel = DockPanel
		if dockPanel is not null:
			dockPanel.SuspendLayout(true)
		
		SuspendSetDockState()
		
		oldDockState as DockState = DockState
		
		if (m_isHidden != isHidden) or (oldDockState == DockState.Unknown):
			m_isHidden = isHidden
		m_visibleState = visibleState
		m_dockState = (DockState.Hidden if isHidden else visibleState)
		
		if visibleState == DockState.Unknown:
			Pane = null
		else:
			m_isFloat = (m_visibleState == DockState.Float)
			
			if Pane is null:
				Pane = DockPanel.DockPaneFactory.CreateDockPane(Content, visibleState, true)
			elif Pane.DockState != visibleState:
				if Pane.Contents.Count == 1:
					Pane.SetDockState(visibleState)
				else:
					Pane = DockPanel.DockPaneFactory.CreateDockPane(Content, visibleState, true)
		
		if Form.ContainsFocus:
			if (DockState == DockState.Hidden) or (DockState == DockState.Unknown):
				DockPanel.ContentFocusManager.GiveUpFocus(Content)
		
		SetPaneAndVisible(Pane)
		
		if ((oldPane is not null) and (not oldPane.IsDisposed)) and (oldDockState == oldPane.DockState):
			RefreshDockPane(oldPane)
		
		if (Pane is not null) and (DockState == Pane.DockState):
			if (Pane != oldPane) or ((Pane == oldPane) and (oldDockState != oldPane.DockState)):
				RefreshDockPane(Pane)
		
		if oldDockState != DockState:
			if ((DockState == DockState.Hidden) or (DockState == DockState.Unknown)) or DockHelper.IsDockStateAutoHide(DockState):
				DockPanel.ContentFocusManager.RemoveFromList(Content)
			else:
				DockPanel.ContentFocusManager.AddToList(Content)
			
			OnDockStateChanged(EventArgs.Empty)
		ResumeSetDockState()
		
		if dockPanel is not null:
			dockPanel.ResumeLayout(true, true)

	
	private static def RefreshDockPane(pane as DockPane):
		pane.RefreshChanges()
		pane.ValidateActiveContent()

	
	internal PersistString as string:
		get:
			return (Form.GetType().ToString() if (GetPersistStringCallback is null) else GetPersistStringCallback())

	
	private m_getPersistStringCallback as GetPersistStringCallback = null

	public GetPersistStringCallback as GetPersistStringCallback:
		get:
			return m_getPersistStringCallback
		set:
			m_getPersistStringCallback = value

	
	
	private m_hideOnClose = false

	public HideOnClose as bool:
		get:
			return m_hideOnClose
		set:
			m_hideOnClose = value

	
	private m_showHint as DockState = DockState.Unknown

	public ShowHint as DockState:
		get:
			return m_showHint
		set:
			if not DockHelper.IsDockStateValid(value, DockAreas):
				raise InvalidOperationException(Strings.DockContentHandler_ShowHint_InvalidValue)
			
			if m_showHint == value:
				return 
			
			m_showHint = value

	
	private m_isActivated = false

	public IsActivated as bool:
		get:
			return m_isActivated
		internal set:
			if m_isActivated == value:
				return 
			
			m_isActivated = value

	
	public def IsDockStateValid(dockState as DockState) as bool:
		if ((DockPanel is not null) and (dockState == DockState.Document)) and (DockPanel.DocumentStyle == DocumentStyle.SystemMdi):
			return false
		else:
			return DockHelper.IsDockStateValid(dockState, DockAreas)

	
	private m_tabPageContextMenu as ContextMenu = null

	public TabPageContextMenu as ContextMenu:
		get:
			return m_tabPageContextMenu
		set:
			m_tabPageContextMenu = value

	
	private m_toolTipText as string = null

	public ToolTipText as string:
		get:
			return m_toolTipText
		set:
			m_toolTipText = value

	
	public def Activate():
		if DockPanel is null:
			Form.Activate()
		elif Pane is null:
			Show(DockPanel)
		else:
			IsHidden = false
			Pane.ActiveContent = Content
			if (DockState == DockState.Document) and (DockPanel.DocumentStyle == DocumentStyle.SystemMdi):
				Form.Activate()
				return 
			elif DockHelper.IsDockStateAutoHide(DockState):
				DockPanel.ActiveAutoHideContent = Content
			
			if not Form.ContainsFocus:
				DockPanel.ContentFocusManager.Activate(Content)

	
	public def GiveUpFocus():
		DockPanel.ContentFocusManager.GiveUpFocus(Content)

	
	private m_activeWindowHandle as IntPtr = IntPtr.Zero

	internal ActiveWindowHandle as IntPtr:
		get:
			return m_activeWindowHandle
		set:
			m_activeWindowHandle = value

	
	public def Hide():
		IsHidden = true

	
	internal def SetPaneAndVisible(pane as DockPane):
		SetPane(pane)
		SetVisible()

	
	private def SetPane(pane as DockPane):
		if ((pane is not null) and (pane.DockState == DockState.Document)) and (DockPanel.DocumentStyle == DocumentStyle.DockingMdi):
			if Form.Parent isa DockPane:
				SetParent(null)
			if Form.MdiParent != DockPanel.ParentForm:
				FlagClipWindow = true
				Form.MdiParent = DockPanel.ParentForm
		else:
			FlagClipWindow = true
			if Form.MdiParent is not null:
				Form.MdiParent = null
			if Form.TopLevel:
				Form.TopLevel = false
			SetParent(pane)

	
	internal def SetVisible():
		visible as bool
		
		if IsHidden:
			visible = false
		elif ((Pane is not null) and (Pane.DockState == DockState.Document)) and (DockPanel.DocumentStyle == DocumentStyle.DockingMdi):
			visible = true
		elif (Pane is not null) and (Pane.ActiveContent == Content):
			visible = true
		elif (Pane is not null) and (Pane.ActiveContent != Content):
			visible = false
		else:
			visible = Form.Visible
		
		if Form.Visible != visible:
			Form.Visible = visible

	
	private def SetParent(value as Control):
		if Form.Parent == value:
			return 
		
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		// Workaround of .Net Framework bug:
		// Change the parent of a control with focus may result in the first
		// MDI child form get activated. 
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		bRestoreFocus = false
		if Form.ContainsFocus:
			if value is null:
				DockPanel.ContentFocusManager.GiveUpFocus(self.Content)
			else:
				DockPanel.SaveFocus()
				bRestoreFocus = true
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		
		Form.Parent = value
		
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		// Workaround of .Net Framework bug:
		// Change the parent of a control with focus may result in the first
		// MDI child form get activated. 
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if bRestoreFocus:
			Activate()
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

	
	public def Show():
		if DockPanel is null:
			Form.Show()
		else:
			Show(DockPanel)

	
	public def Show(dockPanel as DockPanel):
		if dockPanel is null:
			raise ArgumentNullException(Strings.DockContentHandler_Show_NullDockPanel)
		
		if DockState == DockState.Unknown:
			Show(dockPanel, DefaultShowState)
		else:
			Activate()

	
	public def Show(dockPanel as DockPanel, dockState as DockState):
		if dockPanel is null:
			raise ArgumentNullException(Strings.DockContentHandler_Show_NullDockPanel)
		
		if (dockState == DockState.Unknown) or (dockState == DockState.Hidden):
			raise ArgumentException(Strings.DockContentHandler_Show_InvalidDockState)
		
		dockPanel.SuspendLayout(true)
		
		DockPanel = dockPanel
		
		if (dockState == DockState.Float) and (FloatPane is null):
			Pane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.Float, true)
		elif PanelPane is null:
			paneExisting as DockPane = null
			for pane as DockPane in DockPanel.Panes:
				if pane.DockState == dockState:
					paneExisting = pane
					break 
			
			if paneExisting is null:
				Pane = DockPanel.DockPaneFactory.CreateDockPane(Content, dockState, true)
			else:
				Pane = paneExisting
		
		DockState = dockState
		Activate()
		
		dockPanel.ResumeLayout(true, true)

	
	[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters')]
	public def Show(dockPanel as DockPanel, floatWindowBounds as Rectangle):
		if dockPanel is null:
			raise ArgumentNullException(Strings.DockContentHandler_Show_NullDockPanel)
		
		dockPanel.SuspendLayout(true)
		
		DockPanel = dockPanel
		if FloatPane is null:
			IsHidden = true
			// to reduce the screen flicker
			FloatPane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.Float, false)
			FloatPane.FloatWindow.StartPosition = FormStartPosition.Manual
		
		FloatPane.FloatWindow.Bounds = floatWindowBounds
		
		Show(dockPanel, DockState.Float)
		Activate()
		
		dockPanel.ResumeLayout(true, true)

	
	public def Show(pane as DockPane, beforeContent as IDockContent):
		if pane is null:
			raise ArgumentNullException(Strings.DockContentHandler_Show_NullPane)
		
		if (beforeContent is not null) and (pane.Contents.IndexOf(beforeContent) == (-1)):
			raise ArgumentException(Strings.DockContentHandler_Show_InvalidBeforeContent)
		
		pane.DockPanel.SuspendLayout(true)
		
		DockPanel = pane.DockPanel
		Pane = pane
		pane.SetContentIndex(Content, pane.Contents.IndexOf(beforeContent))
		Show()
		
		pane.DockPanel.ResumeLayout(true, true)

	
	public def Show(previousPane as DockPane, alignment as DockAlignment, proportion as double):
		if previousPane is null:
			raise ArgumentException(Strings.DockContentHandler_Show_InvalidPrevPane)
		
		if DockHelper.IsDockStateAutoHide(previousPane.DockState):
			raise ArgumentException(Strings.DockContentHandler_Show_InvalidPrevPane)
		
		previousPane.DockPanel.SuspendLayout(true)
		
		DockPanel = previousPane.DockPanel
		DockPanel.DockPaneFactory.CreateDockPane(Content, previousPane, alignment, proportion, true)
		Show()
		
		previousPane.DockPanel.ResumeLayout(true, true)

	
	public def Close():
		dockPanel as DockPanel = DockPanel
		if dockPanel is not null:
			dockPanel.SuspendLayout(true)
		Form.Close()
		if dockPanel is not null:
			dockPanel.ResumeLayout(true, true)
		

	
	private m_tab as DockPaneStripBase.Tab = null

	internal def GetTab(dockPaneStrip as DockPaneStripBase) as DockPaneStripBase.Tab:
		if m_tab is null:
			m_tab = dockPaneStrip.CreateTab(Content)
		
		return m_tab

	
	private m_autoHideTab as IDisposable = null

	internal AutoHideTab as IDisposable:
		get:
			return m_autoHideTab
		set:
			m_autoHideTab = value

	
	#region Events
	private static final DockStateChangedEvent = object()

	public event DockStateChanged as EventHandler

	protected virtual def OnDockStateChanged(e as EventArgs):
		handler = cast(EventHandler, Events[DockStateChangedEvent])
		handler(self, e)

	#endregion
	
	private def Form_Disposed(sender as object, e as EventArgs):
		Dispose()

	
	private def Form_TextChanged(sender as object, e as EventArgs):
		if DockHelper.IsDockStateAutoHide(DockState):
			DockPanel.RefreshAutoHideStrip()
		elif Pane is not null:
			if Pane.FloatWindow is not null:
				Pane.FloatWindow.SetText()
			Pane.RefreshChanges()

	
	private m_flagClipWindow = false

	internal FlagClipWindow as bool:
		get:
			return m_flagClipWindow
		set:
			if m_flagClipWindow == value:
				return 
			
			m_flagClipWindow = value
			if m_flagClipWindow:
				Form.Region = Region(Rectangle.Empty)
			else:
				Form.Region = null

	
	private m_tabPageContextMenuStrip as ContextMenuStrip = null

	public TabPageContextMenuStrip as ContextMenuStrip:
		get:
			return m_tabPageContextMenuStrip
		set:
			m_tabPageContextMenuStrip = value

	
	#region IDockDragSource Members
	
	IDragSource.DragControl as Control:
		get:
			return Form

	
	def IDockDragSource.CanDockTo(pane as DockPane) as bool:
		if not IsDockStateValid(pane.DockState):
			return false
		
		if (Pane == pane) and (pane.DisplayingContents.Count == 1):
			return false
		
		return true

	
	def IDockDragSource.BeginDrag(ptMouse as Point) as Rectangle:
		size as Size
		floatPane as DockPane = self.FloatPane
		if ((DockState == DockState.Float) or (floatPane is null)) or (floatPane.FloatWindow.NestedPanes.Count != 1):
			size = DockPanel.DefaultFloatWindowSize
		else:
			size = floatPane.FloatWindow.Size
		
		location as Point
		rectPane as Rectangle = Pane.ClientRectangle
		if DockState == DockState.Document:
			location = Point(rectPane.Left, rectPane.Top)
		else:
			location = Point(rectPane.Left, rectPane.Bottom)
			location.Y -= size.Height
		location = Pane.PointToScreen(location)
		
		if ptMouse.X > (location.X + size.Width):
			location.X += ((ptMouse.X - (location.X + size.Width)) + Measures.SplitterSize)
		
		return Rectangle(location, size)

	
	public def FloatAt(floatWindowBounds as Rectangle):
		pane as DockPane = DockPanel.DockPaneFactory.CreateDockPane(Content, floatWindowBounds, true)

	
	public def DockTo(pane as DockPane, dockStyle as DockStyle, contentIndex as int):
		if dockStyle == DockStyle.Fill:
			samePane as bool = (Pane == pane)
			if not samePane:
				Pane = pane
			
			if (contentIndex == (-1)) or (not samePane):
				pane.SetContentIndex(Content, contentIndex)
			else:
				contents as DockContentCollection = pane.Contents
				oldIndex as int = contents.IndexOf(Content)
				newIndex as int = contentIndex
				if oldIndex < newIndex:
					newIndex += 1
					if newIndex > (contents.Count - 1):
						newIndex = (-1)
				pane.SetContentIndex(Content, newIndex)
		else:
			paneFrom as DockPane = DockPanel.DockPaneFactory.CreateDockPane(Content, pane.DockState, true)
			container as INestedPanesContainer = pane.NestedPanesContainer
			if dockStyle == DockStyle.Left:
				paneFrom.DockTo(container, pane, DockAlignment.Left, 0.5)
			elif dockStyle == DockStyle.Right:
				paneFrom.DockTo(container, pane, DockAlignment.Right, 0.5)
			elif dockStyle == DockStyle.Top:
				paneFrom.DockTo(container, pane, DockAlignment.Top, 0.5)
			elif dockStyle == DockStyle.Bottom:
				paneFrom.DockTo(container, pane, DockAlignment.Bottom, 0.5)
			
			paneFrom.DockState = pane.DockState

	
	public def DockTo(panel as DockPanel, dockStyle as DockStyle):
		if panel != DockPanel:
			raise ArgumentException(Strings.IDockDragSource_DockTo_InvalidPanel, 'panel')
		
		pane as DockPane
		
		if dockStyle == DockStyle.Top:
			pane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.DockTop, true)
		elif dockStyle == DockStyle.Bottom:
			pane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.DockBottom, true)
		elif dockStyle == DockStyle.Left:
			pane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.DockLeft, true)
		elif dockStyle == DockStyle.Right:
			pane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.DockRight, true)
		elif dockStyle == DockStyle.Fill:
			pane = DockPanel.DockPaneFactory.CreateDockPane(Content, DockState.Document, true)
		else:
			return 
	
	#endregion

