using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class WishList : Page
    {
        int NumberOfItem = 0;
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        double AmountOfMoneyUserGets = 0;
        string TotalMoneyUserHas;
        int TimesAnItemHasBeenDeleted;
        string Currency = "£";
        Flyout flyout = new Flyout();
        double Divide = 7;
        Thickness Margin0500 = new Thickness(0, 5, 0, 0);
        Thickness Margin5500 = new Thickness(5, 5, 0, 0);
        Thickness Marign03000 = new Thickness(0, 30, 0, 0);
        static string pattern = @"\p{Sc}";
        string replacement = "";
        Regex rgx = new Regex(pattern);
        double elapseddouble = 0;

        public WishList()
        {
            InitializeComponent();
        }

        void AddInfomation(string Name, double TotalMoneyNeeded)
        {
            string Week = "week";
            string Day = "day";
            string An = "an";
            if (!(Name[0] == 'A' || Name[0] == 'E' || Name[0] == 'I' || Name[0] == 'O' || Name[0] == 'U'))
            {
                An = "a";
            }
            string DaysUntil = $"and {elapseddouble} ";
            if (elapseddouble >= 2)
            {
                Day = Day + "s";
            }
            DaysUntil = DaysUntil + Day;
            ChangeWeekText(DaysUntil, Week);
            double MoneyUntilUserCanGetItem = TotalMoneyNeeded - Convert.ToDouble(TotalMoneyUserHas);
            double WeeksUntilUserCanGetItem = (MoneyUntilUserCanGetItem / AmountOfMoneyUserGets) - (elapseddouble / Divide);
            if (WeeksUntilUserCanGetItem >= 2)
            {
                Week = Week + "s";
            }
            if (elapseddouble == 7)
            {
                elapseddouble = 0;
            }
            string WeeksUntil = $"in {(int)WeeksUntilUserCanGetItem} {Week} {DaysUntil}";
            if (WeeksUntilUserCanGetItem < 1 && TotalMoneyNeeded > Convert.ToDouble(TotalMoneyUserHas))
            {
                WeeksUntil = "this " + Convert.ToDateTime(localSettings.Values["WhenMoneyNeedsGoingIn"]).DayOfWeek.ToString().ToLower();
            }

            TextBlock NameOfItemTBL = new TextBlock();
            NameOfItemTBL.Text = Name;
            NameOfItemTBL.Margin = Margin0500;
            NameOfItemTBL.FontWeight = Windows.UI.Text.FontWeights.Bold;
            NameOfItemTBL.FontSize = 25;
            TextBlock WeeksAndDaysNeededTBL = new TextBlock();
            WeeksAndDaysNeededTBL.Text = $"If you don't spend any money you can get {An} {Name} {WeeksUntil}";
            WeeksAndDaysNeededTBL.Margin = Margin0500;
            WeeksAndDaysNeededTBL.TextWrapping = TextWrapping.WrapWholeWords;
            TextBlock AmountOfMoneyNeededTBL = new TextBlock();
            if (MoneyUntilUserCanGetItem > 0)
            {
                AmountOfMoneyNeededTBL.Text = $"Amount of money needed to get your {Name}: {Currency}{MoneyUntilUserCanGetItem}";
            }
            else
            {
                AmountOfMoneyNeededTBL.Text = "You got the amount of money to get the item!";
                WeeksAndDaysNeededTBL.Visibility = Visibility.Collapsed;
            }
            AmountOfMoneyNeededTBL.Margin = Margin0500;
            AmountOfMoneyNeededTBL.TextWrapping = TextWrapping.WrapWholeWords;
            TextBlock AmountOfMoneyItemCostsTBL = new TextBlock();
            AmountOfMoneyItemCostsTBL.Text = $"Amount of money {An} {Name} cost: {Currency}{TotalMoneyNeeded}";
            AmountOfMoneyItemCostsTBL.Margin = Margin0500;
            AmountOfMoneyItemCostsTBL.TextWrapping = TextWrapping.WrapWholeWords;
            Button EditItemButton = new Button();
            EditItemButton.Content = "Edit Infomation";
            EditItemButton.Margin = Margin0500;
            EditItemButton.Tag = NumberOfItem.ToString();
            Button RemoveItem = new Button();
            RemoveItem.Content = "Remove Item";
            RemoveItem.Margin = Margin5500;
            Button GetItem = new Button();
            GetItem.Content = $"Get {An} {Name}!";
            GetItem.Margin = Margin5500;
            int ButtonNumber = Convert.ToInt32(EditItemButton.Tag.ToString()) - TimesAnItemHasBeenDeleted;
            if (localSettings.Values[$"Item{ButtonNumber}Link"] == null || MoneyUntilUserCanGetItem > 0)
            {
                GetItem.Visibility = Visibility.Collapsed;
            }
            else
            {
                GetItem.Visibility = Visibility.Visible;
            }

            StackPanel ButtonSP = new StackPanel();
            ButtonSP.Orientation = Orientation.Horizontal;
            ButtonSP.Children.Add(EditItemButton);
            ButtonSP.Children.Add(RemoveItem);
            ButtonSP.Children.Add(GetItem);

            StackPanel InfomationSP = new StackPanel();
            InfomationSP.Orientation = Orientation.Vertical;
            InfomationSP.Margin = Marign03000;
            InfomationSP.Children.Add(NameOfItemTBL);
            InfomationSP.Children.Add(WeeksAndDaysNeededTBL);
            InfomationSP.Children.Add(AmountOfMoneyNeededTBL);
            InfomationSP.Children.Add(AmountOfMoneyItemCostsTBL);
            InfomationSP.Children.Add(ButtonSP);

            InfomationSPXAML.Children.Add(InfomationSP);

            GetItem.Click += new RoutedEventHandler((sender1, e1) => GetItem_Click(sender1, e1, EditItemButton));
            RemoveItem.Click += new RoutedEventHandler((sender1, e1) => Button_Click(sender1, e1, InfomationSPXAML, InfomationSP, EditItemButton));
            EditItemButton.Click += new RoutedEventHandler((sender1, e1) => Button_Click1(sender1, e1, NameOfItemTBL, WeeksAndDaysNeededTBL, AmountOfMoneyNeededTBL, AmountOfMoneyItemCostsTBL, GetItem));

            NumberOfItem++;
        }

        void ItemNull(int ItemNumberToReplace)
        {
            localSettings.Values[$"Item{ItemNumberToReplace}Name"] = null;
            localSettings.Values[$"Item{ItemNumberToReplace}Price"] = null;
            localSettings.Values[$"Item{ItemNumberToReplace}Link"] = null;
            NumberOfItem--;
        }

        void ChangeWeekText(string DaysUntil, string Week)
        {
            if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Month")
            {
                DaysUntil = null;
                Week = "month";
                Divide = 30.4167;
            }
            else if (localSettings.Values["HowOftenDoesUserGetMoney"].ToString() == "Every Year")
            {
                DaysUntil = null;
                Week = "year";
                Divide = 365;
            }
        }

        private async void GetItem_Click(object sender, RoutedEventArgs e, Button button)
        {
            var success = await Windows.System.Launcher.LaunchUriAsync(new Uri(localSettings.Values[$"Item{Convert.ToInt32(Convert.ToInt32(button.Tag.ToString()) - TimesAnItemHasBeenDeleted)}Link"].ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e, StackPanel stackPanel, StackPanel stackPanel2, Button button)
        {
            int ItemNumber = Convert.ToInt32(button.Tag.ToString());
            if ((ItemNumber - TimesAnItemHasBeenDeleted) == (NumberOfItem - 1))
            {
                ItemNull(ItemNumber - TimesAnItemHasBeenDeleted);
                stackPanel.Children.Remove(stackPanel2);
            }
            else
            {
                int ItemNumberToReplace = ItemNumber - TimesAnItemHasBeenDeleted;
                while (NumberOfItem - 1 > ItemNumberToReplace)
                {
                    localSettings.Values[$"Item{ItemNumberToReplace}Name"] = localSettings.Values[$"Item{ItemNumberToReplace + 1}Name"];
                    localSettings.Values[$"Item{ItemNumberToReplace}Price"] = localSettings.Values[$"Item{ItemNumberToReplace + 1}Price"];
                    localSettings.Values[$"Item{ItemNumberToReplace}Link"] = localSettings.Values[$"Item{ItemNumberToReplace + 1}Link"];
                    stackPanel.Children.Remove(stackPanel2);
                    ItemNumberToReplace++;
                }
                ItemNull(ItemNumberToReplace);
                TimesAnItemHasBeenDeleted++;
            }
        }

        private void Grid_Loading(FrameworkElement sender, object args)
        {
            NumberOfItem = 0;
            TotalMoneyUserHas = localSettings.Values["HowMuchMoneyDoesUserHave"].ToString();
            AmountOfMoneyUserGets = Convert.ToDouble(localSettings.Values["HowMuchMoneyDoesUserGet"]);
            DateTime NextTimeMoneyNeedsToBeAdded = Convert.ToDateTime(localSettings.Values["WhenMoneyNeedsGoingIn"]).Date;
            TimeSpan elapsed = DateTime.Now.Date.Subtract(NextTimeMoneyNeedsToBeAdded.Date);
            elapseddouble = Convert.ToInt32(-elapsed.TotalDays);
            if (!(localSettings.Values["Currency"] == null))
            {
                Currency = MoneyClass.currencysymbols[Convert.ToInt32(localSettings.Values["Currency"])];
            }
            while (true)
            {
                if (localSettings.Values[$"Item{NumberOfItem}Name"] == null)
                {
                    break;
                }
                else
                {
                    AddInfomation(localSettings.Values[$"Item{NumberOfItem}Name"].ToString(), Convert.ToDouble(localSettings.Values[$"Item{NumberOfItem}Price"]));
                }
            }
        }

        private void Button_Click1(object sender, RoutedEventArgs e,TextBlock NameOfItem, TextBlock WeeksAndDaysNeeded, TextBlock AmountOfMoneyNeeded, TextBlock AmountOfMoneyItemCosts,Button GetItem)
        {
            int ItemNumber = Convert.ToInt32(((Button)sender).Tag);

            TextBlock ProductNameTBL = new TextBlock();
            ProductNameTBL.Text = "What is the product name*: ";
            ProductNameTBL.VerticalAlignment = VerticalAlignment.Center; 
            TextBlock ProductPriceTBL = new TextBlock();
            ProductPriceTBL.Text = "What is the product price*: ";
            ProductPriceTBL.VerticalAlignment = VerticalAlignment.Center;
            TextBlock ProductLinkTBL = new TextBlock();
            ProductLinkTBL.Text = "What is the product link: ";
            ProductLinkTBL.VerticalAlignment = VerticalAlignment.Center;
            TextBox ProductNameBox = new TextBox();
            ProductNameBox.PlaceholderText = "Product Name";
            ProductNameBox.Width = 240;
            ProductNameBox.Margin = new Thickness(5, 0, 0, 0);
            TextBox ProductPriceBox = new TextBox();
            ProductPriceBox.PlaceholderText = "Product Price";
            ProductPriceBox.Width = 240;
            ProductPriceBox.Margin = new Thickness(5, 0, 0, 0);
            TextBox ProductLinkBox = new TextBox();
            ProductLinkBox.PlaceholderText = "Product Link";
            ProductLinkBox.Width = 240;
            ProductLinkBox.Margin = new Thickness(5, 0, 0, 0);
            Button ChangeInfomationButton = new Button();
            ChangeInfomationButton.Content = "Change Infomation";
            ChangeInfomationButton.Margin = new Thickness(0,5,0,0);
            ChangeInfomationButton.Tag = ItemNumber;


            ProductNameBox.Text = localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Name"].ToString();
            ProductPriceBox.Text = Currency + localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Price"].ToString();
            if (!(localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Link"] == null))
            {
                ProductLinkBox.Text = localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Link"].ToString();
            }

            StackPanel InfomationSP = new StackPanel();
            InfomationSP.Children.Add(ProductNameTBL);
            InfomationSP.Children.Add(ProductNameBox);
            InfomationSP.Children.Add(ProductPriceTBL);
            InfomationSP.Children.Add(ProductPriceBox);
            InfomationSP.Children.Add(ProductLinkTBL);
            InfomationSP.Children.Add(ProductLinkBox);
            InfomationSP.Children.Add(ChangeInfomationButton);

            flyout.Content = InfomationSP;
            flyout.ShowAt((FrameworkElement)sender);
            ChangeInfomationButton.Click += new RoutedEventHandler((sender1, e1) => Button_Click2(sender1, e1, NameOfItem, WeeksAndDaysNeeded, AmountOfMoneyNeeded, AmountOfMoneyItemCosts, ProductNameBox, ProductPriceBox, flyout,GetItem, ProductLinkBox));
        }

        private void Button_Click2(object sender, RoutedEventArgs e, TextBlock ProductNameTBL, TextBlock AmountOfTimeLeftTBL, TextBlock AmountOfMoneyLeftTBL, TextBlock AmountOfMoneyItemCosts, TextBox ProductNameBox, TextBox ProductPriceBox, Flyout flyout, Button GetItem, TextBox ProductLinkBox)
        {
            void ChangeItem()
            {
                int ItemNumber = Convert.ToInt32(((Button)sender).Tag);
                try
                {
                    Convert.ToDouble(ProductPriceBox.Text);

                    string Week = "week";
                    string Day = "day";
                    string An = "an";
                    if (!(ProductNameBox.Text[0] == 'A' || ProductNameBox.Text[0] == 'E' || ProductNameBox.Text[0] == 'I' || ProductNameBox.Text[0] == 'O' || ProductNameBox.Text[0] == 'U'))
                    {
                        An = "a";
                    }
                    ProductPriceBox.Text = rgx.Replace(ProductPriceBox.Text, replacement);
                    ProductNameTBL.Text = ProductNameBox.Text;
                    string DaysUntil = $"and {elapseddouble} ";
                    if (elapseddouble >= 2)
                    {
                        Day = Day + "s";
                    }
                    DaysUntil = DaysUntil + Day;
                    if (elapseddouble == 7)
                    {
                        elapseddouble = 0;
                    }
                    ChangeWeekText(DaysUntil, Week);

                    double MoneyUntilUserCanGetItem = (Convert.ToDouble(ProductPriceBox.Text) - Convert.ToDouble(TotalMoneyUserHas));
                    double WeeksUntilUserCanGetItem = (MoneyUntilUserCanGetItem / AmountOfMoneyUserGets) - (elapseddouble / Divide);

                    if (MoneyUntilUserCanGetItem > 0)
                    {
                        AmountOfMoneyLeftTBL.Text = $"Amount of money needed to get your {ProductNameBox.Text}: {Currency}{(Convert.ToDouble(ProductPriceBox.Text) - Convert.ToDouble(TotalMoneyUserHas))}";
                        AmountOfTimeLeftTBL.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        AmountOfMoneyLeftTBL.Text = "You got the amount of money to get the item!";
                        AmountOfTimeLeftTBL.Visibility = Visibility.Collapsed;
                    }

                    if (ProductLinkBox.Text.Length == 0)
                    {
                        localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Link"] = null;
                    }
                    if (MoneyUntilUserCanGetItem > 0 || ProductLinkBox.Text.Length == 0)
                    {
                        GetItem.Visibility = Visibility.Collapsed;
                        localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Link"] = null;
                    }
                    else
                    {
                        GetItem.Visibility = Visibility.Visible;
                        localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Link"] = ProductLinkBox.Text;
                    }

                    if (WeeksUntilUserCanGetItem >= 2)
                    {
                        Week = Week + "s";
                    }
                    string weeksuntil = $"in {(int)WeeksUntilUserCanGetItem} {Week} {DaysUntil}";
                    if (WeeksUntilUserCanGetItem < 1 && Convert.ToDouble(ProductPriceBox.Text) > Convert.ToDouble(TotalMoneyUserHas))
                    {
                        weeksuntil = "this " + Convert.ToDateTime(localSettings.Values["WhenMoneyNeedsGoingIn"]).DayOfWeek.ToString().ToLower();
                    }
                    AmountOfTimeLeftTBL.Text = $"If you don't spend any money you can get {An} {ProductNameBox.Text} {weeksuntil}";
                    AmountOfMoneyItemCosts.Text = $"Amount of money {An} {ProductNameBox.Text} cost: {Currency}" + ProductPriceBox.Text;
                    GetItem.Content = $"Get {An} {ProductNameBox.Text}!";
                    localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Name"] = ProductNameBox.Text;
                    localSettings.Values[$"Item{ItemNumber - TimesAnItemHasBeenDeleted}Price"] = ProductPriceBox.Text;
                    flyout.Hide();
                }
#if !DEBUG
                catch (OverflowException)
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "Pocketeer can't handle that much money!";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    flyout.ShowAt((FrameworkElement)sender);
                }
                catch (FormatException)
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "You need to input the money in as 1.00 or 1";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    flyout.ShowAt((FrameworkElement)sender);
                }
                catch
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "An error has occurred, try again";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    flyout.ShowAt((FrameworkElement)sender);
                }
            }
#endif
#if DEBUG
            catch (Exception a)
            {
                TextBlock FlyoutTextBlock = new TextBlock();
                FlyoutTextBlock.Text =
                    "StackTrace:" + Environment.NewLine +
                    a.StackTrace + Environment.NewLine + Environment.NewLine +
                    "Source:" + Environment.NewLine +
                    a.Source + Environment.NewLine + Environment.NewLine +
                    "Message:" + Environment.NewLine +
                    a.Message + Environment.NewLine + Environment.NewLine +
                    "InnerException:" + Environment.NewLine +
                    a.InnerException + Environment.NewLine + Environment.NewLine +
                    "HResult:" + Environment.NewLine +
                    a.HResult + Environment.NewLine + Environment.NewLine +
                    "HelpLink:" + Environment.NewLine +
                    a.HelpLink + Environment.NewLine + Environment.NewLine +
                    "Data:" + Environment.NewLine + a.Data;

                    flyout.Content = FlyoutTextBlock;
                    flyout.ShowAt((FrameworkElement)sender);
                }
            }
#endif

            if (ProductNameBox.Text.Length == 0 || ProductPriceBox.Text.Length == 0)
            {
                TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                NextButtonFlyoutTextBlock.Text = "You need to fill in all the infomation!";
                flyout.Content = NextButtonFlyoutTextBlock;
                flyout.ShowAt((FrameworkElement)sender);
            }
            else if (ProductLinkBox.Text.Length > 0)
            {
                if (!Uri.TryCreate(ProductLinkBox.Text, UriKind.Absolute, out Uri result))
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "Invalid Uri";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    flyout.ShowAt((FrameworkElement)sender);
                }
                else
                {
                    ChangeItem();
                }
            }
            else
            {
                ChangeItem();
            }
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            void AddItem()
            {
                try
                {
                    ProductPriceTextBox.Text = rgx.Replace(ProductPriceTextBox.Text, replacement);
                    Convert.ToDouble(ProductPriceTextBox.Text);
                    localSettings.Values[$"Item{NumberOfItem}Price"] = ProductPriceTextBox.Text;
                    localSettings.Values[$"Item{NumberOfItem}Name"] = ProductNameTextBox.Text;
                    if (Addlink.Text.Length > 0)
                    {
                        localSettings.Values[$"Item{NumberOfItem}Link"] = Addlink.Text;
                    }
                    AddInfomation(ProductNameTextBox.Text, Convert.ToDouble(ProductPriceTextBox.Text));
                    PlusButton.Flyout.Hide();

                    ProductNameTextBox.Text = "";
                    ProductPriceTextBox.Text = "";
                    Addlink.Text = "";
                }
#if !DEBUG
                catch (OverflowException)
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "Pocketeer can't handle that much money!";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    AddItemButton.Flyout = flyout;
                }
                catch (FormatException)
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "You need to input the money in as 1.00 or 1";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    AddItemButton.Flyout = flyout;
                }
                catch
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "An error has occurred, try again";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    AddItemButton.Flyout = flyout;
                }
            }
