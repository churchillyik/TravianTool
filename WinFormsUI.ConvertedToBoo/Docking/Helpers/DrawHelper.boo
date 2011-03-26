
namespace WeifenLuo.WinFormsUI.Docking

import System
import System.Drawing
import System.Drawing.Drawing2D
import System.Drawing.Imaging
import System.Windows.Forms

internal static class DrawHelper:

	public static def RtlTransform(control as Control, point as Point) as Point:
		if control.RightToLeft != RightToLeft.Yes:
			return point
		else:
			return Point((control.Right - point.X), point.Y)

	
	public static def RtlTransform(control as Control, rectangle as Rectangle) as Rectangle:
		if control.RightToLeft != RightToLeft.Yes:
			return rectangle
		else:
			return Rectangle((control.ClientRectangle.Right - rectangle.Right), rectangle.Y, rectangle.Width, rectangle.Height)

	
	public static def GetRoundedCornerTab(graphicsPath as GraphicsPath, rect as Rectangle, upCorner as bool) as GraphicsPath:
		if graphicsPath is null:
			graphicsPath = GraphicsPath()
		else:
			graphicsPath.Reset()
		
		curveSize = 6
		if upCorner:
			graphicsPath.AddLine(rect.Left, rect.Bottom, rect.Left, (rect.Top + (curveSize / 2)))
			graphicsPath.AddArc(Rectangle(rect.Left, rect.Top, curveSize, curveSize), 180, 90)
			graphicsPath.AddLine((rect.Left + (curveSize / 2)), rect.Top, (rect.Right - (curveSize / 2)), rect.Top)
			graphicsPath.AddArc(Rectangle((rect.Right - curveSize), rect.Top, curveSize, curveSize), -90, 90)
			graphicsPath.AddLine(rect.Right, (rect.Top + (curveSize / 2)), rect.Right, rect.Bottom)
		else:
			graphicsPath.AddLine(rect.Right, rect.Top, rect.Right, (rect.Bottom - (curveSize / 2)))
			graphicsPath.AddArc(Rectangle((rect.Right - curveSize), (rect.Bottom - curveSize), curveSize, curveSize), 0, 90)
			graphicsPath.AddLine((rect.Right - (curveSize / 2)), rect.Bottom, (rect.Left + (curveSize / 2)), rect.Bottom)
			graphicsPath.AddArc(Rectangle(rect.Left, (rect.Bottom - curveSize), curveSize, curveSize), 90, 90)
			graphicsPath.AddLine(rect.Left, (rect.Bottom - (curveSize / 2)), rect.Left, rect.Top)
		
		return graphicsPath

	
	public static def CalculateGraphicsPathFromBitmap(bitmap as Bitmap) as GraphicsPath:
		return CalculateGraphicsPathFromBitmap(bitmap, Color.Empty)

	
	// From http://edu.cnzz.cn/show_3281.html
	public static def CalculateGraphicsPathFromBitmap(bitmap as Bitmap, colorTransparent as Color) as GraphicsPath:
		graphicsPath = GraphicsPath()
		if colorTransparent == Color.Empty:
			colorTransparent = bitmap.GetPixel(0, 0)
		for row in range(0, bitmap.Height):
		
			colOpaquePixel = 0
			for col in range(0, bitmap.Width):
				if bitmap.GetPixel(col, row) != colorTransparent:
					colOpaquePixel = col
					colNext as int = col
					colNext = colOpaquePixel
					goto converterGeneratedName1
					while true:
						colNext += 1
						:converterGeneratedName1
						break  unless (colNext <= (bitmap.Width - 1))
						if bitmap.GetPixel(colNext, row) == colorTransparent:
							break 
					
					graphicsPath.AddRectangle(Rectangle(colOpaquePixel, row, (colNext - colOpaquePixel), 1))
					col = colNext
		return graphicsPath

