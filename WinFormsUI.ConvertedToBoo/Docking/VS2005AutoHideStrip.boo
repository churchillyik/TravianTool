
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Windows.Forms
import System.Drawing.Drawing2D
import System.ComponentModel

internal class VS2005AutoHideStrip(AutoHideStripBase):

	private class TabVS2005(Tab):

		internal def constructor(content as IDockContent):
			super(content)

		
		private m_tabX = 0

		public TabX as int:
			get:
				return m_tabX
			set:
				m_tabX = value

		
		private m_tabWidth = 0

		public TabWidth as int:
			get:
				return m_tabWidth
			set:
				m_tabWidth = value
		

	
	private static final _ImageHeight = 16

	private static final _ImageWidth = 16

	private static final _ImageGapTop = 2

	private static final _ImageGapLeft = 4

	private static final _ImageGapRight = 2

	private static final _ImageGapBottom = 2

	private static final _TextGapLeft = 0

	private static final _TextGapRight = 0

	private static final _TabGapTop = 3

	private static final _TabGapLeft = 4

	private static final _TabGapBetween = 10

	
	#region Customizable Properties
	private static TextFont as Font:
		get:
			return SystemInformation.MenuFont

	
	private static _stringFormatTabHorizontal as StringFormat

	private StringFormatTabHorizontal as StringFormat:
		get:
			if _stringFormatTabHorizontal is null:
				_stringFormatTabHorizontal = StringFormat()
				_stringFormatTabHorizontal.Alignment = StringAlignment.Near
				_stringFormatTabHorizontal.LineAlignment = StringAlignment.Center
				_stringFormatTabHorizontal.FormatFlags = StringFormatFlags.NoWrap
			
			if RightToLeft == RightToLeft.Yes:
				_stringFormatTabHorizontal.FormatFlags |= StringFormatFlags.DirectionRightToLeft
			else:
				_stringFormatTabHorizontal.FormatFlags &= (~StringFormatFlags.DirectionRightToLeft)
			
			return _stringFormatTabHorizontal

	
	private static _stringFormatTabVertical as StringFormat

	private StringFormatTabVertical as StringFormat:
		get:
			if _stringFormatTabVertical is null:
				_stringFormatTabVertical = StringFormat()
				_stringFormatTabVertical.Alignment = StringAlignment.Near
				_stringFormatTabVertical.LineAlignment = StringAlignment.Center
				_stringFormatTabVertical.FormatFlags = (StringFormatFlags.NoWrap | StringFormatFlags.DirectionVertical)
			if RightToLeft == RightToLeft.Yes:
				_stringFormatTabVertical.FormatFlags |= StringFormatFlags.DirectionRightToLeft
			else:
				_stringFormatTabVertical.FormatFlags &= (~StringFormatFlags.DirectionRightToLeft)
			
			return _stringFormatTabVertical

	
	private static ImageHeight as int:
		get:
			return _ImageHeight

	
	private static ImageWidth as int:
		get:
			return _ImageWidth

	
	private static ImageGapTop as int:
		get:
			return _ImageGapTop

	
	private static ImageGapLeft as int:
		get:
			return _ImageGapLeft

	
	private static ImageGapRight as int:
		get:
			return _ImageGapRight

	
	private static ImageGapBottom as int:
		get:
			return _ImageGapBottom

	
	private static TextGapLeft as int:
		get:
			return _TextGapLeft

	
	private static TextGapRight as int:
		get:
			return _TextGapRight

	
	private static TabGapTop as int:
		get:
			return _TabGapTop

	
	private static TabGapLeft as int:
		get:
			return _TabGapLeft

	
	private static TabGapBetween as int:
		get:
			return _TabGapBetween

	
	private static BrushTabBackground as Brush:
		get:
			return SystemBrushes.Control

	
	private static PenTabBorder as Pen:
		get:
			return SystemPens.GrayText

	
	private static BrushTabText as Brush:
		get:
			return SystemBrushes.FromSystemColor(SystemColors.ControlDarkDark)

	#endregion
	
	private static _matrixIdentity = Matrix()

	private static MatrixIdentity as Matrix:
		get:
			return _matrixIdentity

	
	private static _dockStates as (DockState)

	private static DockStates as (DockState):
		get:
			if _dockStates is null:
				_dockStates = array(DockState, 4)
				_dockStates[0] = DockState.DockLeftAutoHide
				_dockStates[1] = DockState.DockRightAutoHide
				_dockStates[2] = DockState.DockTopAutoHide
				_dockStates[3] = DockState.DockBottomAutoHide
			return _dockStates

	
	private static _graphicsPath as GraphicsPath

	internal static GraphicsPath as GraphicsPath:
		get:
			if _graphicsPath is null:
				_graphicsPath = GraphicsPath()
			
			return _graphicsPath

	
	public def constructor(panel as DockPanel):
		super(panel)
		SetStyle((((ControlStyles.ResizeRedraw | ControlStyles.UserPaint) | ControlStyles.AllPaintingInWmPaint) | ControlStyles.OptimizedDoubleBuffer), true)
		BackColor = SystemColors.ControlLight

	
	protected override def OnPaint(e as PaintEventArgs):
		g as Graphics = e.Graphics
		DrawTabStrip(g)

	
	protected override def OnLayout(levent as LayoutEventArgs):
		CalculateTabs()
		super.OnLayout(levent)

	
	private def DrawTabStrip(g as Graphics):
		DrawTabStrip(g, DockState.DockTopAutoHide)
		DrawTabStrip(g, DockState.DockBottomAutoHide)
		DrawTabStrip(g, DockState.DockLeftAutoHide)
		DrawTabStrip(g, DockState.DockRightAutoHide)

	
	private def DrawTabStrip(g as Graphics, dockState as DockState):
		rectTabStrip as Rectangle = GetLogicalTabStripRectangle(dockState)
		
		if rectTabStrip.IsEmpty:
			return 
		
		matrixIdentity as Matrix = g.Transform
		if (dockState == DockState.DockLeftAutoHide) or (dockState == DockState.DockRightAutoHide):
			matrixRotated = Matrix()
			matrixRotated.RotateAt(90, PointF((cast(single, rectTabStrip.X) + (cast(single, rectTabStrip.Height) / 2)), (cast(single, rectTabStrip.Y) + (cast(single, rectTabStrip.Height) / 2))))
			g.Transform = matrixRotated
		
		for pane as Pane in GetPanes(dockState):
			for tab as TabVS2005 in pane.AutoHideTabs:
				DrawTab(g, tab)
		g.Transform = matrixIdentity

	
	private def CalculateTabs():
		CalculateTabs(DockState.DockTopAutoHide)
		CalculateTabs(DockState.DockBottomAutoHide)
		CalculateTabs(DockState.DockLeftAutoHide)
		CalculateTabs(DockState.DockRightAutoHide)

	
	private def CalculateTabs(dockState as DockState):
		rectTabStrip as Rectangle = GetLogicalTabStripRectangle(dockState)
		
		imageHeight as int = ((rectTabStrip.Height - ImageGapTop) - ImageGapBottom)
		imageWidth as int = ImageWidth
		if imageHeight > ImageHeight:
			imageWidth = (ImageWidth * (imageHeight / ImageHeight))
		
		x as int = (TabGapLeft + rectTabStrip.X)
		for pane as Pane in GetPanes(dockState):
			for tab as TabVS2005 in pane.AutoHideTabs:
				width as int = (((((imageWidth + ImageGapLeft) + ImageGapRight) + TextRenderer.MeasureText(tab.Content.DockHandler.TabText, TextFont).Width) + TextGapLeft) + TextGapRight)
				tab.TabX = x
				tab.TabWidth = width
				x += width
			
			x += TabGapBetween

	
	private def RtlTransform(rect as Rectangle, dockState as DockState) as Rectangle:
		rectTransformed as Rectangle
		if (dockState == DockState.DockLeftAutoHide) or (dockState == DockState.DockRightAutoHide):
			rectTransformed = rect
		else:
			rectTransformed = DrawHelper.RtlTransform(self, rect)
		
		return rectTransformed

	
	private def GetTabOutline(tab as TabVS2005, transformed as bool, rtlTransform as bool) as GraphicsPath:
		dockState as DockState = tab.Content.DockHandler.DockState
		rectTab as Rectangle = GetTabRectangle(tab, transformed)
		if rtlTransform:
			rectTab = RtlTransform(rectTab, dockState)
		upTab as bool = ((dockState == DockState.DockLeftAutoHide) or (dockState == DockState.DockBottomAutoHide))
		DrawHelper.GetRoundedCornerTab(GraphicsPath, rectTab, upTab)
		
		return GraphicsPath

	
	private def DrawTab(g as Graphics, tab as TabVS2005):
		rectTabOrigin as Rectangle = GetTabRectangle(tab)
		if rectTabOrigin.IsEmpty:
			return 
		
		dockState as DockState = tab.Content.DockHandler.DockState
		content as IDockContent = tab.Content
		
		path as GraphicsPath = GetTabOutline(tab, false, true)
		g.FillPath(BrushTabBackground, path)
		g.DrawPath(PenTabBorder, path)
		
		// Set no rotate for drawing icon and text
		matrixRotate as Matrix = g.Transform
		g.Transform = MatrixIdentity
		
		// Draw the icon
		rectImage as Rectangle = rectTabOrigin
		rectImage.X += ImageGapLeft
		rectImage.Y += ImageGapTop
		imageHeight as int = ((rectTabOrigin.Height - ImageGapTop) - ImageGapBottom)
		imageWidth as int = ImageWidth
		if imageHeight > ImageHeight:
			imageWidth = (ImageWidth * (imageHeight / ImageHeight))
		rectImage.Height = imageHeight
		rectImage.Width = imageWidth
		rectImage = GetTransformedRectangle(dockState, rectImage)
		g.DrawIcon(cast(Form, content).Icon, RtlTransform(rectImage, dockState))
		
		// Draw the text
		rectText as Rectangle = rectTabOrigin
		rectText.X += (((ImageGapLeft + imageWidth) + ImageGapRight) + TextGapLeft)
		rectText.Width -= (((ImageGapLeft + imageWidth) + ImageGapRight) + TextGapLeft)
		rectText = RtlTransform(GetTransformedRectangle(dockState, rectText), dockState)
		if (dockState == DockState.DockLeftAutoHide) or (dockState == DockState.DockRightAutoHide):
			g.DrawString(content.DockHandler.TabText, TextFont, BrushTabText, rectText, StringFormatTabVertical)
		else:
			g.DrawString(content.DockHandler.TabText, TextFont, BrushTabText, rectText, StringFormatTabHorizontal)
		
		// Set rotate back
		g.Transform = matrixRotate

	
	private def GetLogicalTabStripRectangle(dockState as DockState) as Rectangle:
		return GetLogicalTabStripRectangle(dockState, false)

	
	private def GetLogicalTabStripRectangle(dockState as DockState, transformed as bool) as Rectangle:
		if not DockHelper.IsDockStateAutoHide(dockState):
			return Rectangle.Empty
		
		leftPanes as int = GetPanes(DockState.DockLeftAutoHide).Count
		rightPanes as int = GetPanes(DockState.DockRightAutoHide).Count
		topPanes as int = GetPanes(DockState.DockTopAutoHide).Count
		bottomPanes as int = GetPanes(DockState.DockBottomAutoHide).Count
		
		x as int
		y as int
		width as int
		height as int
		
		height = MeasureHeight()
		if (dockState == DockState.DockLeftAutoHide) and (leftPanes > 0):
			x = 0
			y = (0 if (topPanes == 0) else height)
			width = ((Height - (0 if (topPanes == 0) else height)) - (0 if (bottomPanes == 0) else height))
		elif (dockState == DockState.DockRightAutoHide) and (rightPanes > 0):
			x = (Width - height)
			if (leftPanes != 0) and (x < height):
				x = height
			y = (0 if (topPanes == 0) else height)
			width = ((Height - (0 if (topPanes == 0) else height)) - (0 if (bottomPanes == 0) else height))
		elif (dockState == DockState.DockTopAutoHide) and (topPanes > 0):
			x = (0 if (leftPanes == 0) else height)
			y = 0
			width = ((Width - (0 if (leftPanes == 0) else height)) - (0 if (rightPanes == 0) else height))
		elif (dockState == DockState.DockBottomAutoHide) and (bottomPanes > 0):
			x = (0 if (leftPanes == 0) else height)
			y = (Height - height)
			if (topPanes != 0) and (y < height):
				y = height
			width = ((Width - (0 if (leftPanes == 0) else height)) - (0 if (rightPanes == 0) else height))
		else:
			return Rectangle.Empty
		
		if not transformed:
			return Rectangle(x, y, width, height)
		else:
			return GetTransformedRectangle(dockState, Rectangle(x, y, width, height))

	
	private def GetTabRectangle(tab as TabVS2005) as Rectangle:
		return GetTabRectangle(tab, false)

	
	private def GetTabRectangle(tab as TabVS2005, transformed as bool) as Rectangle:
		dockState as DockState = tab.Content.DockHandler.DockState
		rectTabStrip as Rectangle = GetLogicalTabStripRectangle(dockState)
		
		if rectTabStrip.IsEmpty:
			return Rectangle.Empty
		
		x as int = tab.TabX
		y as int = (rectTabStrip.Y + (0 if ((dockState == DockState.DockTopAutoHide) or (dockState == DockState.DockRightAutoHide)) else TabGapTop))
		width as int = tab.TabWidth
		height as int = (rectTabStrip.Height - TabGapTop)
		
		if not transformed:
			return Rectangle(x, y, width, height)
		else:
			return GetTransformedRectangle(dockState, Rectangle(x, y, width, height))

	
	private def GetTransformedRectangle(dockState as DockState, rect as Rectangle) as Rectangle:
		if (dockState != DockState.DockLeftAutoHide) and (dockState != DockState.DockRightAutoHide):
			return rect
		
		pts as (PointF) = array(PointF, 1)
		// the center of the rectangle
		pts[0].X = (cast(single, rect.X) + (cast(single, rect.Width) / 2))
		pts[0].Y = (cast(single, rect.Y) + (cast(single, rect.Height) / 2))
		rectTabStrip as Rectangle = GetLogicalTabStripRectangle(dockState)
		matrix = Matrix()
		matrix.RotateAt(90, PointF((cast(single, rectTabStrip.X) + (cast(single, rectTabStrip.Height) / 2)), (cast(single, rectTabStrip.Y) + (cast(single, rectTabStrip.Height) / 2))))
		matrix.TransformPoints(pts)
		
		return Rectangle(cast(int, ((pts[0].X - (cast(single, rect.Height) / 2)) + 0.5F)), cast(int, ((pts[0].Y - (cast(single, rect.Width) / 2)) + 0.5F)), rect.Height, rect.Width)

	
	protected override def HitTest(ptMouse as Point) as IDockContent:
		for state as DockState in DockStates:
			rectTabStrip as Rectangle = GetLogicalTabStripRectangle(state, true)
			if not rectTabStrip.Contains(ptMouse):
				continue 
			
			for pane as Pane in GetPanes(state):
				dockState as DockState = pane.DockPane.DockState
				for tab as TabVS2005 in pane.AutoHideTabs:
					path as GraphicsPath = GetTabOutline(tab, true, true)
					if path.IsVisible(ptMouse):
						return tab.Content
		
		return null

	
	protected override def MeasureHeight() as int:
		return (Math.Max(((ImageGapBottom + ImageGapTop) + ImageHeight), TextFont.Height) + TabGapTop)

	
	protected override def OnRefreshChanges():
		CalculateTabs()
		Invalidate()

	
	protected override def CreateTab(content as IDockContent) as AutoHideStripBase.Tab:
		return TabVS2005(content)

