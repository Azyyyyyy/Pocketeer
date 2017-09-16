using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Added
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace Pocketeer
{
    class MoneyClass
    {
        static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public async static Task Restore()
        {
            var openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".pmt");
            Windows.Storage.StorageFile fileloc = await openPicker.PickSingleFileAsync();
            if (fileloc == null)
            {
            }
            else if (fileloc.Path.Length >= 1)
            {
                var file = await Windows.Storage.FileIO.ReadLinesAsync(fileloc);
                string[] strings = new string[file.Count];
                file.CopyTo(strings, 0);

                localSettings.Values["DoesUserGetMoney"] = file[1];
                localSettings.Values["WhatDayDoesUserGetMoney"] = file[3];
                localSettings.Values["HowOftenDoesUserGetMoney"] = file[5];
                localSettings.Values["HowMuchMoneyDoesUserGet"] = file[7];
                localSettings.Values["HowMuchMoneyDoesUserHave"] = file[9];
                localSettings.Values["WhenMoneyNeedsGoingIn"] = file[11];
                localSettings.Values["SetupNeeded"] = "false";
            }
        }

        public static void UpdateTileNotifications()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            //TileUpdateManager.CreateTileUpdaterForApplication().Clear();

            string ProgramName = "Pocketeer";
            string subject = "You have got";
            string body = "£" + localSettings.Values["HowMuchMoneyDoesUserHave"].ToString();
            string content = $@"
<tile>
    <visual>
        <binding template='TileMedium'>
            <text>{ProgramName}</text>
            <text hint-style='captionSubtle'>{subject}</text>
            <text hint-style='captionSubtle'>{body}</text>
        </binding>
        <binding template='TileWide'>
            <text hint-style='subtitle'>{ProgramName}</text>
            <text hint-style='captionSubtle'>{subject}</text>
            <text hint-style='captionSubtle'>{body}</text>
        </binding>
    </visual>
</tile>";

            string subject2 = "You get";
            string body2 = "£" + localSettings.Values["HowMuchMoneyDoesUserGet"];
            string content2 = $@"
<tile>
    <visual>
        <binding template='TileMedium'>
            <text>{ProgramName}</text>
            <text hint-style='captionSubtle'>{subject2}</text>
            <text hint-style='captionSubtle'>{body2}</text>
        </binding>
        <binding template='TileWide'>
            <text hint-style='subtitle'>{ProgramName}</text>
            <text hint-style='captionSubtle'>{subject2}</text>
            <text hint-style='captionSubtle'>{body2}</text>
        </binding>
    </visual>
</tile>";

            string subject3 = "You get your money on";
            DateTime DateMoneyIsAddedToTotalDateTime = DateTime.Now;
            DateMoneyIsAddedToTotalDateTime = Convert.ToDateTime(localSettings.Values["WhatDayDoesUserGetMoney"]);
            string body3 = DateMoneyIsAddedToTotalDateTime.ToString("dd/MM/yy");
            string content3 = $@"
<tile>
    <visual>
        <binding template='TileMedium'>
            <text>{ProgramName}</text>
            <text hint-style='captionSubtle'>{subject3}</text>
            <text hint-style='captionSubtle'>{body3}</text>
        </binding>
        <binding template='TileWide'>
            <text hint-style='subtitle'>{ProgramName}</text>
            <text hint-style='captionSubtle'>{subject3}</text>
            <text hint-style='captionSubtle'>{body3}</text>
        </binding>
    </visual>
</tile>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(content);
            var notification = new TileNotification(doc);
            notification.Tag = "HowMuchMoneyDoesUserHave";

            XmlDocument doc2 = new XmlDocument();
            doc2.LoadXml(content2);
            var notification2 = new TileNotification(doc2);
            notification.Tag = "HowMuchMoneyDoesUserGet";

            XmlDocument doc3 = new XmlDocument();
            doc3.LoadXml(content3);
            var notification3 = new TileNotification(doc3);
            notification.Tag = "WhatDayDoesUserGetMoney";

            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification2);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notification3);
        }

        public async static Task MakeBackupFile()
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Pocketeer File", new List<string>() { ".pmt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Pocketeer Data";
            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file == null)
            {
            }
            else if (file.Path.Length >= 1)
            {
                string content = "[DoesUserGetMoney]" + Environment.NewLine +
                                 localSettings.Values["DoesUserGetMoney"].ToString() + Environment.NewLine +
                                 "[WhatDayDoesUserGetMoney]" + Environment.NewLine +
                                 localSettings.Values["WhatDayDoesUserGetMoney"].ToString() + Environment.NewLine +
                                 "[HowOftenDoesUserGetMoney]" + Environment.NewLine +
                                 localSettings.Values["HowOftenDoesUserGetMoney"].ToString() + Environment.NewLine +
                                 "[HowMuchMoneyDoesUserGet]" + Environment.NewLine +
                                 localSettings.Values["HowMuchMoneyDoesUserGet"].ToString() + Environment.NewLine +
                                 "[HowMuchMoneyDoesUserHave]" + Environment.NewLine +
                                 localSettings.Values["HowMuchMoneyDoesUserHave"].ToString() + Environment.NewLine +
                                 "[WhenMoneyNeedsGoingIn]" + Environment.NewLine +
                                 localSettings.Values["WhenMoneyNeedsGoingIn"].ToString();
                await Windows.Storage.FileIO.WriteTextAsync(file, content);
            }
        }

        public static void UpdateTotalMoneyAndWhenMoneyNeedsGoingInNext(bool Setup)
        {
            DateTime NextTimeMoneyNeedsToBeAdded = Convert.ToDateTime(localSettings.Values["WhenMoneyNeedsGoingIn"]);
            DateTime LastTimeAppWasOpened = Convert.ToDateTime(localSettings.Values["LastTimeAppWasOpened"]);
            double AmountOfMoneyGoingIn = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserGet"]);

            LastTimeAppWasOpened = DateTime.Parse(LastTimeAppWasOpened.ToString());
            TimeSpan elapsed = DateTime.Now.Date.Subtract(LastTimeAppWasOpened);

            void UpdateWhenMoneyNeedsToGoIn(double elapsedint, int Days)
            {
                if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Week")
                {
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(DateTime.Now.Date.AddDays(Days));
                }
                else if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Month")
                {
                    DateTime date = NextTimeMoneyNeedsToBeAdded;
                    date = date.AddMonths(Convert.ToInt32(elapsedint));
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(date);
                    localSettings.Values["WhatDayDoesUserGetMoney"] = Convert.ToString(date);
                }
                else if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Year")
                {
                    DateTime date = NextTimeMoneyNeedsToBeAdded;
                    date = date.AddYears(Convert.ToInt32(elapsedint));
                    localSettings.Values["WhenMoneyNeedsGoingIn"] = Convert.ToString(date);
                    localSettings.Values["WhatDayDoesUserGetMoney"] = Convert.ToString(date);
                }
            }

            double daysAgo = elapsed.TotalDays;
            if (daysAgo.ToString("0") == "0" && !Setup)
            {
            }
            else
            {
                localSettings.Values["LastTimeAppWasOpened"] = DateTime.Now.Date.ToString();
                elapsed = DateTime.Now.Date.Subtract(NextTimeMoneyNeedsToBeAdded.Date);
                double elapsedint = Convert.ToInt32(elapsed.TotalDays);
                if (elapsedint >= 0)
                {
                        if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Week")
                        {
                            elapsedint = (elapsedint / 7) + 1;
                        }
                        else if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Month")
                        {
                            elapsedint = (elapsedint / 30.4167) + 1;
                        }
                        else if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Year")
                        {
                            elapsedint = (elapsedint / 365) + 1;
                        }
                        int HowManyTimesToAddMoney = (int)elapsedint;
                        localSettings.Values["HowMuchMoneyDoesUserHave"] = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserHave"]) + (AmountOfMoneyGoingIn * HowManyTimesToAddMoney);

                        if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Month")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 0);
                        }
                        else if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Year")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 0);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Monday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Tuesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Wednesday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Thursday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Friday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Saturday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Sunday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 7);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Saturday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 1);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Friday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 2);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Thursday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 3);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Wednesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 4);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Tuesday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 5);
                        }
                        else if (DateTime.Now.Date.DayOfWeek == DayOfWeek.Monday && localSettings.Values["WhatDayDoesUserGetMoney"].ToString() == "Sunday")
                        {
                            UpdateWhenMoneyNeedsToGoIn(elapsedint, 6);
                        }
                    }
                }
            }
        }
    }