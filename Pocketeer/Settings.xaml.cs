using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pocketeer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Settings()
        {
            this.InitializeComponent();
        }

        BitmapImage White = new BitmapImage(new Uri("ms-appx:///Assets/white-icon.png"));
        BitmapImage Black = new BitmapImage(new Uri("ms-appx:///Assets/black-icon.png"));
        BitmapImage White_Github = new BitmapImage(new Uri("ms-appx:///Assets/github(white)-icon.png"));
        BitmapImage Black_Github = new BitmapImage(new Uri("ms-appx:///Assets/github(black)-icon.png"));

        private void grid_Loading(FrameworkElement sender, object args)
        {
            Object RequestedThemeInfo = localSettings.Values["RequestedTheme"];
            if (RequestedThemeInfo == null)
            {
                AppIcon.Source = Black;
                GithubIcon.Source = Black_Github;
                LightRadioButton.IsChecked = true;
            }
            else if (RequestedThemeInfo.ToString() == "Dark")
            {
                AppIcon.Source = White;
                GithubIcon.Source = White_Github;
                DarkRadioButton.IsChecked = true;
            }
            else if (RequestedThemeInfo.ToString() == "Light")
            {
                AppIcon.Source = Black;
                GithubIcon.Source = Black_Github;
                LightRadioButton.IsChecked = true;
            }
            else if (RequestedThemeInfo.ToString() == "FromUsersSettings")
            {
                UseSystemThemeRadioButton.IsChecked = true;
            }

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            AppVersionTextBlock.Text = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
        }

        private void UseSystemThemeRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (UseSystemThemeRadioButton.IsChecked == true)
            {
                localSettings.Values["RequestedTheme"] = "FromUsersSettings";
            }
        }

        private void DarkRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (DarkRadioButton.IsChecked == true)
            {
                localSettings.Values["RequestedTheme"] = "Dark";
            }
        }

        private void LightRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (LightRadioButton.IsChecked == true)
            {
                localSettings.Values["RequestedTheme"] = "Light";
            }
        }

        private async void ResetDataButton_Click(object sender, RoutedEventArgs e)
        {
            await Task.Delay(1000 * 5);
            localSettings.Values["DoesUserGetMoney"] = null;
            localSettings.Values["WhatDayDoesUserGetMoney"] = null;
            localSettings.Values["HowOftenDoesUserGetMoney"] = null;
            localSettings.Values["HowMuchMoneyDoesUserGet"] = null;
            localSettings.Values["HowMuchMoneyDoesUserHave"] = null;
            localSettings.Values["SetupNeeded"] = null;

            Application.Current.Exit();
        }
    }
}
