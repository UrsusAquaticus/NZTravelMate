using Windows.Foundation;
using Windows.UI.ViewManagement;

namespace NZTravelMate.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            ApplicationView.PreferredLaunchViewSize = new Size(400, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            this.InitializeComponent();
            LoadApplication(new NZTravelMate.App());
        }
    }
}