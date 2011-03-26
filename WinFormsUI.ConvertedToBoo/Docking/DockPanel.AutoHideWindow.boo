
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms
import System.Drawing
import System.Runtime.InteropServices

partial internal class DockPanel:

	private class AutoHideWindowControl(Panel, ISplitterDragSource):

		private class SplitterControl(SplitterBase):

			public def constructor(autoHideWindow as AutoHideWindowControl):
				m_autoHideWindow = autoHideWindow

			
			private m_autoHideWindow as AutoHideWindowControl

			private AutoHideWindow as AutoHideWindowControl:
				get:
					return m_autoHideWindow

			
			protected override SplitterSize as int:
				get:
					return Measures.SplitterSize

			
			protected override def StartDrag():
				AutoHideWindow.DockPanel.BeginDrag(AutoHideWindow, AutoHideWindow.RectangleToScreen(Bounds))

		
		#region consts
		private static final ANIMATE_TIME = 100

		// in mini-seconds
		#endregion
		private m_timerMouseTrack as Timer

		private m_splitter as SplitterControl

		
		public def constructor(dockPanel as DockPanel):
			m_dockPanel = dockPanel
			
			m_timerMouseTrack = Timer()
			m_timerMouseTrack.Tick += TimerMouseTrack_Tick
			
			Visible = false
			m_splitter = SplitterControl(self)
			Controls.Add(m_splitter)

		
		protected override def Dispose(disposing as bool):
			if disposing:
				m_timerMouseTrack.Dispose()
			super.Dispose(disposing)

		
		private m_dockPanel as DockPanel = null

		public DockPanel as DockPanel:
			get:
				return m_dockPanel

		
		private m_activePane as DockPane = null

		public ActivePane as DockPane:
			get:
				return m_activePane

		private def SetActivePane():
			value as DockPane = (null if (ActiveContent is null) else ActiveContent.DockHandler.Pane)
			
			if value == m_activePane:
				return 
			
			m_activePane = value

		
		private m_activeContent as IDockContent = null

		public ActiveContent as IDockContent:
			get:
				return m_activeContent
			set:
				if value == m_activeContent:
					return 
				
				if value is not null:
					if (not DockHelper.IsDockStateAutoHide(value.DockHandler.DockState)) or (value.DockHandler.DockPanel != DockPanel):
						raise InvalidOperationException(Strings.DockPanel_ActiveAutoHideContent_InvalidValue)
				
				DockPanel.SuspendLayout()
				
				if m_activeContent is not null:
					if m_activeContent.DockHandler.Form.ContainsFocus:
						DockPanel.ContentFocusManager.GiveUpFocus(m_activeContent)
					AnimateWindow(false)
				
				m_activeContent = value
				SetActivePane()
				if ActivePane is not null:
					ActivePane.ActiveContent = m_activeContent
				
				if m_activeContent is not null:
					AnimateWindow(true)
				
				DockPanel.ResumeLayout()
				DockPanel.RefreshAutoHideStrip()
				
				SetTimerMouseTrack()

		
		public DockState as DockState:
			get:
				return (DockState.Unknown if (ActiveContent is null) else ActiveContent.DockHandler.DockState)

		
		private m_flagAnimate = true

		private FlagAnimate as bool:
			get:
				return m_flagAnimate
			set:
				m_flagAnimate = value

		
		private m_flagDragging = false

		internal FlagDragging as bool:
			get:
				return m_flagDragging
			set:
				if m_flagDragging == value:
					return 
				
				m_flagDragging = value
				SetTimerMouseTrack()

		
		private def AnimateWindow(show as bool):
			if (not FlagAnimate) and (Visible != show):
				Visible = show
				return 
			
			Parent.SuspendLayout()
			
			rectSource as Rectangle = GetRectangle(not show)
			rectTarget as Rectangle = GetRectangle(show)
			dxLoc as int
			dyLoc as int
			dWidth as int
			dHeight as int
			dxLoc = (dyLoc = (dWidth = (dHeight = 0)))
			if DockState == DockState.DockTopAutoHide:
				dHeight = (1 if show else (-1))
			elif DockState == DockState.DockLeftAutoHide:
				dWidth = (1 if show else (-1))
			elif DockState == DockState.DockRightAutoHide:
				dxLoc = ((-1) if show else 1)
				dWidth = (1 if show else (-1))
			elif DockState == DockState.DockBottomAutoHide:
				dyLoc = ((-1) if show else 1)
				dHeight = (1 if show else (-1))
			
			if show:
				Bounds = DockPanel.GetAutoHideWindowBounds(Rectangle(-rectTarget.Width, -rectTarget.Height, rectTarget.Width, rectTarget.Height))
				if Visible == false:
					Visible = true
				PerformLayout()
			
			SuspendLayout()
			
			LayoutAnimateWindow(rectSource)
			if Visible == false:
				Visible = true
			
			speedFactor = 1
			totalPixels as int = (Math.Abs((rectSource.Width - rectTarget.Width)) if (rectSource.Width != rectTarget.Width) else Math.Abs((rectSource.Height - rectTarget.Height)))
			remainPixels as int = totalPixels
			startingTime as DateTime = DateTime.Now
			while rectSource != rectTarget:
				startPerMove as DateTime = DateTime.Now
				
				rectSource.X += (dxLoc * speedFactor)
				rectSource.Y += (dyLoc * speedFactor)
				rectSource.Width += (dWidth * speedFactor)
				rectSource.Height += (dHeight * speedFactor)
				if Math.Sign((rectTarget.X - rectSource.X)) != Math.Sign(dxLoc):
					rectSource.X = rectTarget.X
				if Math.Sign((rectTarget.Y - rectSource.Y)) != Math.Sign(dyLoc):
					rectSource.Y = rectTarget.Y
				if Math.Sign((rectTarget.Width - rectSource.Width)) != Math.Sign(dWidth):
					rectSource.Width = rectTarget.Width
				if Math.Sign((rectTarget.Height - rectSource.Height)) != Math.Sign(dHeight):
					rectSource.Height = rectTarget.Height
				
				LayoutAnimateWindow(rectSource)
				if Parent is not null:
					Parent.Update()
				
				remainPixels -= speedFactor
				
				while true:
					time = TimeSpan(0, 0, 0, 0, ANIMATE_TIME)
					elapsedPerMove as TimeSpan = (DateTime.Now - startPerMove)
					elapsedTime as TimeSpan = (DateTime.Now - startingTime)
					if cast(int, (time - elapsedTime).TotalMilliseconds) <= 0:
						speedFactor = remainPixels
						break 
					else:
						speedFactor = ((remainPixels * cast(int, elapsedPerMove.TotalMilliseconds)) / cast(int, (time - elapsedTime).TotalMilliseconds))
					if speedFactor >= 1:
						break 
			ResumeLayout()
			Parent.ResumeLayout()

		
		private def LayoutAnimateWindow(rect as Rectangle):
			Bounds = DockPanel.GetAutoHideWindowBounds(rect)
			
			rectClient as Rectangle = ClientRectangle
			
			if DockState == DockState.DockLeftAutoHide:
				ActivePane.Location = Point((((rectClient.Right - 2) - Measures.SplitterSize) - ActivePane.Width), ActivePane.Location.Y)
			elif DockState == DockState.DockTopAutoHide:
				ActivePane.Location = Point(ActivePane.Location.X, (((rectClient.Bottom - 2) - Measures.SplitterSize) - ActivePane.Height))

		
		private def GetRectangle(show as bool) as Rectangle:
			if DockState == DockState.Unknown:
				return Rectangle.Empty
			
			rect as Rectangle = DockPanel.AutoHideWindowRectangle
			
			if show:
				return rect
			
			if DockState == DockState.DockLeftAutoHide:
				rect.Width = 0
			elif DockState == DockState.DockRightAutoHide:
				rect.X += rect.Width
				rect.Width = 0
			elif DockState == DockState.DockTopAutoHide:
				rect.Height = 0
			else:
				rect.Y += rect.Height
				rect.Height = 0
			
			return rect

		
		private def SetTimerMouseTrack():
			if ((ActivePane is null) or ActivePane.IsActivated) or FlagDragging:
				m_timerMouseTrack.Enabled = false
				return 
			
			// start the timer
			hovertime as int = SystemInformation.MouseHoverTime
			
			// assign a default value 400 in case of setting Timer.Interval invalid value exception
			if hovertime <= 0:
				hovertime = 400
			
			m_timerMouseTrack.Interval = (2 * cast(int, hovertime))
			m_timerMouseTrack.Enabled = true

		
		protected virtual DisplayingRectangle as Rectangle:
			get:
				rect as Rectangle = ClientRectangle
				
				// exclude the border and the splitter
				if DockState == DockState.DockBottomAutoHide:
					rect.Y += (2 + Measures.SplitterSize)
					rect.Height -= (2 + Measures.SplitterSize)
				elif DockState == DockState.DockRightAutoHide:
					rect.X += (2 + Measures.SplitterSize)
					rect.Width -= (2 + Measures.SplitterSize)
				elif DockState == DockState.DockTopAutoHide:
					rect.Height -= (2 + Measures.SplitterSize)
				elif DockState == DockState.DockLeftAutoHide:
					rect.Width -= (2 + Measures.SplitterSize)
				
				return rect

		
		protected override def OnLayout(levent as LayoutEventArgs):
			DockPadding.All = 0
			if DockState == DockState.DockLeftAutoHide:
				DockPadding.Right = 2
				m_splitter.Dock = DockStyle.Right
			elif DockState == DockState.DockRightAutoHide:
				DockPadding.Left = 2
				m_splitter.Dock = DockStyle.Left
			elif DockState == DockState.DockTopAutoHide:
				DockPadding.Bottom = 2
				m_splitter.Dock = DockStyle.Bottom
			elif DockState == DockState.DockBottomAutoHide:
				DockPadding.Top = 2
				m_splitter.Dock = DockStyle.Top
			
			rectDisplaying as Rectangle = DisplayingRectangle
			rectHidden = Rectangle(-rectDisplaying.Width, rectDisplaying.Y, rectDisplaying.Width, rectDisplaying.Height)
			for c as Control in Controls:
				pane = (c as DockPane)
				if pane is null:
					continue 
				
				
				if pane == ActivePane:
					pane.Bounds = rectDisplaying
				else:
					pane.Bounds = rectHidden
			
			super.OnLayout(levent)

		
		protected override def OnPaint(e as PaintEventArgs):
			// Draw the border
			g as Graphics = e.Graphics
			
			if DockState == DockState.DockBottomAutoHide:
				g.DrawLine(SystemPens.ControlLightLight, 0, 1, ClientRectangle.Right, 1)
			elif DockState == DockState.DockRightAutoHide:
				g.DrawLine(SystemPens.ControlLightLight, 1, 0, 1, ClientRectangle.Bottom)
			elif DockState == DockState.DockTopAutoHide:
				g.DrawLine(SystemPens.ControlDark, 0, (ClientRectangle.Height - 2), ClientRectangle.Right, (ClientRectangle.Height - 2))
				g.DrawLine(SystemPens.ControlDarkDark, 0, (ClientRectangle.Height - 1), ClientRectangle.Right, (ClientRectangle.Height - 1))
			elif DockState == DockState.DockLeftAutoHide:
				g.DrawLine(SystemPens.ControlDark, (ClientRectangle.Width - 2), 0, (ClientRectangle.Width - 2), ClientRectangle.Bottom)
				g.DrawLine(SystemPens.ControlDarkDark, (ClientRectangle.Width - 1), 0, (ClientRectangle.Width - 1), ClientRectangle.Bottom)
			
			super.OnPaint(e)

		
		public def RefreshActiveContent():
			if ActiveContent is null:
				return 
			
			if not DockHelper.IsDockStateAutoHide(ActiveContent.DockHandler.DockState):
				FlagAnimate = false
				ActiveContent = null
				FlagAnimate = true

		
		public def RefreshActivePane():
			SetTimerMouseTrack()

		
		private def TimerMouseTrack_Tick(sender as object, e as EventArgs):
			if IsDisposed:
				return 
			
			if (ActivePane is null) or ActivePane.IsActivated:
				m_timerMouseTrack.Enabled = false
				return 
			
			pane as DockPane = ActivePane
			ptMouseInAutoHideWindow as Point = PointToClient(Control.MousePosition)
			ptMouseInDockPanel as Point = DockPanel.PointToClient(Control.MousePosition)
			
			rectTabStrip as Rectangle = DockPanel.GetTabStripRectangle(pane.DockState)
			
			if (not ClientRectangle.Contains(ptMouseInAutoHideWindow)) and (not rectTabStrip.Contains(ptMouseInDockPanel)):
				ActiveContent = null
				m_timerMouseTrack.Enabled = false

		
		#region ISplitterDragSource Members
		
		def ISplitterDragSource.BeginDrag(rectSplitter as Rectangle):
			FlagDragging = true

		
		def ISplitterDragSource.EndDrag():
			FlagDragging = false

		
		ISplitterDragSource.IsVertical as bool:
			get:
				return ((DockState == DockState.DockLeftAutoHide) or (DockState == DockState.DockRightAutoHide))

		
		ISplitterDragSource.DragLimitBounds as Rectangle:
			get:
				rectLimit as Rectangle = DockPanel.DockArea
				
				if (self as ISplitterDragSource).IsVertical:
					rectLimit.X += MeasurePane.MinSize
					rectLimit.Width -= (2 * MeasurePane.MinSize)
				else:
					rectLimit.Y += MeasurePane.MinSize
					rectLimit.Height -= (2 * MeasurePane.MinSize)
				
				return DockPanel.RectangleToScreen(rectLimit)

		
		def ISplitterDragSource.MoveSplitter(offset as int):
			rectDockArea as Rectangle = DockPanel.DockArea
			content as IDockContent = ActiveContent
			if (DockState == DockState.DockLeftAutoHide) and (rectDockArea.Width > 0):
				if content.DockHandler.AutoHidePortion < 1:
					content.DockHandler.AutoHidePortion += (cast(double, offset) / cast(double, rectDockArea.Width))
				else:
					content.DockHandler.AutoHidePortion = (Width + offset)
			elif (DockState == DockState.DockRightAutoHide) and (rectDockArea.Width > 0):
				if content.DockHandler.AutoHidePortion < 1:
					content.DockHandler.AutoHidePortion -= (cast(double, offset) / cast(double, rectDockArea.Width))
				else:
					content.DockHandler.AutoHidePortion = (Width - offset)
			elif (DockState == DockState.DockBottomAutoHide) and (rectDockArea.Height > 0):
				if content.DockHandler.AutoHidePortion < 1:
					content.DockHandler.AutoHidePortion -= (cast(double, offset) / cast(double, rectDockArea.Height))
				else:
					content.DockHandler.AutoHidePortion = (Height - offset)
			elif (DockState == DockState.DockTopAutoHide) and (rectDockArea.Height > 0):
				if content.DockHandler.AutoHidePortion < 1:
					content.DockHandler.AutoHidePortion += (cast(double, offset) / cast(double, rectDockArea.Height))
				else:
					content.DockHandler.AutoHidePortion = (Height + offset)

		
		#region IDragSource Members
		
		IDragSource.DragControl as Control:
			get:
				return self
		
		#endregion
		
		#endregion

	
	private AutoHideWindow as AutoHideWindowControl:
		get:
			return m_autoHideWindow

	
	internal AutoHideControl as Control:
		get:
			return m_autoHideWindow

	
	internal def RefreshActiveAutoHideContent():
		AutoHideWindow.RefreshActiveContent()

	
	internal AutoHideWindowRectangle as Rectangle:
		get:
			state as DockState = AutoHideWindow.DockState
			rectDockArea as Rectangle = DockArea
			if ActiveAutoHideContent is null:
				return Rectangle.Empty
			
			if Parent is null:
				return Rectangle.Empty
			
			rect as Rectangle = Rectangle.Empty
			autoHideSize as double = ActiveAutoHideContent.DockHandler.AutoHidePortion
			if state == DockState.DockLeftAutoHide:
				if autoHideSize < 1:
					autoHideSize = (rectDockArea.Width * autoHideSize)
				if autoHideSize > (rectDockArea.Width - MeasurePane.MinSize):
					autoHideSize = (rectDockArea.Width - MeasurePane.MinSize)
				rect.X = rectDockArea.X
				rect.Y = rectDockArea.Y
				rect.Width = cast(int, autoHideSize)
				rect.Height = rectDockArea.Height
			elif state == DockState.DockRightAutoHide:
				if autoHideSize < 1:
					autoHideSize = (rectDockArea.Width * autoHideSize)
				if autoHideSize > (rectDockArea.Width - MeasurePane.MinSize):
					autoHideSize = (rectDockArea.Width - MeasurePane.MinSize)
				rect.X = ((rectDockArea.X + rectDockArea.Width) - cast(int, autoHideSize))
				rect.Y = rectDockArea.Y
				rect.Width = cast(int, autoHideSize)
				rect.Height = rectDockArea.Height
			elif state == DockState.DockTopAutoHide:
				if autoHideSize < 1:
					autoHideSize = (rectDockArea.Height * autoHideSize)
				if autoHideSize > (rectDockArea.Height - MeasurePane.MinSize):
					autoHideSize = (rectDockArea.Height - MeasurePane.MinSize)
				rect.X = rectDockArea.X
				rect.Y = rectDockArea.Y
				rect.Width = rectDockArea.Width
				rect.Height = cast(int, autoHideSize)
			elif state == DockState.DockBottomAutoHide:
				if autoHideSize < 1:
					autoHideSize = (rectDockArea.Height * autoHideSize)
				if autoHideSize > (rectDockArea.Height - MeasurePane.MinSize):
					autoHideSize = (rectDockArea.Height - MeasurePane.MinSize)
				rect.X = rectDockArea.X
				rect.Y = ((rectDockArea.Y + rectDockArea.Height) - cast(int, autoHideSize))
				rect.Width = rectDockArea.Width
				rect.Height = cast(int, autoHideSize)
			
			return rect

	
	internal def GetAutoHideWindowBounds(rectAutoHideWindow as Rectangle) as Rectangle:
		if (DocumentStyle == DocumentStyle.SystemMdi) or (DocumentStyle == DocumentStyle.DockingMdi):
			return (Rectangle.Empty if (Parent is null) else Parent.RectangleToClient(RectangleToScreen(rectAutoHideWindow)))
		else:
			return rectAutoHideWindow

	
	internal def RefreshAutoHideStrip():
		AutoHideStripControl.RefreshChanges()
	

