
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Windows.Forms

internal abstract class DockOutlineBase:

	public def constructor():
		Init()

	
	private def Init():
		SetValues(Rectangle.Empty, null, DockStyle.None, -1)
		SaveOldValues()

	
	private m_oldFloatWindowBounds as Rectangle

	protected OldFloatWindowBounds as Rectangle:
		get:
			return m_oldFloatWindowBounds

	
	private m_oldDockTo as Control

	protected OldDockTo as Control:
		get:
			return m_oldDockTo

	
	private m_oldDock as DockStyle

	protected OldDock as DockStyle:
		get:
			return m_oldDock

	
	private m_oldContentIndex as int

	protected OldContentIndex as int:
		get:
			return m_oldContentIndex

	
	protected SameAsOldValue as bool:
		get:
			return ((((FloatWindowBounds == OldFloatWindowBounds) and (DockTo == OldDockTo)) and (Dock == OldDock)) and (ContentIndex == OldContentIndex))

	
	private m_floatWindowBounds as Rectangle

	public FloatWindowBounds as Rectangle:
		get:
			return m_floatWindowBounds

	
	private m_dockTo as Control

	public DockTo as Control:
		get:
			return m_dockTo

	
	private m_dock as DockStyle

	public Dock as DockStyle:
		get:
			return m_dock

	
	private m_contentIndex as int

	public ContentIndex as int:
		get:
			return m_contentIndex

	
	public FlagFullEdge as bool:
		get:
			return (m_contentIndex != 0)

	
	private m_flagTestDrop = false

	public FlagTestDrop as bool:
		get:
			return m_flagTestDrop
		set:
			m_flagTestDrop = value

	
	private def SaveOldValues():
		m_oldDockTo = m_dockTo
		m_oldDock = m_dock
		m_oldContentIndex = m_contentIndex
		m_oldFloatWindowBounds = m_floatWindowBounds

	
	protected abstract def OnShow():
		pass

	
	protected abstract def OnClose():
		pass

	
	private def SetValues(floatWindowBounds as Rectangle, dockTo as Control, dock as DockStyle, contentIndex as int):
		m_floatWindowBounds = floatWindowBounds
		m_dockTo = dockTo
		m_dock = dock
		m_contentIndex = contentIndex
		FlagTestDrop = true

	
	private def TestChange():
		if (((m_floatWindowBounds != m_oldFloatWindowBounds) or (m_dockTo != m_oldDockTo)) or (m_dock != m_oldDock)) or (m_contentIndex != m_oldContentIndex):
			OnShow()

	
	public def Show():
		SaveOldValues()
		SetValues(Rectangle.Empty, null, DockStyle.None, -1)
		TestChange()

	
	public def Show(pane as DockPane, dock as DockStyle):
		SaveOldValues()
		SetValues(Rectangle.Empty, pane, dock, -1)
		TestChange()

	
	public def Show(pane as DockPane, contentIndex as int):
		SaveOldValues()
		SetValues(Rectangle.Empty, pane, DockStyle.Fill, contentIndex)
		TestChange()

	
	public def Show(dockPanel as DockPanel, dock as DockStyle, fullPanelEdge as bool):
		SaveOldValues()
		SetValues(Rectangle.Empty, dockPanel, dock, ((-1) if fullPanelEdge else 0))
		TestChange()

	
	public def Show(floatWindowBounds as Rectangle):
		SaveOldValues()
		SetValues(floatWindowBounds, null, DockStyle.None, -1)
		TestChange()

	
	public def Close():
		OnClose()

