
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Windows.Forms

internal static class Win32Helper:

	public static def ControlAtPoint(pt as Point) as Control:
		return Control.FromChildHandle(NativeMethods.WindowFromPoint(pt))

	
	public static def MakeLong(low as int, high as int) as uint:
		return cast(uint, ((high << 16) + low))

