
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Text
import System.Windows.Forms
import System.Drawing
import System.Drawing.Drawing2D
import System.ComponentModel

partial internal class DockPanel:

	private final class DockDragHandler(DragHandler):

		private class DockIndicator(DragForm):

			#region IHitTest
			private interface IHitTest:

				def HitTest(pt as Point) as DockStyle

				Status as DockStyle:
					get
					set

			#endregion
			
			#region PanelIndicator
			private class PanelIndicator(PictureBox, IHitTest):

				private static _imagePanelLeft as Image = Resources.DockIndicator_PanelLeft

				private static _imagePanelRight as Image = Resources.DockIndicator_PanelRight

				private static _imagePanelTop as Image = Resources.DockIndicator_PanelTop

				private static _imagePanelBottom as Image = Resources.DockIndicator_PanelBottom

				private static _imagePanelFill as Image = Resources.DockIndicator_PanelFill

				private static _imagePanelLeftActive as Image = Resources.DockIndicator_PanelLeft_Active

				private static _imagePanelRightActive as Image = Resources.DockIndicator_PanelRight_Active

				private static _imagePanelTopActive as Image = Resources.DockIndicator_PanelTop_Active

				private static _imagePanelBottomActive as Image = Resources.DockIndicator_PanelBottom_Active

				private static _imagePanelFillActive as Image = Resources.DockIndicator_PanelFill_Active

				
				public def constructor(dockStyle as DockStyle):
					m_dockStyle = dockStyle
					SizeMode = PictureBoxSizeMode.AutoSize
					Image = ImageInactive

				
				private m_dockStyle as DockStyle

				private DockStyle as DockStyle:
					get:
						return m_dockStyle

				
				private m_status as DockStyle

				public Status as DockStyle:
					get:
						return m_status
					set:
						if (value != DockStyle) and (value != DockStyle.None):
							raise InvalidEnumArgumentException()
						
						if m_status == value:
							return 
						
						m_status = value
						IsActivated = (m_status != DockStyle.None)

				
				private ImageInactive as Image:
					get:
						if DockStyle == DockStyle.Left:
							return _imagePanelLeft
						elif DockStyle == DockStyle.Right:
							return _imagePanelRight
						elif DockStyle == DockStyle.Top:
							return _imagePanelTop
						elif DockStyle == DockStyle.Bottom:
							return _imagePanelBottom
						elif DockStyle == DockStyle.Fill:
							return _imagePanelFill
						else:
							return null

				
				private ImageActive as Image:
					get:
						if DockStyle == DockStyle.Left:
							return _imagePanelLeftActive
						elif DockStyle == DockStyle.Right:
							return _imagePanelRightActive
						elif DockStyle == DockStyle.Top:
							return _imagePanelTopActive
						elif DockStyle == DockStyle.Bottom:
							return _imagePanelBottomActive
						elif DockStyle == DockStyle.Fill:
							return _imagePanelFillActive
						else:
							return null

				
				private m_isActivated = false

				private IsActivated as bool:
					get:
						return m_isActivated
					set:
						m_isActivated = value
						Image = (ImageActive if IsActivated else ImageInactive)

				
				public def HitTest(pt as Point) as DockStyle:
					return (DockStyle if (self.Visible and ClientRectangle.Contains(PointToClient(pt))) else DockStyle.None)

			#endregion PanelIndicator
			
			#region PaneIndicator
			private class PaneIndicator(PictureBox, IHitTest):

				private struct HotSpotIndex:

					public def constructor(x as int, y as int, dockStyle as DockStyle):
						m_x = x
						m_y = y
						m_dockStyle = dockStyle

					
					private m_x as int

					public X as int:
						get:
							return m_x

					
					private m_y as int

					public Y as int:
						get:
							return m_y

					
					private m_dockStyle as DockStyle

					public DockStyle as DockStyle:
						get:
							return m_dockStyle

				
				private static _bitmapPaneDiamond as Bitmap = Resources.DockIndicator_PaneDiamond

				private static _bitmapPaneDiamondLeft as Bitmap = Resources.DockIndicator_PaneDiamond_Left

				private static _bitmapPaneDiamondRight as Bitmap = Resources.DockIndicator_PaneDiamond_Right

				private static _bitmapPaneDiamondTop as Bitmap = Resources.DockIndicator_PaneDiamond_Top

				private static _bitmapPaneDiamondBottom as Bitmap = Resources.DockIndicator_PaneDiamond_Bottom

				private static _bitmapPaneDiamondFill as Bitmap = Resources.DockIndicator_PaneDiamond_Fill

				private static _bitmapPaneDiamondHotSpot as Bitmap = Resources.DockIndicator_PaneDiamond_HotSpot

				private static _bitmapPaneDiamondHotSpotIndex as Bitmap = Resources.DockIndicator_PaneDiamond_HotSpotIndex

				private static _hotSpots as (HotSpotIndex) = (of HotSpotIndex: HotSpotIndex(1, 0, DockStyle.Top), HotSpotIndex(0, 1, DockStyle.Left), HotSpotIndex(1, 1, DockStyle.Fill), HotSpotIndex(2, 1, DockStyle.Right), HotSpotIndex(1, 2, DockStyle.Bottom))

				private static _displayingGraphicsPath as GraphicsPath = DrawHelper.CalculateGraphicsPathFromBitmap(_bitmapPaneDiamond)

				
				public def constructor():
					SizeMode = PictureBoxSizeMode.AutoSize
					Image = _bitmapPaneDiamond
					Region = Region(DisplayingGraphicsPath)

				
				public static DisplayingGraphicsPath as GraphicsPath:
					get:
						return _displayingGraphicsPath

				
				public def HitTest(pt as Point) as DockStyle:
					if not Visible:
						return DockStyle.None
					
					pt = PointToClient(pt)
					if not ClientRectangle.Contains(pt):
						return DockStyle.None
					for i in range(_hotSpots.GetLowerBound(0), (_hotSpots.GetUpperBound(0) + 1)):
					
						if _bitmapPaneDiamondHotSpot.GetPixel(pt.X, pt.Y) == _bitmapPaneDiamondHotSpotIndex.GetPixel(_hotSpots[i].X, _hotSpots[i].Y):
							return _hotSpots[i].DockStyle
					
					return DockStyle.None

				
				private m_status as DockStyle = DockStyle.None

				public Status as DockStyle:
					get:
						return m_status
					set:
						m_status = value
						if m_status == DockStyle.None:
							Image = _bitmapPaneDiamond
						elif m_status == DockStyle.Left:
							Image = _bitmapPaneDiamondLeft
						elif m_status == DockStyle.Right:
							Image = _bitmapPaneDiamondRight
						elif m_status == DockStyle.Top:
							Image = _bitmapPaneDiamondTop
						elif m_status == DockStyle.Bottom:
							Image = _bitmapPaneDiamondBottom
						elif m_status == DockStyle.Fill:
							Image = _bitmapPaneDiamondFill

			#endregion PaneIndicator
			
			#region consts
			private _PanelIndicatorMargin = 10

			#endregion
			
			private m_dragHandler as DockDragHandler

			
			public def constructor(dragHandler as DockDragHandler):
				m_dragHandler = dragHandler
				Controls.AddRange((of Control: PaneDiamond, PanelLeft, PanelRight, PanelTop, PanelBottom, PanelFill))
				Region = Region(Rectangle.Empty)

			
			private m_paneDiamond as PaneIndicator = null

			private PaneDiamond as PaneIndicator:
				get:
					if m_paneDiamond is null:
						m_paneDiamond = PaneIndicator()
					
					return m_paneDiamond

			
			private m_panelLeft as PanelIndicator = null

			private PanelLeft as PanelIndicator:
				get:
					if m_panelLeft is null:
						m_panelLeft = PanelIndicator(DockStyle.Left)
					
					return m_panelLeft

			
			private m_panelRight as PanelIndicator = null

			private PanelRight as PanelIndicator:
				get:
					if m_panelRight is null:
						m_panelRight = PanelIndicator(DockStyle.Right)
					
					return m_panelRight

			
			private m_panelTop as PanelIndicator = null

			private PanelTop as PanelIndicator:
				get:
					if m_panelTop is null:
						m_panelTop = PanelIndicator(DockStyle.Top)
					
					return m_panelTop

			
			private m_panelBottom as PanelIndicator = null

			private PanelBottom as PanelIndicator:
				get:
					if m_panelBottom is null:
						m_panelBottom = PanelIndicator(DockStyle.Bottom)
					
					return m_panelBottom

			
			private m_panelFill as PanelIndicator = null

			private PanelFill as PanelIndicator:
				get:
					if m_panelFill is null:
						m_panelFill = PanelIndicator(DockStyle.Fill)
					
					return m_panelFill

			
			private m_fullPanelEdge = false

			public FullPanelEdge as bool:
				get:
					return m_fullPanelEdge
				set:
					if m_fullPanelEdge == value:
						return 
					
					m_fullPanelEdge = value
					RefreshChanges()

			
			public DragHandler as DockDragHandler:
				get:
					return m_dragHandler

			
			public DockPanel as DockPanel:
				get:
					return DragHandler.DockPanel

			
			private m_dockPane as DockPane = null

			public DockPane as DockPane:
				get:
					return m_dockPane
				internal set:
					if m_dockPane == value:
						return 
					
					oldDisplayingPane as DockPane = DisplayingPane
					m_dockPane = value
					if oldDisplayingPane != DisplayingPane:
						RefreshChanges()

			
			private m_hitTest as IHitTest = null

			private HitTestResult as IHitTest:
				get:
					return m_hitTest
				set:
					if m_hitTest == value:
						return 
					
					if m_hitTest is not null:
						m_hitTest.Status = DockStyle.None
					
					m_hitTest = value

			
			private DisplayingPane as DockPane:
				get:
					return (DockPane if ShouldPaneDiamondVisible() else null)

			
			private def RefreshChanges():
				region = Region(Rectangle.Empty)
				rectDockArea as Rectangle = (DockPanel.DockArea if FullPanelEdge else DockPanel.DocumentWindowBounds)
				
				rectDockArea = RectangleToClient(DockPanel.RectangleToScreen(rectDockArea))
				if ShouldPanelIndicatorVisible(DockState.DockLeft):
					PanelLeft.Location = Point((rectDockArea.X + _PanelIndicatorMargin), (rectDockArea.Y + ((rectDockArea.Height - PanelRight.Height) / 2)))
					PanelLeft.Visible = true
					region.Union(PanelLeft.Bounds)
				else:
					PanelLeft.Visible = false
				
				if ShouldPanelIndicatorVisible(DockState.DockRight):
					PanelRight.Location = Point((((rectDockArea.X + rectDockArea.Width) - PanelRight.Width) - _PanelIndicatorMargin), (rectDockArea.Y + ((rectDockArea.Height - PanelRight.Height) / 2)))
					PanelRight.Visible = true
					region.Union(PanelRight.Bounds)
				else:
					PanelRight.Visible = false
				
				if ShouldPanelIndicatorVisible(DockState.DockTop):
					PanelTop.Location = Point((rectDockArea.X + ((rectDockArea.Width - PanelTop.Width) / 2)), (rectDockArea.Y + _PanelIndicatorMargin))
					PanelTop.Visible = true
					region.Union(PanelTop.Bounds)
				else:
					PanelTop.Visible = false
				
				if ShouldPanelIndicatorVisible(DockState.DockBottom):
					PanelBottom.Location = Point((rectDockArea.X + ((rectDockArea.Width - PanelBottom.Width) / 2)), (((rectDockArea.Y + rectDockArea.Height) - PanelBottom.Height) - _PanelIndicatorMargin))
					PanelBottom.Visible = true
					region.Union(PanelBottom.Bounds)
				else:
					PanelBottom.Visible = false
				
				if ShouldPanelIndicatorVisible(DockState.Document):
					rectDocumentWindow as Rectangle = RectangleToClient(DockPanel.RectangleToScreen(DockPanel.DocumentWindowBounds))
					PanelFill.Location = Point((rectDocumentWindow.X + ((rectDocumentWindow.Width - PanelFill.Width) / 2)), (rectDocumentWindow.Y + ((rectDocumentWindow.Height - PanelFill.Height) / 2)))
					PanelFill.Visible = true
					region.Union(PanelFill.Bounds)
				else:
					PanelFill.Visible = false
				
				if ShouldPaneDiamondVisible():
					rect as Rectangle = RectangleToClient(DockPane.RectangleToScreen(DockPane.ClientRectangle))
					PaneDiamond.Location = Point((rect.Left + ((rect.Width - PaneDiamond.Width) / 2)), (rect.Top + ((rect.Height - PaneDiamond.Height) / 2)))
					PaneDiamond.Visible = true
					using graphicsPath = (PaneIndicator.DisplayingGraphicsPath.Clone() as GraphicsPath):
						pts as (Point) = (of Point: Point(PaneDiamond.Left, PaneDiamond.Top), Point(PaneDiamond.Right, PaneDiamond.Top), Point(PaneDiamond.Left, PaneDiamond.Bottom))
						using matrix = Matrix(PaneDiamond.ClientRectangle, pts):
							graphicsPath.Transform(matrix)
						region.Union(graphicsPath)
				else:
					PaneDiamond.Visible = false
				
				Region = region

			
			private def ShouldPanelIndicatorVisible(dockState as DockState) as bool:
				if not Visible:
					return false
				
				if DockPanel.DockWindows[dockState].Visible:
					return false
				
				return DragHandler.DragSource.IsDockStateValid(dockState)

			
			private def ShouldPaneDiamondVisible() as bool:
				if DockPane is null:
					return false
				
				if not DockPanel.AllowEndUserNestedDocking:
					return false
				
				return DragHandler.DragSource.CanDockTo(DockPane)

			
			public override def Show(bActivate as bool):
				super.Show(bActivate)
				Bounds = SystemInformation.VirtualScreen
				RefreshChanges()

			
			public def TestDrop():
				pt as Point = Control.MousePosition
				DockPane = DockHelper.PaneAtPoint(pt, DockPanel)
				
				if TestDrop(PanelLeft, pt) != DockStyle.None:
					HitTestResult = PanelLeft
				elif TestDrop(PanelRight, pt) != DockStyle.None:
					HitTestResult = PanelRight
				elif TestDrop(PanelTop, pt) != DockStyle.None:
					HitTestResult = PanelTop
				elif TestDrop(PanelBottom, pt) != DockStyle.None:
					HitTestResult = PanelBottom
				elif TestDrop(PanelFill, pt) != DockStyle.None:
					HitTestResult = PanelFill
				elif TestDrop(PaneDiamond, pt) != DockStyle.None:
					HitTestResult = PaneDiamond
				else:
					HitTestResult = null
				
				if HitTestResult is not null:
					if HitTestResult isa PaneIndicator:
						DragHandler.Outline.Show(DockPane, HitTestResult.Status)
					else:
						DragHandler.Outline.Show(DockPanel, HitTestResult.Status, FullPanelEdge)

			
			private static def TestDrop(hitTest as IHitTest, pt as Point) as DockStyle:
				return (hitTest.Status = hitTest.HitTest(pt))

		
		private class DockOutline(DockOutlineBase):

			public def constructor():
				m_dragForm = DragForm()
				SetDragForm(Rectangle.Empty)
				DragForm.BackColor = SystemColors.ActiveCaption
				DragForm.Opacity = 0.5
				DragForm.Show(false)

			
			private m_dragForm as DragForm

			private DragForm as DragForm:
				get:
					return m_dragForm

			
			protected override def OnShow():
				CalculateRegion()

			
			protected override def OnClose():
				DragForm.Close()

			
			private def CalculateRegion():
				if SameAsOldValue:
					return 
				
				if not FloatWindowBounds.IsEmpty:
					SetOutline(FloatWindowBounds)
				elif DockTo isa DockPanel:
					SetOutline((DockTo as DockPanel), Dock, (ContentIndex != 0))
				elif DockTo isa DockPane:
					SetOutline((DockTo as DockPane), Dock, ContentIndex)
				else:
					SetOutline()

			
			private def SetOutline():
				SetDragForm(Rectangle.Empty)

			
			private def SetOutline(floatWindowBounds as Rectangle):
				SetDragForm(floatWindowBounds)

			
			private def SetOutline(dockPanel as DockPanel, dock as DockStyle, fullPanelEdge as bool):
				width as int
				height as int
				rect as Rectangle = (dockPanel.DockArea if fullPanelEdge else dockPanel.DocumentWindowBounds)
				rect.Location = dockPanel.PointToScreen(rect.Location)
				if dock == DockStyle.Top:
					height = dockPanel.GetDockWindowSize(DockState.DockTop)
					rect = Rectangle(rect.X, rect.Y, rect.Width, height)
				elif dock == DockStyle.Bottom:
					height = dockPanel.GetDockWindowSize(DockState.DockBottom)
					rect = Rectangle(rect.X, (rect.Bottom - height), rect.Width, height)
				elif dock == DockStyle.Left:
					width = dockPanel.GetDockWindowSize(DockState.DockLeft)
					rect = Rectangle(rect.X, rect.Y, width, rect.Height)
				elif dock == DockStyle.Right:
					width = dockPanel.GetDockWindowSize(DockState.DockRight)
					rect = Rectangle((rect.Right - width), rect.Y, width, rect.Height)
				elif dock == DockStyle.Fill:
					rect = dockPanel.DocumentWindowBounds
					rect.Location = dockPanel.PointToScreen(rect.Location)
				
				SetDragForm(rect)

			
			private def SetOutline(pane as DockPane, dock as DockStyle, contentIndex as int):
				rect as Rectangle
				if dock != DockStyle.Fill:
					rect = pane.DisplayingRectangle
					if dock == DockStyle.Right:
						rect.X += (rect.Width / 2)
					if dock == DockStyle.Bottom:
						rect.Y += (rect.Height / 2)
					if (dock == DockStyle.Left) or (dock == DockStyle.Right):
						rect.Width -= (rect.Width / 2)
					if (dock == DockStyle.Top) or (dock == DockStyle.Bottom):
						rect.Height -= (rect.Height / 2)
					rect.Location = pane.PointToScreen(rect.Location)
					
					SetDragForm(rect)
				elif contentIndex == (-1):
					rect = pane.DisplayingRectangle
					rect.Location = pane.PointToScreen(rect.Location)
					SetDragForm(rect)
				else:
					using path = pane.TabStripControl.GetOutline(contentIndex):
						rectF as RectangleF = path.GetBounds()
						rect = Rectangle(cast(int, rectF.X), cast(int, rectF.Y), cast(int, rectF.Width), cast(int, rectF.Height))
						using matrix = Matrix(rect, (of Point: Point(0, 0), Point(rect.Width, 0), Point(0, rect.Height))):
							path.Transform(matrix)
						region = Region(path)
						SetDragForm(rect, region)

			
			private def SetDragForm(rect as Rectangle):
				DragForm.Bounds = rect
				if rect == Rectangle.Empty:
					DragForm.Region = Region(Rectangle.Empty)
				elif DragForm.Region is not null:
					DragForm.Region = null

			
			private def SetDragForm(rect as Rectangle, region as Region):
				DragForm.Bounds = rect
				DragForm.Region = region

		
		public def constructor(panel as DockPanel):
			super(panel)

		
		public DragSource as IDockDragSource:
			get:
				return (super.DragSource as IDockDragSource)
			set:
				super.DragSource = value

		
		private m_outline as DockOutlineBase

		public Outline as DockOutlineBase:
			get:
				return m_outline
			private set:
				m_outline = value

		
		private m_indicator as DockIndicator

		private Indicator as DockIndicator:
			get:
				return m_indicator
			set:
				m_indicator = value

		
		private m_floatOutlineBounds as Rectangle

		private FloatOutlineBounds as Rectangle:
			get:
				return m_floatOutlineBounds
			set:
				m_floatOutlineBounds = value

		
		public def BeginDrag(dragSource as IDockDragSource):
			DragSource = dragSource
			
			if not BeginDrag():
				DragSource = null
				return 
			
			Outline = DockOutline()
			Indicator = DockIndicator(self)
			Indicator.Show(false)
			
			FloatOutlineBounds = DragSource.BeginDrag(StartMousePosition)

		
		protected override def OnDragging():
			TestDrop()

		
		protected override def OnEndDrag(abort as bool):
			DockPanel.SuspendLayout(true)
			
			Outline.Close()
			Indicator.Close()
			
			EndDrag(abort)
			
			DockPanel.ResumeLayout(true, true)
			
			DragSource = null

		
		private def TestDrop():
			Outline.FlagTestDrop = false
			
			Indicator.FullPanelEdge = ((Control.ModifierKeys & Keys.Shift) != 0)
			
			if (Control.ModifierKeys & Keys.Control) == 0:
				Indicator.TestDrop()
				
				if not Outline.FlagTestDrop:
					pane as DockPane = DockHelper.PaneAtPoint(Control.MousePosition, DockPanel)
					if (pane is not null) and DragSource.IsDockStateValid(pane.DockState):
						pane.TestDrop(DragSource, Outline)
				
				if (not Outline.FlagTestDrop) and DragSource.IsDockStateValid(DockState.Float):
					floatWindow as FloatWindow = DockHelper.FloatWindowAtPoint(Control.MousePosition, DockPanel)
					if floatWindow is not null:
						floatWindow.TestDrop(DragSource, Outline)
			else:
				Indicator.DockPane = DockHelper.PaneAtPoint(Control.MousePosition, DockPanel)
			
			if not Outline.FlagTestDrop:
				if DragSource.IsDockStateValid(DockState.Float):
					rect as Rectangle = FloatOutlineBounds
					rect.Offset((Control.MousePosition.X - StartMousePosition.X), (Control.MousePosition.Y - StartMousePosition.Y))
					Outline.Show(rect)
			
			if not Outline.FlagTestDrop:
				Cursor.Current = Cursors.No
				Outline.Show()
			else:
				Cursor.Current = DragControl.Cursor

		
		private def EndDrag(abort as bool):
			if abort:
				return 
			
			if not Outline.FloatWindowBounds.IsEmpty:
				DragSource.FloatAt(Outline.FloatWindowBounds)
			elif Outline.DockTo isa DockPane:
				pane = (Outline.DockTo as DockPane)
				DragSource.DockTo(pane, Outline.Dock, Outline.ContentIndex)
			elif Outline.DockTo isa DockPanel:
				panel = (Outline.DockTo as DockPanel)
				panel.UpdateDockWindowZOrder(Outline.Dock, Outline.FlagFullEdge)
				DragSource.DockTo(panel, Outline.Dock)

	
	private m_dockDragHandler as DockDragHandler = null

	private def GetDockDragHandler() as DockDragHandler:
		if m_dockDragHandler is null:
			m_dockDragHandler = DockDragHandler(self)
		return m_dockDragHandler

	
	internal def BeginDrag(dragSource as IDockDragSource):
		GetDockDragHandler().BeginDrag(dragSource)

