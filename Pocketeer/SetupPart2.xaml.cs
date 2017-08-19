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

        private void NextButton_Click(object sender, RoutedEventArgs e)
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
                    NextButton.Flyout.Hide();
                    Frame.Navigate(typeof(FrameForInfoPlusSettingsXAML));
                }
                catch
                {
                }
            }
            else
            {
                localSettings.Values["DoesUserGetMoney"] = "false";
                localSettings.Values["SetupNeeded"] = "false";
                Frame.Navigate(typeof(FrameForInfoPlusSettingsXAML));
            }
        }

        private void DoesUserGetMoney_Toggled(object sender, RoutedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                try
                {
                    Grid.SetRow(NextButton, 6);
                    Grid.SetRow(HowMuchMoneyDoesUserHaveTextBlock, 5);
                    Grid.SetRow(HowMuchMoneyDoesUserHaveTextBox, 5);
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
                Grid.SetRow(HowMuchMoneyDoesUserHaveTextBlock, 2);
                Grid.SetRow(HowMuchMoneyDoesUserHaveTextBox, 2);
                Grid.SetRow(NextButton, 3);
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

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.Flyout.Hide();
        }
    }
}
