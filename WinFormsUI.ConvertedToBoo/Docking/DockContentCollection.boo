
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections
import System.Collections.Generic
import System.Collections.ObjectModel

public class DockContentCollection(ReadOnlyCollection[of IDockContent]):

	private static _emptyList as List[of IDockContent] = List[of IDockContent](0)

	
	internal def constructor():
		super(List[of IDockContent]())

	
	internal def constructor(pane as DockPane):
		super(_emptyList)
		m_dockPane = pane

	
	private m_dockPane as DockPane = null

	private DockPane as DockPane:
		get:
			return m_dockPane

	
	public self[index as int] as IDockContent:
		get:
			if DockPane is null:
				return (Items[index] as IDockContent)
			else:
				return GetVisibleContent(index)

	
	internal def Add(content as IDockContent) as int:
		#if DEBUG
		if DockPane is not null:
			raise InvalidOperationException()
		#endif
		
		if Contains(content):
			return IndexOf(content)
		
		Items.Add(content)
		return (Count - 1)

	
	internal def AddAt(content as IDockContent, index as int):
		#if DEBUG
		if DockPane is not null:
			raise InvalidOperationException()
		#endif
		
		if (index < 0) or (index > (Items.Count - 1)):
			return 
		
		if Contains(content):
			return 
		
		Items.Insert(index, content)

	
	public def Contains(content as IDockContent) as bool:
		if DockPane is null:
			return Items.Contains(content)
		else:
			return (GetIndexOfVisibleContents(content) != (-1))

	
	public Count as int:
		get:
			if DockPane is null:
				return super.Count
			else:
				return CountOfVisibleContents

	
	public def IndexOf(content as IDockContent) as int:
		if DockPane is null:
			if not Contains(content):
				return (-1)
			else:
				return Items.IndexOf(content)
		else:
			return GetIndexOfVisibleContents(content)

	
	internal def Remove(content as IDockContent):
		if DockPane is not null:
			raise InvalidOperationException()
		
		if not Contains(content):
			return 
		
		Items.Remove(content)

	
	private CountOfVisibleContents as int:
		get:
			#if DEBUG
			if DockPane is null:
				raise InvalidOperationException()
			#endif
			
			count = 0
			for content as IDockContent in DockPane.Contents:
				if content.DockHandler.DockState == DockPane.DockState:
					count += 1
			return count

	
	private def GetVisibleContent(index as int) as IDockContent:
		#if DEBUG
		if DockPane is null:
			raise InvalidOperationException()
		#endif
		
		currentIndex as int = (-1)
		for content as IDockContent in DockPane.Contents:
			if content.DockHandler.DockState == DockPane.DockState:
				currentIndex += 1
			
			if currentIndex == index:
				return content
		raise ArgumentOutOfRangeException()

	
	private def GetIndexOfVisibleContents(content as IDockContent) as int:
		#if DEBUG
		if DockPane is null:
			raise InvalidOperationException()
		#endif
		
		if content is null:
			return (-1)
		
		index as int = (-1)
		for c as IDockContent in DockPane.Contents:
			if c.DockHandler.DockState == DockPane.DockState:
				index += 1
				
				if c == content:
					return index
		return (-1)

