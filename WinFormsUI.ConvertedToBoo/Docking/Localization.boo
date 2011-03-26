
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.ComponentModel

[AttributeUsage(AttributeTargets.All)]
internal final class LocalizedDescriptionAttribute(DescriptionAttribute):

	private m_initialized = false

	
	public def constructor(key as string):
		super(key)

	
	public override Description as string:
		get:
			if not m_initialized:
				key as string = super.Description
				DescriptionValue = ResourceHelper.GetString(key)
				if DescriptionValue is null:
					DescriptionValue = String.Empty
				
				m_initialized = true
			
			return DescriptionValue


[AttributeUsage(AttributeTargets.All)]
internal final class LocalizedCategoryAttribute(CategoryAttribute):

	public def constructor(key as string):
		super(key)

	
	protected override def GetLocalizedString(key as string) as string:
		return ResourceHelper.GetString(key)

