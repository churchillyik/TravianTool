
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.ComponentModel
import System.Drawing
import System.Windows.Forms
import System.Runtime.InteropServices
import System.Diagnostics.CodeAnalysis

public class DockContent(Form, IDockContent):

	public def constructor():
		m_dockHandler = DockContentHandler(self, GetPersistStringCallback(GetPersistString))
		m_dockHandler.DockStateChanged += DockHandler_DockStateChanged

	
	private m_dockHandler as DockContentHandler = null

	[Browsable(false)]
	public DockHandler as DockContentHandler:
		get:
			return m_dockHandler

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_AllowEndUserDocking_Description')]
	[DefaultValue(true)]
	public AllowEndUserDocking as bool:
		get:
			return DockHandler.AllowEndUserDocking
		set:
			DockHandler.AllowEndUserDocking = value

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_DockAreas_Description')]
	[DefaultValue((((((DockAreas.DockLeft | DockAreas.DockRight) | DockAreas.DockTop) | DockAreas.DockBottom) | DockAreas.Document) | DockAreas.Float))]
	public DockAreas as DockAreas:
		get:
			return DockHandler.DockAreas
		set:
			DockHandler.DockAreas = value

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_AutoHidePortion_Description')]
	[DefaultValue(0.25)]
	public AutoHidePortion as double:
		get:
			return DockHandler.AutoHidePortion
		set:
			DockHandler.AutoHidePortion = value

	
	[Localizable(true)]
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_TabText_Description')]
	[DefaultValue(null)]
	public TabText as string:
		get:
			return DockHandler.TabText
		set:
			DockHandler.TabText = value

	private def ShouldSerializeTabText() as bool:
		return (DockHandler.TabText is not null)

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_CloseButton_Description')]
	[DefaultValue(true)]
	public CloseButton as bool:
		get:
			return DockHandler.CloseButton
		set:
			DockHandler.CloseButton = value

	
	[Browsable(false)]
	public DockPanel as DockPanel:
		get:
			return DockHandler.DockPanel
		set:
			DockHandler.DockPanel = value

	
	[Browsable(false)]
	public DockState as DockState:
		get:
			return DockHandler.DockState
		set:
			DockHandler.DockState = value

	
	[Browsable(false)]
	public Pane as DockPane:
		get:
			return DockHandler.Pane
		set:
			DockHandler.Pane = value

	
	[Browsable(false)]
	public IsHidden as bool:
		get:
			return DockHandler.IsHidden
		set:
			DockHandler.IsHidden = value

	
	[Browsable(false)]
	public VisibleState as DockState:
		get:
			return DockHandler.VisibleState
		set:
			DockHandler.VisibleState = value

	
	[Browsable(false)]
	public IsFloat as bool:
		get:
			return DockHandler.IsFloat
		set:
			DockHandler.IsFloat = value

	
	[Browsable(false)]
	public PanelPane as DockPane:
		get:
			return DockHandler.PanelPane
		set:
			DockHandler.PanelPane = value

	
	[Browsable(false)]
	public FloatPane as DockPane:
		get:
			return DockHandler.FloatPane
		set:
			DockHandler.FloatPane = value

	
	[SuppressMessage('Microsoft.Design', 'CA1024:UsePropertiesWhereAppropriate')]
	protected virtual def GetPersistString() as string:
		return GetType().ToString()

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_HideOnClose_Description')]
	[DefaultValue(false)]
	public HideOnClose as bool:
		get:
			return DockHandler.HideOnClose
		set:
			DockHandler.HideOnClose = value

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_ShowHint_Description')]
	[DefaultValue(DockState.Unknown)]
	public ShowHint as DockState:
		get:
			return DockHandler.ShowHint
		set:
			DockHandler.ShowHint = value

	
	[Browsable(false)]
	public IsActivated as bool:
		get:
			return DockHandler.IsActivated

	
	public def IsDockStateValid(dockState as DockState) as bool:
		return DockHandler.IsDockStateValid(dockState)

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_TabPageContextMenu_Description')]
	[DefaultValue(null)]
	public TabPageContextMenu as ContextMenu:
		get:
			return DockHandler.TabPageContextMenu
		set:
			DockHandler.TabPageContextMenu = value

	
	[LocalizedCategory('Category_Docking')]
	[LocalizedDescription('DockContent_TabPageContextMenuStrip_Description')]
	[DefaultValue(null)]
	public TabPageContextMenuStrip as ContextMenuStrip:
		get:
			return DockHandler.TabPageContextMenuStrip
		set:
			DockHandler.TabPageContextMenuStrip = value

	
	[Localizable(true)]
	[Category('Appearance')]
	[LocalizedDescription('DockContent_ToolTipText_Description')]
	[DefaultValue(null)]
	public ToolTipText as string:
		get:
			return DockHandler.ToolTipText
		set:
			DockHandler.ToolTipText = value

	
	public def Activate():
		DockHandler.Activate()

	
	public def Hide():
		DockHandler.Hide()

	
	public def Show():
		DockHandler.Show()

	
	public def Show(dockPanel as DockPanel):
		DockHandler.Show(dockPanel)

	
	public def Show(dockPanel as DockPanel, dockState as DockState):
		DockHandler.Show(dockPanel, dockState)

	
	[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters')]
	public def Show(dockPanel as DockPanel, floatWindowBounds as Rectangle):
		DockHandler.Show(dockPanel, floatWindowBounds)

	
	public def Show(pane as DockPane, beforeContent as IDockContent):
		DockHandler.Show(pane, beforeContent)

	
	public def Show(previousPane as DockPane, alignment as DockAlignment, proportion as double):
		DockHandler.Show(previousPane, alignment, proportion)

	
	[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters')]
	public def FloatAt(floatWindowBounds as Rectangle):
		DockHandler.FloatAt(floatWindowBounds)

	
	public def DockTo(paneTo as DockPane, dockStyle as DockStyle, contentIndex as int):
		DockHandler.DockTo(paneTo, dockStyle, contentIndex)

	
	public def DockTo(panel as DockPanel, dockStyle as DockStyle):
		DockHandler.DockTo(panel, dockStyle)

	
	#region Events
	private def DockHandler_DockStateChanged(sender as object, e as EventArgs):
		OnDockStateChanged(e)

	
	private static final DockStateChangedEvent = object()

	[LocalizedCategory('Category_PropertyChanged')]
	[LocalizedDescription('Pane_DockStateChanged_Description')]
	public event DockStateChanged as EventHandler

	protected virtual def OnDockStateChanged(e as EventArgs):
		handler = cast(EventHandler, Events[DockStateChangedEvent])
		handler(self, e)
	#endregion

