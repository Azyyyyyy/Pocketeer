using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocketeer
{
    class MoneyClass
    {
        static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public static void UpdateTotalMoneyAndWhenMoneyNeedsGoingInNext()
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
            if (daysAgo.ToString("0") == "0")
            {
            }
            else
            {
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
