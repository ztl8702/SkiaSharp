﻿using System;
using ElmSharp;
using Tizen.Applications;

using SkiaSharp;
using SkiaSharp.Views.Tizen;

namespace SkiaSharpSample
{
	public class App : CoreUIApplication
	{
		public static void Main(string[] args)
		{
			Elementary.Initialize();
			Elementary.ThemeOverlay();

			var app = new App();
			app.Run(args);
		}

		protected override void OnCreate()
		{
			base.OnCreate();

			Initialize();
		}

		private void Initialize()
		{
			var window = new Window("SkiaSharp");
			window.BackButtonPressed += OnBackButtonPressed;
			window.AvailableRotations = DisplayRotation.Degree_0 | DisplayRotation.Degree_180 | DisplayRotation.Degree_270 | DisplayRotation.Degree_90;
			window.Show();

			var skiaView = new SKCanvasView(window);
			skiaView.PaintSurface += OnPaintSurface;
			skiaView.Show();

			var conformant = new Conformant(window);
			conformant.Show();
			conformant.SetContent(skiaView);
		}

		private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs e)
		{
			var skiaView = sender as SKCanvasView;

			// the the canvas and properties
			var canvas = e.Surface.Canvas;

			// get the screen density for scaling
			var scale = (float)ScalingInfo.ScalingFactor;
			var scaledSize = new SKSize(e.Info.Width / scale, e.Info.Height / scale);

			// handle the device screen density
			canvas.Scale(scale);

			// make sure the canvas is blank
			canvas.Clear(SKColors.White);

			// draw some text
			var paint = new SKPaint
			{
				Color = SKColors.Black,
				IsAntialias = true,
				Style = SKPaintStyle.Fill,
				TextAlign = SKTextAlign.Center,
				TextSize = 24
			};
			var coord = new SKPoint(scaledSize.Width / 2, (scaledSize.Height + paint.TextSize) / 2);
			canvas.DrawText("SkiaSharp", coord, paint);
		}

		private void OnBackButtonPressed(object sender, EventArgs e)
		{
			Exit();
		}
	}
}
