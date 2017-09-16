using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
        string NextButtonFlyoutTextBlockBackup = "";

        public SetupPart2()
        {
            this.InitializeComponent();
        }

        void DoesSaveButtonNeedToBeEnabled()
        {
            if (DoesUserGetMoney.IsOn)
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
            else if (HowMuchMoneyDoesUserHaveTextBox.Text.Length >= 1)
            {
                NextButton.IsEnabled = true;
            }
        }


        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.Flyout.Hide();
            string pattern = "£";
            string replacement = "";
            Regex rgx = new Regex(pattern);
            HowMuchMoneyDoesUserGetTextBox.Text = rgx.Replace(HowMuchMoneyDoesUserGetTextBox.Text, replacement);
            HowMuchMoneyDoesUserHaveTextBox.Text = rgx.Replace(HowMuchMoneyDoesUserHaveTextBox.Text, replacement);
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

                DateTime dateTime = DateTime.Now.Date;

                if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    dateTime = DateThatMoneyGoesIn.Date.DateTime.Date;
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    dateTime = DateAndMonthThatMoneyGoesIn.Date.DateTime.Date;
                }
                int DateCheck = DateTime.Now.Date.CompareTo(dateTime.Date);
                localSettings.Values["DoesUserGetMoney"] = "true";
                if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    localSettings.Values["WhatDayDoesUserGetMoney"] = WhatDayDoesUserGetMoneyComboBox.SelectionBoxItem.ToString();
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    if (DateCheck >= 0)
                    {
                        dateTime = dateTime.AddMonths(1);
                    }
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(dateTime.Date);
                    localSettings.Values["WhatDayDoesUserGetMoney"] = Convert.ToString(dateTime.Date);
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
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
                    localSettings.Values["HowMuchMoneyDoesUserGet"] = Convert.ToString(Convert.ToDouble(HowMuchMoneyDoesUserGetTextBox.Text.ToString()));
                    localSettings.Values["HowMuchMoneyDoesUserHave"] = Convert.ToString(Convert.ToDouble(HowMuchMoneyDoesUserHaveTextBox.Text.ToString()));
                    localSettings.Values["SetupNeeded"] = "false";
                    await Task.Delay(250);
                    Frame.Navigate(typeof(FrameForInfoPlusSettingsXAML));
                }
                catch (OverflowException)
                {
                    NextButton.Flyout.ShowAt(NextButton);
                    NextButtonFlyoutTextBlockBackup = NextButtonFlyoutTextBlock.Text;
                    NextButtonFlyoutTextBlock.Text = "Pocketeer can't handle that much money!";
                }
                catch (FormatException)
                {
                     NextButton.Flyout.ShowAt(NextButton);
                }
            catch
                {
                    NextButton.Flyout.ShowAt(NextButton);
                    NextButtonFlyoutTextBlockBackup = NextButtonFlyoutTextBlock.Text;
                    NextButtonFlyoutTextBlock.Text = "An error has occurred, try again";
                }
            }
            else
            {
                try
                {
                    localSettings.Values["HowMuchMoneyDoesUserHave"] = Convert.ToString(Convert.ToDouble(HowMuchMoneyDoesUserHaveTextBox.Text.ToString()));
                    localSettings.Values["DoesUserGetMoney"] = "false";
                    localSettings.Values["SetupNeeded"] = "false";
                    Frame.Navigate(typeof(FrameForInfoPlusSettingsXAML));
                }
                catch (OverflowException)
                {
                    NextButton.Flyout.ShowAt(NextButton);
                    NextButtonFlyoutTextBlockBackup = NextButtonFlyoutTextBlock.Text;
                    NextButtonFlyoutTextBlock.Text = "Pocketeer can't handle that much money!";
                }
                catch (FormatException)
                {
                    NextButton.Flyout.ShowAt(NextButton);
                }
                catch
                {
                    NextButton.Flyout.ShowAt(NextButton);
                    NextButtonFlyoutTextBlockBackup = NextButtonFlyoutTextBlock.Text;
                    NextButtonFlyoutTextBlock.Text = "An error has occurred, try again";
                }
            }
        }

        private void DoesUserGetMoney_Toggled(object sender, RoutedEventArgs e)
        {
            if (DoesUserGetMoney.IsOn)
            {
                try
                {
                    Grid.SetRow(HowMuchMoneyDoesUserHaveTextBlock, 5);
                    Grid.SetRow(HowMuchMoneyDoesUserHaveTextBox, 5);
                    NextButton.IsEnabled = false;
                    if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == -1)
                    {
                        WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Visible;
                    }
                    else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 0)
                    {
                        WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Visible;
                    }
                    else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 1)
                    {
                        DateThatMoneyGoesIn.Visibility = Visibility.Visible;
                    }
                    else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 2)
                    {
                        DateAndMonthThatMoneyGoesIn.Visibility = Visibility.Visible;
                    }
                    WhatDayDoesUserGetMoneyTextBlock.Visibility = Visibility.Visible;
                    HowOftenDoesUserGetMoneyTextBlock.Visibility = Visibility.Visible;
                    HowOftenDoesUserGetMoneyComboBox.Visibility = Visibility.Visible;
                    HowMuchMoneyDoesUserGetTextBlock.Visibility = Visibility.Visible;
                    HowMuchMoneyDoesUserGetTextBox.Visibility = Visibility.Visible;
                    Grid.SetRowSpan(HowMuchMoneyDoesUserHaveTextBlock, 1);
                    Grid.SetRowSpan(HowMuchMoneyDoesUserHaveTextBox, 1);
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
                Grid.SetRow(HowMuchMoneyDoesUserHaveTextBlock, 3);
                Grid.SetRow(HowMuchMoneyDoesUserHaveTextBox, 3);
                Grid.SetRowSpan(HowMuchMoneyDoesUserHaveTextBlock, 2);
                Grid.SetRowSpan(HowMuchMoneyDoesUserHaveTextBox, 2);
                NextButton.IsEnabled = false;
                if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == -1)
                {
                    WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Collapsed;
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 0)
                {
                    WhatDayDoesUserGetMoneyComboBox.Visibility = Visibility.Collapsed;
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 1)
                {
                    DateThatMoneyGoesIn.Visibility = Visibility.Collapsed;
                }
                else if (HowOftenDoesUserGetMoneyComboBox.SelectedIndex == 2)
                {
                    DateAndMonthThatMoneyGoesIn.Visibility = Visibility.Collapsed;
                }
                WhatDayDoesUserGetMoneyTextBlock.Visibility = Visibility.Collapsed;
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
            DoesSaveButtonNeedToBeEnabled();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            NextButton.Flyout.Hide();
            if (NextButtonFlyoutTextBlockBackup.Length >= 1)
            {
                NextButtonFlyoutTextBlock.Text = NextButtonFlyoutTextBlockBackup;
            }
        }
    }
}
