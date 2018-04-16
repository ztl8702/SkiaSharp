using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;
using Xamarin.Forms.Platform.GTK.Renderers;

using SkiaSharp.Views.Gtk;

[assembly: ExportImageSourceHandler(typeof(SkiaSharp.Views.Forms.SKImageImageSource), typeof(SkiaSharp.Views.Forms.SKImageSourceHandler))]
[assembly: ExportImageSourceHandler(typeof(SkiaSharp.Views.Forms.SKBitmapImageSource), typeof(SkiaSharp.Views.Forms.SKImageSourceHandler))]
[assembly: ExportImageSourceHandler(typeof(SkiaSharp.Views.Forms.SKPixmapImageSource), typeof(SkiaSharp.Views.Forms.SKImageSourceHandler))]
[assembly: ExportImageSourceHandler(typeof(SkiaSharp.Views.Forms.SKPictureImageSource), typeof(SkiaSharp.Views.Forms.SKImageSourceHandler))]

namespace SkiaSharp.Views.Forms
{
	public sealed class SKImageSourceHandler : IImageSourceHandler
	{
		public Task<Gdk.Pixbuf> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1)
		{
			Gdk.Pixbuf image = null;

			var imageImageSource = imagesource as SKImageImageSource;
			if (imageImageSource != null)
			{
				image = imageImageSource.Image?.ToPixbuf();
			}

			var bitmapImageSource = imagesource as SKBitmapImageSource;
			if (bitmapImageSource != null)
			{
				image = bitmapImageSource.Bitmap?.ToPixbuf();
			}

			var pixmapImageSource = imagesource as SKPixmapImageSource;
			if (pixmapImageSource != null)
			{
				image = pixmapImageSource.Pixmap?.ToPixbuf();
			}

			var pictureImageSource = imagesource as SKPictureImageSource;
			if (pictureImageSource != null)
			{
				image = pictureImageSource.Picture?.ToPixbuf(pictureImageSource.Dimensions);
			}

			return Task.FromResult(image);
		}
	}
}
