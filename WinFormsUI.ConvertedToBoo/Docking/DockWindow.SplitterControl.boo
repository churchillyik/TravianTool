
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections
import System.ComponentModel
import System.Drawing
import System.Windows.Forms

partial public class DockWindow:

	private class SplitterControl(SplitterBase):

		protected override SplitterSize as int:
			get:
				return Measures.SplitterSize

		
		protected override def StartDrag():
			window = (Parent as DockWindow)
			if window is null:
				return 
			
			window.DockPanel.BeginDrag(window, window.RectangleToScreen(Bounds))

