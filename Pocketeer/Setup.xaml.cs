using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class Setup : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public Setup()
        {
            this.InitializeComponent();
            if (localSettings.Values["Currency"] == null)
            {
            }
            else
            {
                CurrencyChoose.SelectedIndex = Convert.ToInt32(localSettings.Values["Currency"].ToString());
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SetupPart2));
        }

        private async void GotPMTFileButton_Click(object sender, RoutedEventArgs e)
        {
            await MoneyClass.Restore();
            if (localSettings.Values["SetupNeeded"] == null)
            {
            }
            else if (localSettings.Values["SetupNeeded"].ToString() == "false")
            {
                MoneyClass.UpdateTotalMoneyAndWhenMoneyNeedsGoingInNext(true);
                Frame.Navigate(typeof(FrameForInfoPlusSettingsXAML));
            }
        }

        private void CurrencyChoose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            localSettings.Values["Currency"] = CurrencyChoose.SelectedIndex;
        }
    }
}
