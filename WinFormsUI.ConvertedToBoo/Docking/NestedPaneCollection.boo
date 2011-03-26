
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Collections.ObjectModel
import System.Drawing

public final class NestedPaneCollection(ReadOnlyCollection[of DockPane]):

	private m_container as INestedPanesContainer

	private m_visibleNestedPanes as VisibleNestedPaneCollection

	
	internal def constructor(container as INestedPanesContainer):
		super(List[of DockPane]())
		m_container = container
		m_visibleNestedPanes = VisibleNestedPaneCollection(self)

	
	public Container as INestedPanesContainer:
		get:
			return m_container

	
	public VisibleNestedPanes as VisibleNestedPaneCollection:
		get:
			return m_visibleNestedPanes

	
	public DockState as DockState:
		get:
			return Container.DockState

	
	public IsFloat as bool:
		get:
			return (DockState == DockState.Float)

	
	internal def Add(pane as DockPane):
		if pane is null:
			return 
		
		oldNestedPanes as NestedPaneCollection = (null if (pane.NestedPanesContainer is null) else pane.NestedPanesContainer.NestedPanes)
		if oldNestedPanes is not null:
			oldNestedPanes.InternalRemove(pane)
		Items.Add(pane)
		if oldNestedPanes is not null:
			oldNestedPanes.CheckFloatWindowDispose()

	
	private def CheckFloatWindowDispose():
		if (Count == 0) and (Container.DockState == DockState.Float):
			floatWindow = cast(FloatWindow, Container)
			if (not floatWindow.Disposing) and (not floatWindow.IsDisposed):
				NativeMethods.PostMessage(cast(FloatWindow, Container).Handle, FloatWindow.WM_CHECKDISPOSE, 0, 0)

	
	internal def Remove(pane as DockPane):
		InternalRemove(pane)
		CheckFloatWindowDispose()

	
	private def InternalRemove(pane as DockPane):
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
			lastNestedDock.SetStatus(self, statusPane.PreviousPane, statusPane.Alignment, statusPane.Proportion)
			for i in range((indexLastNestedPane - 1), IndexOf(lastNestedPane), -1):
				status as NestedDockingStatus = self[i].NestedDockingStatus
				if status.PreviousPane == pane:
					status.SetStatus(self, lastNestedPane, status.Alignment, status.Proportion)
		else:
			Items.Remove(pane)
		
		statusPane.SetStatus(null, null, DockAlignment.Left, 0.5)
		statusPane.SetDisplayingStatus(false, null, DockAlignment.Left, 0.5)
		statusPane.SetDisplayingBounds(Rectangle.Empty, Rectangle.Empty, Rectangle.Empty)

	
	public def GetDefaultPreviousPane(pane as DockPane) as DockPane:
		for i in range((Count - 1), -1, -1):
			if self[i] != pane:
				return self[i]
		
		return null

