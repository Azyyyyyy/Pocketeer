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
            MySplitViewButton.Height = 50;
            MySplitViewButton.Width = 50;
        }

        private void MySplitViewButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private async void MySplitViewListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MySplitViewListBox.SelectedIndex == 0)
            {
                if (FrameForInfoPlusSettings == null || localSettings.Values["LastTimeAppWasOpened"] == null)
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

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Height <= 350 || Window.Current.Bounds.Width <= 460)
            {
                int fontsize = 16;
                MySplitViewButton.FontSize = fontsize + 2;
                PageUserIsOn.FontSize = fontsize;
                InfomationIcon.FontSize = fontsize + 2;
                InfomationTextBlock.FontSize = fontsize - 4;
                InfomationTextBlock.Padding = new Thickness(16,0,0,0);
                SettingsIcon.FontSize = fontsize + 2;
                SettingsTextBlock.FontSize = fontsize - 4;
                SettingsTextBlock.Padding = new Thickness(16, 0, 0, 0);
                MySplitViewButton.Height = 46;
                MySplitViewButton.Width = 46;
                MySplitView.CompactPaneLength = 46;
                MySplitView.OpenPaneLength = 130;
            }
            else
            {
                int fontsize = 20;
                MySplitViewButton.FontSize = fontsize + 2;
                PageUserIsOn.FontSize = fontsize;
                InfomationIcon.FontSize = fontsize + 2;
                InfomationTextBlock.FontSize = fontsize - 4;
                InfomationTextBlock.Padding = new Thickness(20, 0, 0, 0);
                SettingsIcon.FontSize = fontsize + 2;
                SettingsTextBlock.FontSize = fontsize - 4;
                SettingsTextBlock.Padding = new Thickness(20, 0, 0, 0);
                MySplitViewButton.Height = 50;
                MySplitViewButton.Width = 50;
                MySplitView.CompactPaneLength = 50;
                MySplitView.OpenPaneLength = 150;
            }
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {

        }
    }
}
