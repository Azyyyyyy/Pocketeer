using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class Infomation : Page
    {

        static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        double TotalMoney = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]);
        string FlyoutTextBackup = "";

        public Infomation()
        {
            this.InitializeComponent();
            MoneyClass.UpdateTileNotifications();
        }

        private void AddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutTextBackup = TextForAddOkButtonFlyout.Text;
            if (AddMoneyTextBox.Text.Length == 0)
            {
                TextForAddOkButtonFlyout.Text = "Put in how much money you what to add!";
            }
            else
            {
                try
                {
                    string money = AddMoneyTextBox.Text;
                    string pattern = "£";
                    string replacement = "";
                    Regex rgx = new Regex(pattern);
                    money = rgx.Replace(money, replacement);
                    if (Convert.ToDouble(money) == 0)
                    {
                        TextForAddOkButtonFlyout.Text = "You can't add no money!";
                    }
                    else
                    {
                        localSettings.Values["HowMuchMoneyDoesUserHave"] = Convert.ToDouble(TotalMoney.ToString()) + Convert.ToDouble(money.ToString());
                        TotalMoney = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]);
                        TotalMoneyTextBlock.Text = $"You have got £{TotalMoney.ToString("0.00")}";
                        AddMoneyTextBox.Text = "";
                        AddMoneyToTotalButton.Flyout.Hide();
                        if (!RemoveMoneyToTotalButton.IsEnabled)
                        {
                            RemoveMoneyToTotalButton.IsEnabled = true;
                        }
                    }
                }
                catch (OverflowException)
                {
                    TextForAddOkButtonFlyout.Text = "You can't add that much money in Pocketeer!";
                }
                catch (FormatException)
                {
                }
                catch
                {
                    TextForAddOkButtonFlyout.Text = "An error has occurred, try again";
                }
            }
        }

        private void RemoveMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutTextBackup = TextForRemoveOkButtonFlyout.Text;
            double TotalMoneyDouble = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]);
            if (RemoveMoneyTextBox.Text.Length == 0)
            {
                TextForRemoveOkButtonFlyout.Text = "Put in how much money you what to remove!";
            }
            else
            {
                try
                {
                    string money = RemoveMoneyTextBox.Text;
                    string pattern = "£";
                    string replacement = "";
                    Regex rgx = new Regex(pattern);
                    money = rgx.Replace(money, replacement);
                    if (Convert.ToDouble(money) == 0)
                    {
                        TextForRemoveOkButtonFlyout.Text = "You can't remove no money!";
                    }
                    else if (Convert.ToDouble(money) > TotalMoneyDouble)
                    {
                        TextForRemoveOkButtonFlyout.Text = "You can't remove more then you got!";
                    }
                    else
                    {
                        TotalMoneyDouble = Convert.ToDouble(TotalMoney.ToString()) - Convert.ToDouble(money.ToString());
                        localSettings.Values["HowMuchMoneyDoesUserHave"] = TotalMoneyDouble;
                        TotalMoney = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]);
                        TotalMoneyTextBlock.Text = $"You have got £{TotalMoney.ToString("0.00")}";
                        RemoveMoneyTextBox.Text = "";
                        RemoveMoneyToTotalButton.Flyout.Hide();
                        if (TotalMoneyDouble == 0)
                        {
                            RemoveMoneyToTotalButton.IsEnabled = false;
                        }
                    }
                }
                catch (OverflowException)
                {
                    TextForAddOkButtonFlyout.Text = "You can't remove that much money in Pocketeer!";
                }
                catch (FormatException)
                {
                }
                catch
                {
                    TextForAddOkButtonFlyout.Text = "An error has occurred, try again";
                }
            }
        }

        private void grid_Loading(FrameworkElement sender, object args)
        {
            MoneyClass.UpdateTotalMoneyAndWhenMoneyNeedsGoingInNext(false);

            Object MoneyUserGets = localSettings.Values["HowMuchMoneyDoesUserGet"];
            TotalMoney = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]);
            Object DateMoneyIsAddedToTotal = localSettings.Values["WhatDayDoesUserGetMoney"];

            if (TotalMoney.ToString() == "0")
            {
                RemoveMoneyToTotalButton.IsEnabled = false;
            }

            DateTime NextTimeMoneyNeedsToBeAdded = Convert.ToDateTime(localSettings.Values["WhenMoneyNeedsGoingIn"]).Date;
            TimeSpan elapsed = DateTime.Now.Date.Subtract(NextTimeMoneyNeedsToBeAdded.Date);
            int elapsedint = Convert.ToInt32(-elapsed.TotalDays);

            TotalMoneyTextBlock.Text = $"You have got £{TotalMoney.ToString("0.00")}";
            if (MoneyUserGets == null)
            {
                HowMuchMoneyUserGetsTextBlock.Visibility = Visibility.Collapsed;
                WhatDayMoneyTextBlock.Visibility = Visibility.Collapsed;
                WhenDayMoneyGetsAddedTextBlock.Visibility = Visibility.Collapsed;
                Grid.SetRowSpan(TotalMoneyTextBlock, 4);
            }
            else
            {
                HowMuchMoneyUserGetsTextBlock.Text = $"You get £{Convert.ToDouble(MoneyUserGets).ToString("0.00")}";
                if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Week")
                {
                    WhatDayMoneyTextBlock.Text = $"You get your money on {DateMoneyIsAddedToTotal.ToString()}";
                }
                else
                {
                    DateTime DateMoneyIsAddedToTotalDateTime = DateTime.Now;
                    DateMoneyIsAddedToTotalDateTime = Convert.ToDateTime(localSettings.Values["WhatDayDoesUserGetMoney"]);
                    WhatDayMoneyTextBlock.Text = $"You get your money on {DateMoneyIsAddedToTotalDateTime.ToString("dd/MM/yy")}";
                }
                if (elapsedint >= 7)
                {
                    string weeks = "week";
                    string days = "day";
                    double week;
                    week = elapsedint / 7;
                    int day = elapsedint - ((int)week * 7);
                    if (week >= 2)
                    {
                        weeks = weeks + "s";
                    }
                    if (day >= 2)
                    {
                        days = days + "s";
                    }
                    if (day <= 0)
                    {
                        WhenDayMoneyGetsAddedTextBlock.Text = $"That is in {week} {weeks}";
                    }
                    else
                    {
                        WhenDayMoneyGetsAddedTextBlock.Text = $"That is in {week} {weeks} and {day} {days}";
                    }
                }
                else if (elapsedint <= 7)
                {
                    WhenDayMoneyGetsAddedTextBlock.Text = $"That is this {NextTimeMoneyNeedsToBeAdded.DayOfWeek}!";
                }
            }
            MoneyClass.UpdateTileNotifications();
        }

        private void RemoveOkButton_Click(object sender, RoutedEventArgs e)
        {
            TextForRemoveOkButtonFlyout.Text = FlyoutTextBackup;
            RemoveMoneyButton.Flyout.Hide();
        }

        private void AddOkButton_Click(object sender, RoutedEventArgs e)
        {
            TextForAddOkButtonFlyout.Text = FlyoutTextBackup;
            AddMoneyButton.Flyout.Hide();
        }

        private void grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (Window.Current.Bounds.Height <= 350 || Window.Current.Bounds.Width <= 460)
            {
                int fontsize = 20;
                TotalMoneyTextBlock.FontSize = fontsize;
                HowMuchMoneyUserGetsTextBlock.FontSize = fontsize;
                WhatDayMoneyTextBlock.FontSize = fontsize;
                WhenDayMoneyGetsAddedTextBlock.FontSize = fontsize;
            }
            else
            {
                int fontsize = 26;
                TotalMoneyTextBlock.FontSize = fontsize;
                HowMuchMoneyUserGetsTextBlock.FontSize = fontsize;
                WhatDayMoneyTextBlock.FontSize = fontsize;
                WhenDayMoneyGetsAddedTextBlock.FontSize = fontsize;
            }
        }
    }
}
