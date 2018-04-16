using Xamarin.Forms;
using Xamarin.Forms.Platform.WPF;

namespace SkiaSharpSample.WPF
{
	public partial class MainWindow : FormsApplicationPage
	{
		public MainWindow()
		{
			InitializeComponent();

			Forms.Init();
			LoadApplication(new SkiaSharpSample.App());
		}
	}
}
