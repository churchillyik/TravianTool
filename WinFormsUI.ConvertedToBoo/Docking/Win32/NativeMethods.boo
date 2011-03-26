
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Runtime.InteropServices
import System.Diagnostics.CodeAnalysis
import WeifenLuo.WinFormsUI.Docking.Win32

internal static class NativeMethods:

	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def DragDetect(hWnd as IntPtr, pt as Point) as bool:
		pass

	
	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def GetFocus() as IntPtr:
		pass

	
	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def SetFocus(hWnd as IntPtr) as IntPtr:
		pass

	
	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def PostMessage(hWnd as IntPtr, Msg as int, wParam as uint, lParam as uint) as bool:
		pass

	
	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def SendMessage(hWnd as IntPtr, Msg as int, wParam as uint, lParam as uint) as uint:
		pass

	
	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def ShowWindow(hWnd as IntPtr, cmdShow as short) as int:
		pass

	
	[DllImport('User32.dll', CharSet: CharSet.Auto)]
	public static def SetWindowPos(hWnd as IntPtr, hWndAfter as IntPtr, X as int, Y as int, Width as int, Height as int, flags as FlagsSetWindowPos) as int:
		pass

	
	[DllImport('user32.dll', CharSet: CharSet.Auto)]
	public static def GetWindowLong(hWnd as IntPtr, Index as int) as int:
		pass

	
	[DllImport('user32.dll', CharSet: CharSet.Auto)]
	public static def SetWindowLong(hWnd as IntPtr, Index as int, Value as int) as int:
		pass

	
	[DllImport('user32.dll', CharSet: CharSet.Auto)]
	public static def ShowScrollBar(hWnd as IntPtr, wBar as int, bShow as int) as int:
		pass

	
	//*********************************
	// FxCop bug, suppress the message
	//*********************************
	[DllImport('user32.dll', CharSet: CharSet.Auto)]
	[SuppressMessage('Microsoft.Portability', 'CA1901:PInvokeDeclarationsShouldBePortable', MessageId: '0')]
	public static def WindowFromPoint(point as Point) as IntPtr:
		pass

	
	[DllImport('Kernel32.dll', CharSet: CharSet.Auto)]
	public static def GetCurrentThreadId() as int:
		pass

	
	public static callable HookProc(code as int, wParam as IntPtr, lParam as IntPtr) as IntPtr
	
	[DllImport('user32.dll')]
	public static def SetWindowsHookEx(code as Win32.HookType, func as HookProc, hInstance as IntPtr, threadID as int) as IntPtr:
		pass

	
	[DllImport('user32.dll')]
	public static def UnhookWindowsHookEx(hhook as IntPtr) as int:
		pass

	
	[DllImport('user32.dll')]
	public static def CallNextHookEx(hhook as IntPtr, code as int, wParam as IntPtr, lParam as IntPtr) as IntPtr:
		pass

