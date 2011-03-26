
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Collections.ObjectModel
import System.Drawing
import System.Windows.Forms

public final class VisibleNestedPaneCollection(ReadOnlyCollection[of DockPane]):

	private m_nestedPanes as NestedPaneCollection

	
	internal def constructor(nestedPanes as NestedPaneCollection):
		super(List[of DockPane]())
		m_nestedPanes = nestedPanes

	
	public NestedPanes as NestedPaneCollection:
		get:
			return m_nestedPanes

	
	public Container as INestedPanesContainer:
		get:
			return NestedPanes.Container

	
	public DockState as DockState:
		get:
			return NestedPanes.DockState

	
	public IsFloat as bool:
		get:
			return NestedPanes.IsFloat

	
	internal def Refresh():
		status as NestedDockingStatus
		Items.Clear()
		for i in range(0, NestedPanes.Count):
			pane as DockPane = NestedPanes[i]
			status = pane.NestedDockingStatus
			status.SetDisplayingStatus(true, status.PreviousPane, status.Alignment, status.Proportion)
			Items.Add(pane)
		
		for pane as DockPane in NestedPanes:
			if (pane.DockState != DockState) or pane.IsHidden:
				pane.Bounds = Rectangle.Empty
				pane.SplitterBounds = Rectangle.Empty
				Remove(pane)
		
		CalculateBounds()
		
		for pane as DockPane in self:
			status = pane.NestedDockingStatus
			pane.Bounds = status.PaneBounds
			pane.SplitterBounds = status.SplitterBounds
			pane.SplitterAlignment = status.Alignment

	
	private def Remove(pane as DockPane):
		if not Contains(pane):
			return 
		
		statusPane as NestedDockingStatus = pane.NestedDockingStatus
		lastNestedPane as DockPane = null
		for i in range((Count - 1), IndexOf(pane), -1):
			if self[i].NestedDockingStatus.PreviousPane == pane:
				lastNestedPane = self[i]
				break 
		
		if lastNestedPane is not null:
			indexLastNestedPane as int = IndexOf(lastNestedPane)
			Items.Remove(lastNestedPane)
			Items[IndexOf(pane)] = lastNestedPane
			lastNestedDock as NestedDockingStatus = lastNestedPane.NestedDockingStatus
			lastNestedDock.SetDisplayingStatus(true, statusPane.DisplayingPreviousPane, statusPane.DisplayingAlignment, statusPane.DisplayingProportion)
			for i in range((indexLastNestedPane - 1), IndexOf(lastNestedPane), -1):
				status as NestedDockingStatus = self[i].NestedDockingStatus
				if status.PreviousPane == pane:
					status.SetDisplayingStatus(true, lastNestedPane, status.DisplayingAlignment, status.DisplayingProportion)
		else:
			Items.Remove(pane)
		
		statusPane.SetDisplayingStatus(false, null, DockAlignment.Left, 0.5)

	
	private def CalculateBounds():
		if Count == 0:
			return 
		
		self[0].NestedDockingStatus.SetDisplayingBounds(Container.DisplayingRectangle, Container.DisplayingRectangle, Rectangle.Empty)
		for i in range(1, Count):
		
			pane as DockPane = self[i]
			status as NestedDockingStatus = pane.NestedDockingStatus
			prevPane as DockPane = status.DisplayingPreviousPane
			statusPrev as NestedDockingStatus = prevPane.NestedDockingStatus
			
			rect as Rectangle = statusPrev.PaneBounds
			bVerticalSplitter as bool = ((status.DisplayingAlignment == DockAlignment.Left) or (status.DisplayingAlignment == DockAlignment.Right))
			
			rectThis as Rectangle = rect
			rectPrev as Rectangle = rect
			rectSplitter as Rectangle = rect
			if status.DisplayingAlignment == DockAlignment.Left:
				rectThis.Width = (cast(int, (cast(double, rect.Width) * status.DisplayingProportion)) - (Measures.SplitterSize / 2))
				rectSplitter.X = (rectThis.X + rectThis.Width)
				rectSplitter.Width = Measures.SplitterSize
				rectPrev.X = (rectSplitter.X + rectSplitter.Width)
				rectPrev.Width = ((rect.Width - rectThis.Width) - rectSplitter.Width)
			elif status.DisplayingAlignment == DockAlignment.Right:
				rectPrev.Width = ((rect.Width - cast(int, (cast(double, rect.Width) * status.DisplayingProportion))) - (Measures.SplitterSize / 2))
				rectSplitter.X = (rectPrev.X + rectPrev.Width)
				rectSplitter.Width = Measures.SplitterSize
				rectThis.X = (rectSplitter.X + rectSplitter.Width)
				rectThis.Width = ((rect.Width - rectPrev.Width) - rectSplitter.Width)
			elif status.DisplayingAlignment == DockAlignment.Top:
				rectThis.Height = (cast(int, (cast(double, rect.Height) * status.DisplayingProportion)) - (Measures.SplitterSize / 2))
				rectSplitter.Y = (rectThis.Y + rectThis.Height)
				rectSplitter.Height = Measures.SplitterSize
				rectPrev.Y = (rectSplitter.Y + rectSplitter.Height)
				rectPrev.Height = ((rect.Height - rectThis.Height) - rectSplitter.Height)
			elif status.DisplayingAlignment == DockAlignment.Bottom:
				rectPrev.Height = ((rect.Height - cast(int, (cast(double, rect.Height) * status.DisplayingProportion))) - (Measures.SplitterSize / 2))
				rectSplitter.Y = (rectPrev.Y + rectPrev.Height)
				rectSplitter.Height = Measures.SplitterSize
				rectThis.Y = (rectSplitter.Y + rectSplitter.Height)
				rectThis.Height = ((rect.Height - rectPrev.Height) - rectSplitter.Height)
			else:
				rectThis = Rectangle.Empty
			
			rectSplitter.Intersect(rect)
			rectThis.Intersect(rect)
			rectPrev.Intersect(rect)
			status.SetDisplayingBounds(rect, rectThis, rectSplitter)
			statusPrev.SetDisplayingBounds(statusPrev.LogicalBounds, rectPrev, statusPrev.SplitterBounds)

