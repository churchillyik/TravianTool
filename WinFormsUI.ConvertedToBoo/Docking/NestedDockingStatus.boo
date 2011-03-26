
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing

public final class NestedDockingStatus:

	internal def constructor(pane as DockPane):
		m_dockPane = pane

	
	private m_dockPane as DockPane = null

	public DockPane as DockPane:
		get:
			return m_dockPane

	
	private m_nestedPanes as NestedPaneCollection = null

	public NestedPanes as NestedPaneCollection:
		get:
			return m_nestedPanes

	
	private m_previousPane as DockPane = null

	public PreviousPane as DockPane:
		get:
			return m_previousPane

	
	private m_alignment as DockAlignment = DockAlignment.Left

	public Alignment as DockAlignment:
		get:
			return m_alignment

	
	private m_proportion = 0.5

	public Proportion as double:
		get:
			return m_proportion

	
	private m_isDisplaying = false

	public IsDisplaying as bool:
		get:
			return m_isDisplaying

	
	private m_displayingPreviousPane as DockPane = null

	public DisplayingPreviousPane as DockPane:
		get:
			return m_displayingPreviousPane

	
	private m_displayingAlignment as DockAlignment = DockAlignment.Left

	public DisplayingAlignment as DockAlignment:
		get:
			return m_displayingAlignment

	
	private m_displayingProportion = 0.5

	public DisplayingProportion as double:
		get:
			return m_displayingProportion

	
	private m_logicalBounds as Rectangle = Rectangle.Empty

	public LogicalBounds as Rectangle:
		get:
			return m_logicalBounds

	
	private m_paneBounds as Rectangle = Rectangle.Empty

	public PaneBounds as Rectangle:
		get:
			return m_paneBounds

	
	private m_splitterBounds as Rectangle = Rectangle.Empty

	public SplitterBounds as Rectangle:
		get:
			return m_splitterBounds

	
	internal def SetStatus(nestedPanes as NestedPaneCollection, previousPane as DockPane, alignment as DockAlignment, proportion as double):
		m_nestedPanes = nestedPanes
		m_previousPane = previousPane
		m_alignment = alignment
		m_proportion = proportion

	
	internal def SetDisplayingStatus(isDisplaying as bool, displayingPreviousPane as DockPane, displayingAlignment as DockAlignment, displayingProportion as double):
		m_isDisplaying = isDisplaying
		m_displayingPreviousPane = displayingPreviousPane
		m_displayingAlignment = displayingAlignment
		m_displayingProportion = displayingProportion

	
	internal def SetDisplayingBounds(logicalBounds as Rectangle, paneBounds as Rectangle, splitterBounds as Rectangle):
		m_logicalBounds = logicalBounds
		m_paneBounds = paneBounds
		m_splitterBounds = splitterBounds

