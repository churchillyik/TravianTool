
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Drawing.Drawing2D
import System.Windows.Forms
import System.ComponentModel
import System.Collections
import System.Collections.Generic

internal class VS2005DockPaneStrip(DockPaneStripBase):

	private class TabVS2005(Tab):

		public def constructor(content as IDockContent):
			super(content)

		
		private m_tabX as int

		public TabX as int:
			get:
				return m_tabX
			set:
				m_tabX = value

		
		private m_tabWidth as int

		public TabWidth as int:
			get:
				return m_tabWidth
			set:
				m_tabWidth = value

		
		private m_maxWidth as int

		public MaxWidth as int:
			get:
				return m_maxWidth
			set:
				m_maxWidth = value

		
		private m_flag as bool

		protected Flag as bool:
			get:
				return m_flag
			set:
				m_flag = value

	
	protected override def CreateTab(content as IDockContent) as DockPaneStripBase.Tab:
		return TabVS2005(content)

	
	private final class InertButton(InertButtonBase):

		private m_image0 as Bitmap

		private m_image1 as Bitmap

		
		public def constructor(image0 as Bitmap, image1 as Bitmap):
			super()
			m_image0 = image0
			m_image1 = image1

		
		private m_imageCategory = 0

		public ImageCategory as int:
			get:
				return m_imageCategory
			set:
				if m_imageCategory == value:
					return 
				
				m_imageCategory = value
				Invalidate()

		
		public override Image as Bitmap:
			get:
				return (m_image0 if (ImageCategory == 0) else m_image1)

		
		protected override def OnRefreshChanges():
			if VS2005DockPaneStrip.ColorDocumentActiveText != ForeColor:
				ForeColor = VS2005DockPaneStrip.ColorDocumentActiveText
				Invalidate()

	
	#region consts
	private static final _ToolWindowStripGapTop = 0

	private static final _ToolWindowStripGapBottom = 1

	private static final _ToolWindowStripGapLeft = 0

	private static final _ToolWindowStripGapRight = 0

	private static final _ToolWindowImageHeight = 16

	private static final _ToolWindowImageWidth = 16

	private static final _ToolWindowImageGapTop = 3

	private static final _ToolWindowImageGapBottom = 1

	private static final _ToolWindowImageGapLeft = 2

	private static final _ToolWindowImageGapRight = 0

	private static final _ToolWindowTextGapRight = 3

	private static final _ToolWindowTabSeperatorGapTop = 3

	private static final _ToolWindowTabSeperatorGapBottom = 3

	
	private static final _DocumentStripGapTop = 0

	private static final _DocumentStripGapBottom = 1

	private static final _DocumentTabMaxWidth = 200

	private static final _DocumentButtonGapTop = 4

	private static final _DocumentButtonGapBottom = 4

	private static final _DocumentButtonGapBetween = 0

	private static final _DocumentButtonGapRight = 3

	private static final _DocumentTabGapTop = 3

	private static final _DocumentTabGapLeft = 3

	private static final _DocumentTabGapRight = 3

	private static final _DocumentIconGapBottom = 2

	private static final _DocumentIconGapLeft = 8

	private static final _DocumentIconGapRight = 0

	private static final _DocumentIconHeight = 16

	private static final _DocumentIconWidth = 16

	private static final _DocumentTextGapRight = 3

	#endregion
	
	private static _imageButtonClose as Bitmap

	private static ImageButtonClose as Bitmap:
		get:
			if _imageButtonClose is null:
				_imageButtonClose = Resources.DockPane_Close
			
			return _imageButtonClose

	
	private m_buttonClose as InertButton

	private ButtonClose as InertButton:
		get:
			if m_buttonClose is null:
				m_buttonClose = InertButton(ImageButtonClose, ImageButtonClose)
				m_toolTip.SetToolTip(m_buttonClose, ToolTipClose)
				m_buttonClose.Click += Close_Click
				Controls.Add(m_buttonClose)
			
			return m_buttonClose

	
	private static _imageButtonWindowList as Bitmap

	private static ImageButtonWindowList as Bitmap:
		get:
			if _imageButtonWindowList is null:
				_imageButtonWindowList = Resources.DockPane_Option
			
			return _imageButtonWindowList

	
	private static _imageButtonWindowListOverflow as Bitmap

	private static ImageButtonWindowListOverflow as Bitmap:
		get:
			if _imageButtonWindowListOverflow is null:
				_imageButtonWindowListOverflow = Resources.DockPane_OptionOverflow
			
			return _imageButtonWindowListOverflow

	
	private m_buttonWindowList as InertButton

	private ButtonWindowList as InertButton:
		get:
			if m_buttonWindowList is null:
				m_buttonWindowList = InertButton(ImageButtonWindowList, ImageButtonWindowListOverflow)
				m_toolTip.SetToolTip(m_buttonWindowList, ToolTipSelect)
				m_buttonWindowList.Click += WindowList_Click
				Controls.Add(m_buttonWindowList)
			
			return m_buttonWindowList

	
	private static GraphicsPath as GraphicsPath:
		get:
			return VS2005AutoHideStrip.GraphicsPath

	
	private m_components as IContainer

	private m_toolTip as ToolTip

	private Components as IContainer:
		get:
			return m_components

	
	#region Customizable Properties
	private static ToolWindowStripGapTop as int:
		get:
			return _ToolWindowStripGapTop

	
	private static ToolWindowStripGapBottom as int:
		get:
			return _ToolWindowStripGapBottom

	
	private static ToolWindowStripGapLeft as int:
		get:
			return _ToolWindowStripGapLeft

	
	private static ToolWindowStripGapRight as int:
		get:
			return _ToolWindowStripGapRight

	
	private static ToolWindowImageHeight as int:
		get:
			return _ToolWindowImageHeight

	
	private static ToolWindowImageWidth as int:
		get:
			return _ToolWindowImageWidth

	
	private static ToolWindowImageGapTop as int:
		get:
			return _ToolWindowImageGapTop

	
	private static ToolWindowImageGapBottom as int:
		get:
			return _ToolWindowImageGapBottom

	
	private static ToolWindowImageGapLeft as int:
		get:
			return _ToolWindowImageGapLeft

	
	private static ToolWindowImageGapRight as int:
		get:
			return _ToolWindowImageGapRight

	
	private static ToolWindowTextGapRight as int:
		get:
			return _ToolWindowTextGapRight

	
	private static ToolWindowTabSeperatorGapTop as int:
		get:
			return _ToolWindowTabSeperatorGapTop

	
	private static ToolWindowTabSeperatorGapBottom as int:
		get:
			return _ToolWindowTabSeperatorGapBottom

	
	private static _toolTipClose as string

	private static ToolTipClose as string:
		get:
			if _toolTipClose is null:
				_toolTipClose = Strings.DockPaneStrip_ToolTipClose
			return _toolTipClose

	
	private static _toolTipSelect as string

	private static ToolTipSelect as string:
		get:
			if _toolTipSelect is null:
				_toolTipSelect = Strings.DockPaneStrip_ToolTipWindowList
			return _toolTipSelect

	
	private ToolWindowTextFormat as TextFormatFlags:
		get:
			textFormat as TextFormatFlags = (((TextFormatFlags.EndEllipsis | TextFormatFlags.HorizontalCenter) | TextFormatFlags.SingleLine) | TextFormatFlags.VerticalCenter)
			if RightToLeft == RightToLeft.Yes:
				return ((textFormat | TextFormatFlags.RightToLeft) | TextFormatFlags.Right)
			else:
				return textFormat

	
	private static DocumentStripGapTop as int:
		get:
			return _DocumentStripGapTop

	
	private static DocumentStripGapBottom as int:
		get:
			return _DocumentStripGapBottom

	
	private DocumentTextFormat as TextFormatFlags:
		get:
			textFormat as TextFormatFlags = ((((TextFormatFlags.PathEllipsis | TextFormatFlags.SingleLine) | TextFormatFlags.VerticalCenter) | TextFormatFlags.PreserveGraphicsClipping) | TextFormatFlags.HorizontalCenter)
			if RightToLeft == RightToLeft.Yes:
				return (textFormat | TextFormatFlags.RightToLeft)
			else:
				return textFormat

	
	private static DocumentTabMaxWidth as int:
		get:
			return _DocumentTabMaxWidth

	
	private static DocumentButtonGapTop as int:
		get:
			return _DocumentButtonGapTop

	
	private static DocumentButtonGapBottom as int:
		get:
			return _DocumentButtonGapBottom

	
	private static DocumentButtonGapBetween as int:
		get:
			return _DocumentButtonGapBetween

	
	private static DocumentButtonGapRight as int:
		get:
			return _DocumentButtonGapRight

	
	private static DocumentTabGapTop as int:
		get:
			return _DocumentTabGapTop

	
	private static DocumentTabGapLeft as int:
		get:
			return _DocumentTabGapLeft

	
	private static DocumentTabGapRight as int:
		get:
			return _DocumentTabGapRight

	
	private static DocumentIconGapBottom as int:
		get:
			return _DocumentIconGapBottom

	
	private static DocumentIconGapLeft as int:
		get:
			return _DocumentIconGapLeft

	
	private static DocumentIconGapRight as int:
		get:
			return _DocumentIconGapRight

	
	private static DocumentIconWidth as int:
		get:
			return _DocumentIconWidth

	
	private static DocumentIconHeight as int:
		get:
			return _DocumentIconHeight

	
	private static DocumentTextGapRight as int:
		get:
			return _DocumentTextGapRight

	
	private static PenToolWindowTabBorder as Pen:
		get:
			return SystemPens.GrayText

	
	private static PenDocumentTabActiveBorder as Pen:
		get:
			return SystemPens.ControlDarkDark

	
	private static PenDocumentTabInactiveBorder as Pen:
		get:
			return SystemPens.GrayText

	
	private static BrushToolWindowActiveBackground as Brush:
		get:
			return SystemBrushes.Control

	
	private static BrushDocumentActiveBackground as Brush:
		get:
			return SystemBrushes.ControlLightLight

	
	private static BrushDocumentInactiveBackground as Brush:
		get:
			return SystemBrushes.ControlLight

	
	private static ColorToolWindowActiveText as Color:
		get:
			return SystemColors.ControlText

	
	private static ColorDocumentActiveText as Color:
		get:
			return SystemColors.ControlText

	
	private static ColorToolWindowInactiveText as Color:
		get:
			return SystemColors.ControlDarkDark

	
	private static ColorDocumentInactiveText as Color:
		get:
			return SystemColors.ControlText

	
	#endregion
	
	public def constructor(pane as DockPane):
		super(pane)
		SetStyle((((ControlStyles.ResizeRedraw | ControlStyles.UserPaint) | ControlStyles.AllPaintingInWmPaint) | ControlStyles.OptimizedDoubleBuffer), true)
		
		SuspendLayout()
		
		m_components = Container()
		m_toolTip = ToolTip(Components)
		m_selectMenu = ContextMenuStrip(Components)
		
		ResumeLayout()

	
	protected override def Dispose(disposing as bool):
		if disposing:
			Components.Dispose()
			if m_boldFont is not null:
				m_boldFont.Dispose()
				m_boldFont = null
		super.Dispose(disposing)

	
	private static TextFont as Font:
		get:
			return SystemInformation.MenuFont

	
	private m_font as Font

	private m_boldFont as Font

	private BoldFont as Font:
		get:
			if IsDisposed:
				return null
			
			if m_boldFont is null:
				m_font = TextFont
				m_boldFont = Font(TextFont, FontStyle.Bold)
			elif m_font != TextFont:
				m_boldFont.Dispose()
				m_font = TextFont
				m_boldFont = Font(TextFont, FontStyle.Bold)
			
			return m_boldFont

	
	private m_startDisplayingTab = 0

	private StartDisplayingTab as int:
		get:
			return m_startDisplayingTab
		set:
			m_startDisplayingTab = value
			Invalidate()

	
	private m_endDisplayingTab = 0

	private EndDisplayingTab as int:
		get:
			return m_endDisplayingTab
		set:
			m_endDisplayingTab = value

	
	private m_documentTabsOverflow = false

	private DocumentTabsOverflow as bool:
		set:
			if m_documentTabsOverflow == value:
				return 
			
			m_documentTabsOverflow = value
			if value:
				ButtonWindowList.ImageCategory = 1
			else:
				ButtonWindowList.ImageCategory = 0

	
	protected override def MeasureHeight() as int:
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			return MeasureHeight_ToolWindow()
		else:
			return MeasureHeight_Document()

	
	private def MeasureHeight_ToolWindow() as int:
		if DockPane.IsAutoHide or (Tabs.Count <= 1):
			return 0
		
		height as int = ((Math.Max(TextFont.Height, ((ToolWindowImageHeight + ToolWindowImageGapTop) + ToolWindowImageGapBottom)) + ToolWindowStripGapTop) + ToolWindowStripGapBottom)
		
		return height

	
	private def MeasureHeight_Document() as int:
		height as int = ((Math.Max((TextFont.Height + DocumentTabGapTop), ((ButtonClose.Height + DocumentButtonGapTop) + DocumentButtonGapBottom)) + DocumentStripGapBottom) + DocumentStripGapTop)
		
		return height

	
	protected override def OnPaint(e as PaintEventArgs):
		if Appearance == DockPane.AppearanceStyle.Document:
			if BackColor != SystemColors.Control:
				BackColor = SystemColors.Control
		elif BackColor != SystemColors.ControlLight:
			BackColor = SystemColors.ControlLight
		super.OnPaint(e)
		CalculateTabs()
		if (Appearance == DockPane.AppearanceStyle.Document) and (DockPane.ActiveContent is not null):
			if EnsureDocumentTabVisible(DockPane.ActiveContent, false):
				CalculateTabs()
		
		DrawTabStrip(e.Graphics)

	
	protected override def OnRefreshChanges():
		SetInertButtons()
		Invalidate()

	
	protected override def GetOutline(index as int) as GraphicsPath:
		
		if Appearance == DockPane.AppearanceStyle.Document:
			return GetOutline_Document(index)
		else:
			return GetOutline_ToolWindow(index)
		

	
	private def GetOutline_Document(index as int) as GraphicsPath:
		rectTab as Rectangle = GetTabRectangle(index)
		rectTab.X -= (rectTab.Height / 2)
		rectTab.Intersect(TabsRectangle)
		rectTab = RectangleToScreen(DrawHelper.RtlTransform(self, rectTab))
		y as int = rectTab.Top
		rectPaneClient as Rectangle = DockPane.RectangleToScreen(DockPane.ClientRectangle)
		
		path = GraphicsPath()
		pathTab as GraphicsPath = GetTabOutline_Document(Tabs[index], true, true, true)
		path.AddPath(pathTab, true)
		path.AddLine(rectTab.Right, rectTab.Bottom, rectPaneClient.Right, rectTab.Bottom)
		path.AddLine(rectPaneClient.Right, rectTab.Bottom, rectPaneClient.Right, rectPaneClient.Bottom)
		path.AddLine(rectPaneClient.Right, rectPaneClient.Bottom, rectPaneClient.Left, rectPaneClient.Bottom)
		path.AddLine(rectPaneClient.Left, rectPaneClient.Bottom, rectPaneClient.Left, rectTab.Bottom)
		path.AddLine(rectPaneClient.Left, rectTab.Bottom, rectTab.Right, rectTab.Bottom)
		return path

	
	private def GetOutline_ToolWindow(index as int) as GraphicsPath:
		rectTab as Rectangle = GetTabRectangle(index)
		rectTab.Intersect(TabsRectangle)
		rectTab = RectangleToScreen(DrawHelper.RtlTransform(self, rectTab))
		y as int = rectTab.Top
		rectPaneClient as Rectangle = DockPane.RectangleToScreen(DockPane.ClientRectangle)
		
		path = GraphicsPath()
		pathTab as GraphicsPath = GetTabOutline(Tabs[index], true, true)
		path.AddPath(pathTab, true)
		path.AddLine(rectTab.Left, rectTab.Top, rectPaneClient.Left, rectTab.Top)
		path.AddLine(rectPaneClient.Left, rectTab.Top, rectPaneClient.Left, rectPaneClient.Top)
		path.AddLine(rectPaneClient.Left, rectPaneClient.Top, rectPaneClient.Right, rectPaneClient.Top)
		path.AddLine(rectPaneClient.Right, rectPaneClient.Top, rectPaneClient.Right, rectTab.Top)
		path.AddLine(rectPaneClient.Right, rectTab.Top, rectTab.Right, rectTab.Top)
		return path

	
	private def CalculateTabs():
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			CalculateTabs_ToolWindow()
		else:
			CalculateTabs_Document()

	
	private def CalculateTabs_ToolWindow():
		if (Tabs.Count <= 1) or DockPane.IsAutoHide:
			return 
		
		rectTabStrip as Rectangle = TabStripRectangle
		
		// Calculate tab widths
		countTabs as int = Tabs.Count
		for tab as TabVS2005 in Tabs:
			tab.MaxWidth = GetMaxTabWidth(Tabs.IndexOf(tab))
			tab.Flag = false
		
		// Set tab whose max width less than average width
		anyWidthWithinAverage = true
		totalWidth as int = ((rectTabStrip.Width - ToolWindowStripGapLeft) - ToolWindowStripGapRight)
		totalAllocatedWidth = 0
		averageWidth as int = (totalWidth / countTabs)
		remainedTabs as int = countTabs
		anyWidthWithinAverage = true
		goto converterGeneratedName1
		while true:
			:converterGeneratedName1
			break  unless (anyWidthWithinAverage and (remainedTabs > 0))
			anyWidthWithinAverage = false
			for tab as TabVS2005 in Tabs:
				if tab.Flag:
					continue 
				
				if tab.MaxWidth <= averageWidth:
					tab.Flag = true
					tab.TabWidth = tab.MaxWidth
					totalAllocatedWidth += tab.TabWidth
					anyWidthWithinAverage = true
					remainedTabs -= 1
			if remainedTabs != 0:
				averageWidth = ((totalWidth - totalAllocatedWidth) / remainedTabs)
		
		// If any tab width not set yet, set it to the average width
		if remainedTabs > 0:
			roundUpWidth as int = ((totalWidth - totalAllocatedWidth) - (averageWidth * remainedTabs))
			for tab as TabVS2005 in Tabs:
				if tab.Flag:
					continue 
				
				tab.Flag = true
				if roundUpWidth > 0:
					tab.TabWidth = (averageWidth + 1)
					roundUpWidth -= 1
				else:
					tab.TabWidth = averageWidth
		
		// Set the X position of the tabs
		x as int = (rectTabStrip.X + ToolWindowStripGapLeft)
		for tab as TabVS2005 in Tabs:
			tab.TabX = x
			x += tab.TabWidth

	
	private def CalculateDocumentTab(rectTabStrip as Rectangle, ref x as int, index as int) as bool:
		overflow = false
		
		tab = (Tabs[index] as TabVS2005)
		tab.MaxWidth = GetMaxTabWidth(index)
		width as int = Math.Min(tab.MaxWidth, DocumentTabMaxWidth)
		if ((x + width) < rectTabStrip.Right) or (index == StartDisplayingTab):
			tab.TabX = x
			tab.TabWidth = width
			EndDisplayingTab = index
		else:
			tab.TabX = 0
			tab.TabWidth = 0
			overflow = true
		x += width
		
		return overflow

	
	private def CalculateTabs_Document():
		if m_startDisplayingTab >= Tabs.Count:
			m_startDisplayingTab = 0
		
		rectTabStrip as Rectangle = TabsRectangle
		
		x as int = (rectTabStrip.X + (rectTabStrip.Height / 2))
		
		overflow = false
		for i in range(StartDisplayingTab, Tabs.Count):
			overflow = CalculateDocumentTab(rectTabStrip, x, i)
		for i in range(0, StartDisplayingTab):
		
			overflow = CalculateDocumentTab(rectTabStrip, x, i)
		
		if not overflow:
			m_startDisplayingTab = 0
			x = (rectTabStrip.X + (rectTabStrip.Height / 2))
			for tab as TabVS2005 in Tabs:
				tab.TabX = x
				x += tab.TabWidth
		DocumentTabsOverflow = overflow

	
	protected override def EnsureTabVisible(content as IDockContent):
		if (Appearance != DockPane.AppearanceStyle.Document) or (not Tabs.Contains(content)):
			return 
		
		CalculateTabs()
		EnsureDocumentTabVisible(content, true)

	
	private def EnsureDocumentTabVisible(content as IDockContent, repaint as bool) as bool:
		index as int = Tabs.IndexOf(content)
		tab = (Tabs[index] as TabVS2005)
		if tab.TabWidth != 0:
			return false
		
		StartDisplayingTab = index
		if repaint:
			Invalidate()
		
		return true

	
	private def GetMaxTabWidth(index as int) as int:
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			return GetMaxTabWidth_ToolWindow(index)
		else:
			return GetMaxTabWidth_Document(index)

	
	private def GetMaxTabWidth_ToolWindow(index as int) as int:
		content as IDockContent = Tabs[index].Content
		sizeString as Size = TextRenderer.MeasureText(content.DockHandler.TabText, TextFont)
		return ((((ToolWindowImageWidth + sizeString.Width) + ToolWindowImageGapLeft) + ToolWindowImageGapRight) + ToolWindowTextGapRight)

	
	private def GetMaxTabWidth_Document(index as int) as int:
		content as IDockContent = Tabs[index].Content
		
		height as int = GetTabRectangle_Document(index).Height
		
		sizeText as Size = TextRenderer.MeasureText(content.DockHandler.TabText, BoldFont, Size(DocumentTabMaxWidth, height), DocumentTextFormat)
		
		if DockPane.DockPanel.ShowDocumentIcon:
			return ((((sizeText.Width + DocumentIconWidth) + DocumentIconGapLeft) + DocumentIconGapRight) + DocumentTextGapRight)
		else:
			return ((sizeText.Width + DocumentIconGapLeft) + DocumentTextGapRight)

	
	private def DrawTabStrip(g as Graphics):
		if Appearance == DockPane.AppearanceStyle.Document:
			DrawTabStrip_Document(g)
		else:
			DrawTabStrip_ToolWindow(g)

	
	private def DrawTabStrip_Document(g as Graphics):
		count as int = Tabs.Count
		if count == 0:
			return 
		
		rectTabStrip as Rectangle = TabStripRectangle
		
		// Draw the tabs
		rectTabOnly as Rectangle = TabsRectangle
		rectTab as Rectangle = Rectangle.Empty
		tabActive as TabVS2005 = null
		g.SetClip(DrawHelper.RtlTransform(self, rectTabOnly))
		for i in range(0, count):
			rectTab = GetTabRectangle(i)
			if Tabs[i].Content == DockPane.ActiveContent:
				tabActive = (Tabs[i] as TabVS2005)
				continue 
			if rectTab.IntersectsWith(rectTabOnly):
				DrawTab(g, (Tabs[i] as TabVS2005), rectTab)
		
		g.SetClip(rectTabStrip)
		g.DrawLine(PenDocumentTabActiveBorder, rectTabStrip.Left, (rectTabStrip.Bottom - 1), rectTabStrip.Right, (rectTabStrip.Bottom - 1))
		g.SetClip(DrawHelper.RtlTransform(self, rectTabOnly))
		if tabActive is not null:
			rectTab = GetTabRectangle(Tabs.IndexOf(tabActive))
			if rectTab.IntersectsWith(rectTabOnly):
				DrawTab(g, tabActive, rectTab)

	
	private def DrawTabStrip_ToolWindow(g as Graphics):
		rectTabStrip as Rectangle = TabStripRectangle
		
		g.DrawLine(PenToolWindowTabBorder, rectTabStrip.Left, rectTabStrip.Top, rectTabStrip.Right, rectTabStrip.Top)
		for i in range(0, Tabs.Count):
		
			DrawTab(g, (Tabs[i] as TabVS2005), GetTabRectangle(i))

	
	private def GetTabRectangle(index as int) as Rectangle:
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			return GetTabRectangle_ToolWindow(index)
		else:
			return GetTabRectangle_Document(index)

	
	private def GetTabRectangle_ToolWindow(index as int) as Rectangle:
		rectTabStrip as Rectangle = TabStripRectangle
		
		tab = cast(TabVS2005, Tabs[index])
		return Rectangle(tab.TabX, rectTabStrip.Y, tab.TabWidth, rectTabStrip.Height)

	
	private def GetTabRectangle_Document(index as int) as Rectangle:
		rectTabStrip as Rectangle = TabStripRectangle
		tab = cast(TabVS2005, Tabs[index])
		
		return Rectangle(tab.TabX, (rectTabStrip.Y + DocumentTabGapTop), tab.TabWidth, (rectTabStrip.Height - DocumentTabGapTop))

	
	private def DrawTab(g as Graphics, tab as TabVS2005, rect as Rectangle):
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			DrawTab_ToolWindow(g, tab, rect)
		else:
			DrawTab_Document(g, tab, rect)

	
	private def GetTabOutline(tab as Tab, rtlTransform as bool, toScreen as bool) as GraphicsPath:
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			return GetTabOutline_ToolWindow(tab, rtlTransform, toScreen)
		else:
			return GetTabOutline_Document(tab, rtlTransform, toScreen, false)

	
	private def GetTabOutline_ToolWindow(tab as Tab, rtlTransform as bool, toScreen as bool) as GraphicsPath:
		rect as Rectangle = GetTabRectangle(Tabs.IndexOf(tab))
		if rtlTransform:
			rect = DrawHelper.RtlTransform(self, rect)
		if toScreen:
			rect = RectangleToScreen(rect)
		
		DrawHelper.GetRoundedCornerTab(GraphicsPath, rect, false)
		return GraphicsPath

	
	private def GetTabOutline_Document(tab as Tab, rtlTransform as bool, toScreen as bool, full as bool) as GraphicsPath:
		curveSize = 6
		
		GraphicsPath.Reset()
		rect as Rectangle = GetTabRectangle(Tabs.IndexOf(tab))
		if rtlTransform:
			rect = DrawHelper.RtlTransform(self, rect)
		if toScreen:
			rect = RectangleToScreen(rect)
		
		if ((tab.Content == DockPane.ActiveContent) or (Tabs.IndexOf(tab) == StartDisplayingTab)) or full:
			if RightToLeft == RightToLeft.Yes:
				GraphicsPath.AddLine(rect.Right, rect.Bottom, (rect.Right + (rect.Height / 2)), rect.Bottom)
				GraphicsPath.AddLine((rect.Right + (rect.Height / 2)), rect.Bottom, ((rect.Right - (rect.Height / 2)) + (curveSize / 2)), (rect.Top + (curveSize / 2)))
			else:
				GraphicsPath.AddLine(rect.Left, rect.Bottom, (rect.Left - (rect.Height / 2)), rect.Bottom)
				GraphicsPath.AddLine((rect.Left - (rect.Height / 2)), rect.Bottom, ((rect.Left + (rect.Height / 2)) - (curveSize / 2)), (rect.Top + (curveSize / 2)))
		elif RightToLeft == RightToLeft.Yes:
			GraphicsPath.AddLine(rect.Right, rect.Bottom, rect.Right, (rect.Bottom - (rect.Height / 2)))
			GraphicsPath.AddLine(rect.Right, (rect.Bottom - (rect.Height / 2)), ((rect.Right - (rect.Height / 2)) + (curveSize / 2)), (rect.Top + (curveSize / 2)))
		else:
			GraphicsPath.AddLine(rect.Left, rect.Bottom, rect.Left, (rect.Bottom - (rect.Height / 2)))
			GraphicsPath.AddLine(rect.Left, (rect.Bottom - (rect.Height / 2)), ((rect.Left + (rect.Height / 2)) - (curveSize / 2)), (rect.Top + (curveSize / 2)))
		
		if RightToLeft == RightToLeft.Yes:
			GraphicsPath.AddLine(((rect.Right - (rect.Height / 2)) - (curveSize / 2)), rect.Top, (rect.Left + (curveSize / 2)), rect.Top)
			GraphicsPath.AddArc(Rectangle(rect.Left, rect.Top, curveSize, curveSize), 180, 90)
		else:
			GraphicsPath.AddLine(((rect.Left + (rect.Height / 2)) + (curveSize / 2)), rect.Top, (rect.Right - (curveSize / 2)), rect.Top)
			GraphicsPath.AddArc(Rectangle((rect.Right - curveSize), rect.Top, curveSize, curveSize), -90, 90)
		
		if ((Tabs.IndexOf(tab) != EndDisplayingTab) and ((Tabs.IndexOf(tab) != (Tabs.Count - 1)) and (Tabs[(Tabs.IndexOf(tab) + 1)].Content == DockPane.ActiveContent))) and (not full):
			if RightToLeft == RightToLeft.Yes:
				GraphicsPath.AddLine(rect.Left, (rect.Top + (curveSize / 2)), rect.Left, (rect.Top + (rect.Height / 2)))
				GraphicsPath.AddLine(rect.Left, (rect.Top + (rect.Height / 2)), (rect.Left + (rect.Height / 2)), rect.Bottom)
			else:
				GraphicsPath.AddLine(rect.Right, (rect.Top + (curveSize / 2)), rect.Right, (rect.Top + (rect.Height / 2)))
				GraphicsPath.AddLine(rect.Right, (rect.Top + (rect.Height / 2)), (rect.Right - (rect.Height / 2)), rect.Bottom)
		elif RightToLeft == RightToLeft.Yes:
			GraphicsPath.AddLine(rect.Left, (rect.Top + (curveSize / 2)), rect.Left, rect.Bottom)
		else:
			GraphicsPath.AddLine(rect.Right, (rect.Top + (curveSize / 2)), rect.Right, rect.Bottom)
		
		return GraphicsPath

	
	private def DrawTab_ToolWindow(g as Graphics, tab as TabVS2005, rect as Rectangle):
		rectIcon = Rectangle((rect.X + ToolWindowImageGapLeft), ((((rect.Y + rect.Height) - 1) - ToolWindowImageGapBottom) - ToolWindowImageHeight), ToolWindowImageWidth, ToolWindowImageHeight)
		rectText as Rectangle = rectIcon
		rectText.X += (rectIcon.Width + ToolWindowImageGapRight)
		rectText.Width = ((((rect.Width - rectIcon.Width) - ToolWindowImageGapLeft) - ToolWindowImageGapRight) - ToolWindowTextGapRight)
		
		rectTab as Rectangle = DrawHelper.RtlTransform(self, rect)
		rectText = DrawHelper.RtlTransform(self, rectText)
		rectIcon = DrawHelper.RtlTransform(self, rectIcon)
		path as GraphicsPath = GetTabOutline(tab, true, false)
		if DockPane.ActiveContent == tab.Content:
			g.FillPath(BrushToolWindowActiveBackground, path)
			g.DrawPath(PenToolWindowTabBorder, path)
			TextRenderer.DrawText(g, tab.Content.DockHandler.TabText, TextFont, rectText, ColorToolWindowActiveText, ToolWindowTextFormat)
		else:
			if Tabs.IndexOf(DockPane.ActiveContent) != (Tabs.IndexOf(tab) + 1):
				pt1 = Point(rect.Right, (rect.Top + ToolWindowTabSeperatorGapTop))
				pt2 = Point(rect.Right, (rect.Bottom - ToolWindowTabSeperatorGapBottom))
				g.DrawLine(PenToolWindowTabBorder, DrawHelper.RtlTransform(self, pt1), DrawHelper.RtlTransform(self, pt2))
			TextRenderer.DrawText(g, tab.Content.DockHandler.TabText, TextFont, rectText, ColorToolWindowInactiveText, ToolWindowTextFormat)
		
		if rectTab.Contains(rectIcon):
			g.DrawIcon(tab.Content.DockHandler.Icon, rectIcon)

	
	private def DrawTab_Document(g as Graphics, tab as TabVS2005, rect as Rectangle):
		if tab.TabWidth == 0:
			return 
		
		rectIcon = Rectangle((rect.X + DocumentIconGapLeft), ((((rect.Y + rect.Height) - 1) - DocumentIconGapBottom) - DocumentIconHeight), DocumentIconWidth, DocumentIconHeight)
		rectText as Rectangle = rectIcon
		if DockPane.DockPanel.ShowDocumentIcon:
			rectText.X += (rectIcon.Width + DocumentIconGapRight)
			rectText.Y = rect.Y
			rectText.Width = ((((rect.Width - rectIcon.Width) - DocumentIconGapLeft) - DocumentIconGapRight) - DocumentTextGapRight)
			rectText.Height = rect.Height
		else:
			rectText.Width = ((rect.Width - DocumentIconGapLeft) - DocumentTextGapRight)
		
		rectTab as Rectangle = DrawHelper.RtlTransform(self, rect)
		rectText = DrawHelper.RtlTransform(self, rectText)
		rectIcon = DrawHelper.RtlTransform(self, rectIcon)
		path as GraphicsPath = GetTabOutline(tab, true, false)
		if DockPane.ActiveContent == tab.Content:
			g.FillPath(BrushDocumentActiveBackground, path)
			g.DrawPath(PenDocumentTabActiveBorder, path)
			if DockPane.IsActiveDocumentPane:
				TextRenderer.DrawText(g, tab.Content.DockHandler.TabText, BoldFont, rectText, ColorDocumentActiveText, DocumentTextFormat)
			else:
				TextRenderer.DrawText(g, tab.Content.DockHandler.TabText, TextFont, rectText, ColorDocumentActiveText, DocumentTextFormat)
		else:
			g.FillPath(BrushDocumentInactiveBackground, path)
			g.DrawPath(PenDocumentTabInactiveBorder, path)
			TextRenderer.DrawText(g, tab.Content.DockHandler.TabText, TextFont, rectText, ColorDocumentInactiveText, DocumentTextFormat)
		
		if rectTab.Contains(rectIcon) and DockPane.DockPanel.ShowDocumentIcon:
			g.DrawIcon(tab.Content.DockHandler.Icon, rectIcon)

	
	private TabStripRectangle as Rectangle:
		get:
			if Appearance == DockPane.AppearanceStyle.Document:
				return TabStripRectangle_Document
			else:
				return TabStripRectangle_ToolWindow

	
	private TabStripRectangle_ToolWindow as Rectangle:
		get:
			rect as Rectangle = ClientRectangle
			return Rectangle(rect.X, (rect.Top + ToolWindowStripGapTop), rect.Width, ((rect.Height - ToolWindowStripGapTop) - ToolWindowStripGapBottom))

	
	private TabStripRectangle_Document as Rectangle:
		get:
			rect as Rectangle = ClientRectangle
			return Rectangle(rect.X, (rect.Top + DocumentStripGapTop), rect.Width, ((rect.Height - DocumentStripGapTop) - ToolWindowStripGapBottom))

	
	private TabsRectangle as Rectangle:
		get:
			if Appearance == DockPane.AppearanceStyle.ToolWindow:
				return TabStripRectangle
			
			rectWindow as Rectangle = TabStripRectangle
			x as int = rectWindow.X
			y as int = rectWindow.Y
			width as int = rectWindow.Width
			height as int = rectWindow.Height
			
			x += DocumentTabGapLeft
			width -= (((((DocumentTabGapLeft + DocumentTabGapRight) + DocumentButtonGapRight) + ButtonClose.Width) + ButtonWindowList.Width) + (2 * DocumentButtonGapBetween))
			
			return Rectangle(x, y, width, height)

	
	private m_selectMenu as ContextMenuStrip

	private SelectMenu as ContextMenuStrip:
		get:
			return m_selectMenu

	
	private def WindowList_Click(sender as object, e as EventArgs):
		x = 0
		y as int = (ButtonWindowList.Location.Y + ButtonWindowList.Height)
		
		SelectMenu.Items.Clear()
		for tab as TabVS2005 in Tabs:
			content as IDockContent = tab.Content
			item as ToolStripItem = SelectMenu.Items.Add(content.DockHandler.TabText, content.DockHandler.Icon.ToBitmap())
			item.Tag = tab.Content
			item.Click += ContextMenuItem_Click
		SelectMenu.Show(ButtonWindowList, x, y)

	
	private def ContextMenuItem_Click(sender as object, e as EventArgs):
		item = (sender as ToolStripMenuItem)
		if item is not null:
			content = cast(IDockContent, item.Tag)
			DockPane.ActiveContent = content

	
	private def SetInertButtons():
		if Appearance == DockPane.AppearanceStyle.ToolWindow:
			if m_buttonClose is not null:
				m_buttonClose.Left = (-m_buttonClose.Width)
			
			if m_buttonWindowList is not null:
				m_buttonWindowList.Left = (-m_buttonWindowList.Width)
		else:
			showCloseButton as bool = (true if (DockPane.ActiveContent is null) else DockPane.ActiveContent.DockHandler.CloseButton)
			ButtonClose.Enabled = showCloseButton
			ButtonClose.RefreshChanges()
			ButtonWindowList.RefreshChanges()

	
	protected override def OnLayout(levent as LayoutEventArgs):
		if Appearance != DockPane.AppearanceStyle.Document:
			super.OnLayout(levent)
			return 
		
		rectTabStrip as Rectangle = TabStripRectangle
		
		// Set position and size of the buttons
		buttonWidth as int = ButtonClose.Image.Width
		buttonHeight as int = ButtonClose.Image.Height
		height as int = ((rectTabStrip.Height - DocumentButtonGapTop) - DocumentButtonGapBottom)
		if buttonHeight < height:
			buttonWidth = (buttonWidth * (height / buttonHeight))
			buttonHeight = height
		buttonSize = Size(buttonWidth, buttonHeight)
		
		x as int = ((((rectTabStrip.X + rectTabStrip.Width) - DocumentTabGapLeft) - DocumentButtonGapRight) - buttonWidth)
		y as int = (rectTabStrip.Y + DocumentButtonGapTop)
		point = Point(x, y)
		ButtonClose.Bounds = DrawHelper.RtlTransform(self, Rectangle(point, buttonSize))
		point.Offset(-(DocumentButtonGapBetween + buttonWidth), 0)
		ButtonWindowList.Bounds = DrawHelper.RtlTransform(self, Rectangle(point, buttonSize))
		
		OnRefreshChanges()
		
		super.OnLayout(levent)

	
	private def Close_Click(sender as object, e as EventArgs):
		DockPane.CloseActiveContent()

	
	protected override def HitTest(ptMouse as Point) as int:
		rectTabStrip as Rectangle = TabsRectangle
		if not TabsRectangle.Contains(ptMouse):
			return (-1)
		
		for tab as Tab in Tabs:
			path as GraphicsPath = GetTabOutline(tab, true, false)
			if path.IsVisible(ptMouse):
				return Tabs.IndexOf(tab)
		return (-1)

	
	protected override def OnMouseHover(e as EventArgs):
		index as int = HitTest(PointToClient(Control.MousePosition))
		toolTip as string = string.Empty
		
		super.OnMouseHover(e)
		
		if index != (-1):
			tab = (Tabs[index] as TabVS2005)
			if not String.IsNullOrEmpty(tab.Content.DockHandler.ToolTipText):
				toolTip = tab.Content.DockHandler.ToolTipText
			elif tab.MaxWidth > tab.TabWidth:
				toolTip = tab.Content.DockHandler.TabText
		
		if m_toolTip.GetToolTip(self) != toolTip:
			m_toolTip.Active = false
			m_toolTip.SetToolTip(self, toolTip)
			m_toolTip.Active = true
		
		// requires further tracking of mouse hover behavior,
		ResetMouseEventArgs()

	
	protected override def OnRightToLeftChanged(e as EventArgs):
		super.OnRightToLeftChanged(e)
		PerformLayout()

