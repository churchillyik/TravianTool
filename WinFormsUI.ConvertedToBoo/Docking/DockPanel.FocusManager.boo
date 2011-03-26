
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Text
import System.ComponentModel
import System.Runtime.InteropServices
import System.Windows.Forms
import System.Diagnostics.CodeAnalysis

internal interface IContentFocusManager:

	def Activate(content as IDockContent)

	def GiveUpFocus(content as IDockContent)

	def AddToList(content as IDockContent)

	def RemoveFromList(content as IDockContent)


partial internal class DockPanel:

	private interface IFocusManager:

		def SuspendFocusTracking()

		def ResumeFocusTracking()

		IsFocusTrackingSuspended as bool:
			get

		ActiveContent as IDockContent:
			get

		ActivePane as DockPane:
			get

		ActiveDocument as IDockContent:
			get

		ActiveDocumentPane as DockPane:
			get

	
	private class FocusManagerImpl(Component, IContentFocusManager, IFocusManager):

		private class HookEventArgs(EventArgs):

			[SuppressMessage('Microsoft.Performance', 'CA1823:AvoidUnusedPrivateFields')]
			public HookCode as int

			[SuppressMessage('Microsoft.Performance', 'CA1823:AvoidUnusedPrivateFields')]
			public wParam as IntPtr

			public lParam as IntPtr

		
		private class LocalWindowsHook(IDisposable):

			// Internal properties
			private m_hHook as IntPtr = IntPtr.Zero

			private m_filterFunc as NativeMethods.HookProc = null

			private m_hookType as Win32.HookType

			
			// Event delegate
			public callable HookEventHandler(sender as object, e as HookEventArgs) as void
			
			// Event: HookInvoked 
			public event HookInvoked as HookEventHandler

			protected def OnHookInvoked(e as HookEventArgs):
				HookInvoked(self, e)

			
			public def constructor(hook as Win32.HookType):
				m_hookType = hook
				m_filterFunc = NativeMethods.HookProc(self.CoreHookProc)

			
			// Default filter function
			public def CoreHookProc(code as int, wParam as IntPtr, lParam as IntPtr) as IntPtr:
				if code < 0:
					return NativeMethods.CallNextHookEx(m_hHook, code, wParam, lParam)
				
				// Let clients determine what to do
				e = HookEventArgs()
				e.HookCode = code
				e.wParam = wParam
				e.lParam = lParam
				OnHookInvoked(e)
				
				// Yield to the next hook in the chain
				return NativeMethods.CallNextHookEx(m_hHook, code, wParam, lParam)

			
			// Install the hook
			public def Install():
				if m_hHook != IntPtr.Zero:
					Uninstall()
				
				threadId as int = NativeMethods.GetCurrentThreadId()
				m_hHook = NativeMethods.SetWindowsHookEx(m_hookType, m_filterFunc, IntPtr.Zero, threadId)

			
			// Uninstall the hook
			public def Uninstall():
				if m_hHook != IntPtr.Zero:
					NativeMethods.UnhookWindowsHookEx(m_hHook)
					m_hHook = IntPtr.Zero

			
			def destructor():
				Dispose(false)

			
			public def Dispose():
				Dispose(true)
				GC.SuppressFinalize(self)

			
			protected virtual def Dispose(disposing as bool):
				Uninstall()

		
		private m_localWindowsHook as LocalWindowsHook

		private m_hookEventHandler as LocalWindowsHook.HookEventHandler

		
		public def constructor(dockPanel as DockPanel):
			m_dockPanel = dockPanel
			m_localWindowsHook = LocalWindowsHook(Win32.HookType.WH_CALLWNDPROCRET)
			m_hookEventHandler = HookEventHandler
			m_localWindowsHook.HookInvoked += m_hookEventHandler
			m_localWindowsHook.Install()

		
		private m_dockPanel as DockPanel

		public DockPanel as DockPanel:
			get:
				return m_dockPanel

		
		private m_disposed = false

		protected override def Dispose(disposing as bool):
			lock self:
				if (not m_disposed) and disposing:
					m_localWindowsHook.Dispose()
					m_disposed = true
				
				super.Dispose(disposing)

		
		private m_contentActivating as IDockContent = null

		private ContentActivating as IDockContent:
			get:
				return m_contentActivating
			set:
				m_contentActivating = value

		
		public def Activate(content as IDockContent):
			if IsFocusTrackingSuspended:
				ContentActivating = content
				return 
			
			if content is null:
				return 
			handler as DockContentHandler = content.DockHandler
			if handler.Form.IsDisposed:
				return 
			// Should not reach here, but better than throwing an exception
			if ContentContains(content, handler.ActiveWindowHandle):
				NativeMethods.SetFocus(handler.ActiveWindowHandle)
			if not handler.Form.ContainsFocus:
				if not handler.Form.SelectNextControl(handler.Form.ActiveControl, true, true, true, true):
					// Since DockContent Form is not selectalbe, use Win32 SetFocus instead
					NativeMethods.SetFocus(handler.Form.Handle)

		
		private m_listContent as List[of IDockContent] = List[of IDockContent]()

		private ListContent as List[of IDockContent]:
			get:
				return m_listContent

		public def AddToList(content as IDockContent):
			if ListContent.Contains(content) or IsInActiveList(content):
				return 
			
			ListContent.Add(content)

		
		public def RemoveFromList(content as IDockContent):
			if IsInActiveList(content):
				RemoveFromActiveList(content)
			if ListContent.Contains(content):
				ListContent.Remove(content)

		
		private m_lastActiveContent as IDockContent = null

		private LastActiveContent as IDockContent:
			get:
				return m_lastActiveContent
			set:
				m_lastActiveContent = value

		
		private def IsInActiveList(content as IDockContent) as bool:
			return (not ((content.DockHandler.NextActive is null) and (LastActiveContent != content)))

		
		private def AddLastToActiveList(content as IDockContent):
			last as IDockContent = LastActiveContent
			if last == content:
				return 
			
			handler as DockContentHandler = content.DockHandler
			
			if IsInActiveList(content):
				RemoveFromActiveList(content)
			
			handler.PreviousActive = last
			handler.NextActive = null
			LastActiveContent = content
			if last is not null:
				last.DockHandler.NextActive = LastActiveContent

		
		private def RemoveFromActiveList(content as IDockContent):
			if LastActiveContent == content:
				LastActiveContent = content.DockHandler.PreviousActive
			
			prev as IDockContent = content.DockHandler.PreviousActive
			next as IDockContent = content.DockHandler.NextActive
			if prev is not null:
				prev.DockHandler.NextActive = next
			if next is not null:
				next.DockHandler.PreviousActive = prev
			
			content.DockHandler.PreviousActive = null
			content.DockHandler.NextActive = null

		
		public def GiveUpFocus(content as IDockContent):
			handler as DockContentHandler = content.DockHandler
			if not handler.Form.ContainsFocus:
				return 
			
			if IsFocusTrackingSuspended:
				DockPanel.DummyControl.Focus()
			
			if LastActiveContent == content:
				prev as IDockContent = handler.PreviousActive
				if prev is not null:
					Activate(prev)
				elif ListContent.Count > 0:
					Activate(ListContent[(ListContent.Count - 1)])
			elif LastActiveContent is not null:
				Activate(LastActiveContent)
			elif ListContent.Count > 0:
				Activate(ListContent[(ListContent.Count - 1)])

		
		private static def ContentContains(content as IDockContent, hWnd as IntPtr) as bool:
			control as Control = Control.FromChildHandle(hWnd)
			parent as Control = control
			goto converterGeneratedName1
			while true:
				parent = parent.Parent
				:converterGeneratedName1
				break  unless (parent is not null)
				if parent == content.DockHandler.Form:
					return true
			
			return false

		
		private m_countSuspendFocusTracking = 0

		public def SuspendFocusTracking():
			m_countSuspendFocusTracking += 1
			m_localWindowsHook.HookInvoked -= m_hookEventHandler

		
		public def ResumeFocusTracking():
			if m_countSuspendFocusTracking > 0:
				m_countSuspendFocusTracking -= 1
			
			if m_countSuspendFocusTracking == 0:
				if ContentActivating is not null:
					Activate(ContentActivating)
					ContentActivating = null
				m_localWindowsHook.HookInvoked += m_hookEventHandler
				if not InRefreshActiveWindow:
					RefreshActiveWindow()

		
		public IsFocusTrackingSuspended as bool:
			get:
				return (m_countSuspendFocusTracking != 0)

		
		// Windows hook event handler
		private def HookEventHandler(sender as object, e as HookEventArgs):
			msg = cast(Win32.Msgs, Marshal.ReadInt32(e.lParam, (IntPtr.Size * 3)))
			
			if msg == Win32.Msgs.WM_KILLFOCUS:
				wParam as IntPtr = Marshal.ReadIntPtr(e.lParam, (IntPtr.Size * 2))
				pane as DockPane = GetPaneFromHandle(wParam)
				if pane is null:
					RefreshActiveWindow()
			elif msg == Win32.Msgs.WM_SETFOCUS:
				RefreshActiveWindow()

		
		private def GetPaneFromHandle(hWnd as IntPtr) as DockPane:
			control as Control = Control.FromChildHandle(hWnd)
			
			content as IDockContent = null
			pane as DockPane = null
			goto converterGeneratedName2
			while true:
				control = control.Parent
				:converterGeneratedName2
				break  unless (control is not null)
				content = (control as IDockContent)
				if content is not null:
					content.DockHandler.ActiveWindowHandle = hWnd
				
				if (content is not null) and (content.DockHandler.DockPanel == DockPanel):
					return content.DockHandler.Pane
				
				pane = (control as DockPane)
				if (pane is not null) and (pane.DockPanel == DockPanel):
					break 
			
			return pane

		
		private m_inRefreshActiveWindow = false

		private InRefreshActiveWindow as bool:
			get:
				return m_inRefreshActiveWindow

		
		private def RefreshActiveWindow():
			SuspendFocusTracking()
			m_inRefreshActiveWindow = true
			
			oldActivePane as DockPane = ActivePane
			oldActiveContent as IDockContent = ActiveContent
			oldActiveDocument as IDockContent = ActiveDocument
			
			SetActivePane()
			SetActiveContent()
			SetActiveDocumentPane()
			SetActiveDocument()
			DockPanel.AutoHideWindow.RefreshActivePane()
			
			ResumeFocusTracking()
			m_inRefreshActiveWindow = false
			
			if oldActiveContent != ActiveContent:
				DockPanel.OnActiveContentChanged(EventArgs.Empty)
			if oldActiveDocument != ActiveDocument:
				DockPanel.OnActiveDocumentChanged(EventArgs.Empty)
			if oldActivePane != ActivePane:
				DockPanel.OnActivePaneChanged(EventArgs.Empty)

		
		private m_activePane as DockPane = null

		public ActivePane as DockPane:
			get:
				return m_activePane

		
		private def SetActivePane():
			value as DockPane = GetPaneFromHandle(NativeMethods.GetFocus())
			if m_activePane == value:
				return 
			
			if m_activePane is not null:
				m_activePane.SetIsActivated(false)
			
			m_activePane = value
			
			if m_activePane is not null:
				m_activePane.SetIsActivated(true)

		
		private m_activeContent as IDockContent = null

		public ActiveContent as IDockContent:
			get:
				return m_activeContent

		
		internal def SetActiveContent():
			value as IDockContent = (null if (ActivePane is null) else ActivePane.ActiveContent)
			
			if m_activeContent == value:
				return 
			
			if m_activeContent is not null:
				m_activeContent.DockHandler.IsActivated = false
			
			m_activeContent = value
			
			if m_activeContent is not null:
				m_activeContent.DockHandler.IsActivated = true
				if not DockHelper.IsDockStateAutoHide(m_activeContent.DockHandler.DockState):
					AddLastToActiveList(m_activeContent)

		
		private m_activeDocumentPane as DockPane = null

		public ActiveDocumentPane as DockPane:
			get:
				return m_activeDocumentPane

		
		private def SetActiveDocumentPane():
			value as DockPane = null
			
			if (ActivePane is not null) and (ActivePane.DockState == DockState.Document):
				value = ActivePane
			
			if (value is null) and (DockPanel.DockWindows is not null):
				if ActiveDocumentPane is null:
					value = DockPanel.DockWindows[DockState.Document].DefaultPane
				elif (ActiveDocumentPane.DockPanel != DockPanel) or (ActiveDocumentPane.DockState != DockState.Document):
					value = DockPanel.DockWindows[DockState.Document].DefaultPane
				else:
					value = ActiveDocumentPane
			
			if m_activeDocumentPane == value:
				return 
			
			if m_activeDocumentPane is not null:
				m_activeDocumentPane.SetIsActiveDocumentPane(false)
			
			m_activeDocumentPane = value
			
			if m_activeDocumentPane is not null:
				m_activeDocumentPane.SetIsActiveDocumentPane(true)

		
		private m_activeDocument as IDockContent = null

		public ActiveDocument as IDockContent:
			get:
				return m_activeDocument

		
		private def SetActiveDocument():
			value as IDockContent = (null if (ActiveDocumentPane is null) else ActiveDocumentPane.ActiveContent)
			
			if m_activeDocument == value:
				return 
			
			m_activeDocument = value

	
	private FocusManager as IFocusManager:
		get:
			return m_focusManager

	
	internal ContentFocusManager as IContentFocusManager:
		get:
			return m_focusManager

	
	internal def SaveFocus():
		DummyControl.Focus()

	
	[Browsable(false)]
	public ActiveContent as IDockContent:
		get:
			return FocusManager.ActiveContent

	
	[Browsable(false)]
	public ActivePane as DockPane:
		get:
			return FocusManager.ActivePane

	
	[Browsable(false)]
	public ActiveDocument as IDockContent:
		get:
			return FocusManager.ActiveDocument

	
	[Browsable(false)]
	public ActiveDocumentPane as DockPane:
		get:
			return FocusManager.ActiveDocumentPane

	
	private static final ActiveDocumentChangedEvent = object()

	[LocalizedCategory('Category_PropertyChanged')]
	[LocalizedDescription('DockPanel_ActiveDocumentChanged_Description')]
	public event ActiveDocumentChanged as EventHandler

	protected virtual def OnActiveDocumentChanged(e as EventArgs):
		handler = cast(EventHandler, Events[ActiveDocumentChangedEvent])
		handler(self, e)

	
	private static final ActiveContentChangedEvent = object()

	[LocalizedCategory('Category_PropertyChanged')]
	[LocalizedDescription('DockPanel_ActiveContentChanged_Description')]
	public event ActiveContentChanged as EventHandler

	protected def OnActiveContentChanged(e as EventArgs):
		handler = cast(EventHandler, Events[ActiveContentChangedEvent])
		handler(self, e)

	
	private static final ActivePaneChangedEvent = object()

	[LocalizedCategory('Category_PropertyChanged')]
	[LocalizedDescription('DockPanel_ActivePaneChanged_Description')]
	public event ActivePaneChanged as EventHandler

	protected virtual def OnActivePaneChanged(e as EventArgs):
		handler = cast(EventHandler, Events[ActivePaneChangedEvent])
		handler(self, e)

