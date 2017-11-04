using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.System.Power;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Pocketeer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        BitmapImage Black_MenuLogo = new BitmapImage(new Uri("ms-appx:///Assets/MenuBarlogoblack.png"));
        BitmapImage White_MenuLogo = new BitmapImage(new Uri("ms-appx:///Assets/MenuBarlogowhite.png"));

        public MainPage()
        {
            this.InitializeComponent();
            Window.Current.Activated += Current_Activated;
        }

        void CoreTitleBar_IsVisibleChanged(CoreApplicationViewTitleBar titleBar, object args)
        {
            MenuBarGrid.Visibility = titleBar.IsVisible ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Current_Activated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState != CoreWindowActivationState.Deactivated)
            {
                if (PowerManager.EnergySaverStatus == EnergySaverStatus.On)
                {
                    AppName.Foreground = new SolidColorBrush(Colors.White);
                    MenuLogo.Source = White_MenuLogo;
                }
                else if (App.Current.RequestedTheme == ApplicationTheme.Light && MoneyClass.DoesAcrylicBrushWorks)
                {
                    AppName.Foreground = new SolidColorBrush(Colors.Black);
                    MenuLogo.Source = Black_MenuLogo;
                }
                AppName.Opacity = 1;
                MenuLogo.Opacity = 1;
            }
            else
            {
                CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                CoreApplicationViewTitleBar applicationViewTitleBar = CoreApplication.GetCurrentView().TitleBar;
                if (PowerManager.EnergySaverStatus == EnergySaverStatus.On)
                {
                    titleBar.ButtonBackgroundColor = Colors.Gray;
                    titleBar.ButtonInactiveBackgroundColor = Colors.Gray;
                }
                else
                {
                    titleBar.ButtonBackgroundColor = Colors.Transparent;
                }
                if (App.Current.RequestedTheme == ApplicationTheme.Light && MoneyClass.DoesAcrylicBrushWorks)
                {
                    AppName.Foreground = new SolidColorBrush(Colors.White);
                    MenuLogo.Source = White_MenuLogo;
                }
                AppName.Opacity = 0.5;
                MenuLogo.Opacity = 0.5;
            }
        }

        private void MainGrid_Loading(FrameworkElement sender, object args)
        {
            Object SetupNeeded = localSettings.Values["SetupNeeded"];
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
                coreTitleBar.ExtendViewIntoTitleBar = true;
                CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = true;
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                try
                {
                    if (ApiInformation.IsTypePresent("Windows.UI.Xaml.Media.XamlCompositionBrushBase"))
                    {
                        AcrylicBrush myBrush = new AcrylicBrush();
                        NavBar.Visibility = Visibility.Collapsed;
                        myBrush.BackgroundSource = AcrylicBackgroundSource.HostBackdrop;
                        if (App.Current.RequestedTheme == ApplicationTheme.Dark)
                        {
                            titleBar.ButtonForegroundColor = Colors.White;
                            if (localSettings.Values["CustomEnabled"] != null)
                            {
                                myBrush.TintColor = Color.FromArgb(Convert.ToByte(localSettings.Values["CustomA"]), Convert.ToByte(localSettings.Values["CustomR"]), Convert.ToByte(localSettings.Values["CustomG"]), Convert.ToByte(localSettings.Values["CustomB"]));
                            }
                            else
                            {
                                myBrush.TintColor = Color.FromArgb(255, 0, 0, 0);
                            }
                            myBrush.FallbackColor = Colors.Gray;
                        }
                        else
                        {
                            MenuLogo.Source = Black_MenuLogo;
                            AppName.Foreground = new SolidColorBrush(Colors.Black);
                            if (localSettings.Values["CustomEnabled"] != null)
                            {
                                myBrush.TintColor = Color.FromArgb(Convert.ToByte(localSettings.Values["CustomA"]), Convert.ToByte(localSettings.Values["CustomR"]), Convert.ToByte(localSettings.Values["CustomG"]), Convert.ToByte(localSettings.Values["CustomB"]));
                            }
                            else
                            {
                                myBrush.TintColor = Color.FromArgb(255, 255, 255, 255);
                            }
                            myBrush.FallbackColor = Colors.Gray;
                        }
                        myBrush.TintOpacity = 0.6;

                        MainGrid.Background = myBrush;
                        if (PowerManager.EnergySaverStatus == EnergySaverStatus.Off || PowerManager.EnergySaverStatus == EnergySaverStatus.Disabled)
                        {
                            titleBar.ButtonBackgroundColor = Colors.Transparent;
                        }
                        else
                        {
                            titleBar.ButtonBackgroundColor = Colors.Gray;
                        }
                        titleBar.ButtonInactiveBackgroundColor = Colors.Gray;
                    }
                    else
                    {
                        CoreApplicationViewTitleBar applicationViewTitleBar = CoreApplication.GetCurrentView().TitleBar;
                        Window.Current.SetTitleBar(MenuBarGrid);
                        if (PowerManager.EnergySaverStatus == EnergySaverStatus.Off)
                        {
                            titleBar.ButtonBackgroundColor = Colors.Transparent;
                        }
                        else
                        {
                            titleBar.ButtonBackgroundColor = Colors.Gray;
                        }
                        titleBar.ButtonInactiveBackgroundColor = Colors.Gray;
                    }
                }
                catch
                {
                    AppName.Foreground = new SolidColorBrush(Colors.White);
                    MenuLogo.Source = White_MenuLogo;
                    MoneyClass.DoesAcrylicBrushWorks = false;
                    CoreApplicationViewTitleBar applicationViewTitleBar = CoreApplication.GetCurrentView().TitleBar;
                    Window.Current.SetTitleBar(MenuBarGrid);
                    titleBar.ButtonBackgroundColor = Colors.Gray;
                    titleBar.ButtonInactiveBackgroundColor = Colors.Gray;
                }
                coreTitleBar.IsVisibleChanged += CoreTitleBar_IsVisibleChanged;
            }
            else
            {
                NavBar.Visibility = Visibility.Collapsed;
                AppName.Visibility = Visibility.Collapsed;
            }

            if (SetupNeeded == null)
            {
                OtherUI.Navigate(typeof(Setup));
            }
            else if (SetupNeeded.ToString() == "false")
            {
                OtherUI.Navigate(typeof(FrameForInfoPlusSettingsXAML));
            }
        }
    }
}
