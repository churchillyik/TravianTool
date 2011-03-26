
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Text
import System.Windows.Forms
import System.Drawing
import System.Drawing.Drawing2D
import System.ComponentModel

partial internal class DockPanel:

	private abstract class DragHandlerBase(NativeWindow, IMessageFilter):

		protected def constructor():
			pass

		
		protected abstract DragControl as Control:
			get:
				pass

		
		private m_startMousePosition as Point = Point.Empty

		protected StartMousePosition as Point:
			get:
				return m_startMousePosition
			private set:
				m_startMousePosition = value

		
		protected def BeginDrag() as bool:
			// Avoid re-entrance;
			lock self:
				if DragControl is null:
					return false
				
				StartMousePosition = Control.MousePosition
				
				if not NativeMethods.DragDetect(DragControl.Handle, StartMousePosition):
					return false
				
				DragControl.FindForm().Capture = true
				AssignHandle(DragControl.FindForm().Handle)
				Application.AddMessageFilter(self)
				return true

		
		protected abstract def OnDragging():
			pass

		
		protected abstract def OnEndDrag(abort as bool):
			pass

		
		private def EndDrag(abort as bool):
			ReleaseHandle()
			Application.RemoveMessageFilter(self)
			DragControl.FindForm().Capture = false
			
			OnEndDrag(abort)

		
		def IMessageFilter.PreFilterMessage(ref m as Message) as bool:
			if m.Msg == cast(int, Win32.Msgs.WM_MOUSEMOVE):
				OnDragging()
			elif m.Msg == cast(int, Win32.Msgs.WM_LBUTTONUP):
				EndDrag(false)
			elif m.Msg == cast(int, Win32.Msgs.WM_CAPTURECHANGED):
				EndDrag(true)
			elif (m.Msg == cast(int, Win32.Msgs.WM_KEYDOWN)) and (cast(int, m.WParam) == cast(int, Keys.Escape)):
				EndDrag(true)
			
			return OnPreFilterMessage(m)

		
		protected virtual def OnPreFilterMessage(ref m as Message) as bool:
			return false

		
		protected override final def WndProc(ref m as Message):
			if (m.Msg == cast(int, Win32.Msgs.WM_CANCELMODE)) or (m.Msg == cast(int, Win32.Msgs.WM_CAPTURECHANGED)):
				EndDrag(true)
			
			super.WndProc(m)

	
	private abstract class DragHandler(DragHandlerBase):

		private m_dockPanel as DockPanel

		
		protected def constructor(dockPanel as DockPanel):
			m_dockPanel = dockPanel

		
		public DockPanel as DockPanel:
			get:
				return m_dockPanel

		
		private m_dragSource as IDragSource

		protected DragSource as IDragSource:
			get:
				return m_dragSource
			set:
				m_dragSource = value

		
		protected override final DragControl as Control:
			get:
				return (null if (DragSource is null) else DragSource.DragControl)

		
		protected override final def OnPreFilterMessage(ref m as Message) as bool:
			if ((m.Msg == cast(int, Win32.Msgs.WM_KEYDOWN)) or (m.Msg == cast(int, Win32.Msgs.WM_KEYUP))) and ((cast(int, m.WParam) == cast(int, Keys.ControlKey)) or (cast(int, m.WParam) == cast(int, Keys.ShiftKey))):
				OnDragging()
			
			return super.OnPreFilterMessage(m)

