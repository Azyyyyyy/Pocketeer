using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.Services.Store;
using Windows.UI;
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
        int CompactPaneLength = 50;

        public FrameForInfoPlusSettingsXAML()
        {
            InitializeComponent();
            MySplitView.CompactPaneLength = CompactPaneLength;
        }

        private void MySplitViewButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        void ChangeUISizing(int fontsize, Thickness thickness, int buttonsize, int openpanelength)
        {
            MySplitViewButton.FontSize = fontsize + 2;
            PageUserIsOn.FontSize = fontsize;
            InfomationIcon.FontSize = fontsize + 2;
            InfomationTextBlock.FontSize = fontsize - 4;
            InfomationTextBlock.Padding = thickness;
            SettingsIcon.FontSize = fontsize + 2;
            SettingsTextBlock.FontSize = fontsize - 4;
            SettingsTextBlock.Padding = thickness;
            DebugIcon.FontSize = fontsize + 2;
            DebugTextBlock.FontSize = fontsize - 4;
            DebugTextBlock.Padding = thickness;
            WishListIcon.FontSize = fontsize + 2;
            WishListTextBlock.FontSize = fontsize - 4;
            WishListTextBlock.Padding = thickness;
            MySplitViewButton.Width = buttonsize;
            MySplitView.CompactPaneLength = buttonsize;
            MySplitView.OpenPaneLength = openpanelength;
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
                PageUserIsOn.Text = "Wish List";
                FrameForInfoPlusSettings.Navigate(typeof(WishList));
            }
            else if (MySplitViewListBox.SelectedIndex == 2)
            {
                PageUserIsOn.Text = "Debug";
                FrameForInfoPlusSettings.Navigate(typeof(Debug));
            }
            else if (MySplitViewListBox.SelectedIndex == 3)
            {
                PageUserIsOn.Text = "Settings";
                FrameForInfoPlusSettings.Navigate(typeof(Settings));
            }
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Height <= 350 || Window.Current.Bounds.Width <= 460)
            {
                ChangeUISizing(16,new Thickness(16,0,0,0), CompactPaneLength - 4, CompactPaneLength + 80);
            }
            else
            {
                ChangeUISizing(20, new Thickness(20, 0, 0, 0), CompactPaneLength, CompactPaneLength + 100);
            }
        }

        private async void Grid_Loading(FrameworkElement sender, object args)
        {
#if DEBUG
            LBIDebug.Visibility = Visibility.Visible;
#endif
            MySplitViewButton.Background = new SolidColorBrush((Color)this.Resources["SystemAccentColor"]);
            await Task.Delay(1000);
            StoreContext context = StoreContext.GetDefault();
            StoreAppLicense appLicense = await context.GetAppLicenseAsync();
            foreach (KeyValuePair<string, StoreLicense> item in appLicense.AddOnLicenses)
            {
                StoreLicense addOnLicense = item.Value;
                if (addOnLicense.InAppOfferToken == "RemoveAdvertsInPocketeer")
                {
                    if (addOnLicense.IsActive)
                    {
                        Ad.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void MySplitViewButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            MySplitViewButton.Background = new SolidColorBrush((Color)this.Resources["SystemAccentColor"]);
        }
    }
}
