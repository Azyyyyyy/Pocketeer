using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class SetupPart2 : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public SetupPart2()
        {
            this.InitializeComponent();
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                localSettings.Values["DoesUserGetMoney"] = "true";
                localSettings.Values["WhatDayDoesUserGetMoney"] = WhatDayDoesUserGetMoneyComboBox.SelectionBoxItem.ToString();
                localSettings.Values["HowOftenDoesUserGetMoney"] = HowOftenDoesUserGetMoneyComboBox.SelectionBoxItem.ToString();
                try
                {
                    int HowMuchMoneyDoesUserGetInt = Convert.ToInt32(HowMuchMoneyDoesUserGetTextBox.Text.ToString());
                    int HowMuchMoneyDoesUserHaveInt = Convert.ToInt32(HowMuchMoneyDoesUserHaveTextBox.Text.ToString());
                    localSettings.Values["HowMuchMoneyDoesUserGet"] = HowMuchMoneyDoesUserGetTextBox.Text.ToString();
                    localSettings.Values["HowMuchMoneyDoesUserHave"] = HowMuchMoneyDoesUserHaveTextBox.Text.ToString();
                    localSettings.Values["SetupNeeded"] = "false";
                    Frame.Navigate(typeof(Infomation));
                }
                catch
                {
                    MessageDialog dialog = new MessageDialog("Input the money as 1.00 not £1.00", "Pocketeer");
                    await dialog.ShowAsync();
                }
            }
            else
            {
                localSettings.Values["DoesUserGetMoney"] = "false";
                localSettings.Values["SetupNeeded"] = "false";
                Frame.Navigate(typeof(Infomation));
            }
        }

        private void DoesUserGetMoney_Toggled(object sender, RoutedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                try
                {
                    NextButton.IsEnabled = false;
                    WhatDayDoesUserGetMoneyTextBlock.Visibility = Visibility.Visible;
                    WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Visible;
                    HowOftenDoesUserGetMoneyTextBlock.Visibility = Visibility.Visible;
                    HowOftenDoesUserGetMoneyComboBox.Visibility = Visibility.Visible;
                    HowMuchMoneyDoesUserGetTextBlock.Visibility = Visibility.Visible;
                    HowMuchMoneyDoesUserGetTextBox.Visibility = Visibility.Visible;
                    if (WhatDayDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowOftenDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowMuchMoneyDoesUserGetTextBox.Text.Length >= 1 && HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
                    {
                        NextButton.IsEnabled = true;
                    }
                }
                catch
                {

                }
            }
            else
            {
                NextButton.IsEnabled = false;
                WhatDayDoesUserGetMoneyTextBlock.Visibility = Visibility.Collapsed;
                WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Collapsed;
                HowOftenDoesUserGetMoneyTextBlock.Visibility = Visibility.Collapsed;
                HowOftenDoesUserGetMoneyComboBox.Visibility = Visibility.Collapsed;
                HowMuchMoneyDoesUserGetTextBlock.Visibility = Visibility.Collapsed;
                HowMuchMoneyDoesUserGetTextBox.Visibility = Visibility.Collapsed;
                if (HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
                {
                    NextButton.IsEnabled = true;
                }
            }
        }

        private void WhatDayDoesUserGetMoneyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WhatDayDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowOftenDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowMuchMoneyDoesUserGetTextBox.Text.Length >= 1 && HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void HowOftenDoesUserGetMoneyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WhatDayDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowOftenDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowMuchMoneyDoesUserGetTextBox.Text.Length >= 1 && HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void HowMuchMoneyDoesUserGetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (WhatDayDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowOftenDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowMuchMoneyDoesUserGetTextBox.Text.Length >= 1 && HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void HowMuchMoneyDoesUserHaveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                if (WhatDayDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowOftenDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowMuchMoneyDoesUserGetTextBox.Text.Length >= 1 && HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
                {
                    NextButton.IsEnabled = true;
                }
                else
                {
                    NextButton.IsEnabled = false;
                }
            }
            else if (HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }
    }
}
