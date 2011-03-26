
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Windows.Forms

internal static class DockHelper:

	public static def IsDockStateAutoHide(dockState as DockState) as bool:
		if (((dockState == DockState.DockLeftAutoHide) or (dockState == DockState.DockRightAutoHide)) or (dockState == DockState.DockTopAutoHide)) or (dockState == DockState.DockBottomAutoHide):
			return true
		else:
			return false

	
	public static def IsDockStateValid(dockState as DockState, dockableAreas as DockAreas) as bool:
		if ((dockableAreas & DockAreas.Float) == 0) and (dockState == DockState.Float):
			return false
		elif ((dockableAreas & DockAreas.Document) == 0) and (dockState == DockState.Document):
			return false
		elif ((dockableAreas & DockAreas.DockLeft) == 0) and ((dockState == DockState.DockLeft) or (dockState == DockState.DockLeftAutoHide)):
			return false
		elif ((dockableAreas & DockAreas.DockRight) == 0) and ((dockState == DockState.DockRight) or (dockState == DockState.DockRightAutoHide)):
			return false
		elif ((dockableAreas & DockAreas.DockTop) == 0) and ((dockState == DockState.DockTop) or (dockState == DockState.DockTopAutoHide)):
			return false
		elif ((dockableAreas & DockAreas.DockBottom) == 0) and ((dockState == DockState.DockBottom) or (dockState == DockState.DockBottomAutoHide)):
			return false
		else:
			return true

	
	public static def IsDockWindowState(state as DockState) as bool:
		if ((((state == DockState.DockTop) or (state == DockState.DockBottom)) or (state == DockState.DockLeft)) or (state == DockState.DockRight)) or (state == DockState.Document):
			return true
		else:
			return false

	
	public static def ToggleAutoHideState(state as DockState) as DockState:
		if state == DockState.DockLeft:
			return DockState.DockLeftAutoHide
		elif state == DockState.DockRight:
			return DockState.DockRightAutoHide
		elif state == DockState.DockTop:
			return DockState.DockTopAutoHide
		elif state == DockState.DockBottom:
			return DockState.DockBottomAutoHide
		elif state == DockState.DockLeftAutoHide:
			return DockState.DockLeft
		elif state == DockState.DockRightAutoHide:
			return DockState.DockRight
		elif state == DockState.DockTopAutoHide:
			return DockState.DockTop
		elif state == DockState.DockBottomAutoHide:
			return DockState.DockBottom
		else:
			return state

	
	public static def PaneAtPoint(pt as Point, dockPanel as DockPanel) as DockPane:
		control as Control = Win32Helper.ControlAtPoint(pt)
		goto converterGeneratedName1
		while true:
			control = control.Parent
			:converterGeneratedName1
			break  unless (control is not null)
			content = (control as IDockContent)
			if (content is not null) and (content.DockHandler.DockPanel == dockPanel):
				return content.DockHandler.Pane
			
			pane = (control as DockPane)
			if (pane is not null) and (pane.DockPanel == dockPanel):
				return pane
		
		return null

	
	public static def FloatWindowAtPoint(pt as Point, dockPanel as DockPanel) as FloatWindow:
		control as Control = Win32Helper.ControlAtPoint(pt)
		goto converterGeneratedName2
		while true:
			control = control.Parent
			:converterGeneratedName2
			break  unless (control is not null)
			floatWindow = (control as FloatWindow)
			if (floatWindow is not null) and (floatWindow.DockPanel == dockPanel):
				return floatWindow
		
		return null

