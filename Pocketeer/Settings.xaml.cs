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
using Windows.UI;
using Windows.UI.ViewManagement;
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
        Random random = new Random();

        public Settings()
        {
            this.InitializeComponent();
            UISettings uISettings = new UISettings();
        }

        void ResetData()
        {
            localSettings.Values["DoesUserGetMoney"] = null;
            localSettings.Values["WhatDayDoesUserGetMoney"] = null;
            localSettings.Values["HowOftenDoesUserGetMoney"] = null;
            localSettings.Values["HowMuchMoneyDoesUserGet"] = null;
            localSettings.Values["HowMuchMoneyDoesUserHave"] = null;
            localSettings.Values["WhenMoneyNeedsGoingIn"] = null;
            localSettings.Values["SetupNeeded"] = null;
            var newWindow = Window.Current;
            var newAppView = ApplicationView.GetForCurrentView();
            var frame = new Frame();
            frame.Navigate(typeof(MainPage), null);
            newWindow.Content = frame;
            ResetDataButton.Flyout.Hide();
            newWindow.Activate();
        }

        BitmapImage White = new BitmapImage(new Uri("ms-appx:///Assets/white-icon.png"));
        BitmapImage Black = new BitmapImage(new Uri("ms-appx:///Assets/black-icon.png"));
        BitmapImage White_Github = new BitmapImage(new Uri("ms-appx:///Assets/github(white)-icon.png"));
        BitmapImage Black_Github = new BitmapImage(new Uri("ms-appx:///Assets/github(black)-icon.png"));

        private void grid_Loading(FrameworkElement sender, object args)
        {
            if (random.Next(100) >= 90)
            {
                if (random.Next(100) <= 50)
                {
                    InfomationTextBlock.Text = "Savings account for dummies - Discord server i'm in";
                }
                else
                {
                    InfomationTextBlock.Text = "Respect the setup screen - Dylan 2017";
                }
            }
            Object RequestedThemeInfo = localSettings.Values["RequestedTheme"];
            var IsThemeDark = App.Current.RequestedTheme == ApplicationTheme.Dark;
            if (IsThemeDark)
            {
                AppIcon.Source = White;
                GithubIcon.Source = White_Github;
            }
            else
            {
                AppIcon.Source = Black;
                GithubIcon.Source = Black_Github;
            }
            if (RequestedThemeInfo == null || RequestedThemeInfo.ToString() == "FromUsersSettings")
            {
                UseSystemThemeRadioButton.IsChecked = true;
            }
            else if (RequestedThemeInfo.ToString() == "Dark")
            {
                DarkRadioButton.IsChecked = true;
            }
            else if (RequestedThemeInfo.ToString() == "Light")
            {
                LightRadioButton.IsChecked = true;
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
                apprestarttextblock.Visibility = Visibility.Visible;
            }
        }

        private void DarkRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (DarkRadioButton.IsChecked == true)
            {
                localSettings.Values["RequestedTheme"] = "Dark";
                apprestarttextblock.Visibility = Visibility.Visible;
            }
        }

        private void LightRadioButton_Click(object sender, RoutedEventArgs e)
        {
            if (LightRadioButton.IsChecked == true)
            {
                localSettings.Values["RequestedTheme"] = "Light";
                apprestarttextblock.Visibility = Visibility.Visible;
            }
        }

        private async void YesButton_Click(object sender, RoutedEventArgs e)
        {
            await MoneyClass.MakeBackupFile();
            ResetData();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            ResetData();
        }

        private async void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            await MoneyClass.Restore();
        }

        private async void BackupButton_Click(object sender, RoutedEventArgs e)
        {
            await MoneyClass.MakeBackupFile();
        }

        private void ResetDataButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
