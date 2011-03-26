
namespace WeifenLuo.WinFormsUI.Docking

import System

public class DockContentEventArgs(EventArgs):

	private m_content as IDockContent

	
	public def constructor(content as IDockContent):
		m_content = content

	
	public Content as IDockContent:
		get:
			return m_content

