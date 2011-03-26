
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Windows.Forms
import System.ComponentModel
import System.ComponentModel.Design
import System.Runtime.InteropServices

partial internal class DockPanel:

	//  This class comes from Jacob Slusser's MdiClientController class:
	//  http://www.codeproject.com/cs/miscctrl/mdiclientcontroller.asp
	private class MdiClientController(NativeWindow, IComponent, IDisposable):

		private m_autoScroll = true

		private m_borderStyle as BorderStyle = BorderStyle.Fixed3D

		private m_mdiClient as MdiClient = null

		private m_parentForm as Form = null

		private m_site as ISite = null

		
		public def constructor():
			pass

		
		public def Dispose():
			Dispose(true)
			GC.SuppressFinalize(self)

		
		protected virtual def Dispose(disposing as bool):
			if disposing:
				lock self:
					if (Site is not null) and (Site.Container is not null):
						Site.Container.Remove(self)
					Disposed(self, EventArgs.Empty)
					

		
		public AutoScroll as bool:
			get:
				return m_autoScroll
			set:
				// By default the MdiClient control scrolls. It can appear though that
				// there are no scrollbars by turning them off when the non-client
				// area is calculated. I decided to expose this method following
				// the .NET vernacular of an AutoScroll property.
				m_autoScroll = value
				if MdiClient is not null:
					UpdateStyles()

		
		public BorderStyle as BorderStyle:
			set:
				// Error-check the enum.
				if not Enum.IsDefined(typeof(BorderStyle), value):
					raise InvalidEnumArgumentException()
				
				m_borderStyle = value
				
				if MdiClient is null:
					return 
				
				// This property can actually be visible in design-mode,
				// but to keep it consistent with the others,
				// prevent this from being show at design-time.
				if (Site is not null) and Site.DesignMode:
					return 
				
				// There is no BorderStyle property exposed by the MdiClient class,
				// but this can be controlled by Win32 functions. A Win32 ExStyle
				// of WS_EX_CLIENTEDGE is equivalent to a Fixed3D border and a
				// Style of WS_BORDER is equivalent to a FixedSingle border.
				
				// This code is inspired Jason Dori's article:
				// "Adding designable borders to user controls".
				// http://www.codeproject.com/cs/miscctrl/CsAddingBorders.asp
				
				// Get styles using Win32 calls
				style as int = NativeMethods.GetWindowLong(MdiClient.Handle, cast(int, Win32.GetWindowLongIndex.GWL_STYLE))
				exStyle as int = NativeMethods.GetWindowLong(MdiClient.Handle, cast(int, Win32.GetWindowLongIndex.GWL_EXSTYLE))
				converterGeneratedName1 = m_borderStyle
				
				// Add or remove style flags as necessary.
				if converterGeneratedName1 == BorderStyle.Fixed3D:
					exStyle |= cast(int, Win32.WindowExStyles.WS_EX_CLIENTEDGE)
					style &= (~cast(int, Win32.WindowStyles.WS_BORDER))
				elif converterGeneratedName1 == BorderStyle.FixedSingle:
				
					exStyle &= (~cast(int, Win32.WindowExStyles.WS_EX_CLIENTEDGE))
					style |= cast(int, Win32.WindowStyles.WS_BORDER)
				elif converterGeneratedName1 == BorderStyle.None:
				
					style &= (~cast(int, Win32.WindowStyles.WS_BORDER))
					exStyle &= (~cast(int, Win32.WindowExStyles.WS_EX_CLIENTEDGE))
				
				// Set the styles using Win32 calls
				NativeMethods.SetWindowLong(MdiClient.Handle, cast(int, Win32.GetWindowLongIndex.GWL_STYLE), style)
				NativeMethods.SetWindowLong(MdiClient.Handle, cast(int, Win32.GetWindowLongIndex.GWL_EXSTYLE), exStyle)
				
				// Cause an update of the non-client area.
				UpdateStyles()

		
		public MdiClient as MdiClient:
			get:
				return m_mdiClient

		
		[Browsable(false)]
		public ParentForm as Form:
			get:
				return m_parentForm
			set:
				// If the ParentForm has previously been set,
				// unwire events connected to the old parent.
				if m_parentForm is not null:
					m_parentForm.HandleCreated -= ParentFormHandleCreated
					m_parentForm.MdiChildActivate -= ParentFormMdiChildActivate
				
				m_parentForm = value
				
				if m_parentForm is null:
					return 
				
				// If the parent form has not been created yet,
				// wait to initialize the MDI client until it is.
				if m_parentForm.IsHandleCreated:
					InitializeMdiClient()
					RefreshProperties()
				else:
					m_parentForm.HandleCreated += ParentFormHandleCreated
				
				m_parentForm.MdiChildActivate += ParentFormMdiChildActivate

		
		public Site as ISite:
			get:
				return m_site
			set:
				m_site = value
				
				if m_site is null:
					return 
				
				// If the component is dropped onto a form during design-time,
				// set the ParentForm property.
				host = (value.GetService(typeof(IDesignerHost)) as IDesignerHost)
				if host is not null:
					parent = (host.RootComponent as Form)
					if parent is not null:
						ParentForm = parent

		
		public def RenewMdiClient():
			// Reinitialize the MdiClient and its properties.
			InitializeMdiClient()
			RefreshProperties()

		
		public event Disposed as EventHandler

		
		public event HandleAssigned as EventHandler

		
		public event MdiChildActivate as EventHandler

		
		public event Layout as LayoutEventHandler

		
		protected virtual def OnHandleAssigned(e as EventArgs):
			HandleAssigned(self, e)
			// Raise the HandleAssigned event.

		
		protected virtual def OnMdiChildActivate(e as EventArgs):
			MdiChildActivate(self, e)
			// Raise the MdiChildActivate event

		
		protected virtual def OnLayout(e as LayoutEventArgs):
			Layout(self, e)
			// Raise the Layout event

		
		public event Paint as PaintEventHandler

		
		protected virtual def OnPaint(e as PaintEventArgs):
			Paint(self, e)
			// Raise the Paint event.

		
		protected override def WndProc(ref m as Message):
			converterGeneratedName2 = m.Msg
			if converterGeneratedName2 == cast(int, Win32.Msgs.WM_NCCALCSIZE):
				// If AutoScroll is set to false, hide the scrollbars when the control
				// calculates its non-client area.
				if not AutoScroll:
					NativeMethods.ShowScrollBar(m.HWnd, cast(int, Win32.ScrollBars.SB_BOTH), 0)
					/*false*/
			
			super.WndProc(m)

		
		private def ParentFormHandleCreated(sender as object, e as EventArgs):
			// The form has been created, unwire the event, and initialize the MdiClient.
			self.m_parentForm.HandleCreated -= ParentFormHandleCreated
			InitializeMdiClient()
			RefreshProperties()

		
		private def ParentFormMdiChildActivate(sender as object, e as EventArgs):
			OnMdiChildActivate(e)

		
		private def MdiClientLayout(sender as object, e as LayoutEventArgs):
			OnLayout(e)

		
		private def MdiClientHandleDestroyed(sender as object, e as EventArgs):
			// If the MdiClient handle has been released, drop the reference and
			// release the handle.
			if m_mdiClient is not null:
				m_mdiClient.HandleDestroyed -= MdiClientHandleDestroyed
				m_mdiClient = null
			
			ReleaseHandle()

		
		private def InitializeMdiClient():
			// If the mdiClient has previously been set, unwire events connected
			// to the old MDI.
			if MdiClient is not null:
				MdiClient.HandleDestroyed -= MdiClientHandleDestroyed
				MdiClient.Layout -= MdiClientLayout
			
			if ParentForm is null:
				return 
			
			// Get the MdiClient from the parent form.
			for control as Control in ParentForm.Controls:
				// If the form is an MDI container, it will contain an MdiClient control
				// just as it would any other control.
				
				m_mdiClient = (control as MdiClient)
				if m_mdiClient is null:
					continue 
				
				// Assign the MdiClient Handle to the NativeWindow.
				ReleaseHandle()
				AssignHandle(MdiClient.Handle)
				
				// Raise the HandleAssigned event.
				OnHandleAssigned(EventArgs.Empty)
				
				// Monitor the MdiClient for when its handle is destroyed.
				MdiClient.HandleDestroyed += MdiClientHandleDestroyed
				MdiClient.Layout += MdiClientLayout
				
				break 

		
		private def RefreshProperties():
			// Refresh all the properties
			BorderStyle = m_borderStyle
			AutoScroll = m_autoScroll

		
		private def UpdateStyles():
			// To show style changes, the non-client area must be repainted. Using the
			// control's Invalidate method does not affect the non-client area.
			// Instead use a Win32 call to signal the style has changed.
			NativeMethods.SetWindowPos(MdiClient.Handle, IntPtr.Zero, 0, 0, 0, 0, (((((Win32.FlagsSetWindowPos.SWP_NOACTIVATE | Win32.FlagsSetWindowPos.SWP_NOMOVE) | Win32.FlagsSetWindowPos.SWP_NOSIZE) | Win32.FlagsSetWindowPos.SWP_NOZORDER) | Win32.FlagsSetWindowPos.SWP_NOOWNERZORDER) | Win32.FlagsSetWindowPos.SWP_FRAMECHANGED))

	
	private m_mdiClientController as MdiClientController = null

	private def GetMdiClientController() as MdiClientController:
		if m_mdiClientController is null:
			m_mdiClientController = MdiClientController()
			m_mdiClientController.HandleAssigned += MdiClientHandleAssigned
			m_mdiClientController.MdiChildActivate += ParentFormMdiChildActivate
			m_mdiClientController.Layout += MdiClient_Layout
		
		return m_mdiClientController

	
	private def ParentFormMdiChildActivate(sender as object, e as EventArgs):
		if GetMdiClientController().ParentForm is null:
			return 
		
		content = (GetMdiClientController().ParentForm.ActiveMdiChild as IDockContent)
		if content is null:
			return 
		
		if (content.DockHandler.DockPanel == self) and (content.DockHandler.Pane is not null):
			content.DockHandler.Pane.ActiveContent = content

	
	private MdiClientExists as bool:
		get:
			return (GetMdiClientController().MdiClient is not null)

	
	private def SetMdiClientBounds(bounds as Rectangle):
		GetMdiClientController().MdiClient.Bounds = bounds

	
	private def SuspendMdiClientLayout():
		if GetMdiClientController().MdiClient is not null:
			GetMdiClientController().MdiClient.PerformLayout()

	
	private def ResumeMdiClientLayout(perform as bool):
		if GetMdiClientController().MdiClient is not null:
			GetMdiClientController().MdiClient.ResumeLayout(perform)

	
	private def PerformMdiClientLayout():
		if GetMdiClientController().MdiClient is not null:
			GetMdiClientController().MdiClient.PerformLayout()

	
	// Called when:
	// 1. DockPanel.DocumentStyle changed
	// 2. DockPanel.Visible changed
	// 3. MdiClientController.Handle assigned
	private def SetMdiClient():
		controller as MdiClientController = GetMdiClientController()
		
		if self.DocumentStyle == DocumentStyle.DockingMdi:
			controller.AutoScroll = false
			controller.BorderStyle = BorderStyle.None
			if MdiClientExists:
				controller.MdiClient.Dock = DockStyle.Fill
		elif (DocumentStyle == DocumentStyle.DockingSdi) or (DocumentStyle == DocumentStyle.DockingWindow):
			controller.AutoScroll = true
			controller.BorderStyle = BorderStyle.Fixed3D
			if MdiClientExists:
				controller.MdiClient.Dock = DockStyle.Fill
		elif self.DocumentStyle == DocumentStyle.SystemMdi:
			controller.AutoScroll = true
			controller.BorderStyle = BorderStyle.Fixed3D
			if controller.MdiClient is not null:
				controller.MdiClient.Dock = DockStyle.None
				controller.MdiClient.Bounds = SystemMdiClientBounds

	
	internal def RectangleToMdiClient(rect as Rectangle) as Rectangle:
		if MdiClientExists:
			return GetMdiClientController().MdiClient.RectangleToClient(rect)
		else:
			return Rectangle.Empty

