
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Collections.ObjectModel
import System.Drawing
import System.Windows.Forms

public class FloatWindowCollection(ReadOnlyCollection[of FloatWindow]):

	internal def constructor():
		super(List[of FloatWindow]())

	
	internal def Add(fw as FloatWindow) as int:
		if Items.Contains(fw):
			return Items.IndexOf(fw)
		
		Items.Add(fw)
		return (Count - 1)

	
	internal def Dispose():
		for i in range((Count - 1), -1, -1):
			self[i].Close()

	
	internal def Remove(fw as FloatWindow):
		Items.Remove(fw)

	
	internal def BringWindowToFront(fw as FloatWindow):
		Items.Remove(fw)
		Items.Add(fw)

