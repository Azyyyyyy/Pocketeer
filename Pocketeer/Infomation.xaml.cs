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
        Object TotalMoney = localSettings.Values["HowMuchMoneyDoesUserHave"];
        Object MoneyUserGets = localSettings.Values["HowMuchMoneyDoesUserGet"];
        Object DayMoneyIsPutIn = localSettings.Values["WhatDayDoesUserGetMoney"];
        string TextForAddOkButtonFlyoutBackup = "";
        string TextForRemoveOkButtonFlyoutBackup = "";
        int TotalMoneyInt = 0;

        public Infomation()
        {
            this.InitializeComponent();
        }

        private void AddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            TextForAddOkButtonFlyoutBackup = TextForAddOkButtonFlyout.Text;
            if (AddMoneyTextBox.Text.Length == 0)
            {
                TextForAddOkButtonFlyout.Text = "Put in how much money you what to add!";
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(AddMoneyTextBox.Text.ToString()) == 0)
                    {
                        TextForAddOkButtonFlyout.Text = "You can't add no money!";
                    }
                    else
                    {
                        TotalMoneyInt = Convert.ToInt32(TotalMoney.ToString()) + Convert.ToInt32(AddMoneyTextBox.Text.ToString());
                        localSettings.Values["HowMuchMoneyDoesUserHave"] = TotalMoneyInt;
                        TotalMoney = localSettings.Values["HowMuchMoneyDoesUserHave"];
                        TotalMoneyTextBlock.Text = $"You have got £{TotalMoney.ToString()}";
                        AddMoneyTextBox.Text = "";
                        AddMoneyButton.Flyout.Hide();
                        AddMoneyToTotalButton.Flyout.Hide();
                    }
                }
                catch
                {
                }
            }
        }

        private void RemoveMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            TextForRemoveOkButtonFlyoutBackup = TextForRemoveOkButtonFlyout.Text;
            TotalMoneyInt = Convert.ToInt32(localSettings.Values["HowMuchMoneyDoesUserHave"]);
            if (RemoveMoneyTextBox.Text.Length == 0)
            {
                TextForRemoveOkButtonFlyout.Text = "Put in how much money you what to remove!";
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(RemoveMoneyTextBox.Text.ToString()) == 0)
                    {
                        TextForRemoveOkButtonFlyout.Text = "You can't remove no money!";
                    }
                    else if (Convert.ToInt32(RemoveMoneyTextBox.Text.ToString()) > TotalMoneyInt)
                    {
                        TextForRemoveOkButtonFlyout.Text = "You can't remove more then you got!";
                    }
                    else
                    {
                        TotalMoneyInt = Convert.ToInt32(TotalMoney.ToString()) - Convert.ToInt32(RemoveMoneyTextBox.Text.ToString());
                        localSettings.Values["HowMuchMoneyDoesUserHave"] = TotalMoneyInt;
                        TotalMoney = localSettings.Values["HowMuchMoneyDoesUserHave"];
                        TotalMoneyTextBlock.Text = $"You have got £{TotalMoney.ToString()}";
                        RemoveMoneyTextBox.Text = "";
                        RemoveMoneyButton.Flyout.Hide();
                        RemoveMoneyToTotalButton.Flyout.Hide();
                    }
                }
                catch
                {
                }
            }
        }

        private void grid_Loading(FrameworkElement sender, object args)
        {
            //localSettings.Values["HowOftenDoesUserGetMoney"] = HowOftenDoesUserGetMoneyComboBox.SelectionBoxItem.ToString();

            TotalMoneyTextBlock.Text = $"You have got £{TotalMoney.ToString()}";
            HowMuchMoneyUserGetsTextBlock.Text = $"You get £{MoneyUserGets.ToString()}";
            WhatDayMoneyTextBlock.Text = $"You get your money on {DayMoneyIsPutIn.ToString()}";
        }

        private void RemoveOkButton_Click(object sender, RoutedEventArgs e)
        {
            TextForRemoveOkButtonFlyout.Text = TextForRemoveOkButtonFlyoutBackup;
            RemoveMoneyButton.Flyout.Hide();
        }

        private void AddOkButton_Click(object sender, RoutedEventArgs e)
        {
            TextForAddOkButtonFlyout.Text = TextForAddOkButtonFlyoutBackup;
            AddMoneyButton.Flyout.Hide();
        }
    }
}
