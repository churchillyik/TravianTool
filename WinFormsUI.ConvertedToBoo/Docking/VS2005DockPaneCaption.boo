
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Drawing.Drawing2D
import System.Windows.Forms
import System.ComponentModel
import System.Windows.Forms.VisualStyles

internal class VS2005DockPaneCaption(DockPaneCaptionBase):

	private final class InertButton(InertButtonBase):

		private m_image as Bitmap

		private m_imageAutoHide as Bitmap

		
		public def constructor(dockPaneCaption as VS2005DockPaneCaption, image as Bitmap, imageAutoHide as Bitmap):
			super()
			m_dockPaneCaption = dockPaneCaption
			m_image = image
			m_imageAutoHide = imageAutoHide
			RefreshChanges()

		
		private m_dockPaneCaption as VS2005DockPaneCaption

		private DockPaneCaption as VS2005DockPaneCaption:
			get:
				return m_dockPaneCaption

		
		public IsAutoHide as bool:
			get:
				return DockPaneCaption.DockPane.IsAutoHide

		
		public override Image as Bitmap:
			get:
				return (m_imageAutoHide if IsAutoHide else m_image)

		
		protected override def OnRefreshChanges():
			if DockPaneCaption.TextColor != ForeColor:
				ForeColor = DockPaneCaption.TextColor
				Invalidate()

	
	#region consts
	private static final _TextGapTop = 2

	private static final _TextGapBottom = 0

	private static final _TextGapLeft = 3

	private static final _TextGapRight = 3

	private static final _ButtonGapTop = 2

	private static final _ButtonGapBottom = 1

	private static final _ButtonGapBetween = 1

	private static final _ButtonGapLeft = 1

	private static final _ButtonGapRight = 2

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
				m_buttonClose = InertButton(self, ImageButtonClose, ImageButtonClose)
				m_toolTip.SetToolTip(m_buttonClose, ToolTipClose)
				m_buttonClose.Click += Close_Click
				Controls.Add(m_buttonClose)
			
			return m_buttonClose

	
	private static _imageButtonAutoHide as Bitmap

	private static ImageButtonAutoHide as Bitmap:
		get:
			if _imageButtonAutoHide is null:
				_imageButtonAutoHide = Resources.DockPane_AutoHide
			
			return _imageButtonAutoHide

	
	private static _imageButtonDock as Bitmap

	private static ImageButtonDock as Bitmap:
		get:
			if _imageButtonDock is null:
				_imageButtonDock = Resources.DockPane_Dock
			
			return _imageButtonDock

	
	private m_buttonAutoHide as InertButton

	private ButtonAutoHide as InertButton:
		get:
			if m_buttonAutoHide is null:
				m_buttonAutoHide = InertButton(self, ImageButtonDock, ImageButtonAutoHide)
				m_toolTip.SetToolTip(m_buttonAutoHide, ToolTipAutoHide)
				m_buttonAutoHide.Click += AutoHide_Click
				Controls.Add(m_buttonAutoHide)
			
			return m_buttonAutoHide

	
	private static _imageButtonOptions as Bitmap

	private static ImageButtonOptions as Bitmap:
		get:
			if _imageButtonOptions is null:
				_imageButtonOptions = Resources.DockPane_Option
			
			return _imageButtonOptions

	
	private m_buttonOptions as InertButton

	private ButtonOptions as InertButton:
		get:
			if m_buttonOptions is null:
				m_buttonOptions = InertButton(self, ImageButtonOptions, ImageButtonOptions)
				m_toolTip.SetToolTip(m_buttonOptions, ToolTipOptions)
				m_buttonOptions.Click += Options_Click
				Controls.Add(m_buttonOptions)
			return m_buttonOptions

	
	private m_components as IContainer

	private Components as IContainer:
		get:
			return m_components

	
	private m_toolTip as ToolTip

	
	public def constructor(pane as DockPane):
		super(pane)
		SuspendLayout()
		
		m_components = Container()
		m_toolTip = ToolTip(Components)
		
		ResumeLayout()

	
	protected override def Dispose(disposing as bool):
		if disposing:
			Components.Dispose()
		super.Dispose(disposing)

	
	private static TextGapTop as int:
		get:
			return _TextGapTop

	
	private static TextFont as Font:
		get:
			return SystemInformation.MenuFont

	
	private static TextGapBottom as int:
		get:
			return _TextGapBottom

	
	private static TextGapLeft as int:
		get:
			return _TextGapLeft

	
	private static TextGapRight as int:
		get:
			return _TextGapRight

	
	private static ButtonGapTop as int:
		get:
			return _ButtonGapTop

	
	private static ButtonGapBottom as int:
		get:
			return _ButtonGapBottom

	
	private static ButtonGapLeft as int:
		get:
			return _ButtonGapLeft

	
	private static ButtonGapRight as int:
		get:
			return _ButtonGapRight

	
	private static ButtonGapBetween as int:
		get:
			return _ButtonGapBetween

	
	private static _toolTipClose as string

	private static ToolTipClose as string:
		get:
			if _toolTipClose is null:
				_toolTipClose = Strings.DockPaneCaption_ToolTipClose
			return _toolTipClose

	
	private static _toolTipOptions as string

	private static ToolTipOptions as string:
		get:
			if _toolTipOptions is null:
				_toolTipOptions = Strings.DockPaneCaption_ToolTipOptions
			
			return _toolTipOptions

	
	private static _toolTipAutoHide as string

	private static ToolTipAutoHide as string:
		get:
			if _toolTipAutoHide is null:
				_toolTipAutoHide = Strings.DockPaneCaption_ToolTipAutoHide
			return _toolTipAutoHide

	
	private static _activeBackColorGradientBlend as Blend

	private static ActiveBackColorGradientBlend as Blend:
		get:
			if _activeBackColorGradientBlend is null:
				blend = Blend(2)
				
				blend.Factors = (of single: 0.5F, 1.0F)
				blend.Positions = (of single: 0.0F, 1.0F)
				_activeBackColorGradientBlend = blend
			
			return _activeBackColorGradientBlend

	
	private static ActiveBackColorGradientBegin as Color:
		get:
			return SystemColors.GradientActiveCaption

	
	private static ActiveBackColorGradientEnd as Color:
		get:
			return SystemColors.ActiveCaption

	
	private static InactiveBackColor as Color:
		get:
			colorScheme as string = VisualStyleInformation.ColorScheme
			
			if (colorScheme == 'HomeStead') or (colorScheme == 'Metallic'):
				return SystemColors.GradientInactiveCaption
			else:
				return SystemColors.GrayText

	
	private static ActiveTextColor as Color:
		get:
			return SystemColors.ActiveCaptionText

	
	private static InactiveTextColor as Color:
		get:
			return SystemColors.ControlText

	
	private TextColor as Color:
		get:
			return (ActiveTextColor if DockPane.IsActivated else InactiveTextColor)

	
	private static _textFormat as TextFormatFlags = ((TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis) | TextFormatFlags.VerticalCenter)

	private TextFormat as TextFormatFlags:
		get:
			if RightToLeft == RightToLeft.No:
				return _textFormat
			else:
				return ((_textFormat | TextFormatFlags.RightToLeft) | TextFormatFlags.Right)

	
	protected override def MeasureHeight() as int:
		height as int = ((TextFont.Height + TextGapTop) + TextGapBottom)
		
		if height < ((ButtonClose.Image.Height + ButtonGapTop) + ButtonGapBottom):
			height = ((ButtonClose.Image.Height + ButtonGapTop) + ButtonGapBottom)
		
		return height

	
	protected override def OnPaint(e as PaintEventArgs):
		super.OnPaint(e)
		DrawCaption(e.Graphics)

	
	private def DrawCaption(g as Graphics):
		if (ClientRectangle.Width == 0) or (ClientRectangle.Height == 0):
			return 
		
		if DockPane.IsActivated:
			using brush = LinearGradientBrush(ClientRectangle, ActiveBackColorGradientBegin, ActiveBackColorGradientEnd, LinearGradientMode.Vertical):
				brush.Blend = ActiveBackColorGradientBlend
				g.FillRectangle(brush, ClientRectangle)
		else:
			using brush = SolidBrush(InactiveBackColor):
				g.FillRectangle(brush, ClientRectangle)
		
		rectCaption as Rectangle = ClientRectangle
		
		rectCaptionText as Rectangle = rectCaption
		rectCaptionText.X += TextGapLeft
		rectCaptionText.Width -= (TextGapLeft + TextGapRight)
		rectCaptionText.Width -= ((ButtonGapLeft + ButtonClose.Width) + ButtonGapRight)
		if ShouldShowAutoHideButton:
			rectCaptionText.Width -= (ButtonAutoHide.Width + ButtonGapBetween)
		if HasTabPageContextMenu:
			rectCaptionText.Width -= (ButtonOptions.Width + ButtonGapBetween)
		rectCaptionText.Y += TextGapTop
		rectCaptionText.Height -= (TextGapTop + TextGapBottom)
		TextRenderer.DrawText(g, DockPane.CaptionText, TextFont, DrawHelper.RtlTransform(self, rectCaptionText), TextColor, TextFormat)

	
	protected override def OnLayout(levent as LayoutEventArgs):
		SetButtonsPosition()
		super.OnLayout(levent)

	
	protected override def OnRefreshChanges():
		SetButtons()
		Invalidate()

	
	private CloseButtonEnabled as bool:
		get:
			return (DockPane.ActiveContent.DockHandler.CloseButton if (DockPane.ActiveContent is not null) else false)

	
	private ShouldShowAutoHideButton as bool:
		get:
			return (not DockPane.IsFloat)

	
	private def SetButtons():
		ButtonClose.Enabled = CloseButtonEnabled
		ButtonAutoHide.Visible = ShouldShowAutoHideButton
		ButtonOptions.Visible = HasTabPageContextMenu
		ButtonClose.RefreshChanges()
		ButtonAutoHide.RefreshChanges()
		ButtonOptions.RefreshChanges()
		
		SetButtonsPosition()

	
	private def SetButtonsPosition():
		// set the size and location for close and auto-hide buttons
		rectCaption as Rectangle = ClientRectangle
		buttonWidth as int = ButtonClose.Image.Width
		buttonHeight as int = ButtonClose.Image.Height
		height as int = ((rectCaption.Height - ButtonGapTop) - ButtonGapBottom)
		if buttonHeight < height:
			buttonWidth = (buttonWidth * (height / buttonHeight))
			buttonHeight = height
		buttonSize = Size(buttonWidth, buttonHeight)
		x as int = ((((rectCaption.X + rectCaption.Width) - 1) - ButtonGapRight) - m_buttonClose.Width)
		y as int = (rectCaption.Y + ButtonGapTop)
		point = Point(x, y)
		ButtonClose.Bounds = DrawHelper.RtlTransform(self, Rectangle(point, buttonSize))
		point.Offset(-(buttonWidth + ButtonGapBetween), 0)
		ButtonAutoHide.Bounds = DrawHelper.RtlTransform(self, Rectangle(point, buttonSize))
		if ShouldShowAutoHideButton:
			point.Offset(-(buttonWidth + ButtonGapBetween), 0)
		ButtonOptions.Bounds = DrawHelper.RtlTransform(self, Rectangle(point, buttonSize))

	
	private def Close_Click(sender as object, e as EventArgs):
		DockPane.CloseActiveContent()

	
	private def AutoHide_Click(sender as object, e as EventArgs):
		DockPane.DockState = DockHelper.ToggleAutoHideState(DockPane.DockState)

	
	private def Options_Click(sender as object, e as EventArgs):
		ShowTabPageContextMenu(PointToClient(Control.MousePosition))

	
	protected override def OnRightToLeftChanged(e as EventArgs):
		super.OnRightToLeftChanged(e)
		PerformLayout()

