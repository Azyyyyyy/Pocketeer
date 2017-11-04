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
        Color bgcolor = new Color();

        public FrameForInfoPlusSettingsXAML()
        {
            InitializeComponent();
            if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase") && MoneyClass.DoesAcrylicBrushWorks)
            {
                AcrylicBrush myBrush = new AcrylicBrush();
                myBrush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                AcrylicBrush myBrush2 = new AcrylicBrush();
                myBrush2.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                if (App.Current.RequestedTheme == ApplicationTheme.Dark)
                {
                    if (localSettings.Values["CustomEnabled"] != null)
                    {
                        myBrush2.TintColor = Color.FromArgb(Convert.ToByte(localSettings.Values["CustomA"]), Convert.ToByte(localSettings.Values["CustomR"]), Convert.ToByte(localSettings.Values["CustomG"]), Convert.ToByte(localSettings.Values["CustomB"]));
                        myBrush.TintColor = Color.FromArgb(Convert.ToByte(localSettings.Values["CustomA"]), Convert.ToByte(localSettings.Values["CustomR"]), Convert.ToByte(localSettings.Values["CustomG"]), Convert.ToByte(localSettings.Values["CustomB"]));
                    }
                    else
                    {
                        myBrush.TintColor = Color.FromArgb(255, 0, 0, 0);
                        myBrush2.TintColor = Color.FromArgb(255, 0, 0, 0);
                    }
                    myBrush.FallbackColor = Color.FromArgb(255, 0, 0, 0);
                    myBrush2.FallbackColor = Color.FromArgb(255, 45, 45, 45);
                }
                else
                {
                    if (localSettings.Values["CustomEnabled"] != null)
                    {
                        myBrush2.TintColor = Color.FromArgb(Convert.ToByte(localSettings.Values["CustomA"]), Convert.ToByte(localSettings.Values["CustomR"]), Convert.ToByte(localSettings.Values["CustomG"]), Convert.ToByte(localSettings.Values["CustomB"]));
                        myBrush.TintColor = Color.FromArgb(Convert.ToByte(localSettings.Values["CustomA"]), Convert.ToByte(localSettings.Values["CustomR"]), Convert.ToByte(localSettings.Values["CustomG"]), Convert.ToByte(localSettings.Values["CustomB"]));
                    }
                    else
                    {
                        myBrush.TintColor = Color.FromArgb(255, 255, 255, 255);
                        myBrush2.TintColor = Color.FromArgb(255, 255, 255, 255);
                    }
                    myBrush.FallbackColor = Color.FromArgb(255, 240, 240, 240);
                    myBrush2.FallbackColor = Color.FromArgb(255, 240, 240, 240);
                }
                myBrush.TintOpacity = 0.6;
                myBrush2.TintOpacity = 0.4;

                RecPageUserIsOn.Fill = myBrush;
                MySplitViewListBox.Background = myBrush2;
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
            MySplitView.CompactPaneLength = 50;
            MySplitView.OpenPaneLength = 150;
            InfomationIcon.Visibility = Visibility.Visible;
            WishListIcon.Visibility = Visibility.Visible;
            DebugIcon.Visibility = Visibility.Visible;
            SettingsIcon.Visibility = Visibility.Visible;
        }

        private async void Grid_Loading(FrameworkElement sender, object args)
        {
#if DEBUG
            LBIDebug.Visibility = Visibility.Visible;
#endif
            bgcolor = (Color)this.Resources["SystemAccentColor"];
            MySplitViewButton.Background = new SolidColorBrush((Color)this.Resources["SystemAccentColor"]); 
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
    }
}