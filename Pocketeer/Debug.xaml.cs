using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Store;
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
    public sealed partial class Debug : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        bool Ads;

        public Debug()
        {
            this.InitializeComponent();
        }

        private async void Grid_Loading(FrameworkElement sender, object args)
        {
            string Currency = "";
            string RequestedTheme = "";
            string SetupNeeded = "";
            string AdShown = "";
            int AmountOfAddOns = 0;
            string AddsOnsInfo = "";

            Package package = Package.Current;
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            StoreContext context = StoreContext.GetDefault();
            StoreAppLicense appLicense = await context.GetAppLicenseAsync();
            foreach (KeyValuePair<string, StoreLicense> item in appLicense.AddOnLicenses)
            {
                AmountOfAddOns++;
                StoreLicense addOnLicense = item.Value;
                AddsOnsInfo =
                    AddsOnsInfo + Environment.NewLine + Environment.NewLine +
                    $"{addOnLicense.InAppOfferToken}," + Environment.NewLine + addOnLicense.ExtendedJsonData;
                if (addOnLicense.InAppOfferToken == "RemoveAdvertsInPocketeer")
                {
                    Ads = addOnLicense.IsActive;
                }
            }

            if (localSettings.Values["Currency"] == null)
            {
                Currency = "null";
            }
            else
            {
                Currency = localSettings.Values["Currency"].ToString() + $" ({MoneyClass.currencysymbols[Convert.ToInt32(localSettings.Values["Currency"].ToString())]})";
            }

            if (localSettings.Values["RequestedTheme"] == null)
            {
                RequestedTheme = "null";
            }
            else
            {
                RequestedTheme = localSettings.Values["RequestedTheme"].ToString();
            }

            if (localSettings.Values["SetupNeeded"] == null)
            {
                SetupNeeded = "null";
            }
            else
            {
                SetupNeeded = localSettings.Values["SetupNeeded"].ToString();
            }

            if (localSettings.Values["AdShown"] == null)
            {
                AdShown = "null";
            }
            else
            {
                AdShown = localSettings.Values["AdShown"].ToString();
            }

            int itemint = 0;
            string Item = Environment.NewLine + Environment.NewLine;
            while (true)
            {
                if (localSettings.Values[$"Item{itemint}Name"] == null)
                {
                    break;
                }
                else
                {
                    string Link = "Null";
                    if (!(localSettings.Values[$"Item{itemint}Link"] == null))
                    {
                        Link = localSettings.Values[$"Item{itemint}Link"].ToString();
                    }
                    Item = Item + $"Item {itemint}:" + Environment.NewLine +
                        $"Item {itemint} Name: {localSettings.Values[$"Item{itemint}Name"].ToString()}" + Environment.NewLine +
                        $"Item {itemint} Price: {localSettings.Values[$"Item{itemint}Price"].ToString()}" + Environment.NewLine +
                        $"Item {itemint} Link: {Link}" + Environment.NewLine + Environment.NewLine;
                    itemint++;
                }
            }


            Info.Text =
                Environment.NewLine +
                "DoesUserGetMoney: " + localSettings.Values["DoesUserGetMoney"].ToString() + Environment.NewLine +
                "WhatDayDoesUserGetMoney: " + localSettings.Values["WhatDayDoesUserGetMoney"].ToString() + Environment.NewLine +
                "HowOftenDoesUserGetMoney: " + localSettings.Values["HowOftenDoesUserGetMoney"].ToString() + Environment.NewLine +
                "HowMuchMoneyDoesUserGet: " + localSettings.Values["HowMuchMoneyDoesUserGet"].ToString() + Environment.NewLine +
                "HowMuchMoneyDoesUserHave: " + localSettings.Values["HowMuchMoneyDoesUserHave"].ToString() + Environment.NewLine +
                "WhenMoneyNeedsGoingIn: " + localSettings.Values["WhenMoneyNeedsGoingIn"].ToString() + Environment.NewLine +
                "Currency: " + Currency + Environment.NewLine +
                "RequestedTheme: " + RequestedTheme + Environment.NewLine +
                "SetupNeeded: " + SetupNeeded + Environment.NewLine +
                "AdShown: " + AdShown + Environment.NewLine +
                "AmountOfAddOns: " + AmountOfAddOns + Environment.NewLine +
                "AddsOnsInfo: " + AddsOnsInfo + Environment.NewLine +
                "Items Info: " + Item +
                "Version: " + version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision + Environment.NewLine;

        }
    }
}
