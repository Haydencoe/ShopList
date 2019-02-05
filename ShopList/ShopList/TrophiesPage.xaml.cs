using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ShopList
{
    public partial class TrophiesPage : ContentPage
    {

        public SQLDatabase sqlDatabase;
        public static List<Trophies> trophies = new List<Trophies>();
        public int counter = 0;
        public int totalTrophies = 0;

        public TrophiesPage()
        {
            InitializeComponent();

            sqlDatabase = new SQLDatabase();
            trophies = sqlDatabase.GetAllTrophies();

            GridPage();
        }
   
        public void GridPage() {

            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());

            if (trophies.Count == 0)
            {
                totalTrophies = 1;
            }

            else if (trophies.Count > 0)
            {
                totalTrophies = trophies.Count;
            }

            for (int rowIndex = 0; rowIndex < 10; rowIndex++) // Adds all the Rows
            {
                for (int columnIndex = 0; columnIndex< 1; columnIndex++)// Adds all the columns 
                {

                    if (counter == totalTrophies)// Amount of images to display
                    {
                        break;
                    }

                    var starTopImage = new Image { Margin = new Thickness(0, 10, 0, 0) };
                    var starBotImage = new Image { Margin = new Thickness(0, 0, 0, 10) };
       
                    var layout = new AbsoluteLayout { HeightRequest = 260, BackgroundColor = Color.Transparent, WidthRequest = 500, Margin = new Thickness(0, 20, 0, 0) };

                    var trophyIndexStack = new StackLayout { BackgroundColor = Color.FromHex("#32AE96"), HeightRequest = 260, WidthRequest = 50, Margin = new Thickness(20, 0, 0, 0) };

                    var dateFrame = new Frame { BackgroundColor = Color.FromHex("#32AE96"), HasShadow = false, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(5, 5, 5, 5), Margin = new Thickness(0, 10, 0, 0) };

                    var trophyFrame = new Frame { HasShadow = true, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(15, 15, 15, 15), Margin = new Thickness(20, 0, 20, 0) };
                    var trophyStack = new StackLayout { BackgroundColor = Color.White, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(20, 20, 20, 10) };

                    var trophyIndexLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    var trophyLabel = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Start, Margin = new Thickness(0, 40, 0, 0), FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) };
                    var nameLabel = new Label { Margin = new Thickness(0, 5, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.End };
                    var dateLabel = new Label { TextColor = Color.White, Margin = new Thickness(0, 0, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };

                    var frameStack = new StackLayout { BackgroundColor = Color.Transparent, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };

                    var trophyImage = new Image {Margin = new Thickness(15, 15, 15, 15) };


                    AbsoluteLayout.SetLayoutBounds(frameStack, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(frameStack, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(trophyFrame, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(trophyFrame, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(trophyStack, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(trophyStack, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(trophyIndexStack, new Rectangle(0, 0, 50, 260));

                    AbsoluteLayout.SetLayoutBounds(dateFrame, new Rectangle(1, 0, 0.3, 0.2));
                    AbsoluteLayout.SetLayoutFlags(dateFrame, AbsoluteLayoutFlags.All);

                    frameStack.Children.Add(trophyFrame);
                    trophyFrame.Content = trophyStack;
                    dateFrame.Content = dateLabel;

                    trophyStack.Children.Add(trophyLabel);
                    trophyStack.Children.Add(trophyImage);
                    trophyStack.Children.Add(nameLabel);

                    trophyIndexStack.Children.Add(starTopImage);
                    trophyIndexStack.Children.Add(trophyIndexLabel);
                    trophyIndexStack.Children.Add(starBotImage);

                  

                    if (trophies.Count > 0)
                    {

                        trophyIndexStack.BackgroundColor = Color.FromHex("#DAA520");
                        dateFrame.BackgroundColor = Color.FromHex("#DAA520");
                        starTopImage.Source = "star";
                        starBotImage.Source = "star";

                        layout.Children.Add(frameStack);
                        layout.Children.Add(trophyStack);

                        layout.Children.Add(trophyIndexStack);
                        layout.Children.Add(dateFrame);

                        trophyIndexLabel.Text = (counter + 1).ToString();

                        string roundSubStr = trophies[counter].Trophy;
                        trophyLabel.Text = roundSubStr;// Trophy name to display

                        DateTime dt = trophies[counter].CreatedOn;
                        string dateSubStr = dt.ToString("dd.MM.yyyy");
                        dateLabel.Text = dateSubStr;//Date the trophy was achieved to display.

                        string nameSubStr = trophies[counter].Name;
                        nameLabel.Text = nameSubStr;

                        string trophyPic = trophies[counter].TrophyPic;
                        trophyImage.Source = trophyPic;

                        gridLayout.Children.Add(layout, columnIndex, rowIndex);
                    }
                   
                   else if (trophies.Count == 0) // The trophy list is empty.
                    {
                        trophyStack.BackgroundColor = Color.Transparent;

                        layout.Children.Add(frameStack);
                        layout.Children.Add(trophyStack);
                        // layout.Children.Add(trophyIndexStack);

                        trophyLabel.Margin = new Thickness(0, 0, 0, 0);
                        trophyLabel.HorizontalTextAlignment = TextAlignment.Center;

                        trophyLabel.Text = "You don't have any trophies";
                        trophyImage.Source = "sad";
                        gridLayout.Children.Add(layout, columnIndex, rowIndex);

                    }

                    counter++;

                }// End of for column. 
           
             }// End of for row.

        }// End of Grid page Met

    } // End of class.
}// End of namespace.
