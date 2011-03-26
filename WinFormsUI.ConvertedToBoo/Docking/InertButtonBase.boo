
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Collections.Generic
import System.Text
import System.Windows.Forms
import System.Drawing
import System.Drawing.Imaging

internal abstract class InertButtonBase(Control):

	protected def constructor():
		SetStyle(ControlStyles.SupportsTransparentBackColor, true)
		BackColor = Color.Transparent

	
	public abstract Image as Bitmap:
		get:
			pass

	
	private m_isMouseOver = false

	protected IsMouseOver as bool:
		get:
			return m_isMouseOver
		private set:
			if m_isMouseOver == value:
				return 
			
			m_isMouseOver = value
			Invalidate()

	
	protected override DefaultSize as Size:
		get:
			return Resources.DockPane_Close.Size

	
	protected override def OnMouseMove(e as MouseEventArgs):
		super.OnMouseMove(e)
		over as bool = ClientRectangle.Contains(e.X, e.Y)
		if IsMouseOver != over:
			IsMouseOver = over

	
	protected override def OnMouseEnter(e as EventArgs):
		super.OnMouseEnter(e)
		if not IsMouseOver:
			IsMouseOver = true

	
	protected override def OnMouseLeave(e as EventArgs):
		super.OnMouseLeave(e)
		if IsMouseOver:
			IsMouseOver = false

	
	protected override def OnPaint(e as PaintEventArgs):
		if IsMouseOver and Enabled:
			using pen = Pen(ForeColor):
				e.Graphics.DrawRectangle(pen, Rectangle.Inflate(ClientRectangle, -1, -1))
		
		using imageAttributes = ImageAttributes():
			colorMap as (ColorMap) = array(ColorMap, 2)
			colorMap[0] = ColorMap()
			colorMap[0].OldColor = Color.FromArgb(0, 0, 0)
			colorMap[0].NewColor = ForeColor
			colorMap[1] = ColorMap()
			colorMap[1].OldColor = Image.GetPixel(0, 0)
			colorMap[1].NewColor = Color.Transparent
			
			imageAttributes.SetRemapTable(colorMap)
			
			e.Graphics.DrawImage(Image, Rectangle(0, 0, Image.Width, Image.Height), 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel, imageAttributes)
		
		super.OnPaint(e)

	
	public def RefreshChanges():
		if IsDisposed:
			return 
		
		mouseOver as bool = ClientRectangle.Contains(PointToClient(Control.MousePosition))
		if mouseOver != IsMouseOver:
			IsMouseOver = mouseOver
		
		OnRefreshChanges()

	
	protected virtual def OnRefreshChanges():
		pass

