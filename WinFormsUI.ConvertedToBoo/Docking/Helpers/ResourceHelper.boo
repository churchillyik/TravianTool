
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Reflection
import System.Resources
import System.Windows.Forms

internal static class ResourceHelper:

	private static _resourceManager as ResourceManager = null

	
	private static ResourceManager as ResourceManager:
		static get:
			if _resourceManager is null:
				_resourceManager = ResourceManager('WeifenLuo.WinFormsUI.Docking.Strings', typeof(ResourceHelper).Assembly)
			return _resourceManager

	
	
	public static def GetString(name as string) as string:
		return ResourceManager.GetString(name)

