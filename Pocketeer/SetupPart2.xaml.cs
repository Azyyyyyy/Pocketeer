using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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

        void DoesSaveButtonNeedToBeEnabled()
        {
            if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex >= 0 && HowMuchMoneyDoesUserGetTextBox.Text.Length >= 1 && HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
            {
                if (WhatDayDoesUserGetMoneyComboBox.Visibility == Visibility.Visible && WhatDayDoesUserGetMoneyComboBox.SelectedIndex >= 0)
                {
                    NextButton.IsEnabled = true;
                }
                else if (WhatDayDoesUserGetMoneyComboBox.Visibility == Visibility.Collapsed)
                {
                    NextButton.IsEnabled = true;
                }
                else if (WhatDayDoesUserGetMoneyComboBox == null)
                {
                    NextButton.IsEnabled = false;
                }
                else
                {
                    NextButton.IsEnabled = false;
                }
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }


        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 3)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 4)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 5)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(7));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(1));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(2));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(3));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(4));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(5));
                }
                else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && WhatDayDoesUserGetMoneyComboBox.SelectedIndex == 6)
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(6));
                }

                localSettings.Values["DoesUserGetMoney"] = "true";
                if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhatDayDoesUserGetMoney"] = WhatDayDoesUserGetMoneyComboBox.SelectionBoxItem.ToString();
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    DateTime dateTime = DateThatMoneyGoesIn.Date.DateTime.Date;
                    int DateCheck = DateTime.Now.Date.CompareTo(dateTime.Date);
                    if (DateCheck >= 0)
                    {
                        dateTime = dateTime.AddMonths(1);
                    }
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(dateTime.Date);
                    localSettings.Values["WhatDayDoesUserGetMoney"] = Convert.ToString(dateTime.Date);
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    DateTime dateTime = DateAndMonthThatMoneyGoesIn.Date.DateTime.Date;
                    int DateCheck = DateTime.Now.Date.CompareTo(dateTime.Date);
                    if (DateCheck >= 0)
                    {
                        dateTime = dateTime.AddYears(1);
                    }
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(dateTime.Date);
                    localSettings.Values["WhatDayDoesUserGetMoney"] = Convert.ToString(dateTime.Date);
                }
                localSettings.Values["HowOftenDoesUserGetMoney"] = HowOftenDoesUserGetMoneyComboBox.SelectionBoxItem.ToString();

                try
                {
                    double HowMuchMoneyDoesUserGetDouble = Convert.ToDouble(HowMuchMoneyDoesUserGetTextBox.Text.ToString());
                    double HowMuchMoneyDoesUserHaveDouble = Convert.ToDouble(HowMuchMoneyDoesUserHaveTextBox.Text.ToString());
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
                try
                {
                    double HowMuchMoneyDoesUserHaveDouble = Convert.ToDouble(HowMuchMoneyDoesUserHaveTextBox.Text.ToString());
                    localSettings.Values["HowMuchMoneyDoesUserHave"] = HowMuchMoneyDoesUserHaveTextBox.Text.ToString();
                    localSettings.Values["DoesUserGetMoney"] = "false";
                    localSettings.Values["SetupNeeded"] = "false";
                    Frame.Navigate(typeof(FrameForInfoPlusSettingsXAML));
                }
                catch
                {
                }
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
            DoesSaveButtonNeedToBeEnabled();
        }

        private void HowOftenDoesUserGetMoneyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 0)
            {
                DateThatMoneyGoesIn.Visibility = Visibility.Collapsed;
                DateAndMonthThatMoneyGoesIn.Visibility = Visibility.Collapsed;
                WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Visible;
                WhatDayDoesUserGetMoneyTextBlock.Text = "What day do you get your money?";
            }
            else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 1)
            {
                DateThatMoneyGoesIn.Visibility = Visibility.Visible;
                DateAndMonthThatMoneyGoesIn.Visibility = Visibility.Collapsed;
                WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Collapsed;
                WhatDayDoesUserGetMoneyTextBlock.Text = "What date do you get your money?";
            }
            else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 2)
            {
                DateThatMoneyGoesIn.Visibility = Visibility.Collapsed;
                DateAndMonthThatMoneyGoesIn.Visibility = Visibility.Visible;
                WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Collapsed;
                WhatDayDoesUserGetMoneyTextBlock.Text = "When do you get your money?";
            }
            DoesSaveButtonNeedToBeEnabled();
        }

        private void HowMuchMoneyDoesUserGetTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            DoesSaveButtonNeedToBeEnabled();
        }

        private void HowMuchMoneyDoesUserHaveTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                DoesSaveButtonNeedToBeEnabled();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.Flyout.Hide();
        }
    }
}
