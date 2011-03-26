
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections
import System.ComponentModel
import System.Drawing
import System.Windows.Forms

internal class SplitterBase(Control):

	public def constructor():
		SetStyle(ControlStyles.Selectable, false)

	
	public override Dock as DockStyle:
		get:
			return super.Dock
		set:
			SuspendLayout()
			super.Dock = value
			
			if (Dock == DockStyle.Left) or (Dock == DockStyle.Right):
				Width = SplitterSize
			elif (Dock == DockStyle.Top) or (Dock == DockStyle.Bottom):
				Height = SplitterSize
			else:
				Bounds = Rectangle.Empty
			
			if (Dock == DockStyle.Left) or (Dock == DockStyle.Right):
				Cursor = Cursors.VSplit
			elif (Dock == DockStyle.Top) or (Dock == DockStyle.Bottom):
				Cursor = Cursors.HSplit
			else:
				Cursor = Cursors.Default
			
			ResumeLayout()

	
	protected virtual SplitterSize as int:
		get:
			return 0

	
	protected override def OnMouseDown(e as MouseEventArgs):
		super.OnMouseDown(e)
		
		if e.Button != MouseButtons.Left:
			return 
		
		StartDrag()

	
	protected virtual def StartDrag():
		pass

	
	protected override def WndProc(ref m as Message):
		// eat the WM_MOUSEACTIVATE message
		if m.Msg == cast(int, Win32.Msgs.WM_MOUSEACTIVATE):
			return 
		
		super.WndProc(m)

