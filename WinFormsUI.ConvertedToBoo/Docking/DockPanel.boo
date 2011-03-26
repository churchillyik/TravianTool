
// To simplify the process of finding the toolbox bitmap resource:
// #1 Create an internal class called "resfinder" outside of the root namespace.
// #2 Use "resfinder" in the toolbox bitmap attribute instead of the control name.
// #3 use the "<default namespace>.<resourcename>" string to locate the resource.
// See: http://www.bobpowell.net/toolboxbitmap.htm

namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Drawing.Drawing2D
import System.Windows.Forms
import System.ComponentModel
import System.Runtime.InteropServices
import System.IO
import System.Text
import System.Diagnostics.CodeAnalysis
import System.Collections.Generic

internal class resfinder:
	pass

[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters', MessageId: '0#')]
public callable DeserializeDockContent(persistString as string) as IDockContent

[LocalizedDescription('DockPanel_Description')]
[Designer(typeof(System.Windows.Forms.Design.ControlDesigner))]
[ToolboxBitmap(typeof(resfinder), 'WeifenLuo.WinFormsUI.Docking.DockPanel.bmp')]
[DefaultProperty('DocumentStyle')]
[DefaultEvent('ActiveContentChanged')]
partial public class DockPanel(Panel):

	private m_focusManager as FocusManagerImpl

	private m_extender as DockPanelExtender

	private m_panes as DockPaneCollection

	private m_floatWindows as FloatWindowCollection

	private m_autoHideWindow as AutoHideWindowControl

	private m_dockWindows as DockWindowCollection

	private m_dummyContent as DockContent

	private m_dummyControl as Control

	
	public def constructor():
		m_focusManager = FocusManagerImpl(self)
		m_extender = DockPanelExtender(self)
		m_panes = DockPaneCollection()
		m_floatWindows = FloatWindowCollection()
		
		SuspendLayout()
		
		m_autoHideWindow = AutoHideWindowControl(self)
		m_autoHideWindow.Visible = false
		SetAutoHideWindowParent()
		
		m_dummyControl = DummyControl()
		m_dummyControl.Bounds = Rectangle(0, 0, 1, 1)
		Controls.Add(m_dummyControl)
		
		m_dockWindows = DockWindowCollection(self)
		Controls.AddRange((of Control: DockWindows[DockState.Document], DockWindows[DockState.DockLeft], DockWindows[DockState.DockRight], DockWindows[DockState.DockTop], DockWindows[DockState.DockBottom]))
		
		m_dummyContent = DockContent()
		ResumeLayout()

	
	private m_autoHideStripControl as AutoHideStripBase = null

	internal AutoHideStripControl as AutoHideStripBase:
		get:
			if m_autoHideStripControl is null:
				m_autoHideStripControl = AutoHideStripFactory.CreateAutoHideStrip(self)
				Controls.Add(m_autoHideStripControl)
			return m_autoHideStripControl

	internal def ResetAutoHideStripControl():
		if m_autoHideStripControl is not null:
			m_autoHideStripControl.Dispose()
		
		m_autoHideStripControl = null

	
	private def MdiClientHandleAssigned(sender as object, e as EventArgs):
		SetMdiClient()
		PerformLayout()

	
	private def MdiClient_Layout(sender as object, e as LayoutEventArgs):
		if DocumentStyle != DocumentStyle.DockingMdi:
			return 
		
		for pane as DockPane in Panes:
			if pane.DockState == DockState.Document:
				pane.SetContentBounds()
		
		InvalidateWindowRegion()

	
	private m_disposed = false

	protected override def Dispose(disposing as bool):
		lock self:
			if (not m_disposed) and disposing:
				m_focusManager.Dispose()
				if m_mdiClientController is not null:
					m_mdiClientController.HandleAssigned -= MdiClientHandleAssigned
					m_mdiClientController.MdiChildActivate -= ParentFormMdiChildActivate
					m_mdiClientController.Layout -= MdiClient_Layout
					m_mdiClientController.Dispose()
				FloatWindows.Dispose()
				Panes.Dispose()
				DummyContent.Dispose()
				
				m_disposed = true
			
			super.Dispose(disposing)

	
	[Browsable(false)]
	public ActiveAutoHideContent as IDockContent:
		get:
			return AutoHideWindow.ActiveContent
		set:
			AutoHideWindow.ActiveContent = value

	
	private m_allowEndUserDocking = true

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_AllowEndUserDocking_Description')]
	[DefaultValue(true)]
	public AllowEndUserDocking as bool:
		get:
			return m_allowEndUserDocking
		set:
			m_allowEndUserDocking = value

	
	private m_allowEndUserNestedDocking = true

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_AllowEndUserNestedDocking_Description')]
	[DefaultValue(true)]
	public AllowEndUserNestedDocking as bool:
		get:
			return m_allowEndUserNestedDocking
		set:
			m_allowEndUserNestedDocking = value

	
	private m_contents = DockContentCollection()

	[Browsable(false)]
	public Contents as DockContentCollection:
		get:
			return m_contents

	
	internal DummyContent as DockContent:
		get:
			return m_dummyContent

	
	private m_rightToLeftLayout = false

	[DefaultValue(false)]
	[LocalizedCategory('Appearance')]
	[LocalizedDescription('DockPanel_RightToLeftLayout_Description')]
	public RightToLeftLayout as bool:
		get:
			return m_rightToLeftLayout
		set:
			if m_rightToLeftLayout == value:
				return 
			
			m_rightToLeftLayout = value
			for floatWindow as FloatWindow in FloatWindows:
				floatWindow.RightToLeftLayout = value

	
	protected override def OnRightToLeftChanged(e as EventArgs):
		super.OnRightToLeftChanged(e)
		for floatWindow as FloatWindow in FloatWindows:
			if floatWindow.RightToLeft != RightToLeft:
				floatWindow.RightToLeft = RightToLeft

	
	private m_showDocumentIcon = false

	[DefaultValue(false)]
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_ShowDocumentIcon_Description')]
	public ShowDocumentIcon as bool:
		get:
			return m_showDocumentIcon
		set:
			if m_showDocumentIcon == value:
				return 
			
			m_showDocumentIcon = value
			Refresh()

	
	[Browsable(false)]
	public Extender as DockPanelExtender:
		get:
			return m_extender

	
	public DockPaneFactory as DockPanelExtender.IDockPaneFactory:
		get:
			return Extender.DockPaneFactory

	
	public FloatWindowFactory as DockPanelExtender.IFloatWindowFactory:
		get:
			return Extender.FloatWindowFactory

	
	internal DockPaneCaptionFactory as DockPanelExtender.IDockPaneCaptionFactory:
		get:
			return Extender.DockPaneCaptionFactory

	
	internal DockPaneStripFactory as DockPanelExtender.IDockPaneStripFactory:
		get:
			return Extender.DockPaneStripFactory

	
	internal AutoHideStripFactory as DockPanelExtender.IAutoHideStripFactory:
		get:
			return Extender.AutoHideStripFactory

	
	[Browsable(false)]
	public Panes as DockPaneCollection:
		get:
			return m_panes

	
	internal DockArea as Rectangle:
		get:
			return Rectangle(DockPadding.Left, DockPadding.Top, ((ClientRectangle.Width - DockPadding.Left) - DockPadding.Right), ((ClientRectangle.Height - DockPadding.Top) - DockPadding.Bottom))

	
	private m_dockBottomPortion = 0.25

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_DockBottomPortion_Description')]
	[DefaultValue(0.25)]
	public DockBottomPortion as double:
		get:
			return m_dockBottomPortion
		set:
			if value <= 0:
				raise ArgumentOutOfRangeException('value')
			
			if value == m_dockBottomPortion:
				return 
			
			m_dockBottomPortion = value
			
			if (m_dockBottomPortion < 1) and (m_dockTopPortion < 1):
				if (m_dockTopPortion + m_dockBottomPortion) > 1:
					m_dockTopPortion = (1 - m_dockBottomPortion)
			
			PerformLayout()

	
	private m_dockLeftPortion = 0.25

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_DockLeftPortion_Description')]
	[DefaultValue(0.25)]
	public DockLeftPortion as double:
		get:
			return m_dockLeftPortion
		set:
			if value <= 0:
				raise ArgumentOutOfRangeException('value')
			
			if value == m_dockLeftPortion:
				return 
			
			m_dockLeftPortion = value
			
			if (m_dockLeftPortion < 1) and (m_dockRightPortion < 1):
				if (m_dockLeftPortion + m_dockRightPortion) > 1:
					m_dockRightPortion = (1 - m_dockLeftPortion)
			PerformLayout()

	
	private m_dockRightPortion = 0.25

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_DockRightPortion_Description')]
	[DefaultValue(0.25)]
	public DockRightPortion as double:
		get:
			return m_dockRightPortion
		set:
			if value <= 0:
				raise ArgumentOutOfRangeException('value')
			
			if value == m_dockRightPortion:
				return 
			
			m_dockRightPortion = value
			
			if (m_dockLeftPortion < 1) and (m_dockRightPortion < 1):
				if (m_dockLeftPortion + m_dockRightPortion) > 1:
					m_dockLeftPortion = (1 - m_dockRightPortion)
			PerformLayout()

	
	private m_dockTopPortion = 0.25

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_DockTopPortion_Description')]
	[DefaultValue(0.25)]
	public DockTopPortion as double:
		get:
			return m_dockTopPortion
		set:
			if value <= 0:
				raise ArgumentOutOfRangeException('value')
			
			if value == m_dockTopPortion:
				return 
			
			m_dockTopPortion = value
			
			if (m_dockTopPortion < 1) and (m_dockBottomPortion < 1):
				if (m_dockTopPortion + m_dockBottomPortion) > 1:
					m_dockBottomPortion = (1 - m_dockTopPortion)
			PerformLayout()

	
	[Browsable(false)]
	public DockWindows as DockWindowCollection:
		get:
			return m_dockWindows

	
	public def UpdateDockWindowZOrder(dockStyle as DockStyle, fullPanelEdge as bool):
		if dockStyle == DockStyle.Left:
			if fullPanelEdge:
				DockWindows[DockState.DockLeft].SendToBack()
			else:
				DockWindows[DockState.DockLeft].BringToFront()
		elif dockStyle == DockStyle.Right:
			if fullPanelEdge:
				DockWindows[DockState.DockRight].SendToBack()
			else:
				DockWindows[DockState.DockRight].BringToFront()
		elif dockStyle == DockStyle.Top:
			if fullPanelEdge:
				DockWindows[DockState.DockTop].SendToBack()
			else:
				DockWindows[DockState.DockTop].BringToFront()
		elif dockStyle == DockStyle.Bottom:
			if fullPanelEdge:
				DockWindows[DockState.DockBottom].SendToBack()
			else:
				DockWindows[DockState.DockBottom].BringToFront()

	
	public DocumentsCount as int:
		get:
			count = 0
			for content as IDockContent in Documents:
				count += 1
			
			return count

	
	public def DocumentsToArray() as (IDockContent):
		count as int = DocumentsCount
		documents as (IDockContent) = array(IDockContent, count)
		i = 0
		for content as IDockContent in Documents:
			documents[i] = content
			i += 1
		
		return documents

	
	public Documents as IEnumerable[of IDockContent]:
		get:
			for content as IDockContent in Contents:
				if content.DockHandler.DockState == DockState.Document:
					yield content

	
	private DocumentRectangle as Rectangle:
		get:
			rect as Rectangle = DockArea
			if DockWindows[DockState.DockLeft].VisibleNestedPanes.Count != 0:
				rect.X += cast(int, (DockArea.Width * DockLeftPortion))
				rect.Width -= cast(int, (DockArea.Width * DockLeftPortion))
			if DockWindows[DockState.DockRight].VisibleNestedPanes.Count != 0:
				rect.Width -= cast(int, (DockArea.Width * DockRightPortion))
			if DockWindows[DockState.DockTop].VisibleNestedPanes.Count != 0:
				rect.Y += cast(int, (DockArea.Height * DockTopPortion))
				rect.Height -= cast(int, (DockArea.Height * DockTopPortion))
			if DockWindows[DockState.DockBottom].VisibleNestedPanes.Count != 0:
				rect.Height -= cast(int, (DockArea.Height * DockBottomPortion))
			
			return rect

	
	private DummyControl as Control:
		get:
			return m_dummyControl

	
	[Browsable(false)]
	public FloatWindows as FloatWindowCollection:
		get:
			return m_floatWindows

	
	private m_defaultFloatWindowSize = Size(300, 300)

	[Category('Layout')]
	[LocalizedDescription('DockPanel_DefaultFloatWindowSize_Description')]
	public DefaultFloatWindowSize as Size:
		get:
			return m_defaultFloatWindowSize
		set:
			m_defaultFloatWindowSize = value

	private def ShouldSerializeDefaultFloatWindowSize() as bool:
		return (DefaultFloatWindowSize != Size(300, 300))

	
	private m_documentStyle as DocumentStyle = DocumentStyle.DockingMdi

	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockPanel_DocumentStyle_Description')]
	[DefaultValue(DocumentStyle.DockingMdi)]
	public DocumentStyle as DocumentStyle:
		get:
			return m_documentStyle
		set:
			if value == m_documentStyle:
				return 
			
			if not Enum.IsDefined(typeof(DocumentStyle), value):
				raise InvalidEnumArgumentException()
			
			if (value == DocumentStyle.SystemMdi) and (DockWindows[DockState.Document].VisibleNestedPanes.Count > 0):
				raise InvalidEnumArgumentException()
			
			m_documentStyle = value
			
			SuspendLayout(true)
			
			SetAutoHideWindowParent()
			SetMdiClient()
			InvalidateWindowRegion()
			
			for content as IDockContent in Contents:
				if content.DockHandler.DockState == DockState.Document:
					content.DockHandler.SetPaneAndVisible(content.DockHandler.Pane)
			
			PerformMdiClientLayout()
			
			ResumeLayout(true, true)

	
	private def GetDockWindowSize(dockState as DockState) as int:
		
		
		adjust as int
		if (dockState == DockState.DockLeft) or (dockState == DockState.DockRight):
			width as int = ((ClientRectangle.Width - DockPadding.Left) - DockPadding.Right)
			dockLeftSize as int = (cast(int, m_dockLeftPortion) if (m_dockLeftPortion >= 1) else cast(int, (width * m_dockLeftPortion)))
			dockRightSize as int = (cast(int, m_dockRightPortion) if (m_dockRightPortion >= 1) else cast(int, (width * m_dockRightPortion)))
			if dockLeftSize < MeasurePane.MinSize:
				dockLeftSize = MeasurePane.MinSize
			if dockRightSize < MeasurePane.MinSize:
				dockRightSize = MeasurePane.MinSize
			if (dockLeftSize + dockRightSize) > (width - MeasurePane.MinSize):
				adjust = ((dockLeftSize + dockRightSize) - (width - MeasurePane.MinSize))
				dockLeftSize -= (adjust / 2)
				dockRightSize -= (adjust / 2)
			
			return (dockLeftSize if (dockState == DockState.DockLeft) else dockRightSize)
		elif (dockState == DockState.DockTop) or (dockState == DockState.DockBottom):
			height as int = ((ClientRectangle.Height - DockPadding.Top) - DockPadding.Bottom)
			dockTopSize as int = (cast(int, m_dockTopPortion) if (m_dockTopPortion >= 1) else cast(int, (height * m_dockTopPortion)))
			dockBottomSize as int = (cast(int, m_dockBottomPortion) if (m_dockBottomPortion >= 1) else cast(int, (height * m_dockBottomPortion)))
			
			if dockTopSize < MeasurePane.MinSize:
				dockTopSize = MeasurePane.MinSize
			if dockBottomSize < MeasurePane.MinSize:
				dockBottomSize = MeasurePane.MinSize
			
			if (dockTopSize + dockBottomSize) > (height - MeasurePane.MinSize):
				adjust = ((dockTopSize + dockBottomSize) - (height - MeasurePane.MinSize))
				dockTopSize -= (adjust / 2)
				dockBottomSize -= (adjust / 2)
			
			return (dockTopSize if (dockState == DockState.DockTop) else dockBottomSize)
		else:
			return 0

	
	protected override def OnLayout(levent as LayoutEventArgs):
		SuspendLayout(true)
		
		AutoHideStripControl.Bounds = ClientRectangle
		
		CalculateDockPadding()
		
		DockWindows[DockState.DockLeft].Width = GetDockWindowSize(DockState.DockLeft)
		DockWindows[DockState.DockRight].Width = GetDockWindowSize(DockState.DockRight)
		DockWindows[DockState.DockTop].Height = GetDockWindowSize(DockState.DockTop)
		DockWindows[DockState.DockBottom].Height = GetDockWindowSize(DockState.DockBottom)
		
		AutoHideWindow.Bounds = GetAutoHideWindowBounds(AutoHideWindowRectangle)
		
		DockWindows[DockState.Document].BringToFront()
		AutoHideWindow.BringToFront()
		
		super.OnLayout(levent)
		
		if (DocumentStyle == DocumentStyle.SystemMdi) and MdiClientExists:
			SetMdiClientBounds(SystemMdiClientBounds)
			InvalidateWindowRegion()
		elif DocumentStyle == DocumentStyle.DockingMdi:
			InvalidateWindowRegion()
		
		ResumeLayout(true, true)

	
	internal def GetTabStripRectangle(dockState as DockState) as Rectangle:
		return AutoHideStripControl.GetTabStripRectangle(dockState)

	
	protected override def OnPaint(e as PaintEventArgs):
		super.OnPaint(e)
		
		g as Graphics = e.Graphics
		g.FillRectangle(SystemBrushes.AppWorkspace, ClientRectangle)

	
	internal def AddContent(content as IDockContent):
		if content is null:
			raise ArgumentNullException()
		
		if not Contents.Contains(content):
			Contents.Add(content)
			OnContentAdded(DockContentEventArgs(content))

	
	internal def AddPane(pane as DockPane):
		if Panes.Contains(pane):
			return 
		
		Panes.Add(pane)

	
	internal def AddFloatWindow(floatWindow as FloatWindow):
		if FloatWindows.Contains(floatWindow):
			return 
		
		FloatWindows.Add(floatWindow)

	
	private def CalculateDockPadding():
		DockPadding.All = 0
		
		height as int = AutoHideStripControl.MeasureHeight()
		
		if AutoHideStripControl.GetNumberOfPanes(DockState.DockLeftAutoHide) > 0:
			DockPadding.Left = height
		if AutoHideStripControl.GetNumberOfPanes(DockState.DockRightAutoHide) > 0:
			DockPadding.Right = height
		if AutoHideStripControl.GetNumberOfPanes(DockState.DockTopAutoHide) > 0:
			DockPadding.Top = height
		if AutoHideStripControl.GetNumberOfPanes(DockState.DockBottomAutoHide) > 0:
			DockPadding.Bottom = height

	
	internal def RemoveContent(content as IDockContent):
		if content is null:
			raise ArgumentNullException()
		
		if Contents.Contains(content):
			Contents.Remove(content)
			OnContentRemoved(DockContentEventArgs(content))

	
	internal def RemovePane(pane as DockPane):
		if not Panes.Contains(pane):
			return 
		
		Panes.Remove(pane)

	
	internal def RemoveFloatWindow(floatWindow as FloatWindow):
		if not FloatWindows.Contains(floatWindow):
			return 
		
		FloatWindows.Remove(floatWindow)

	
	public def SetPaneIndex(pane as DockPane, index as int):
		oldIndex as int = Panes.IndexOf(pane)
		if oldIndex == (-1):
			raise ArgumentException(Strings.DockPanel_SetPaneIndex_InvalidPane)
		
		if (index < 0) or (index > (Panes.Count - 1)):
			if index != (-1):
				raise ArgumentOutOfRangeException(Strings.DockPanel_SetPaneIndex_InvalidIndex)
		
		if oldIndex == index:
			return 
		if (oldIndex == (Panes.Count - 1)) and (index == (-1)):
			return 
		
		Panes.Remove(pane)
		if index == (-1):
			Panes.Add(pane)
		elif oldIndex < index:
			Panes.AddAt(pane, (index - 1))
		else:
			Panes.AddAt(pane, index)

	
	public def SuspendLayout(allWindows as bool):
		FocusManager.SuspendFocusTracking()
		SuspendLayout()
		if allWindows:
			SuspendMdiClientLayout()

	
	public def ResumeLayout(performLayout as bool, allWindows as bool):
		FocusManager.ResumeFocusTracking()
		ResumeLayout(performLayout)
		if allWindows:
			ResumeMdiClientLayout(performLayout)

	
	internal ParentForm as Form:
		get:
			if not IsParentFormValid():
				raise InvalidOperationException(Strings.DockPanel_ParentForm_Invalid)
			
			return GetMdiClientController().ParentForm

	
	private def IsParentFormValid() as bool:
		if (DocumentStyle == DocumentStyle.DockingSdi) or (DocumentStyle == DocumentStyle.DockingWindow):
			return true
		
		if not MdiClientExists:
			GetMdiClientController().RenewMdiClient()
		
		return MdiClientExists

	
	protected override def OnParentChanged(e as EventArgs):
		SetAutoHideWindowParent()
		GetMdiClientController().ParentForm = (self.Parent as Form)
		super.OnParentChanged(e)

	
	private def SetAutoHideWindowParent():
		parent as Control
		if (DocumentStyle == DocumentStyle.DockingMdi) or (DocumentStyle == DocumentStyle.SystemMdi):
			parent = self.Parent
		else:
			parent = self
		if AutoHideWindow.Parent != parent:
			AutoHideWindow.Parent = parent
			AutoHideWindow.BringToFront()

	
	protected override def OnVisibleChanged(e as EventArgs):
		super.OnVisibleChanged(e)
		
		if Visible:
			SetMdiClient()

	
	private SystemMdiClientBounds as Rectangle:
		get:
			if (not IsParentFormValid()) or (not Visible):
				return Rectangle.Empty
			
			rect as Rectangle = ParentForm.RectangleToClient(RectangleToScreen(DocumentWindowBounds))
			return rect

	
	internal DocumentWindowBounds as Rectangle:
		get:
			rectDocumentBounds as Rectangle = DisplayRectangle
			if DockWindows[DockState.DockLeft].Visible:
				rectDocumentBounds.X += DockWindows[DockState.DockLeft].Width
				rectDocumentBounds.Width -= DockWindows[DockState.DockLeft].Width
			if DockWindows[DockState.DockRight].Visible:
				rectDocumentBounds.Width -= DockWindows[DockState.DockRight].Width
			if DockWindows[DockState.DockTop].Visible:
				rectDocumentBounds.Y += DockWindows[DockState.DockTop].Height
				rectDocumentBounds.Height -= DockWindows[DockState.DockTop].Height
			if DockWindows[DockState.DockBottom].Visible:
				rectDocumentBounds.Height -= DockWindows[DockState.DockBottom].Height
			
			return rectDocumentBounds

	
	
	private m_dummyControlPaintEventHandler as PaintEventHandler = null

	private def InvalidateWindowRegion():
		if DesignMode:
			return 
		
		if m_dummyControlPaintEventHandler is null:
			m_dummyControlPaintEventHandler = DummyControl_Paint
		
		DummyControl.Paint += m_dummyControlPaintEventHandler
		DummyControl.Invalidate()

	
	private def DummyControl_Paint(sender as object, e as PaintEventArgs):
		DummyControl.Paint -= m_dummyControlPaintEventHandler
		UpdateWindowRegion()

	
	private def UpdateWindowRegion():
		if self.DocumentStyle == DocumentStyle.DockingMdi:
			UpdateWindowRegion_ClipContent()
		elif (self.DocumentStyle == DocumentStyle.DockingSdi) or (self.DocumentStyle == DocumentStyle.DockingWindow):
			UpdateWindowRegion_FullDocumentArea()
		elif self.DocumentStyle == DocumentStyle.SystemMdi:
			UpdateWindowRegion_EmptyDocumentArea()

	
	private def UpdateWindowRegion_FullDocumentArea():
		SetRegion(null)

	
	private def UpdateWindowRegion_EmptyDocumentArea():
		rect as Rectangle = DocumentWindowBounds
		SetRegion((of Rectangle: rect))

	
	private def UpdateWindowRegion_ClipContent():
		count = 0
		for pane as DockPane in self.Panes:
			if (not pane.Visible) or (pane.DockState != DockState.Document):
				continue 
			
			count += 1
		
		if count == 0:
			SetRegion(null)
			return 
		
		rects as (Rectangle) = array(Rectangle, count)
		i = 0
		for pane as DockPane in self.Panes:
			if (not pane.Visible) or (pane.DockState != DockState.Document):
				continue 
			
			rects[i] = RectangleToClient(pane.RectangleToScreen(pane.ContentRectangle))
			i += 1
		
		SetRegion(rects)

	
	private m_clipRects as (Rectangle) = null

	private def SetRegion(clipRects as (Rectangle)):
		if not IsClipRectsChanged(clipRects):
			return 
		
		m_clipRects = clipRects
		
		if (m_clipRects is null) or (m_clipRects.GetLength(0) == 0):
			Region = null
		else:
			region = Region(Rectangle(0, 0, self.Width, self.Height))
			for rect as Rectangle in m_clipRects:
				region.Exclude(rect)
			Region = region

	
	private def IsClipRectsChanged(clipRects as (Rectangle)) as bool:
		
		matched as bool
		if (clipRects is null) and (m_clipRects is null):
			return false
		elif (clipRects is null) != (m_clipRects is null):
			return true
		for rect as Rectangle in clipRects:
			matched = false
			for rect2 as Rectangle in m_clipRects:
				if rect == rect2:
					matched = true
					break 
			if not matched:
				return true
		
		for rect2 as Rectangle in m_clipRects:
			matched = false
			for rect as Rectangle in clipRects:
				if rect == rect2:
					matched = true
					break 
			if not matched:
				return true
		return false

	
	private static final ContentAddedEvent = object()

	[LocalizedCategory('Category_DockingNotification')]
	[LocalizedDescription('DockPanel_ContentAdded_Description')]
	public event ContentAdded as EventHandler[of DockContentEventArgs]

	protected virtual def OnContentAdded(e as DockContentEventArgs):
		handler = cast(EventHandler[of DockContentEventArgs], Events[ContentAddedEvent])
		handler(self, e)

	
	private static final ContentRemovedEvent = object()

	[LocalizedCategory('Category_DockingNotification')]
	[LocalizedDescription('DockPanel_ContentRemoved_Description')]
	public event ContentRemoved as EventHandler[of DockContentEventArgs]

	protected virtual def OnContentRemoved(e as DockContentEventArgs):
		handler = cast(EventHandler[of DockContentEventArgs], Events[ContentRemovedEvent])
		handler(self, e)