#endif
#if DEBUG
            catch (Exception a)
            {
                TextBlock FlyoutTextBlock = new TextBlock();
                FlyoutTextBlock.Text =
                    "StackTrace:" + Environment.NewLine +
                    a.StackTrace + Environment.NewLine + Environment.NewLine +
                    "Source:" + Environment.NewLine +
                    a.Source + Environment.NewLine + Environment.NewLine +
                    "Message:" + Environment.NewLine +
                    a.Message + Environment.NewLine + Environment.NewLine +
                    "InnerException:" + Environment.NewLine +
                    a.InnerException + Environment.NewLine + Environment.NewLine +
                    "HResult:" + Environment.NewLine +
                    a.HResult + Environment.NewLine + Environment.NewLine +
                    "HelpLink:" + Environment.NewLine +
                    a.HelpLink + Environment.NewLine + Environment.NewLine +
                    "Data:" + Environment.NewLine + a.Data;

                    flyout.Content = FlyoutTextBlock;
                    AddItemButton.Flyout = flyout;
                }
            }
#endif

            if (ProductNameTextBox.Text.Length == 0 || ProductPriceTextBox.Text.Length == 0)
            {
                TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                NextButtonFlyoutTextBlock.Text = "You need to fill in all the infomation!";
                flyout.Content = NextButtonFlyoutTextBlock;
                flyout.ShowAt((FrameworkElement)sender);
            }
            else if (Addlink.Text.Length > 0)
            {
                if (!Uri.TryCreate(Addlink.Text, UriKind.Absolute, out Uri result))
                {
                    TextBlock NextButtonFlyoutTextBlock = new TextBlock();
                    NextButtonFlyoutTextBlock.Text = "Invalid Uri";
                    flyout.Content = NextButtonFlyoutTextBlock;
                    flyout.ShowAt((FrameworkElement)sender);
                }
                else
                {
                    AddItem();
                }
            }
            else
            {
                AddItem();
            }
        }
    }
}