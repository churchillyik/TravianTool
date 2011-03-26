
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.ObjectModel
import System.Collections.Generic
import System.Drawing
import System.Windows.Forms

public class DockPaneCollection(ReadOnlyCollection[of DockPane]):

	internal def constructor():
		super(List[of DockPane]())

	
	internal def Add(pane as DockPane) as int:
		if Items.Contains(pane):
			return Items.IndexOf(pane)
		
		Items.Add(pane)
		return (Count - 1)

	
	internal def AddAt(pane as DockPane, index as int):
		if (index < 0) or (index > (Items.Count - 1)):
			return 
		
		if Contains(pane):
			return 
		
		Items.Insert(index, pane)

	
	internal def Dispose():
		for i in range((Count - 1), -1, -1):
			self[i].Close()

	
	internal def Remove(pane as DockPane):
		Items.Remove(pane)

