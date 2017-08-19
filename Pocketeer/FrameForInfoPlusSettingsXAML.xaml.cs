using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pocketeer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FrameForInfoPlusSettingsXAML : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public FrameForInfoPlusSettingsXAML()
        {
            this.InitializeComponent();
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            Object RequestedThemeInfo = localSettings.Values["RequestedTheme"];
            if (RequestedThemeInfo.ToString() == "Dark")
            {
                RequestedTheme = ElementTheme.Dark;
            }
            else if (RequestedThemeInfo.ToString() == "Light")
            {
                RequestedTheme = ElementTheme.Light;
            }
        }

        private void MySplitViewButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void MySplitViewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MySplitViewListBox.SelectedIndex == 0)
            {
                if (FrameForInfoPlusSettings == null)
                {
                    await Task.Delay(TimeSpan.FromSeconds(0.25));
                }
                PageUserIsOn.Text = "Infomation";
                FrameForInfoPlusSettings.Navigate(typeof(Infomation));
            }
            else if (MySplitViewListBox.SelectedIndex == 1)
            {
                PageUserIsOn.Text = "Settings";
                FrameForInfoPlusSettings.Navigate(typeof(Settings));
            }
        }
    }
}
