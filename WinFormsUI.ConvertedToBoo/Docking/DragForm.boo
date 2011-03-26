
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms

// Inspired by Chris Sano's article:
// http://msdn.microsoft.com/smartclient/default.aspx?pull=/library/en-us/dnwinforms/html/colorpicker.asp
// In Sano's article, the DragForm needs to meet the following criteria:
// (1) it was not to show up in the task bar;
//     ShowInTaskBar = false
// (2) it needed to be the top-most window;
//     TopMost = true (not necessary here)
// (3) its icon could not show up in the ALT+TAB window if the user pressed ALT+TAB during a drag-and-drop;
//     FormBorderStyle = FormBorderStyle.None;
//     Create with WS_EX_TOOLWINDOW window style.
//     Compares with the solution in the artile by setting FormBorderStyle as FixedToolWindow,
//     and then clip the window caption and border, this way is much simplier.
// (4) it was not to steal focus from the application when displayed.
//     User Win32 ShowWindow API with SW_SHOWNOACTIVATE
// In addition, this form should only for display and therefore should act as transparent, otherwise
// WindowFromPoint will return this form, instead of the control beneath. Need BOTH of the following to
// achieve this (don't know why, spent hours to try it out :( ):
//  1. Enabled = false;
//  2. WM_NCHITTEST returns HTTRANSPARENT
internal class DragForm(Form):

	public def constructor():
		FormBorderStyle = FormBorderStyle.None
		ShowInTaskbar = false
		SetStyle(ControlStyles.Selectable, false)
		Enabled = false

	
	protected override CreateParams as CreateParams:
		get:
			createParams as CreateParams = super.CreateParams
			createParams.ExStyle |= cast(int, Win32.WindowExStyles.WS_EX_TOOLWINDOW)
			return createParams

	
	protected override def WndProc(ref m as Message):
		if m.Msg == cast(int, Win32.Msgs.WM_NCHITTEST):
			m.Result = cast(IntPtr, Win32.HitTest.HTTRANSPARENT)
			return 
		
		super.WndProc(m)

	
	public virtual def Show(bActivate as bool):
		if bActivate:
			Show()
		else:
			NativeMethods.ShowWindow(Handle, cast(int, Win32.ShowWindowStyles.SW_SHOWNOACTIVATE))

