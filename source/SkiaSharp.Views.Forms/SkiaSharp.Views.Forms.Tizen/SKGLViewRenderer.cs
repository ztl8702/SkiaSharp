﻿using SkiaSharp.Views.Tizen;
using Xamarin.Forms.Platform.Tizen;

using SKFormsView = SkiaSharp.Views.Forms.SKGLView;
using SKNativeView = SkiaSharp.Views.Tizen.SKGLSurfaceView;

[assembly: ExportRenderer(typeof(SKFormsView), typeof(SkiaSharp.Views.Forms.SKGLViewRenderer))]

namespace SkiaSharp.Views.Forms
{
	public class SKGLViewRenderer : SKGLViewRendererBase<SKFormsView, SKNativeView>
	{
		protected override void SetupRenderLoop(bool oneShot)
		{
			if (oneShot)
			{
				Control.Invalidate();
			}

			Control.RenderingMode = Element.HasRenderLoop ? RenderingMode.Continuously : RenderingMode.WhenDirty;
		}
	}
}
