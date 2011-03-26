
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Diagnostics.CodeAnalysis

public final class DockPanelExtender:

	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	public interface IDockPaneFactory:

		def CreateDockPane(content as IDockContent, visibleState as DockState, show as bool) as DockPane

		[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters', MessageId: '1#')]
		def CreateDockPane(content as IDockContent, floatWindow as FloatWindow, show as bool) as DockPane

		def CreateDockPane(content as IDockContent, previousPane as DockPane, alignment as DockAlignment, proportion as double, show as bool) as DockPane

		[SuppressMessage('Microsoft.Naming', 'CA1720:AvoidTypeNamesInParameters', MessageId: '1#')]
		def CreateDockPane(content as IDockContent, floatWindowBounds as Rectangle, show as bool) as DockPane

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	public interface IFloatWindowFactory:

		def CreateFloatWindow(dockPanel as DockPanel, pane as DockPane) as FloatWindow

		def CreateFloatWindow(dockPanel as DockPanel, pane as DockPane, bounds as Rectangle) as FloatWindow

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	public interface IDockPaneCaptionFactory:

		def CreateDockPaneCaption(pane as DockPane) as DockPaneCaptionBase

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	public interface IDockPaneStripFactory:

		def CreateDockPaneStrip(pane as DockPane) as DockPaneStripBase

	
	[SuppressMessage('Microsoft.Design', 'CA1034:NestedTypesShouldNotBeVisible')]
	public interface IAutoHideStripFactory:

		def CreateAutoHideStrip(panel as DockPanel) as AutoHideStripBase

	
	#region DefaultDockPaneFactory
	private class DefaultDockPaneFactory(IDockPaneFactory):

		public def CreateDockPane(content as IDockContent, visibleState as DockState, show as bool) as DockPane:
			return DockPane(content, visibleState, show)

		
		public def CreateDockPane(content as IDockContent, floatWindow as FloatWindow, show as bool) as DockPane:
			return DockPane(content, floatWindow, show)

		
		public def CreateDockPane(content as IDockContent, prevPane as DockPane, alignment as DockAlignment, proportion as double, show as bool) as DockPane:
			return DockPane(content, prevPane, alignment, proportion, show)

		
		public def CreateDockPane(content as IDockContent, floatWindowBounds as Rectangle, show as bool) as DockPane:
			return DockPane(content, floatWindowBounds, show)

	#endregion
	
	#region DefaultFloatWindowFactory
	private class DefaultFloatWindowFactory(IFloatWindowFactory):

		public def CreateFloatWindow(dockPanel as DockPanel, pane as DockPane) as FloatWindow:
			return FloatWindow(dockPanel, pane)

		
		public def CreateFloatWindow(dockPanel as DockPanel, pane as DockPane, bounds as Rectangle) as FloatWindow:
			return FloatWindow(dockPanel, pane, bounds)

	#endregion
	
	#region DefaultDockPaneCaptionFactory
	private class DefaultDockPaneCaptionFactory(IDockPaneCaptionFactory):

		public def CreateDockPaneCaption(pane as DockPane) as DockPaneCaptionBase:
			return VS2005DockPaneCaption(pane)

	#endregion
	
	#region DefaultDockPaneTabStripFactory
	private class DefaultDockPaneStripFactory(IDockPaneStripFactory):

		public def CreateDockPaneStrip(pane as DockPane) as DockPaneStripBase:
			return VS2005DockPaneStrip(pane)

	#endregion
	
	#region DefaultAutoHideStripFactory
	private class DefaultAutoHideStripFactory(IAutoHideStripFactory):

		public def CreateAutoHideStrip(panel as DockPanel) as AutoHideStripBase:
			return VS2005AutoHideStrip(panel)

	#endregion
	
	internal def constructor(dockPanel as DockPanel):
		m_dockPanel = dockPanel

	
	private m_dockPanel as DockPanel

	private DockPanel as DockPanel:
		get:
			return m_dockPanel

	
	private m_dockPaneFactory as IDockPaneFactory = null

	public DockPaneFactory as IDockPaneFactory:
		get:
			if m_dockPaneFactory is null:
				m_dockPaneFactory = DefaultDockPaneFactory()
			
			return m_dockPaneFactory
		set:
			if DockPanel.Panes.Count > 0:
				raise InvalidOperationException()
			
			m_dockPaneFactory = value

	
	private m_floatWindowFactory as IFloatWindowFactory = null

	public FloatWindowFactory as IFloatWindowFactory:
		get:
			if m_floatWindowFactory is null:
				m_floatWindowFactory = DefaultFloatWindowFactory()
			
			return m_floatWindowFactory
		set:
			if DockPanel.FloatWindows.Count > 0:
				raise InvalidOperationException()
			
			m_floatWindowFactory = value

	
	private m_dockPaneCaptionFactory as IDockPaneCaptionFactory = null

	public DockPaneCaptionFactory as IDockPaneCaptionFactory:
		get:
			if m_dockPaneCaptionFactory is null:
				m_dockPaneCaptionFactory = DefaultDockPaneCaptionFactory()
			
			return m_dockPaneCaptionFactory
		set:
			if DockPanel.Panes.Count > 0:
				raise InvalidOperationException()
			
			m_dockPaneCaptionFactory = value

	
	private m_dockPaneStripFactory as IDockPaneStripFactory = null

	public DockPaneStripFactory as IDockPaneStripFactory:
		get:
			if m_dockPaneStripFactory is null:
				m_dockPaneStripFactory = DefaultDockPaneStripFactory()
			
			return m_dockPaneStripFactory
		set:
			if DockPanel.Contents.Count > 0:
				raise InvalidOperationException()
			
			m_dockPaneStripFactory = value

	
	private m_autoHideStripFactory as IAutoHideStripFactory = null

	public AutoHideStripFactory as IAutoHideStripFactory:
		get:
			if m_autoHideStripFactory is null:
				m_autoHideStripFactory = DefaultAutoHideStripFactory()
			
			return m_autoHideStripFactory
		set:
			if DockPanel.Contents.Count > 0:
				raise InvalidOperationException()
			
			if m_autoHideStripFactory == value:
				return 
			
			m_autoHideStripFactory = value
			DockPanel.ResetAutoHideStripControl()

