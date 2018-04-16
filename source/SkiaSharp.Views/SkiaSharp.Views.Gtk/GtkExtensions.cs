namespace SkiaSharp.Views.Gtk
{
	public static class GtkExtensions
	{
		// Point

		public static SKPoint ToSKPoint(this Gdk.Point point)
		{
			return new SKPoint(point.X, point.Y);
		}

		// Rect

		public static SKRect ToSKRect(this Gdk.Rectangle rect)
		{
			return new SKRect(rect.Left, rect.Top, rect.Right, rect.Bottom);
		}

		// Size

		public static SKSize ToSKSize(this Gdk.Size size)
		{
			return new SKSize(size.Width, size.Height);
		}

		// Pixbuf

		public static Gdk.Pixbuf ToPixbuf(this SKPicture picture, SKSizeI dimensions)
		{
			using (var image = SKImage.FromPicture(picture, dimensions))
			{
				return image.ToPixbuf();
			}
		}

		public static Gdk.Pixbuf ToPixbuf(this SKImage skiaImage)
		{
			// TODO: maybe keep the same color types where we can, instead of just going to the platform default

			var info = new SKImageInfo(skiaImage.Width, skiaImage.Height);
			var bitmap = new Gdk.Pixbuf(Gdk.Colorspace.Rgb, true, 8, info.Width, info.Height);

			// copy
			using (var pixmap = new SKPixmap(info, bitmap.Pixels, bitmap.Rowstride))
			{
				skiaImage.ReadPixels(pixmap, 0, 0);

				// swap R and B
				if (info.ColorType == SKColorType.Bgra8888)
				{
					SKSwizzle.SwapRedBlue(pixmap.GetPixels(), info.Width * info.Height);
				}
			}

			return bitmap;
		}

		public static Gdk.Pixbuf ToPixbuf(this SKBitmap skiaBitmap)
		{
			using (var image = SKImage.FromPixels(skiaBitmap.PeekPixels()))
			{
				return image.ToPixbuf();
			}
		}

		public static Gdk.Pixbuf ToPixbuf(this SKPixmap pixmap)
		{
			using (var image = SKImage.FromPixels(pixmap))
			{
				return image.ToPixbuf();
			}
		}

		public static SKBitmap ToSKBitmap(this Gdk.Pixbuf bitmap)
		{
			// TODO: maybe keep the same color types where we can, instead of just going to the platform default

			var info = new SKImageInfo(bitmap.Width, bitmap.Height);
			var skiaBitmap = new SKBitmap(info);
			using (var pixmap = skiaBitmap.PeekPixels())
			{
				bitmap.ToSKPixmap(pixmap);
			}
			return skiaBitmap;
		}

		public static SKImage ToSKImage(this Gdk.Pixbuf bitmap)
		{
			// TODO: maybe keep the same color types where we can, instead of just going to the platform default

			var info = new SKImageInfo(bitmap.Width, bitmap.Height);
			var image = SKImage.Create(info);
			using (var pixmap = image.PeekPixels())
			{
				bitmap.ToSKPixmap(pixmap);
			}
			return image;
		}

		public static void ToSKPixmap(this Gdk.Pixbuf bitmap, SKPixmap pixmap)
		{
			//// TODO: maybe keep the same color types where we can, instead of just going to the platform default

			//if (pixmap.ColorType == SKImageInfo.PlatformColorType)
			//{
			//	var info = pixmap.Info;
			//	var converted = new FormatConvertedBitmap(bitmap, PixelFormats.Pbgra32, null, 0);
			//	converted.CopyPixels(new Int32Rect(0, 0, info.Width, info.Height), pixmap.GetPixels(), info.BytesSize, info.RowBytes);
			//}
			//else
			//{
			//	// we have to copy the pixels into a format that we understand
			//	// and then into a desired format
			//	// TODO: we can still do a bit more for other cases where the color types are the same
			//	using (var tempImage = bitmap.ToSKImage())
			//	{
			//		tempImage.ReadPixels(pixmap, 0, 0);
			//	}
			//}
		}
	}
}
