
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Windows.Forms

internal class DummyControl(Control):

	public def constructor():
		SetStyle(ControlStyles.Selectable, false)

