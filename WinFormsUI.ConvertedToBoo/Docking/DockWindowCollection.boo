
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Collections.ObjectModel

public class DockWindowCollection(ReadOnlyCollection[of DockWindow]):

	internal def constructor(dockPanel as DockPanel):
		super(List[of DockWindow]())
		Items.Add(DockWindow(dockPanel, DockState.Document))
		Items.Add(DockWindow(dockPanel, DockState.DockLeft))
		Items.Add(DockWindow(dockPanel, DockState.DockRight))
		Items.Add(DockWindow(dockPanel, DockState.DockTop))
		Items.Add(DockWindow(dockPanel, DockState.DockBottom))

	
	public self[dockState as DockState] as DockWindow:
		get:
			if dockState == DockState.Document:
				return Items[0]
			elif (dockState == DockState.DockLeft) or (dockState == DockState.DockLeftAutoHide):
				return Items[1]
			elif (dockState == DockState.DockRight) or (dockState == DockState.DockRightAutoHide):
				return Items[2]
			elif (dockState == DockState.DockTop) or (dockState == DockState.DockTopAutoHide):
				return Items[3]
			elif (dockState == DockState.DockBottom) or (dockState == DockState.DockBottomAutoHide):
				return Items[4]
			
			raise ArgumentOutOfRangeException()

