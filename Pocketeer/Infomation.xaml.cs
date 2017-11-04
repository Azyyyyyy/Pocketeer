using Microsoft.Advertising.WinRT.UI;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Services.Store;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

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
        InterstitialAd myInterstitialAd = null;
        string currency = "£";
#if DEBUG
        string myAppId = "3f83fe91-d6be-434d-a0ae-7351c5a997f1";
        string myAdUnitId = "test";
#endif
#if !DEBUG
        string myAppId = "9n3h90r55cr1";
        string myAdUnitId = "11700722";
#endif

        public Infomation()
        {
            InitializeComponent();
            MoneyClass.UpdateTileNotifications();
            MoneyClass.AddAcrylicBrush(grid, null);
            myInterstitialAd = new InterstitialAd();
            myInterstitialAd.AdReady += MyInterstitialAd_AdReady;
            myInterstitialAd.ErrorOccurred += MyInterstitialAd_ErrorOccurred;
            myInterstitialAd.Completed += MyInterstitialAd_Completed;
            myInterstitialAd.Cancelled += MyInterstitialAd_Cancelled;
        }

        void ChangeAmountOfMoneyUI()
        {
            if (localSettings.Values["Currency"] == null)
            {
                TotalMoneyTextBlock.Text = $"£{TotalMoney.ToString("0.00")}";
            }
            else
            {
                currency = MoneyClass.currencysymbols[Convert.ToInt32(localSettings.Values["Currency"])];
                TotalMoneyTextBlock.Text = $"{currency}{TotalMoney.ToString("0.00")}";
            }
        }

        private void MyInterstitialAd_Cancelled(object sender, object e)
        {
        }

        private void MyInterstitialAd_Completed(object sender, object e)
        {
            localSettings.Values["AdShown"] = "true";
        }

        private void MyInterstitialAd_ErrorOccurred(object sender, AdErrorEventArgs e)
        {
        }

        private void MyInterstitialAd_AdReady(object sender, object e)
        {
            myInterstitialAd.Show();
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
                    string pattern = @"\p{Sc}";
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
                        ChangeAmountOfMoneyUI();
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
                    string pattern = @"\p{Sc}";
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
                        ChangeAmountOfMoneyUI();
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

        private async void grid_Loading(FrameworkElement sender, object args)
        {
            StoreContext context = StoreContext.GetDefault();
            StoreAppLicense appLicense = await context.GetAppLicenseAsync();
            foreach (KeyValuePair<string, StoreLicense> item in appLicense.AddOnLicenses)
            {
                StoreLicense addOnLicense = item.Value;
                if (addOnLicense.InAppOfferToken == "RemoveAdvertsInPocketeer")
                {
                    MoneyClass.ShowAds = !addOnLicense.IsActive;
                }
            }
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

            ChangeAmountOfMoneyUI();
            if (MoneyUserGets == null)
            {
                HowMuchMoneyUserGetsTextBlock.Visibility = Visibility.Collapsed;
                WhatDayMoneyTextBlock.Visibility = Visibility.Collapsed;
                WhenDayMoneyGetsAddedTextBlock.Visibility = Visibility.Collapsed;
                Grid.SetRowSpan(TotalMoneyTextBlock, 4);
            }
            else
            {
                if (localSettings.Values["Currency"] == null)
                {
                    HowMuchMoneyUserGetsTextBlock.Text = $"£{Convert.ToDouble(MoneyUserGets).ToString("0.00")}";
                }
                else
                {
                    string currency = MoneyClass.currencysymbols[Convert.ToInt32(localSettings.Values["Currency"])];
                    HowMuchMoneyUserGetsTextBlock.Text = $"{currency}{Convert.ToDouble(MoneyUserGets).ToString("0.00")}";
                }
                DateTime DateMoneyIsAddedToTotalDateTime = DateTime.Now;
                DateMoneyIsAddedToTotalDateTime = Convert.ToDateTime(localSettings.Values["WhatDayDoesUserGetMoney"]);
                WhatDayMoneyTextBlock.Text = $"{DateMoneyIsAddedToTotalDateTime.ToString("dd/MM/yy")}";
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
                        WhenDayMoneyGetsAddedTextBlock.Text = $"{week} {weeks}";
                    }
                    else
                    {
                        WhenDayMoneyGetsAddedTextBlock.Text = $"{week} {weeks} and {day} {days}";
                    }
                }
                else if (elapsedint <= 7)
                {
                    ThatInTextBlock.Text = "That this";
                    WhenDayMoneyGetsAddedTextBlock.Text = $"{NextTimeMoneyNeedsToBeAdded.DayOfWeek}!";
                }
            }
            MoneyClass.UpdateTileNotifications();
            if (MoneyClass.ShowAds)
            {
                if (localSettings.Values["AdShown"] == null || localSettings.Values["AdShown"].ToString() == "false")
                {
                    myInterstitialAd.RequestAd(AdType.Display, myAppId, myAdUnitId);
                }
            }

            while (true)
            {
                await Task.Delay(1000 * 60);
                if (MoneyClass.ShowAds)
                {
                    myInterstitialAd.RequestAd(AdType.Display, myAppId, myAdUnitId);
                }
                else
                {
                    break;
                }
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
