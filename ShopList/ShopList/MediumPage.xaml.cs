using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ShopList
{
    public partial class MediumPage : ContentPage
    {
        public int counter = 0;

        public int a = 0;

        public SQLDatabase sqlDatabase;
        public static List<MediumHighscore> mediumHighScores = new List<MediumHighscore>();

        public MediumPage()
        {
            InitializeComponent();

            sqlDatabase = new SQLDatabase();
            mediumHighScores = sqlDatabase.GetAllMediumHighscores();

            mediumHighScores.Reverse();

            gridPage();

           
        }

        public void gridPage()
        {

            int totalScores = 0;

            if (mediumHighScores.Count == 0)
            {
                totalScores = 1;
                a = 1;

            }

            else if (mediumHighScores.Count > 0 && mediumHighScores.Count < 10)
            {
                totalScores = mediumHighScores.Count;
            }

            else if (mediumHighScores.Count >= 10)
            {
                totalScores = 10;
            }


            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());
            gridLayout.RowDefinitions.Add(new RowDefinition());

            gridLayout.ColumnDefinitions.Add(new ColumnDefinition());



            for (int rowIndex = 0; rowIndex < 10; rowIndex++) // Adds all the Rows
            {
                for (int columnIndex = 0; columnIndex < 1; columnIndex++)// Adds all the columns 
                {

                    if (counter == totalScores)// Amount of images to display
                    {
                        break;
                    }


                    var starTopImage = new Image { Margin = new Thickness(0, 10, 0, 0) };
                    var starBotImage = new Image { Margin = new Thickness(0, 0, 0, 10) };
                    // var scoreButton = new Button { HeightRequest = 100, WidthRequest = 50, BackgroundColor = Color.Blue, TextColor = Color.White };

                    var layout = new AbsoluteLayout { HeightRequest = 100, BackgroundColor = Color.Transparent, WidthRequest = 500, Margin = new Thickness(0, 20, 0, 0) };

                    var scoreIndexStack = new StackLayout { BackgroundColor = Color.FromHex("#32AE96"), HeightRequest = 100, WidthRequest = 50, Margin = new Thickness(20, 0, 0, 0) };

                    var dateFrame = new Frame { BackgroundColor = Color.FromHex("#32AE96"), HasShadow = false, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(5, 5, 5, 5), Margin = new Thickness(0, 10, 0, 0) };

                    var scoreFrame = new Frame { HasShadow = true, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(10, 10, 10, 10), Margin = new Thickness(20, 0, 20, 0) };
                    var scoreStack = new StackLayout { BackgroundColor = Color.White, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(20, 20, 20, 10) };


                    var scoreIndexLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    var scoreLabel = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Start };
                    var nameLabel = new Label { Margin = new Thickness(0, 15, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.End };

                    var dateLabel = new Label { TextColor = Color.White, Margin = new Thickness(0, 0, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    //FontSize = 12,

                    var frameStack = new StackLayout { BackgroundColor = Color.Transparent, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };

                    AbsoluteLayout.SetLayoutBounds(frameStack, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(frameStack, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(scoreFrame, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(scoreFrame, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(scoreStack, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(scoreStack, AbsoluteLayoutFlags.All);


                    AbsoluteLayout.SetLayoutBounds(scoreIndexStack, new Rectangle(0, 0, 50, 100));

                    AbsoluteLayout.SetLayoutBounds(dateFrame, new Rectangle(1, 0, 0.3, 0.4));
                    AbsoluteLayout.SetLayoutFlags(dateFrame, AbsoluteLayoutFlags.All);

                    frameStack.Children.Add(scoreFrame);
                    scoreFrame.Content = scoreStack;
                    dateFrame.Content = dateLabel;

                    scoreStack.Children.Add(scoreLabel);
                    scoreStack.Children.Add(nameLabel);

                    scoreIndexStack.Children.Add(starTopImage);
                    scoreIndexStack.Children.Add(scoreIndexLabel);
                    scoreIndexStack.Children.Add(starBotImage);

                   
                    if (mediumHighScores.Count > 0)
                    {
                        if (counter == 0)
                        {
                            scoreIndexStack.BackgroundColor = Color.FromHex("#DAA520");
                            dateFrame.BackgroundColor = Color.FromHex("#DAA520");
                            starTopImage.Source = "star";
                            starBotImage.Source = "star";
                        }

                        else if (counter == 1)
                        {
                            scoreIndexStack.BackgroundColor = Color.FromHex("#E5E5E5");
                            dateFrame.BackgroundColor = Color.FromHex("#E5E5E5");

                        }

                        layout.Children.Add(frameStack);
                        layout.Children.Add(scoreStack);

                        layout.Children.Add(scoreIndexStack);
                        layout.Children.Add(dateFrame);

                        scoreIndexLabel.Text = (counter + 1).ToString();

                        MediumHighscore score = mediumHighScores[counter];

                        string roundSubStr = score.Round;
                        scoreLabel.Text = "Round: " + roundSubStr;//Round number score to display.

                        DateTime dt = score.CreatedOn;
                        string dateSubStr = dt.ToString("dd.MM.yyyy");
                        dateLabel.Text = dateSubStr;//Date of the score to display.

                        string nameSubStr = score.Name;
                        nameLabel.Text = nameSubStr;

                        gridLayout.Children.Add(layout, columnIndex, rowIndex);

                    }

                    else if (mediumHighScores.Count == 0 && a == 1) // The high scores list is empty and there are none to load. 

                    {
                        scoreStack.BackgroundColor = Color.Transparent;

                       
                        layout.Children.Add(frameStack);
                        layout.Children.Add(scoreStack);

                        //layout.Children.Add(scoreIndexStack);

                        //scoreLabel.Margin = new Thickness(50, 0, 0, 0);

                        scoreLabel.HorizontalTextAlignment = TextAlignment.Center;
                        scoreLabel.Text = "No highscores to show. \nLet's go make some!";

                        gridLayout.Children.Add(layout, columnIndex, rowIndex);

                    }


                    counter++;

                }// End of for column.
           
             }// End of for row.

        }// End of Grid page Met

    }

}
