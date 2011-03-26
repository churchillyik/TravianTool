
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.ComponentModel
import System.Windows.Forms
import System.Drawing
import WeifenLuo.WinFormsUI.Docking
import System.IO
import System.Text
import System.Xml
import System.Globalization

partial internal class DockPanel:

	private static class Persistor:

		private static final ConfigFileVersion = '1.0'

		private static CompatibleConfigFileVersions as (string) = (of string: ,)

		
		private class DummyContent(DockContent):
			pass

		
		private struct DockPanelStruct:

			private m_dockLeftPortion as double

			public DockLeftPortion as double:
				get:
					return m_dockLeftPortion
				set:
					m_dockLeftPortion = value

			
			private m_dockRightPortion as double

			public DockRightPortion as double:
				get:
					return m_dockRightPortion
				set:
					m_dockRightPortion = value

			
			private m_dockTopPortion as double

			public DockTopPortion as double:
				get:
					return m_dockTopPortion
				set:
					m_dockTopPortion = value

			
			private m_dockBottomPortion as double

			public DockBottomPortion as double:
				get:
					return m_dockBottomPortion
				set:
					m_dockBottomPortion = value

			
			private m_indexActiveDocumentPane as int

			public IndexActiveDocumentPane as int:
				get:
					return m_indexActiveDocumentPane
				set:
					m_indexActiveDocumentPane = value

			
			private m_indexActivePane as int

			public IndexActivePane as int:
				get:
					return m_indexActivePane
				set:
					m_indexActivePane = value

		
		private struct ContentStruct:

			private m_persistString as string

			public PersistString as string:
				get:
					return m_persistString
				set:
					m_persistString = value

			
			private m_autoHidePortion as double

			public AutoHidePortion as double:
				get:
					return m_autoHidePortion
				set:
					m_autoHidePortion = value

			
			private m_isHidden as bool

			public IsHidden as bool:
				get:
					return m_isHidden
				set:
					m_isHidden = value

			
			private m_isFloat as bool

			public IsFloat as bool:
				get:
					return m_isFloat
				set:
					m_isFloat = value

		
		private struct PaneStruct:

			private m_dockState as DockState

			public DockState as DockState:
				get:
					return m_dockState
				set:
					m_dockState = value

			
			private m_indexActiveContent as int

			public IndexActiveContent as int:
				get:
					return m_indexActiveContent
				set:
					m_indexActiveContent = value

			
			private m_indexContents as (int)

			public IndexContents as (int):
				get:
					return m_indexContents
				set:
					m_indexContents = value

			
			private m_zOrderIndex as int

			public ZOrderIndex as int:
				get:
					return m_zOrderIndex
				set:
					m_zOrderIndex = value

		
		private struct NestedPane:

			private m_indexPane as int

			public IndexPane as int:
				get:
					return m_indexPane
				set:
					m_indexPane = value

			
			private m_indexPrevPane as int

			public IndexPrevPane as int:
				get:
					return m_indexPrevPane
				set:
					m_indexPrevPane = value

			
			private m_alignment as DockAlignment

			public Alignment as DockAlignment:
				get:
					return m_alignment
				set:
					m_alignment = value

			
			private m_proportion as double

			public Proportion as double:
				get:
					return m_proportion
				set:
					m_proportion = value

		
		private struct DockWindowStruct:

			private m_dockState as DockState

			public DockState as DockState:
				get:
					return m_dockState
				set:
					m_dockState = value

			
			private m_zOrderIndex as int

			public ZOrderIndex as int:
				get:
					return m_zOrderIndex
				set:
					m_zOrderIndex = value

			
			private m_nestedPanes as (NestedPane)

			public NestedPanes as (NestedPane):
				get:
					return m_nestedPanes
				set:
					m_nestedPanes = value

		
		private struct FloatWindowStruct:

			private m_bounds as Rectangle

			public Bounds as Rectangle:
				get:
					return m_bounds
				set:
					m_bounds = value

			
			private m_zOrderIndex as int

			public ZOrderIndex as int:
				get:
					return m_zOrderIndex
				set:
					m_zOrderIndex = value

			
			private m_nestedPanes as (NestedPane)

			public NestedPanes as (NestedPane):
				get:
					return m_nestedPanes
				set:
					m_nestedPanes = value

		
		public static def SaveAsXml(dockPanel as DockPanel, fileName as string):
			SaveAsXml(dockPanel, fileName, Encoding.Unicode)

		
		public static def SaveAsXml(dockPanel as DockPanel, fileName as string, encoding as Encoding):
			fs = FileStream(fileName, FileMode.Create)
			try:
				SaveAsXml(dockPanel, fs, encoding)
			ensure:
				fs.Close()

		
		public static def SaveAsXml(dockPanel as DockPanel, stream as Stream, encoding as Encoding):
			SaveAsXml(dockPanel, stream, encoding, false)

		
		public static def SaveAsXml(dockPanel as DockPanel, stream as Stream, encoding as Encoding, upstream as bool):
			
			// Use indenting for readability
			
			
			// Always begin file with identification and warning
			
			// Associate a version number with the root element so that future version of the code
			// will be able to be backwards compatible or at least recognise out of date versions
			
			// Contents
			
			// Panes
			
			// DockWindows
			status as NestedDockingStatus
			xmlOut = XmlTextWriter(stream, encoding)
			xmlOut.Formatting = Formatting.Indented
			if not upstream:
				xmlOut.WriteStartDocument()
			xmlOut.WriteComment(Strings.DockPanel_Persistor_XmlFileComment1)
			xmlOut.WriteComment(Strings.DockPanel_Persistor_XmlFileComment2)
			xmlOut.WriteStartElement('DockPanel')
			xmlOut.WriteAttributeString('FormatVersion', ConfigFileVersion)
			xmlOut.WriteAttributeString('DockLeftPortion', dockPanel.DockLeftPortion.ToString(CultureInfo.InvariantCulture))
			xmlOut.WriteAttributeString('DockRightPortion', dockPanel.DockRightPortion.ToString(CultureInfo.InvariantCulture))
			xmlOut.WriteAttributeString('DockTopPortion', dockPanel.DockTopPortion.ToString(CultureInfo.InvariantCulture))
			xmlOut.WriteAttributeString('DockBottomPortion', dockPanel.DockBottomPortion.ToString(CultureInfo.InvariantCulture))
			xmlOut.WriteAttributeString('ActiveDocumentPane', dockPanel.Panes.IndexOf(dockPanel.ActiveDocumentPane).ToString(CultureInfo.InvariantCulture))
			xmlOut.WriteAttributeString('ActivePane', dockPanel.Panes.IndexOf(dockPanel.ActivePane).ToString(CultureInfo.InvariantCulture))
			xmlOut.WriteStartElement('Contents')
			xmlOut.WriteAttributeString('Count', dockPanel.Contents.Count.ToString(CultureInfo.InvariantCulture))
			for content as IDockContent in dockPanel.Contents:
				xmlOut.WriteStartElement('Content')
				xmlOut.WriteAttributeString('ID', dockPanel.Contents.IndexOf(content).ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteAttributeString('PersistString', content.DockHandler.PersistString)
				xmlOut.WriteAttributeString('AutoHidePortion', content.DockHandler.AutoHidePortion.ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteAttributeString('IsHidden', content.DockHandler.IsHidden.ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteAttributeString('IsFloat', content.DockHandler.IsFloat.ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteEndElement()
			xmlOut.WriteEndElement()
			xmlOut.WriteStartElement('Panes')
			xmlOut.WriteAttributeString('Count', dockPanel.Panes.Count.ToString(CultureInfo.InvariantCulture))
			for pane as DockPane in dockPanel.Panes:
				xmlOut.WriteStartElement('Pane')
				xmlOut.WriteAttributeString('ID', dockPanel.Panes.IndexOf(pane).ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteAttributeString('DockState', pane.DockState.ToString())
				xmlOut.WriteAttributeString('ActiveContent', dockPanel.Contents.IndexOf(pane.ActiveContent).ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteStartElement('Contents')
				xmlOut.WriteAttributeString('Count', pane.Contents.Count.ToString(CultureInfo.InvariantCulture))
				for content as IDockContent in pane.Contents:
					xmlOut.WriteStartElement('Content')
					xmlOut.WriteAttributeString('ID', pane.Contents.IndexOf(content).ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteAttributeString('RefID', dockPanel.Contents.IndexOf(content).ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteEndElement()
				xmlOut.WriteEndElement()
				xmlOut.WriteEndElement()
			xmlOut.WriteEndElement()
			xmlOut.WriteStartElement('DockWindows')
			dockWindowId = 0
			for dw as DockWindow in dockPanel.DockWindows:
				xmlOut.WriteStartElement('DockWindow')
				xmlOut.WriteAttributeString('ID', dockWindowId.ToString(CultureInfo.InvariantCulture))
				dockWindowId += 1
				xmlOut.WriteAttributeString('DockState', dw.DockState.ToString())
				xmlOut.WriteAttributeString('ZOrderIndex', dockPanel.Controls.IndexOf(dw).ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteStartElement('NestedPanes')
				xmlOut.WriteAttributeString('Count', dw.NestedPanes.Count.ToString(CultureInfo.InvariantCulture))
				for pane as DockPane in dw.NestedPanes:
					xmlOut.WriteStartElement('Pane')
					xmlOut.WriteAttributeString('ID', dw.NestedPanes.IndexOf(pane).ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteAttributeString('RefID', dockPanel.Panes.IndexOf(pane).ToString(CultureInfo.InvariantCulture))
					status = pane.NestedDockingStatus
					xmlOut.WriteAttributeString('PrevPane', dockPanel.Panes.IndexOf(status.PreviousPane).ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteAttributeString('Alignment', status.Alignment.ToString())
					xmlOut.WriteAttributeString('Proportion', status.Proportion.ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteEndElement()
				xmlOut.WriteEndElement()
				xmlOut.WriteEndElement()
			xmlOut.WriteEndElement()
			
			// FloatWindows
			rectConverter = RectangleConverter()
			xmlOut.WriteStartElement('FloatWindows')
			xmlOut.WriteAttributeString('Count', dockPanel.FloatWindows.Count.ToString(CultureInfo.InvariantCulture))
			for fw as FloatWindow in dockPanel.FloatWindows:
				xmlOut.WriteStartElement('FloatWindow')
				xmlOut.WriteAttributeString('ID', dockPanel.FloatWindows.IndexOf(fw).ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteAttributeString('Bounds', rectConverter.ConvertToInvariantString(fw.Bounds))
				xmlOut.WriteAttributeString('ZOrderIndex', fw.DockPanel.FloatWindows.IndexOf(fw).ToString(CultureInfo.InvariantCulture))
				xmlOut.WriteStartElement('NestedPanes')
				xmlOut.WriteAttributeString('Count', fw.NestedPanes.Count.ToString(CultureInfo.InvariantCulture))
				for pane as DockPane in fw.NestedPanes:
					xmlOut.WriteStartElement('Pane')
					xmlOut.WriteAttributeString('ID', fw.NestedPanes.IndexOf(pane).ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteAttributeString('RefID', dockPanel.Panes.IndexOf(pane).ToString(CultureInfo.InvariantCulture))
					status = pane.NestedDockingStatus
					xmlOut.WriteAttributeString('PrevPane', dockPanel.Panes.IndexOf(status.PreviousPane).ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteAttributeString('Alignment', status.Alignment.ToString())
					xmlOut.WriteAttributeString('Proportion', status.Proportion.ToString(CultureInfo.InvariantCulture))
					xmlOut.WriteEndElement()
				xmlOut.WriteEndElement()
				xmlOut.WriteEndElement()
			xmlOut.WriteEndElement()
			//	</FloatWindows>
			xmlOut.WriteEndElement()
			
			if not upstream:
				xmlOut.WriteEndDocument()
				xmlOut.Close()
			else:
				xmlOut.Flush()

		
		public static def LoadFromXml(dockPanel as DockPanel, fileName as string, deserializeContent as DeserializeDockContent):
			fs = FileStream(fileName, FileMode.Open, FileAccess.Read)
			try:
				LoadFromXml(dockPanel, fs, deserializeContent)
			ensure:
				fs.Close()

		
		public static def LoadFromXml(dockPanel as DockPanel, stream as Stream, deserializeContent as DeserializeDockContent):
			LoadFromXml(dockPanel, stream, deserializeContent, true)

		
		private static def LoadContents(xmlIn as XmlTextReader) as (ContentStruct):
			countOfContents as int = Convert.ToInt32(xmlIn.GetAttribute('Count'), CultureInfo.InvariantCulture)
			contents as (ContentStruct) = array(ContentStruct, countOfContents)
			MoveToNextElement(xmlIn)
			for i in range(0, countOfContents):
				id as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
				if (xmlIn.Name != 'Content') or (id != i):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				
				contents[i].PersistString = xmlIn.GetAttribute('PersistString')
				contents[i].AutoHidePortion = Convert.ToDouble(xmlIn.GetAttribute('AutoHidePortion'), CultureInfo.InvariantCulture)
				contents[i].IsHidden = Convert.ToBoolean(xmlIn.GetAttribute('IsHidden'), CultureInfo.InvariantCulture)
				contents[i].IsFloat = Convert.ToBoolean(xmlIn.GetAttribute('IsFloat'), CultureInfo.InvariantCulture)
				MoveToNextElement(xmlIn)
			
			return contents

		
		private static def LoadPanes(xmlIn as XmlTextReader) as (PaneStruct):
			dockStateConverter = EnumConverter(typeof(DockState))
			countOfPanes as int = Convert.ToInt32(xmlIn.GetAttribute('Count'), CultureInfo.InvariantCulture)
			panes as (PaneStruct) = array(PaneStruct, countOfPanes)
			MoveToNextElement(xmlIn)
			for i in range(0, countOfPanes):
				id as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
				if (xmlIn.Name != 'Pane') or (id != i):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				
				panes[i].DockState = cast(DockState, dockStateConverter.ConvertFrom(xmlIn.GetAttribute('DockState')))
				panes[i].IndexActiveContent = Convert.ToInt32(xmlIn.GetAttribute('ActiveContent'), CultureInfo.InvariantCulture)
				panes[i].ZOrderIndex = (-1)
				
				MoveToNextElement(xmlIn)
				if xmlIn.Name != 'Contents':
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				countOfPaneContents as int = Convert.ToInt32(xmlIn.GetAttribute('Count'), CultureInfo.InvariantCulture)
				panes[i].IndexContents = array(int, countOfPaneContents)
				MoveToNextElement(xmlIn)
				for j in range(0, countOfPaneContents):
					id2 as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
					if (xmlIn.Name != 'Content') or (id2 != j):
						raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
					
					panes[i].IndexContents[j] = Convert.ToInt32(xmlIn.GetAttribute('RefID'), CultureInfo.InvariantCulture)
					MoveToNextElement(xmlIn)
			
			return panes

		
		private static def LoadDockWindows(xmlIn as XmlTextReader, dockPanel as DockPanel) as (DockWindowStruct):
			dockStateConverter = EnumConverter(typeof(DockState))
			dockAlignmentConverter = EnumConverter(typeof(DockAlignment))
			countOfDockWindows as int = dockPanel.DockWindows.Count
			dockWindows as (DockWindowStruct) = array(DockWindowStruct, countOfDockWindows)
			MoveToNextElement(xmlIn)
			for i in range(0, countOfDockWindows):
				id as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
				if (xmlIn.Name != 'DockWindow') or (id != i):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				
				dockWindows[i].DockState = cast(DockState, dockStateConverter.ConvertFrom(xmlIn.GetAttribute('DockState')))
				dockWindows[i].ZOrderIndex = Convert.ToInt32(xmlIn.GetAttribute('ZOrderIndex'), CultureInfo.InvariantCulture)
				MoveToNextElement(xmlIn)
				if (xmlIn.Name != 'DockList') and (xmlIn.Name != 'NestedPanes'):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				countOfNestedPanes as int = Convert.ToInt32(xmlIn.GetAttribute('Count'), CultureInfo.InvariantCulture)
				dockWindows[i].NestedPanes = array(NestedPane, countOfNestedPanes)
				MoveToNextElement(xmlIn)
				for j in range(0, countOfNestedPanes):
					id2 as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
					if (xmlIn.Name != 'Pane') or (id2 != j):
						raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
					dockWindows[i].NestedPanes[j].IndexPane = Convert.ToInt32(xmlIn.GetAttribute('RefID'), CultureInfo.InvariantCulture)
					dockWindows[i].NestedPanes[j].IndexPrevPane = Convert.ToInt32(xmlIn.GetAttribute('PrevPane'), CultureInfo.InvariantCulture)
					dockWindows[i].NestedPanes[j].Alignment = cast(DockAlignment, dockAlignmentConverter.ConvertFrom(xmlIn.GetAttribute('Alignment')))
					dockWindows[i].NestedPanes[j].Proportion = Convert.ToDouble(xmlIn.GetAttribute('Proportion'), CultureInfo.InvariantCulture)
					MoveToNextElement(xmlIn)
			
			return dockWindows

		
		private static def LoadFloatWindows(xmlIn as XmlTextReader) as (FloatWindowStruct):
			dockAlignmentConverter = EnumConverter(typeof(DockAlignment))
			rectConverter = RectangleConverter()
			countOfFloatWindows as int = Convert.ToInt32(xmlIn.GetAttribute('Count'), CultureInfo.InvariantCulture)
			floatWindows as (FloatWindowStruct) = array(FloatWindowStruct, countOfFloatWindows)
			MoveToNextElement(xmlIn)
			for i in range(0, countOfFloatWindows):
				id as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
				if (xmlIn.Name != 'FloatWindow') or (id != i):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				
				floatWindows[i].Bounds = cast(Rectangle, rectConverter.ConvertFromInvariantString(xmlIn.GetAttribute('Bounds')))
				floatWindows[i].ZOrderIndex = Convert.ToInt32(xmlIn.GetAttribute('ZOrderIndex'), CultureInfo.InvariantCulture)
				MoveToNextElement(xmlIn)
				if (xmlIn.Name != 'DockList') and (xmlIn.Name != 'NestedPanes'):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
				countOfNestedPanes as int = Convert.ToInt32(xmlIn.GetAttribute('Count'), CultureInfo.InvariantCulture)
				floatWindows[i].NestedPanes = array(NestedPane, countOfNestedPanes)
				MoveToNextElement(xmlIn)
				for j in range(0, countOfNestedPanes):
					id2 as int = Convert.ToInt32(xmlIn.GetAttribute('ID'), CultureInfo.InvariantCulture)
					if (xmlIn.Name != 'Pane') or (id2 != j):
						raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
					floatWindows[i].NestedPanes[j].IndexPane = Convert.ToInt32(xmlIn.GetAttribute('RefID'), CultureInfo.InvariantCulture)
					floatWindows[i].NestedPanes[j].IndexPrevPane = Convert.ToInt32(xmlIn.GetAttribute('PrevPane'), CultureInfo.InvariantCulture)
					floatWindows[i].NestedPanes[j].Alignment = cast(DockAlignment, dockAlignmentConverter.ConvertFrom(xmlIn.GetAttribute('Alignment')))
					floatWindows[i].NestedPanes[j].Proportion = Convert.ToDouble(xmlIn.GetAttribute('Proportion'), CultureInfo.InvariantCulture)
					MoveToNextElement(xmlIn)
			
			return floatWindows

		
		public static def LoadFromXml(dockPanel as DockPanel, stream as Stream, deserializeContent as DeserializeDockContent, closeStream as bool):
			
			
			
			
			
			
			// Load Contents
			
			// Load Panes
			
			// Load DockWindows
			
			// Load FloatWindows
			
			
			
			
			// Set DockWindow ZOrders
			
			
			// Create Contents
			content as IDockContent
			
			// Create panes
			
			// Assign Panes to DockWindows
			proportion as double
			alignment as DockAlignment
			prevPane as DockPane
			indexPrevPane as int
			pane as DockPane
			indexPane as int
			if dockPanel.Contents.Count != 0:
				raise InvalidOperationException(Strings.DockPanel_LoadFromXml_AlreadyInitialized)
			xmlIn = XmlTextReader(stream)
			xmlIn.WhitespaceHandling = WhitespaceHandling.None
			xmlIn.MoveToContent()
			while not xmlIn.Name.Equals('DockPanel'):
				if not MoveToNextElement(xmlIn):
					raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
			formatVersion as string = xmlIn.GetAttribute('FormatVersion')
			if not IsFormatVersionValid(formatVersion):
				raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidFormatVersion)
			dockPanelStruct = DockPanelStruct()
			dockPanelStruct.DockLeftPortion = Convert.ToDouble(xmlIn.GetAttribute('DockLeftPortion'), CultureInfo.InvariantCulture)
			dockPanelStruct.DockRightPortion = Convert.ToDouble(xmlIn.GetAttribute('DockRightPortion'), CultureInfo.InvariantCulture)
			dockPanelStruct.DockTopPortion = Convert.ToDouble(xmlIn.GetAttribute('DockTopPortion'), CultureInfo.InvariantCulture)
			dockPanelStruct.DockBottomPortion = Convert.ToDouble(xmlIn.GetAttribute('DockBottomPortion'), CultureInfo.InvariantCulture)
			dockPanelStruct.IndexActiveDocumentPane = Convert.ToInt32(xmlIn.GetAttribute('ActiveDocumentPane'), CultureInfo.InvariantCulture)
			dockPanelStruct.IndexActivePane = Convert.ToInt32(xmlIn.GetAttribute('ActivePane'), CultureInfo.InvariantCulture)
			MoveToNextElement(xmlIn)
			if xmlIn.Name != 'Contents':
				raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
			contents as (ContentStruct) = LoadContents(xmlIn)
			if xmlIn.Name != 'Panes':
				raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
			panes as (PaneStruct) = LoadPanes(xmlIn)
			if xmlIn.Name != 'DockWindows':
				raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
			dockWindows as (DockWindowStruct) = LoadDockWindows(xmlIn, dockPanel)
			if xmlIn.Name != 'FloatWindows':
				raise ArgumentException(Strings.DockPanel_LoadFromXml_InvalidXmlFormat)
			floatWindows as (FloatWindowStruct) = LoadFloatWindows(xmlIn)
			if closeStream:
				xmlIn.Close()
			dockPanel.SuspendLayout(true)
			dockPanel.DockLeftPortion = dockPanelStruct.DockLeftPortion
			dockPanel.DockRightPortion = dockPanelStruct.DockRightPortion
			dockPanel.DockTopPortion = dockPanelStruct.DockTopPortion
			dockPanel.DockBottomPortion = dockPanelStruct.DockBottomPortion
			prevMaxDockWindowZOrder as int = int.MaxValue
			for i in range(0, dockWindows.Length):
				maxDockWindowZOrder as int = (-1)
				index as int = (-1)
				for j in range(0, dockWindows.Length):
					if (dockWindows[j].ZOrderIndex > maxDockWindowZOrder) and (dockWindows[j].ZOrderIndex < prevMaxDockWindowZOrder):
						maxDockWindowZOrder = dockWindows[j].ZOrderIndex
						index = j
				dockPanel.DockWindows[dockWindows[index].DockState].BringToFront()
				prevMaxDockWindowZOrder = maxDockWindowZOrder
			for i in range(0, contents.Length):
				content = deserializeContent(contents[i].PersistString)
				if content is null:
					content = DummyContent()
				content.DockHandler.DockPanel = dockPanel
				content.DockHandler.AutoHidePortion = contents[i].AutoHidePortion
				content.DockHandler.IsHidden = true
				content.DockHandler.IsFloat = contents[i].IsFloat
			for i in range(0, panes.Length):
				pane = null
				for j in range(0, panes[i].IndexContents.Length):
					content = dockPanel.Contents[panes[i].IndexContents[j]]
					if j == 0:
						pane = dockPanel.DockPaneFactory.CreateDockPane(content, panes[i].DockState, false)
					elif panes[i].DockState == DockState.Float:
						content.DockHandler.FloatPane = pane
					else:
						content.DockHandler.PanelPane = pane
			for i in range(0, dockWindows.Length):
				for j in range(0, dockWindows[i].NestedPanes.Length):
					dw as DockWindow = dockPanel.DockWindows[dockWindows[i].DockState]
					indexPane = dockWindows[i].NestedPanes[j].IndexPane
					pane = dockPanel.Panes[indexPane]
					indexPrevPane = dockWindows[i].NestedPanes[j].IndexPrevPane
					prevPane = (dw.NestedPanes.GetDefaultPreviousPane(pane) if (indexPrevPane == (-1)) else dockPanel.Panes[indexPrevPane])
					alignment = dockWindows[i].NestedPanes[j].Alignment
					proportion = dockWindows[i].NestedPanes[j].Proportion
					pane.DockTo(dw, prevPane, alignment, proportion)
					if panes[indexPane].DockState == dw.DockState:
						panes[indexPane].ZOrderIndex = dockWindows[i].ZOrderIndex
			for i in range(0, floatWindows.Length):
			
			// Create float windows
				fw as FloatWindow = null
				for j in range(0, floatWindows[i].NestedPanes.Length):
					indexPane = floatWindows[i].NestedPanes[j].IndexPane
					pane = dockPanel.Panes[indexPane]
					if j == 0:
						fw = dockPanel.FloatWindowFactory.CreateFloatWindow(dockPanel, pane, floatWindows[i].Bounds)
					else:
						indexPrevPane = floatWindows[i].NestedPanes[j].IndexPrevPane
						prevPane = (null if (indexPrevPane == (-1)) else dockPanel.Panes[indexPrevPane])
						alignment = floatWindows[i].NestedPanes[j].Alignment
						proportion = floatWindows[i].NestedPanes[j].Proportion
						pane.DockTo(fw, prevPane, alignment, proportion)
						if panes[indexPane].DockState == fw.DockState:
							panes[indexPane].ZOrderIndex = floatWindows[i].ZOrderIndex
			
			// sort IDockContent by its Pane's ZOrder
			sortedContents as (int) = null
			if contents.Length > 0:
				sortedContents = array(int, contents.Length)
				for i in range(0, contents.Length):
					sortedContents[i] = i
				
				lastDocument as int = contents.Length
				for i in range(0, (contents.Length - 1)):
					for j in range((i + 1), contents.Length):
						pane1 as DockPane = dockPanel.Contents[sortedContents[i]].DockHandler.Pane
						ZOrderIndex1 as int = (0 if (pane1 is null) else panes[dockPanel.Panes.IndexOf(pane1)].ZOrderIndex)
						pane2 as DockPane = dockPanel.Contents[sortedContents[j]].DockHandler.Pane
						ZOrderIndex2 as int = (0 if (pane2 is null) else panes[dockPanel.Panes.IndexOf(pane2)].ZOrderIndex)
						if ZOrderIndex1 > ZOrderIndex2:
							temp as int = sortedContents[i]
							sortedContents[i] = sortedContents[j]
							sortedContents[j] = temp
			for i in range(0, contents.Length):
			
			// show non-document IDockContent first to avoid screen flickers
				content = dockPanel.Contents[sortedContents[i]]
				if (content.DockHandler.Pane is not null) and (content.DockHandler.Pane.DockState != DockState.Document):
					content.DockHandler.IsHidden = contents[sortedContents[i]].IsHidden
			for i in range(0, contents.Length):
			
			// after all non-document IDockContent, show document IDockContent
				content = dockPanel.Contents[sortedContents[i]]
				if (content.DockHandler.Pane is not null) and (content.DockHandler.Pane.DockState == DockState.Document):
					content.DockHandler.IsHidden = contents[sortedContents[i]].IsHidden
			for i in range(0, panes.Length):
			
				dockPanel.Panes[i].ActiveContent = (null if (panes[i].IndexActiveContent == (-1)) else dockPanel.Contents[panes[i].IndexActiveContent])
			
			if dockPanelStruct.IndexActiveDocumentPane != (-1):
				dockPanel.Panes[dockPanelStruct.IndexActiveDocumentPane].Activate()
			
			if dockPanelStruct.IndexActivePane != (-1):
				dockPanel.Panes[dockPanelStruct.IndexActivePane].Activate()
			for i in range((dockPanel.Contents.Count - 1), -1, -1):
			
				if dockPanel.Contents[i] isa DummyContent:
					dockPanel.Contents[i].DockHandler.Form.Close()
			
			dockPanel.ResumeLayout(true, true)

		
		private static def MoveToNextElement(xmlIn as XmlTextReader) as bool:
			if not xmlIn.Read():
				return false
			
			while xmlIn.NodeType == XmlNodeType.EndElement:
				if not xmlIn.Read():
					return false
			
			return true

		
		private static def IsFormatVersionValid(formatVersion as string) as bool:
			if formatVersion == ConfigFileVersion:
				return true
			
			for s as string in CompatibleConfigFileVersions:
				if s == formatVersion:
					return true
			
			return false

	
	public def SaveAsXml(fileName as string):
		Persistor.SaveAsXml(self, fileName)

	
	public def SaveAsXml(fileName as string, encoding as Encoding):
		Persistor.SaveAsXml(self, fileName, encoding)

	
	public def SaveAsXml(stream as Stream, encoding as Encoding):
		Persistor.SaveAsXml(self, stream, encoding)

	
	public def SaveAsXml(stream as Stream, encoding as Encoding, upstream as bool):
		Persistor.SaveAsXml(self, stream, encoding, upstream)

	
	public def LoadFromXml(fileName as string, deserializeContent as DeserializeDockContent):
		Persistor.LoadFromXml(self, fileName, deserializeContent)

	
	public def LoadFromXml(stream as Stream, deserializeContent as DeserializeDockContent):
		Persistor.LoadFromXml(self, stream, deserializeContent)

	
	public def LoadFromXml(stream as Stream, deserializeContent as DeserializeDockContent, closeStream as bool):
		Persistor.LoadFromXml(self, stream, deserializeContent, closeStream)

