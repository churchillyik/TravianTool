
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.ComponentModel
import System.Drawing
import System.Drawing.Design
import System.Windows.Forms
import System.Windows.Forms.Design

internal class DockAreasEditor(UITypeEditor):

	private class DockAreasEditorControl(System.Windows.Forms.UserControl):

		private checkBoxFloat as CheckBox

		private checkBoxDockLeft as CheckBox

		private checkBoxDockRight as CheckBox

		private checkBoxDockTop as CheckBox

		private checkBoxDockBottom as CheckBox

		private checkBoxDockFill as CheckBox

		private m_oldDockAreas as DockAreas

		
		public DockAreas as DockAreas:
			get:
				dockAreas as DockAreas = 0
				if checkBoxFloat.Checked:
					dockAreas |= DockAreas.Float
				if checkBoxDockLeft.Checked:
					dockAreas |= DockAreas.DockLeft
				if checkBoxDockRight.Checked:
					dockAreas |= DockAreas.DockRight
				if checkBoxDockTop.Checked:
					dockAreas |= DockAreas.DockTop
				if checkBoxDockBottom.Checked:
					dockAreas |= DockAreas.DockBottom
				if checkBoxDockFill.Checked:
					dockAreas |= DockAreas.Document
				
				if dockAreas == 0:
					return m_oldDockAreas
				else:
					return dockAreas

		
		public def constructor():
			checkBoxFloat = CheckBox()
			checkBoxDockLeft = CheckBox()
			checkBoxDockRight = CheckBox()
			checkBoxDockTop = CheckBox()
			checkBoxDockBottom = CheckBox()
			checkBoxDockFill = CheckBox()
			
			SuspendLayout()
			
			checkBoxFloat.Appearance = Appearance.Button
			checkBoxFloat.Dock = DockStyle.Top
			checkBoxFloat.Height = 24
			checkBoxFloat.Text = Strings.DockAreaEditor_FloatCheckBoxText
			checkBoxFloat.TextAlign = ContentAlignment.MiddleCenter
			checkBoxFloat.FlatStyle = FlatStyle.System
			
			checkBoxDockLeft.Appearance = System.Windows.Forms.Appearance.Button
			checkBoxDockLeft.Dock = System.Windows.Forms.DockStyle.Left
			checkBoxDockLeft.Width = 24
			checkBoxDockLeft.FlatStyle = FlatStyle.System
			
			checkBoxDockRight.Appearance = System.Windows.Forms.Appearance.Button
			checkBoxDockRight.Dock = System.Windows.Forms.DockStyle.Right
			checkBoxDockRight.Width = 24
			checkBoxDockRight.FlatStyle = FlatStyle.System
			
			checkBoxDockTop.Appearance = System.Windows.Forms.Appearance.Button
			checkBoxDockTop.Dock = System.Windows.Forms.DockStyle.Top
			checkBoxDockTop.Height = 24
			checkBoxDockTop.FlatStyle = FlatStyle.System
			
			checkBoxDockBottom.Appearance = System.Windows.Forms.Appearance.Button
			checkBoxDockBottom.Dock = System.Windows.Forms.DockStyle.Bottom
			checkBoxDockBottom.Height = 24
			checkBoxDockBottom.FlatStyle = FlatStyle.System
			
			checkBoxDockFill.Appearance = System.Windows.Forms.Appearance.Button
			checkBoxDockFill.Dock = System.Windows.Forms.DockStyle.Fill
			checkBoxDockFill.FlatStyle = FlatStyle.System
			
			self.Controls.AddRange((of Control: checkBoxDockFill, checkBoxDockBottom, checkBoxDockTop, checkBoxDockRight, checkBoxDockLeft, checkBoxFloat))
			
			Size = System.Drawing.Size(160, 144)
			BackColor = SystemColors.Control
			ResumeLayout()

		
		public def SetStates(dockAreas as DockAreas):
			m_oldDockAreas = dockAreas
			if (dockAreas & DockAreas.DockLeft) != 0:
				checkBoxDockLeft.Checked = true
			if (dockAreas & DockAreas.DockRight) != 0:
				checkBoxDockRight.Checked = true
			if (dockAreas & DockAreas.DockTop) != 0:
				checkBoxDockTop.Checked = true
			if (dockAreas & DockAreas.DockTop) != 0:
				checkBoxDockTop.Checked = true
			if (dockAreas & DockAreas.DockBottom) != 0:
				checkBoxDockBottom.Checked = true
			if (dockAreas & DockAreas.Document) != 0:
				checkBoxDockFill.Checked = true
			if (dockAreas & DockAreas.Float) != 0:
				checkBoxFloat.Checked = true

	
	private m_ui as DockAreasEditor.DockAreasEditorControl = null

	
	public override def GetEditStyle(context as ITypeDescriptorContext) as UITypeEditorEditStyle:
		return UITypeEditorEditStyle.DropDown

	
	public override def EditValue(context as ITypeDescriptorContext, sp as IServiceProvider, value as object) as object:
		if m_ui is null:
			m_ui = DockAreasEditor.DockAreasEditorControl()
		
		m_ui.SetStates(cast(DockAreas, value))
		
		edSvc = cast(IWindowsFormsEditorService, sp.GetService(typeof(IWindowsFormsEditorService)))
		edSvc.DropDownControl(m_ui)
		
		return m_ui.DockAreas

