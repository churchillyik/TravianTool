
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Windows.Forms

public interface IDockContent:

	DockHandler as DockContentHandler:
		get


public interface INestedPanesContainer:

	DockState as DockState:
		get

	DisplayingRectangle as Rectangle:
		get

	NestedPanes as NestedPaneCollection:
		get

	VisibleNestedPanes as VisibleNestedPaneCollection:
		get

	IsFloat as bool:
		get


internal interface IDragSource:

	DragControl as Control:
		get


internal interface IDockDragSource(IDragSource):

	def BeginDrag(ptMouse as Point) as Rectangle

	def IsDockStateValid(dockState as DockState) as bool

	def CanDockTo(pane as DockPane) as bool

	def FloatAt(floatWindowBounds as Rectangle)

	def DockTo(pane as DockPane, dockStyle as DockStyle, contentIndex as int)

	def DockTo(panel as DockPanel, dockStyle as DockStyle)


internal interface ISplitterDragSource(IDragSource):

	def BeginDrag(rectSplitter as Rectangle)

	def EndDrag()

	IsVertical as bool:
		get

	DragLimitBounds as Rectangle:
		get

	def MoveSplitter(offset as int)

