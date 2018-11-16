using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;
using Newtonsoft.Json;

using Naxam.Controls.Forms;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Xamarin.Essentials;

using System.Collections.ObjectModel;

namespace ShopList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]



    public partial class ScoresPage : TopTabbedPage
    {

       // public int totalScores = 0;

        public int counter = 0;

        public int a = 0;


        public ScoresPage(int flag)
        {
            InitializeComponent();

           // var metrics = DeviceDisplay.ScreenMetrics;// Not needed but maybe useful else where.
            //var width = metrics.Width;


        }


        /*
        public void gridPage()
        {
            
           
            int totalScores = 0;

            if (HighScores.highScores.Count == 0)
            {
                totalScores = 1;
                a = 1;
            
            }

            else if (HighScores.highScores.Count > 0 && HighScores.highScores.Count < 10)
            {
                totalScores = HighScores.highScores.Count;
            }

            else if (HighScores.highScores.Count >= 10)
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


                   

                    var starTopImage = new Image {Margin = new Thickness(0, 10, 0, 0)};
                    var starBotImage = new Image {Margin = new Thickness(0, 0, 0, 10) };
                    // var scoreButton = new Button { HeightRequest = 100, WidthRequest = 50, BackgroundColor = Color.Blue, TextColor = Color.White };

                    var layout = new AbsoluteLayout { HeightRequest = 100, BackgroundColor = Color.Transparent, WidthRequest = 500, Margin = new Thickness(0, 20, 0, 0)};

                    var scoreIndexStack = new StackLayout {BackgroundColor = Color.FromHex("#32AE96"), HeightRequest = 100, WidthRequest = 50, Margin = new Thickness(20, 0, 0, 0) };
 
                   
                    var dateFrame = new Frame {BackgroundColor = Color.FromHex("#32AE96"), HasShadow = false, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(5, 5, 5, 5), Margin = new Thickness(0, 10, 0, 0) };

                    var scoreFrame = new Frame { HasShadow = true, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(10, 10, 10, 10), Margin = new Thickness(20, 0, 20, 0) };
                    var scoreStack = new StackLayout { BackgroundColor = Color.White, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(20, 20, 20, 0) };


                    var scoreIndexLabel = new Label {TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    var scoreLabel = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Start };
                    var nameLabel = new Label {Margin = new Thickness(0, 15, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.End };

                    var dateLabel = new Label { TextColor = Color.White, Margin = new Thickness(0, 0, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    //FontSize = 12,

                    AbsoluteLayout.SetLayoutBounds(scoreFrame, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(scoreFrame, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(scoreStack, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(scoreStack, AbsoluteLayoutFlags.All);


                    AbsoluteLayout.SetLayoutBounds(scoreIndexStack, new Rectangle(0, 0, 50, 100));

                    AbsoluteLayout.SetLayoutBounds(dateFrame, new Rectangle(1, 0, 0.3, 0.4));
                    AbsoluteLayout.SetLayoutFlags(dateFrame, AbsoluteLayoutFlags.All);

                   

                    scoreStack.Children.Add(scoreLabel);
                    scoreStack.Children.Add(nameLabel);

                    scoreIndexStack.Children.Add(starTopImage);
                    scoreIndexStack.Children.Add(scoreIndexLabel);
                    scoreIndexStack.Children.Add(starBotImage);

                    scoreFrame.Content = scoreStack;
                    dateFrame.Content = dateLabel;
                   

                  

                    if (HighScores.highScores.Count > 0)
                    {
                        if(counter == 0)
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

                       


                        layout.Children.Add(scoreFrame);
                        layout.Children.Add(scoreStack);
                       
                        layout.Children.Add(scoreIndexStack);
                        layout.Children.Add(dateFrame);

                        scoreIndexLabel.Text = (counter+1).ToString();

                        string productString = HighScores.highScores[counter];


                        string roundSubStr = productString.Substring(0, 9);
                        scoreLabel.Text = roundSubStr;//Round number score to display.

                        string dateSubStr = productString.Substring(9, 11);
                        dateLabel.Text = dateSubStr;//Date of the score to display.

                        string nameSubStr = productString.Substring(20);
                        nameLabel.Text = nameSubStr;

                        gridLayout.Children.Add(layout, columnIndex, rowIndex);
                           
                    }

                    else if (HighScores.highScores.Count == 0 && a == 1) // The high scores list is empty and there are none to load. 
                        
                    {
                        scoreStack.BackgroundColor = Color.Transparent;

                        layout.Children.Add(scoreFrame);
                        layout.Children.Add(scoreStack);
                        layout.Children.Add(scoreIndexStack);

                        scoreLabel.Margin = new Thickness(50, 0, 0, 0);
                        scoreLabel.HorizontalTextAlignment = TextAlignment.Center;
                        scoreLabel.Text = "No highscores to show. \nLet's go make some!";
                        
                        gridLayout.Children.Add(layout, columnIndex, rowIndex);
                     
                    }


                    counter++;
              

       

                }// End of for column.
            }// End of for row.

           

        }// End of Grid page Method. 

        /*
        public void gridPage2()
        {


            int totalScores = 0;

            if (HighScores.highScores.Count == 0)
            {
                totalScores = 1;
                a = 1;

            }

            else if (HighScores.highScores.Count > 0 && HighScores.highScores.Count < 10)
            {
                totalScores = HighScores.highScores.Count;
            }

            else if (HighScores.highScores.Count >= 10)
            {
                totalScores = 10;
            }


            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());
            gridLayout2.RowDefinitions.Add(new RowDefinition());

            gridLayout2.ColumnDefinitions.Add(new ColumnDefinition());



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
                    var scoreStack = new StackLayout { BackgroundColor = Color.White, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(20, 20, 20, 0) };


                    var scoreIndexLabel = new Label { TextColor = Color.White, HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    var scoreLabel = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.Start };
                    var nameLabel = new Label { Margin = new Thickness(0, 15, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.End };

                    var dateLabel = new Label { TextColor = Color.White, Margin = new Thickness(0, 0, 0, 0), HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                    //FontSize = 12,

                    AbsoluteLayout.SetLayoutBounds(scoreFrame, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(scoreFrame, AbsoluteLayoutFlags.All);

                    AbsoluteLayout.SetLayoutBounds(scoreStack, new Rectangle(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(scoreStack, AbsoluteLayoutFlags.All);


                    AbsoluteLayout.SetLayoutBounds(scoreIndexStack, new Rectangle(0, 0, 50, 100));

                    AbsoluteLayout.SetLayoutBounds(dateFrame, new Rectangle(1, 0, 0.3, 0.4));
                    AbsoluteLayout.SetLayoutFlags(dateFrame, AbsoluteLayoutFlags.All);



                    scoreStack.Children.Add(scoreLabel);
                    scoreStack.Children.Add(nameLabel);

                    scoreIndexStack.Children.Add(starTopImage);
                    scoreIndexStack.Children.Add(scoreIndexLabel);
                    scoreIndexStack.Children.Add(starBotImage);

                    scoreFrame.Content = scoreStack;
                    dateFrame.Content = dateLabel;




                    if (HighScores.highScores.Count > 0)
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




                        layout.Children.Add(scoreFrame);
                        layout.Children.Add(scoreStack);

                        layout.Children.Add(scoreIndexStack);
                        layout.Children.Add(dateFrame);

                        scoreIndexLabel.Text = (counter + 1).ToString();

                        string productString = HighScores.highScores[counter];


                        string roundSubStr = productString.Substring(0, 9);
                        scoreLabel.Text = roundSubStr;//Round number score to display.

                        string dateSubStr = productString.Substring(9, 11);
                        dateLabel.Text = dateSubStr;//Date of the score to display.

                        string nameSubStr = productString.Substring(20);
                        nameLabel.Text = nameSubStr;

                        gridLayout2.Children.Add(layout, columnIndex, rowIndex);

                    }

                    else if (HighScores.highScores.Count == 0 && a == 1) // The high scores list is empty and there are none to load. 

                    {
                        scoreStack.BackgroundColor = Color.Transparent;

                        layout.Children.Add(scoreFrame);
                        layout.Children.Add(scoreStack);
                        layout.Children.Add(scoreIndexStack);

                        scoreLabel.Margin = new Thickness(50, 0, 0, 0);
                        scoreLabel.HorizontalTextAlignment = TextAlignment.Center;
                        scoreLabel.Text = "No highscores to show. \nLet's go make some!";

                        gridLayout2.Children.Add(layout, columnIndex, rowIndex);

                    }


                    counter++;




                }// End of for column.
            }// End of for row.


        }// End of Grid page Method. 



        private void EasyButton_Clicked(object sender, EventArgs e)
        {
            this.CurrentPage = easyPage;
        }

        private void MediumButton_Clicked(object sender, EventArgs e)
        {
            this.CurrentPage = mediumPage;
        }

        private void HardButton_Clicked(object sender, EventArgs e)
        {
            this.CurrentPage = hardPage;
        }

        private void EasyButton_Clicked2(object sender, EventArgs e)
        {
            this.CurrentPage = easyPage;
        }

        private void MediumButton_Clicked2(object sender, EventArgs e)
        {
            this.CurrentPage = mediumPage;
        }

        private void HardButton_Clicked2(object sender, EventArgs e)
        {
            this.CurrentPage = hardPage;
        }

        private void EasyButton_Clicked3(object sender, EventArgs e)
        {
            this.CurrentPage = easyPage;
        }

        private void MediumButton_Clicked3(object sender, EventArgs e)
        {
            this.CurrentPage = mediumPage;
        }

        private void HardButton_Clicked3(object sender, EventArgs e)
        {
            this.CurrentPage = hardPage;
        }
*/

        /*
        public static async Task<string> GetScores()
        {
            string value = String.Empty;
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            // Read file
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync("highScoreString.txt");
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync("highScoreString.txt");
                value = await file.ReadAllTextAsync();
            }

            return value;
        }
        */

        /*
        public async static Task<string> ReadAllTextAsync(IFolder rootFolder = null)  
     {  
         string content = "";  
        
            String fileName = "highScoreString.txt";

            IFolder folder = rootFolder ?? FileSystem.Current.LocalStorage;  
        


            bool exist = await fileName.IsFileExistAsync(folder);  
         if (exist == true)  
         {  
             IFile file = await folder.GetFileAsync(fileName);  
             content = await file.ReadAllTextAsync();  
         }  
         return content;  
     }  




            String folderName = "highScores";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);
             

            String filename = "highScoreString.txt";
            // IFolder folder = FileSystem.Current.LocalStorage;
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
       
            await file.WriteAllTextAsync(json); 

      
        
          var scoreFrame = new Frame { HasShadow = true, HorizontalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 0, 0, 0), Margin = new Thickness(20, 20, 20, 0) };
          var scoreStack = new StackLayout { HeightRequest = 100, BackgroundColor = Color.White, WidthRequest = 500 };
          var scoreLabel = new Label { HorizontalOptions = LayoutOptions.CenterAndExpand, VerticalOptions = LayoutOptions.CenterAndExpand };
                 



                    //string str = HighScores.highScores.Count.ToString();
                    //theLabel.Text = str+totalScores.ToString()+counter.ToString();


*/

        /*
                   var buttonStyle = new Style(typeof(Button))
                   {
                       Setters = {
                           new Setter { Property = BackgroundColorProperty, Value = "#32AE96" },

                          // new Setter { Property = View.MarginProperty, Value = (2,20,2,2) },
                           new Setter { Property = View.HorizontalOptionsProperty, Value = LayoutOptions.FillAndExpand },
                           new Setter { Property = Button.TextColorProperty, Value = Color.White },
                           new Setter { Property = Button.FontSizeProperty, Value = NamedSize.Large }

                       }// End of setters


                   };

                   var scoreLabel = new Button { HeightRequest = 100, WidthRequest = 500 };
                   scoreLabel.Style = buttonStyle;





RelativeLayout.WidthConstraint=
        "{ConstraintExpression Type=RelativeToParent,
                               Property=Width,
                               Factor=1}"



                    */
        /*
        public void TableView()
        {
            
            scoreList.RowHeight = 100;


            //lst.ItemsSource = new List<string>() {HighScores.highScores};

            var oc = new ObservableCollection<String>();
            foreach (var item in HighScores.highScores)
                oc.Add(item);


            scoreList.ItemsSource = oc;
         
            
        }

       */
        /*
            buttonStack.WidthRequest = (width-20 ) / 3;
            easyButton.WidthRequest = (width-90)/9 ;
            mediumButton.WidthRequest = (width - 90) / 9;
            hardButton.WidthRequest = (width - 90) / 9;

            underLineStack.WidthRequest = (width-20) / 3;
            underLine.WidthRequest = (width-90) / 9;

            buttonStack2.WidthRequest = (width - 20) / 3;
            easyButton2.WidthRequest = (width - 90) / 9;
            mediumButton2.WidthRequest = (width - 90) / 9;
            hardButton2.WidthRequest = (width - 90) / 9;

            underLineStack2.WidthRequest = (width - 20) / 3;
            underLine2.WidthRequest = (width - 90) / 9;

            buttonStack3.WidthRequest = (width - 20) / 3;
            easyButton3.WidthRequest = (width - 90) / 9;
            mediumButton3.WidthRequest = (width - 90) / 9;
            hardButton3.WidthRequest = (width - 90) / 9;

            underLineStack3.WidthRequest = (width - 20) / 3;
            underLine3.WidthRequest = (width - 90) / 9; */

    }// End of class.          

}// End of namespace.