using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
        Object MoneyUserGets = localSettings.Values["HowMuchMoneyDoesUserGet"];
        string FlyoutTextBackup = "";

        public Infomation()
        {
            this.InitializeComponent();
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
                    if (Convert.ToDouble(AddMoneyTextBox.Text.ToString()) == 0)
                    {
                        TextForAddOkButtonFlyout.Text = "You can't add no money!";
                    }
                    else
                    {
                        localSettings.Values["HowMuchMoneyDoesUserHave"] = Convert.ToDouble(TotalMoney.ToString()) + Convert.ToDouble(AddMoneyTextBox.Text.ToString());
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
                catch
                {
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
                    if (Convert.ToDouble(RemoveMoneyTextBox.Text.ToString()) == 0)
                    {
                        TextForRemoveOkButtonFlyout.Text = "You can't remove no money!";
                    }
                    else if (Convert.ToDouble(RemoveMoneyTextBox.Text.ToString()) > TotalMoneyDouble)
                    {
                        TextForRemoveOkButtonFlyout.Text = "You can't remove more then you got!";
                    }
                    else
                    {
                        TotalMoneyDouble = Convert.ToDouble(TotalMoney.ToString()) - Convert.ToDouble(RemoveMoneyTextBox.Text.ToString());
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
                catch
                {
                }
            }
        }

        private void grid_Loading(FrameworkElement sender, object args)
        {
            MoneyClass.UpdateTotalMoneyAndWhenMoneyNeedsGoingInNext();

            TotalMoney = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]);
            Object DateMoneyIsAddedToTotal = localSettings.Values["WhatDayDoesUserGetMoney"];
            DateTime DateMoneyIsAddedToTotalDateTime = DateTime.Now;
            try
            {
                DateMoneyIsAddedToTotalDateTime = Convert.ToDateTime(localSettings.Values["WhatDayDoesUserGetMoney"]);
            }
            catch
            {
            }

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
                HowMuchMoneyUserGetsTextBlock.Text = $"You get £{MoneyUserGets.ToString()}";

                if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Week")
                {
                    WhatDayMoneyTextBlock.Text = $"You get your money on {DateMoneyIsAddedToTotal.ToString()}";
                }
                else
                {
                    WhatDayMoneyTextBlock.Text = $"You get your money on {DateMoneyIsAddedToTotalDateTime.ToString("dd/MM/yy")}";
                }
                WhenDayMoneyGetsAddedTextBlock.Text = $"That is in {elapsedint} days";
            }
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
    }
}
