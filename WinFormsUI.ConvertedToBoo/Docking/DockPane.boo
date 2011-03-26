
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.ComponentModel
import System.Drawing
import System.Drawing.Drawing2D
import System.Windows.Forms
import System.Runtime.InteropServices
import System.Security.Permissions
import System.Diagnostics.CodeAnalysis

[ToolboxItem(false)]
partial public class DockPane(UserControl, IDockDragSource):

	public enum AppearanceStyle:

		ToolWindow

		Document

	
	private enum HitTestArea:

		Caption

		TabStrip

		Content

		None

	
	private struct HitTestResult:

		public HitArea as HitTestArea

		public Index as int

		
		public def constructor(hitTestArea as HitTestArea, index as int):
			HitArea = hitTestArea
			Index = index

	
	private m_captionControl as DockPaneCaptionBase

	private CaptionControl as DockPaneCaptionBase:
		get:
			return m_captionControl

	
	private m_tabStripControl as DockPaneStripBase

	internal TabStripControl as DockPaneStripBase:
		get:
			return m_tabStripControl

	
	protected def constructor(content as IDockContent, visibleState as DockState, show as bool):
		InternalConstruct(content, visibleState, false, Rectangle.Empty, null, DockAlignment.Right, 0.5, show)

	
	[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters', MessageId: '1#')]
	protected def constructor(content as IDockContent, floatWindow as FloatWindow, show as bool):
		if floatWindow is null:
			raise ArgumentNullException('floatWindow')
		
		InternalConstruct(content, DockState.Float, false, Rectangle.Empty, floatWindow.NestedPanes.GetDefaultPreviousPane(self), DockAlignment.Right, 0.5, show)

	
	protected def constructor(content as IDockContent, previousPane as DockPane, alignment as DockAlignment, proportion as double, show as bool):
		if previousPane is null:
			raise ArgumentNullException('previousPane')
		InternalConstruct(content, previousPane.DockState, false, Rectangle.Empty, previousPane, alignment, proportion, show)

	
	[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters', MessageId: '1#')]
	protected def constructor(content as IDockContent, floatWindowBounds as Rectangle, show as bool):
		InternalConstruct(content, DockState.Float, true, floatWindowBounds, null, DockAlignment.Right, 0.5, show)

	
	private def InternalConstruct(content as IDockContent, dockState as DockState, flagBounds as bool, floatWindowBounds as Rectangle, prevPane as DockPane, alignment as DockAlignment, proportion as double, show as bool):
		if (dockState == DockState.Hidden) or (dockState == DockState.Unknown):
			raise ArgumentException(Strings.DockPane_SetDockState_InvalidState)
		
		if content is null:
			raise ArgumentNullException(Strings.DockPane_Constructor_NullContent)
		
		if content.DockHandler.DockPanel is null:
			raise ArgumentException(Strings.DockPane_Constructor_NullDockPanel)
		
		
		SuspendLayout()
		SetStyle(ControlStyles.Selectable, false)
		
		m_isFloat = (dockState == DockState.Float)
		
		m_contents = DockContentCollection()
		m_displayingContents = DockContentCollection(self)
		m_dockPanel = content.DockHandler.DockPanel
		m_dockPanel.AddPane(self)
		
		m_splitter = SplitterControl(self)
		
		m_nestedDockingStatus = NestedDockingStatus(self)
		
		m_captionControl = DockPanel.DockPaneCaptionFactory.CreateDockPaneCaption(self)
		m_tabStripControl = DockPanel.DockPaneStripFactory.CreateDockPaneStrip(self)
		Controls.AddRange((of Control: m_captionControl, m_tabStripControl))
		
		DockPanel.SuspendLayout(true)
		if flagBounds:
			FloatWindow = DockPanel.FloatWindowFactory.CreateFloatWindow(DockPanel, self, floatWindowBounds)
		elif prevPane is not null:
			DockTo(prevPane.NestedPanesContainer, prevPane, alignment, proportion)
		
		SetDockState(dockState)
		if show:
			content.DockHandler.Pane = self
		elif self.IsFloat:
			content.DockHandler.FloatPane = self
		else:
			content.DockHandler.PanelPane = self
		
		ResumeLayout()
		DockPanel.ResumeLayout(true, true)

	
	protected override def Dispose(disposing as bool):
		if disposing:
			m_dockState = DockState.Unknown
			
			if NestedPanesContainer is not null:
				NestedPanesContainer.NestedPanes.Remove(self)
			
			if DockPanel is not null:
				DockPanel.RemovePane(self)
				m_dockPanel = null
			
			Splitter.Dispose()
			if m_autoHidePane is not null:
				m_autoHidePane.Dispose()
		super.Dispose(disposing)

	
	private m_activeContent as IDockContent = null

	public virtual ActiveContent as IDockContent:
		get:
			return m_activeContent
		set:
			if ActiveContent == value:
				return 
			
			if value is not null:
				if not DisplayingContents.Contains(value):
					raise InvalidOperationException(Strings.DockPane_ActiveContent_InvalidValue)
			elif DisplayingContents.Count != 0:
				raise InvalidOperationException(Strings.DockPane_ActiveContent_InvalidValue)
			
			oldValue as IDockContent = m_activeContent
			
			if DockPanel.ActiveAutoHideContent == oldValue:
				DockPanel.ActiveAutoHideContent = null
			
			m_activeContent = value
			
			if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi) and (DockState == DockState.Document):
				if m_activeContent is not null:
					m_activeContent.DockHandler.Form.BringToFront()
			else:
				if m_activeContent is not null:
					m_activeContent.DockHandler.SetVisible()
				if (oldValue is not null) and DisplayingContents.Contains(oldValue):
					oldValue.DockHandler.SetVisible()
				if IsActivated and (m_activeContent is not null):
					m_activeContent.DockHandler.Activate()
			
			if FloatWindow is not null:
				FloatWindow.SetText()
			
			if (DockPanel.DocumentStyle == DocumentStyle.DockingMdi) and (DockState == DockState.Document):
				RefreshChanges(false)
			else:
				// delayed layout to reduce screen flicker
				RefreshChanges()
			
			if m_activeContent is not null:
				TabStripControl.EnsureTabVisible(m_activeContent)

	
	private m_allowDockDragAndDrop = true

	public virtual AllowDockDragAndDrop as bool:
		get:
			return m_allowDockDragAndDrop
		set:
			m_allowDockDragAndDrop = value

	
	private m_autoHidePane as IDisposable = null

	internal AutoHidePane as IDisposable:
		get:
			return m_autoHidePane
		set:
			m_autoHidePane = value

	
	private m_autoHideTabs as object = null

	internal AutoHideTabs as object:
		get:
			return m_autoHideTabs
		set:
			m_autoHideTabs = value

	
	private TabPageContextMenu as object:
		get:
			content as IDockContent = ActiveContent
			
			if content is null:
				return null
			
			if content.DockHandler.TabPageContextMenuStrip is not null:
				return content.DockHandler.TabPageContextMenuStrip
			elif content.DockHandler.TabPageContextMenu is not null:
				return content.DockHandler.TabPageContextMenu
			else:
				return null

	
	internal HasTabPageContextMenu as bool:
		get:
			return (TabPageContextMenu is not null)

	
	internal def ShowTabPageContextMenu(control as Control, position as Point):
		menu as object = TabPageContextMenu
		
		if menu is null:
			return 
		
		contextMenuStrip = (menu as ContextMenuStrip)
		if contextMenuStrip is not null:
			contextMenuStrip.Show(control, position)
			return 
		
		contextMenu = (menu as ContextMenu)
		if contextMenu is not null:
			contextMenu.Show(self, position)

	
	private CaptionRectangle as Rectangle:
		get:
			if not HasCaption:
				return Rectangle.Empty
			
			rectWindow as Rectangle = DisplayingRectangle
			x as int
			y as int
			width as int
			x = rectWindow.X
			y = rectWindow.Y
			width = rectWindow.Width
			height as int = CaptionControl.MeasureHeight()
			
			return Rectangle(x, y, width, height)

	
	internal ContentRectangle as Rectangle:
		get:
			rectWindow as Rectangle = DisplayingRectangle
			rectCaption as Rectangle = CaptionRectangle
			rectTabStrip as Rectangle = TabStripRectangle
			
			x as int = rectWindow.X
			y as int = ((rectWindow.Y + (0 if rectCaption.IsEmpty else rectCaption.Height)) + (rectTabStrip.Height if (DockState == DockState.Document) else 0))
			width as int = rectWindow.Width
			height as int = ((rectWindow.Height - rectCaption.Height) - rectTabStrip.Height)
			
			return Rectangle(x, y, width, height)

	
	internal TabStripRectangle as Rectangle:
		get:
			if Appearance == AppearanceStyle.ToolWindow:
				return TabStripRectangle_ToolWindow
			else:
				return TabStripRectangle_Document

	
	private TabStripRectangle_ToolWindow as Rectangle:
		get:
			if (DisplayingContents.Count <= 1) or IsAutoHide:
				return Rectangle.Empty
			
			rectWindow as Rectangle = DisplayingRectangle
			
			width as int = rectWindow.Width
			height as int = TabStripControl.MeasureHeight()
			x as int = rectWindow.X
			y as int = (rectWindow.Bottom - height)
			rectCaption as Rectangle = CaptionRectangle
			if rectCaption.Contains(x, y):
				y = (rectCaption.Y + rectCaption.Height)
			
			return Rectangle(x, y, width, height)

	
	private TabStripRectangle_Document as Rectangle:
		get:
			if DisplayingContents.Count == 0:
				return Rectangle.Empty
			
			if (DisplayingContents.Count == 1) and (DockPanel.DocumentStyle == DocumentStyle.DockingSdi):
				return Rectangle.Empty
			
			rectWindow as Rectangle = DisplayingRectangle
			x as int = rectWindow.X
			y as int = rectWindow.Y
			width as int = rectWindow.Width
			height as int = TabStripControl.MeasureHeight()
			
			return Rectangle(x, y, width, height)

	
	public virtual CaptionText as string:
		get:
			return (string.Empty if (ActiveContent is null) else ActiveContent.DockHandler.TabText)

	
	private m_contents as DockContentCollection

	public Contents as DockContentCollection:
		get:
			return m_contents

	
	private m_displayingContents as DockContentCollection

	public DisplayingContents as DockContentCollection:
		get:
			return m_displayingContents

	
	private m_dockPanel as DockPanel

	public DockPanel as DockPanel:
		get:
			return m_dockPanel

	
	private HasCaption as bool:
		get:
			if (((DockState == DockState.Document) or (DockState == DockState.Hidden)) or (DockState == DockState.Unknown)) or ((DockState == DockState.Float) and (FloatWindow.VisibleNestedPanes.Count <= 1)):
				return false
			else:
				return true

	
	private m_isActivated = false

	public IsActivated as bool:
		get:
			return m_isActivated

	internal def SetIsActivated(value as bool):
		if m_isActivated == value:
			return 
		
		m_isActivated = value
		if DockState != DockState.Document:
			RefreshChanges(false)
		OnIsActivatedChanged(EventArgs.Empty)

	
	private m_isActiveDocumentPane = false

	public IsActiveDocumentPane as bool:
		get:
			return m_isActiveDocumentPane

	internal def SetIsActiveDocumentPane(value as bool):
		if m_isActiveDocumentPane == value:
			return 
		
		m_isActiveDocumentPane = value
		if DockState == DockState.Document:
			RefreshChanges()
		OnIsActiveDocumentPaneChanged(EventArgs.Empty)

	
	public def IsDockStateValid(dockState as DockState) as bool:
		for content as IDockContent in Contents:
			if not content.DockHandler.IsDockStateValid(dockState):
				return false
		
		return true

	
	public IsAutoHide as bool:
		get:
			return DockHelper.IsDockStateAutoHide(DockState)

	
	public Appearance as AppearanceStyle:
		get:
			return (AppearanceStyle.Document if (DockState == DockState.Document) else AppearanceStyle.ToolWindow)

	
	internal DisplayingRectangle as Rectangle:
		get:
			return ClientRectangle

	
	public def Activate():
		if DockHelper.IsDockStateAutoHide(DockState) and (DockPanel.ActiveAutoHideContent != ActiveContent):
			DockPanel.ActiveAutoHideContent = ActiveContent
		elif (not IsActivated) and (ActiveContent is not null):
			ActiveContent.DockHandler.Activate()

	
	internal def AddContent(content as IDockContent):
		if Contents.Contains(content):
			return 
		
		Contents.Add(content)

	
	internal def Close():
		Dispose()

	
	public def CloseActiveContent():
		CloseContent(ActiveContent)

	
	internal def CloseContent(content as IDockContent):
		dockPanel as DockPanel = DockPanel
		dockPanel.SuspendLayout(true)
		
		if content is null:
			return 
		
		if not content.DockHandler.CloseButton:
			return 
		
		if content.DockHandler.HideOnClose:
			content.DockHandler.Hide()
		else:
			content.DockHandler.Close()
		
		dockPanel.ResumeLayout(true, true)

	
	private def GetHitTest(ptMouse as Point) as HitTestResult:
		ptMouseClient as Point = PointToClient(ptMouse)
		
		rectCaption as Rectangle = CaptionRectangle
		if rectCaption.Contains(ptMouseClient):
			return HitTestResult(HitTestArea.Caption, -1)
		
		rectContent as Rectangle = ContentRectangle
		if rectContent.Contains(ptMouseClient):
			return HitTestResult(HitTestArea.Content, -1)
		
		rectTabStrip as Rectangle = TabStripRectangle
		if rectTabStrip.Contains(ptMouseClient):
			return HitTestResult(HitTestArea.TabStrip, TabStripControl.HitTest(TabStripControl.PointToClient(ptMouse)))
		
		return HitTestResult(HitTestArea.None, -1)

	
	private m_isHidden = true

	public IsHidden as bool:
		get:
			return m_isHidden

	private def SetIsHidden(value as bool):
		if m_isHidden == value:
			return 
		
		m_isHidden = value
		if DockHelper.IsDockStateAutoHide(DockState):
			DockPanel.RefreshAutoHideStrip()
			DockPanel.PerformLayout()
		elif NestedPanesContainer is not null:
			cast(Control, NestedPanesContainer).PerformLayout()

	
	protected override def OnLayout(levent as LayoutEventArgs):
		SetIsHidden((DisplayingContents.Count == 0))
		if not IsHidden:
			CaptionControl.Bounds = CaptionRectangle
			TabStripControl.Bounds = TabStripRectangle
			
			SetContentBounds()
			
			for content as IDockContent in Contents:
				if DisplayingContents.Contains(content):
					if content.DockHandler.FlagClipWindow and content.DockHandler.Form.Visible:
						content.DockHandler.FlagClipWindow = false
		
		super.OnLayout(levent)

	
	internal def SetContentBounds():
		rectContent as Rectangle = ContentRectangle
		if (DockState == DockState.Document) and (DockPanel.DocumentStyle == DocumentStyle.DockingMdi):
			rectContent = DockPanel.RectangleToMdiClient(RectangleToScreen(rectContent))
		
		rectInactive = Rectangle(-rectContent.Width, rectContent.Y, rectContent.Width, rectContent.Height)
		for content as IDockContent in Contents:
			if content.DockHandler.Pane == self:
				if content == ActiveContent:
					content.DockHandler.Form.Bounds = rectContent
				else:
					content.DockHandler.Form.Bounds = rectInactive

	
	internal def RefreshChanges():
		RefreshChanges(true)

	
	private def RefreshChanges(performLayout as bool):
		if IsDisposed:
			return 
		
		CaptionControl.RefreshChanges()
		TabStripControl.RefreshChanges()
		if DockState == DockState.Float:
			FloatWindow.RefreshChanges()
		if DockHelper.IsDockStateAutoHide(DockState) and (DockPanel is not null):
			DockPanel.RefreshAutoHideStrip()
			DockPanel.PerformLayout()
		
		if performLayout:
			PerformLayout()

	
	internal def RemoveContent(content as IDockContent):
		if not Contents.Contains(content):
			return 
		
		Contents.Remove(content)

	
	public def SetContentIndex(content as IDockContent, index as int):
		oldIndex as int = Contents.IndexOf(content)
		if oldIndex == (-1):
			raise ArgumentException(Strings.DockPane_SetContentIndex_InvalidContent)
		
		if (index < 0) or (index > (Contents.Count - 1)):
			if index != (-1):
				raise ArgumentOutOfRangeException(Strings.DockPane_SetContentIndex_InvalidIndex)
		
		if oldIndex == index:
			return 
		if (oldIndex == (Contents.Count - 1)) and (index == (-1)):
			return 
		
		Contents.Remove(content)
		if index == (-1):
			Contents.Add(content)
		elif oldIndex < index:
			Contents.AddAt(content, (index - 1))
		else:
			Contents.AddAt(content, index)
		
		RefreshChanges()

	
	private def SetParent():
		if (DockState == DockState.Unknown) or (DockState == DockState.Hidden):
			SetParent(null)
			Splitter.Parent = null
		elif DockState == DockState.Float:
			SetParent(FloatWindow)
			Splitter.Parent = FloatWindow
		elif DockHelper.IsDockStateAutoHide(DockState):
			SetParent(DockPanel.AutoHideControl)
			Splitter.Parent = null
		else:
			SetParent(DockPanel.DockWindows[DockState])
			Splitter.Parent = Parent

	
	private def SetParent(value as Control):
		if Parent == value:
			return 
		
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		// Workaround of .Net Framework bug:
		// Change the parent of a control with focus may result in the first
		// MDI child form get activated. 
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		contentFocused as IDockContent = GetFocusedContent()
		if contentFocused is not null:
			DockPanel.SaveFocus()
		
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		
		Parent = value
		
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		// Workaround of .Net Framework bug:
		// Change the parent of a control with focus may result in the first
		// MDI child form get activated. 
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
		if contentFocused is not null:
			contentFocused.DockHandler.Activate()
		//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

	
	public def Show():
		Activate()

	
	internal def TestDrop(dragSource as IDockDragSource, dockOutline as DockOutlineBase):
		if not dragSource.CanDockTo(self):
			return 
		
		ptMouse as Point = Control.MousePosition
		
		hitTestResult as HitTestResult = GetHitTest(ptMouse)
		if hitTestResult.HitArea == HitTestArea.Caption:
			dockOutline.Show(self, -1)
		elif (hitTestResult.HitArea == HitTestArea.TabStrip) and (hitTestResult.Index != (-1)):
			dockOutline.Show(self, hitTestResult.Index)

	
	internal def ValidateActiveContent():
		if ActiveContent is null:
			if DisplayingContents.Count != 0:
				ActiveContent = DisplayingContents[0]
			return 
		
		if DisplayingContents.IndexOf(ActiveContent) >= 0:
			return 
		
		prevVisible as IDockContent = null
		for i in range((Contents.IndexOf(ActiveContent) - 1), -1, -1):
			if Contents[i].DockHandler.DockState == DockState:
				prevVisible = Contents[i]
				break 
		
		nextVisible as IDockContent = null
		for i in range((Contents.IndexOf(ActiveContent) + 1), Contents.Count):
			if Contents[i].DockHandler.DockState == DockState:
				nextVisible = Contents[i]
				break 
		
		if prevVisible is not null:
			ActiveContent = prevVisible
		elif nextVisible is not null:
			ActiveContent = nextVisible
		else:
			ActiveContent = null

	
	private static final DockStateChangedEvent = object()

	public event DockStateChanged as EventHandler

	protected virtual def OnDockStateChanged(e as EventArgs):
		handler = cast(EventHandler, Events[DockStateChangedEvent])
		handler(self, e)

	
	private static final IsActivatedChangedEvent = object()

	public event IsActivatedChanged as EventHandler

	protected virtual def OnIsActivatedChanged(e as EventArgs):
		handler = cast(EventHandler, Events[IsActivatedChangedEvent])
		handler(self, e)

	
	private static final IsActiveDocumentPaneChangedEvent = object()

	public event IsActiveDocumentPaneChanged as EventHandler

	protected virtual def OnIsActiveDocumentPaneChanged(e as EventArgs):
		handler = cast(EventHandler, Events[IsActiveDocumentPaneChangedEvent])
		handler(self, e)

	
	public DockWindow as DockWindow:
		get:
			return (null if (m_nestedDockingStatus.NestedPanes is null) else (m_nestedDockingStatus.NestedPanes.Container as DockWindow))
		set:
			oldValue as DockWindow = DockWindow
			if oldValue == value:
				return 
			
			DockTo(value)

	
	public FloatWindow as FloatWindow:
		get:
			return (null if (m_nestedDockingStatus.NestedPanes is null) else (m_nestedDockingStatus.NestedPanes.Container as FloatWindow))
		set:
			oldValue as FloatWindow = FloatWindow
			if oldValue == value:
				return 
			
			DockTo(value)

	
	private m_nestedDockingStatus as NestedDockingStatus

	public NestedDockingStatus as NestedDockingStatus:
		get:
			return m_nestedDockingStatus

	
	private m_isFloat as bool

	public IsFloat as bool:
		get:
			return m_isFloat

	
	public NestedPanesContainer as INestedPanesContainer:
		get:
			if NestedDockingStatus.NestedPanes is null:
				return null
			else:
				return NestedDockingStatus.NestedPanes.Container

	
	private m_dockState as DockState = DockState.Unknown

	public DockState as DockState:
		get:
			return m_dockState
		set:
			SetDockState(value)

	
	public def SetDockState(value as DockState) as DockPane:
		
		
		
		content as IDockContent
		if (value == DockState.Unknown) or (value == DockState.Hidden):
			raise InvalidOperationException(Strings.DockPane_SetDockState_InvalidState)
		if (value == DockState.Float) == self.IsFloat:
			InternalSetDockState(value)
			return self
		if DisplayingContents.Count == 0:
			return null
		firstContent as IDockContent = null
		for i in range(0, DisplayingContents.Count):
			content = DisplayingContents[i]
			if content.DockHandler.IsDockStateValid(value):
				firstContent = content
				break 
		if firstContent is null:
			return null
		
		firstContent.DockHandler.DockState = value
		pane as DockPane = firstContent.DockHandler.Pane
		DockPanel.SuspendLayout(true)
		for i in range(0, DisplayingContents.Count):
			content = DisplayingContents[i]
			if content.DockHandler.IsDockStateValid(value):
				content.DockHandler.Pane = pane
		DockPanel.ResumeLayout(true, true)
		return pane

	
	private def InternalSetDockState(value as DockState):
		if m_dockState == value:
			return 
		
		oldDockState as DockState = m_dockState
		oldContainer as INestedPanesContainer = NestedPanesContainer
		
		m_dockState = value
		
		SuspendRefreshStateChange()
		
		contentFocused as IDockContent = GetFocusedContent()
		if contentFocused is not null:
			DockPanel.SaveFocus()
		
		if not IsFloat:
			DockWindow = DockPanel.DockWindows[DockState]
		elif FloatWindow is null:
			FloatWindow = DockPanel.FloatWindowFactory.CreateFloatWindow(DockPanel, self)
		
		if contentFocused is not null:
			DockPanel.ContentFocusManager.Activate(contentFocused)
		
		ResumeRefreshStateChange(oldContainer, oldDockState)

	
	private m_countRefreshStateChange = 0

	private def SuspendRefreshStateChange():
		m_countRefreshStateChange += 1
		DockPanel.SuspendLayout(true)

	
	private def ResumeRefreshStateChange():
		m_countRefreshStateChange -= 1
		System.Diagnostics.Debug.Assert((m_countRefreshStateChange >= 0))
		DockPanel.ResumeLayout(true, true)

	
	private IsRefreshStateChangeSuspended as bool:
		get:
			return (m_countRefreshStateChange != 0)

	
	private def ResumeRefreshStateChange(oldContainer as INestedPanesContainer, oldDockState as DockState):
		ResumeRefreshStateChange()
		RefreshStateChange(oldContainer, oldDockState)

	
	private def RefreshStateChange(oldContainer as INestedPanesContainer, oldDockState as DockState):
		lock self:
			if IsRefreshStateChangeSuspended:
				return 
			
			SuspendRefreshStateChange()
		
		DockPanel.SuspendLayout(true)
		
		contentFocused as IDockContent = GetFocusedContent()
		if contentFocused is not null:
			DockPanel.SaveFocus()
		SetParent()
		
		if ActiveContent is not null:
			ActiveContent.DockHandler.SetDockState(ActiveContent.DockHandler.IsHidden, DockState, ActiveContent.DockHandler.Pane)
		for content as IDockContent in Contents:
			if content.DockHandler.Pane == self:
				content.DockHandler.SetDockState(content.DockHandler.IsHidden, DockState, content.DockHandler.Pane)
		
		if oldContainer is not null:
			oldContainerControl = cast(Control, oldContainer)
			if (oldContainer.DockState == oldDockState) and (not oldContainerControl.IsDisposed):
				oldContainerControl.PerformLayout()
		if DockHelper.IsDockStateAutoHide(oldDockState):
			DockPanel.RefreshActiveAutoHideContent()
		
		if NestedPanesContainer.DockState == DockState:
			cast(Control, NestedPanesContainer).PerformLayout()
		if DockHelper.IsDockStateAutoHide(DockState):
			DockPanel.RefreshActiveAutoHideContent()
		
		if DockHelper.IsDockStateAutoHide(oldDockState) or DockHelper.IsDockStateAutoHide(DockState):
			DockPanel.RefreshAutoHideStrip()
			DockPanel.PerformLayout()
		
		ResumeRefreshStateChange()
		
		if contentFocused is not null:
			contentFocused.DockHandler.Activate()
		
		DockPanel.ResumeLayout(true, true)
		
		if oldDockState != DockState:
			OnDockStateChanged(EventArgs.Empty)

	
	private def GetFocusedContent() as IDockContent:
		contentFocused as IDockContent = null
		for content as IDockContent in Contents:
			if content.DockHandler.Form.ContainsFocus:
				contentFocused = content
				break 
		
		return contentFocused

	
	public def DockTo(container as INestedPanesContainer) as DockPane:
		if container is null:
			raise InvalidOperationException(Strings.DockPane_DockTo_NullContainer)
		
		alignment as DockAlignment
		if (container.DockState == DockState.DockLeft) or (container.DockState == DockState.DockRight):
			alignment = DockAlignment.Bottom
		else:
			alignment = DockAlignment.Right
		
		return DockTo(container, container.NestedPanes.GetDefaultPreviousPane(self), alignment, 0.5)

	
	public def DockTo(container as INestedPanesContainer, previousPane as DockPane, alignment as DockAlignment, proportion as double) as DockPane:
		if container is null:
			raise InvalidOperationException(Strings.DockPane_DockTo_NullContainer)
		
		if container.IsFloat == self.IsFloat:
			InternalAddToDockList(container, previousPane, alignment, proportion)
			return self
		
		firstContent as IDockContent = GetFirstContent(container.DockState)
		if firstContent is null:
			return null
		
		pane as DockPane
		DockPanel.DummyContent.DockPanel = DockPanel
		if container.IsFloat:
			pane = DockPanel.DockPaneFactory.CreateDockPane(DockPanel.DummyContent, cast(FloatWindow, container), true)
		else:
			pane = DockPanel.DockPaneFactory.CreateDockPane(DockPanel.DummyContent, container.DockState, true)
		
		pane.DockTo(container, previousPane, alignment, proportion)
		SetVisibleContentsToPane(pane)
		DockPanel.DummyContent.DockPanel = null
		
		return pane

	
	private def SetVisibleContentsToPane(pane as DockPane):
		SetVisibleContentsToPane(pane, ActiveContent)

	
	private def SetVisibleContentsToPane(pane as DockPane, activeContent as IDockContent):
		for i in range(0, DisplayingContents.Count):
			content as IDockContent = DisplayingContents[i]
			if content.DockHandler.IsDockStateValid(pane.DockState):
				content.DockHandler.Pane = pane
				i -= 1
		
		if activeContent.DockHandler.Pane == pane:
			pane.ActiveContent = activeContent

	
	private def InternalAddToDockList(container as INestedPanesContainer, prevPane as DockPane, alignment as DockAlignment, proportion as double):
		if (container.DockState == DockState.Float) != IsFloat:
			raise InvalidOperationException(Strings.DockPane_DockTo_InvalidContainer)
		
		count as int = container.NestedPanes.Count
		if container.NestedPanes.Contains(self):
			count -= 1
		if (prevPane is null) and (count > 0):
			raise InvalidOperationException(Strings.DockPane_DockTo_NullPrevPane)
		
		if (prevPane is not null) and (not container.NestedPanes.Contains(prevPane)):
			raise InvalidOperationException(Strings.DockPane_DockTo_NoPrevPane)
		
		if prevPane == self:
			raise InvalidOperationException(Strings.DockPane_DockTo_SelfPrevPane)
		
		oldContainer as INestedPanesContainer = NestedPanesContainer
		oldDockState as DockState = DockState
		container.NestedPanes.Add(self)
		NestedDockingStatus.SetStatus(container.NestedPanes, prevPane, alignment, proportion)
		
		if DockHelper.IsDockWindowState(DockState):
			m_dockState = container.DockState
		
		RefreshStateChange(oldContainer, oldDockState)

	
	public def SetNestedDockingProportion(proportion as double):
		NestedDockingStatus.SetStatus(NestedDockingStatus.NestedPanes, NestedDockingStatus.PreviousPane, NestedDockingStatus.Alignment, proportion)
		if NestedPanesContainer is not null:
			cast(Control, NestedPanesContainer).PerformLayout()

	
	public def Float() as DockPane:
		DockPanel.SuspendLayout(true)
		
		activeContent as IDockContent = ActiveContent
		
		floatPane as DockPane = GetFloatPaneFromContents()
		if floatPane is null:
			firstContent as IDockContent = GetFirstContent(DockState.Float)
			if firstContent is null:
				DockPanel.ResumeLayout(true, true)
				return null
			floatPane = DockPanel.DockPaneFactory.CreateDockPane(firstContent, DockState.Float, true)
		SetVisibleContentsToPane(floatPane, activeContent)
		
		DockPanel.ResumeLayout(true, true)
		return floatPane

	
	private def GetFloatPaneFromContents() as DockPane:
		floatPane as DockPane = null
		for i in range(0, DisplayingContents.Count):
			content as IDockContent = DisplayingContents[i]
			if not content.DockHandler.IsDockStateValid(DockState.Float):
				continue 
			
			if (floatPane is not null) and (content.DockHandler.FloatPane != floatPane):
				return null
			else:
				floatPane = content.DockHandler.FloatPane
		
		return floatPane

	
	private def GetFirstContent(dockState as DockState) as IDockContent:
		for i in range(0, DisplayingContents.Count):
			content as IDockContent = DisplayingContents[i]
			if content.DockHandler.IsDockStateValid(dockState):
				return content
		return null

	
	public def RestoreToPanel():
		DockPanel.SuspendLayout(true)
		
		activeContent as IDockContent = DockPanel.ActiveContent
		for i in range((DisplayingContents.Count - 1), -1, -1):
		
			content as IDockContent = DisplayingContents[i]
			if content.DockHandler.CheckDockState(false) != DockState.Unknown:
				content.DockHandler.IsFloat = false
		
		DockPanel.ResumeLayout(true, true)

	
	[SecurityPermission(SecurityAction.LinkDemand, Flags: SecurityPermissionFlag.UnmanagedCode)]
	protected override def WndProc(ref m as Message):
		if m.Msg == cast(int, Win32.Msgs.WM_MOUSEACTIVATE):
			Activate()
		
		super.WndProc(m)

	
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
		
		if pane == self:
			return false
		
		return true

	
	def IDockDragSource.BeginDrag(ptMouse as Point) as Rectangle:
		location as Point = PointToScreen(Point(0, 0))
		size as Size
		
		floatPane as DockPane = ActiveContent.DockHandler.FloatPane
		if ((DockState == DockState.Float) or (floatPane is null)) or (floatPane.FloatWindow.NestedPanes.Count != 1):
			size = DockPanel.DefaultFloatWindowSize
		else:
			size = floatPane.FloatWindow.Size
		
		if ptMouse.X > (location.X + size.Width):
			location.X += ((ptMouse.X - (location.X + size.Width)) + Measures.SplitterSize)
		
		return Rectangle(location, size)

	
	public def FloatAt(floatWindowBounds as Rectangle):
		if (FloatWindow is null) or (FloatWindow.NestedPanes.Count != 1):
			FloatWindow = DockPanel.FloatWindowFactory.CreateFloatWindow(DockPanel, self, floatWindowBounds)
		else:
			FloatWindow.Bounds = floatWindowBounds
		
		DockState = DockState.Float

	
	public def DockTo(pane as DockPane, dockStyle as DockStyle, contentIndex as int):
		if dockStyle == DockStyle.Fill:
			activeContent as IDockContent = ActiveContent
			for i in range((Contents.Count - 1), -1, -1):
				c as IDockContent = Contents[i]
				c.DockHandler.Pane = pane
				if contentIndex != (-1):
					pane.SetContentIndex(c, contentIndex)
			pane.ActiveContent = activeContent
		else:
			if dockStyle == DockStyle.Left:
				DockTo(pane.NestedPanesContainer, pane, DockAlignment.Left, 0.5)
			elif dockStyle == DockStyle.Right:
				DockTo(pane.NestedPanesContainer, pane, DockAlignment.Right, 0.5)
			elif dockStyle == DockStyle.Top:
				DockTo(pane.NestedPanesContainer, pane, DockAlignment.Top, 0.5)
			elif dockStyle == DockStyle.Bottom:
				DockTo(pane.NestedPanesContainer, pane, DockAlignment.Bottom, 0.5)
			
			DockState = pane.DockState

	
	public def DockTo(panel as DockPanel, dockStyle as DockStyle):
		if panel != DockPanel:
			raise ArgumentException(Strings.IDockDragSource_DockTo_InvalidPanel, 'panel')
		
		if dockStyle == DockStyle.Top:
			DockState = DockState.DockTop
		elif dockStyle == DockStyle.Bottom:
			DockState = DockState.DockBottom
		elif dockStyle == DockStyle.Left:
			DockState = DockState.DockLeft
		elif dockStyle == DockStyle.Right:
			DockState = DockState.DockRight
		elif dockStyle == DockStyle.Fill:
			DockState = DockState.Document
	
	#endregion

