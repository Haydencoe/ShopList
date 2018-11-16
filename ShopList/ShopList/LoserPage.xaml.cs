using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PCLStorage;
using System.Threading.Tasks;
using DLToolkit.Forms.Controls;


using Xamarin.Forms;

namespace ShopList
{
    public static class HighScores
    {
       
      
        public static List<string> highScores = new List<string>();

        public static List<string> mediumHighScores = new List<string>();

        public static List<string> hardHighScores = new List<string>();
        //public static List<int> intHighScores = new List<int>();

    }

    public static class GameReport
    {
        // ******************Lists needed for the post game report*******************

        // Stores the wrong items that were picked.
        public static List<int> wrongFoodList = new List<int>();

        // Stores the right items that were picked.
        public static List<int> rightFoodList = new List<int>();

        // Stores all the items that were picked.
        public static List<int> pickedFoodList = new List<int>();

        // Strores all the items shown to the user.
        //public static List<int> foodList = new List<int>();

        // *******************************************************************

    }


    public partial class LoserPage : ContentPage
    {
        public string nameText;

        public int roundCount;

        public bool alreadyAdded = false;
        public bool newHigh = false;

        public string difFlag;

        //public Grid pickedGridLayout = new Grid();

        public List<string> localList = new List<string>();




        public LoserPage(int correctCount, int foodToFind, int roundCountRe, string difFlagIn)
        {
            // Sets local report lists to the ones brought in from playing page.
            roundCount = roundCountRe;
            difFlag = difFlagIn;

            InitializeComponent();


           

            // test.Text = GameReport.pickedFoodList.Count.ToString();

            nameEntry.IsVisible = false;

            mainMenu.Text = "Main Menu";
            restart.Text = "Restart Game";

            scoreLabel.HorizontalTextAlignment = TextAlignment.Center;
            scoreLabel.Text = "In the last round\nYou got " + correctCount + " out of " + foodToFind + " correct.";

            newHSLabel.HorizontalTextAlignment = TextAlignment.Center;
            nameEntry.HorizontalTextAlignment = TextAlignment.Center;

           
           
            switch (difFlag)
            {
                case "easy":
                    localList = HighScores.highScores;
                    break;
                case "medium":
                    localList = HighScores.mediumHighScores;
                    break;
                case "hard":
                    localList = HighScores.hardHighScores;
                    break;
            }

            if (localList.Count < 10)
                {

                    nameEntry.IsVisible = true;
                    LoadName();


                    newHigh = true;


                    int topScore = 0;


                if (localList.Count > 0)
                    {

                    string topScoreStr = localList[0].Substring(7, 2);

                        Int32.TryParse(topScoreStr, out topScore);


                        if (roundCount > topScore)
                        {
                            endGame.Source = "podium2";
                            newHSLabel.Text = "Wow! \n New Top Highscore!";


                        }

                        else
                        {

                            endGame.Source = "bigStar";
                            newHSLabel.Text = "New Highscore!";


                        }
                    }

                else if (localList.Count == 0)
                    {

                        endGame.Source = "podium2";
                        newHSLabel.Text = "Wow! \n New Top Highscore!";


                    }




                }// end of if.


            else if (localList.Count == 10)
                {

                    ScoreChecker(roundCount);

                }


           

           

            mainMenu.Clicked += mainMenu_Clicked;

            restart.Clicked += restart_Clicked;



            pickedGridLayout.RowDefinitions.Add(new RowDefinition());
            pickedGridLayout.ColumnDefinitions.Add(new ColumnDefinition());
           

            string itemStr;
            int counter = 0;


            int rowAmount = (GameReport.pickedFoodList.Count/4)+1;
           

            for (int rowIndex = 0; rowIndex < rowAmount; rowIndex++) // Adds all the Rows
            {
                for (int columnIndex = 0; columnIndex < 4; columnIndex++)// Adds all the columns 
                {
                   

                    if (counter == GameReport.pickedFoodList.Count)// Amount of images to display
                    {

                        break;
                    }





                     var imageButton = new Button { HeightRequest = 100 };

                     itemStr = GameReport.pickedFoodList[counter].ToString();
                     int itemInt = GameReport.pickedFoodList[counter];


                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        //iOS stuff
                        imageButton.Image = (itemStr);//sets the images to go into the grid

                        pickedGridLayout.Children.Add(imageButton, columnIndex, rowIndex);


                    }

                    //****** Right or Wrong markers on picked images list.
                    var rightOrWrongIndi = new Image { };

                    rightOrWrongIndi.HorizontalOptions = LayoutOptions.End;
                    rightOrWrongIndi.VerticalOptions   = LayoutOptions.End;

                    if (GameReport.rightFoodList.Contains(itemInt))
                    {
                        rightOrWrongIndi.Source        = "right";
                        rightOrWrongIndi.WidthRequest  = 25;
                        rightOrWrongIndi.HeightRequest = 25;
                        pickedGridLayout.Children.Add(rightOrWrongIndi, columnIndex, rowIndex);// adds the selected item indicator

                    }

                    else 
                    {
                        rightOrWrongIndi.Source        = "wrong";
                        rightOrWrongIndi.WidthRequest  = 15;
                        rightOrWrongIndi.HeightRequest = 15;
                        pickedGridLayout.Children.Add(rightOrWrongIndi, columnIndex, rowIndex);// adds the selected item indicator

                    }

                    //******





                    counter++;




                }// End of For loop for coloum.
            }


           






            shoppingListGridLayout.RowDefinitions.Add(new RowDefinition());
            //shoppingListGridLayout.RowDefinitions.Add(new RowDefinition());
           


            shoppingListGridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            //shoppingListGridLayout.ColumnDefinitions.Add(new ColumnDefinition());
           // shoppingListGridLayout.ColumnDefinitions.Add(new ColumnDefinition());


            string shownStr;
            int shownCounter = 0;

            int rowFoodListAmount = (List.foodList.Count / 4) + 1;

            for (int rowIndex = 0; rowIndex < rowFoodListAmount; rowIndex++) // Adds all the Rows
            {
                for (int columnIndex = 0; columnIndex < 4; columnIndex++)// Adds all the columns 
                {


                    if (shownCounter == List.foodList.Count)// Amount of images to display
                    {

                        break;
                    }

                    var imageButton = new Button { HeightRequest = 100 };

                    shownStr = List.foodList[shownCounter].ToString();



                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        //iOS stuff
                        imageButton.Image = (shownStr);//sets the images to go into the grid

                        shoppingListGridLayout.Children.Add(imageButton, columnIndex, rowIndex);
                    }

                    shownCounter++;

                }
            }




           // endRound.Children.Add(pickedGridLayout);


                }// End of main method.

        public void GridPage()
        {


        }


        private void FlowListView_OnFlowItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            DisplayAlert("OnFlowItemAppearing", "OnFlowItemAppearing", "ok");
        }

        public async void LoadName()
        {
           
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder highScoresFolder = await rootFolder.CreateFolderAsync("highScores", CreationCollisionOption.OpenIfExists);

            string fileContent = await ReadFileContent("highScoreName.txt", highScoresFolder);


           
            nameEntry.Text = fileContent;

        }

        private async void SaveScore(string nameSave)
        {
            Notifications.notFlag++;// New high score notification counter increase.

            switch (difFlag)
            {
                case "easy":
                    HighScores.highScores = localList;
                    await DisplayAlert("hi", "ths is happenign", "cancel");
                    break;
                case "medium":
                    HighScores.mediumHighScores = localList;
                    break;
                case "hard":
                    HighScores.hardHighScores = localList;
                    break;
            }



            String folderName = "highScores";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);



                //turn list into string
                var json       = JsonConvert.SerializeObject(HighScores.highScores);
                var jsonMedium = JsonConvert.SerializeObject(HighScores.mediumHighScores);
                var jsonHard   = JsonConvert.SerializeObject(HighScores.hardHighScores);


            String filename = "highScoreString.txt";
                // IFolder folder = FileSystem.Current.LocalStorage;
                IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

                await file.WriteAllTextAsync(json);

            String mediumFilename = "mediumHighScoreString.txt";
            // IFolder folder = FileSystem.Current.LocalStorage;
            IFile mediumFile = await folder.CreateFileAsync(mediumFilename, CreationCollisionOption.ReplaceExisting);

            await mediumFile.WriteAllTextAsync(jsonMedium);


            String hardFilename = "hardHighScoreString.txt";
            // IFolder folder = FileSystem.Current.LocalStorage;
            IFile hardFile = await folder.CreateFileAsync(hardFilename, CreationCollisionOption.ReplaceExisting);

            await hardFile.WriteAllTextAsync(jsonHard);


            String scoreName = "highScoreName.txt";
                IFile file2 = await folder.CreateFileAsync(scoreName, CreationCollisionOption.ReplaceExisting);

                await file2.WriteAllTextAsync(nameSave);


        }




        private async void mainMenu_Clicked(object sender, EventArgs e)
        {
            string nText = nameEntry.Text;

            if (alreadyAdded == false && newHigh == true)
                AddToList(nText);

            nameText = nameEntry.Text;

            int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
            for (int currModal = 0; currModal < numModals; currModal++)
            {

                await Application.Current.MainPage.Navigation.PopModalAsync(false);


            }


        }


        private async void restart_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PopModalAsync();
            string nText = nameEntry.Text;

            if (alreadyAdded == false && newHigh == true)
                AddToList(nText);
            
          
            await Application.Current.MainPage.Navigation.PopModalAsync(true);

            //await Navigation.PushModalAsync(new playingGamePage());// Loads game play page
        }



        private void ScoreChecker(int roundToCheck)
        {
            int intScore = 0;

            foreach (string strScore in localList)
            {

                string prtStrScore = strScore.Substring(7,2);

                Int32.TryParse(prtStrScore, out intScore);


                if (roundToCheck > intScore)
                {


                    LoadName();
                    //Enter Name for new highscore.
                    nameEntry.IsVisible = true;

                    newHigh = true;


                    string topScoreStr = localList[0].Substring(7, 2);

                    Int32.TryParse(topScoreStr, out int topScore);


                    if (roundToCheck > topScore)
                    {
                        endGame.Source = "podium2";

                        newHSLabel.Text = "Wow! \n New Top Highscore!";

                       
                    
                    }

                    else
                    {

                        endGame.Source = "bigStar";
                        newHSLabel.Text = "New Highscore!";

                       
                    }
                  

                    localList.RemoveAt(localList.Count - 1);//Remove the lowest score


                  

                    break;
                }

                else
                {
                    endGame.Source = "sad";

                }




            }
        }// End of roundToCheck

        void Entry_Completed(object sender, EventArgs e)
        {
            nameText = ((Entry)sender).Text; //cast sender to access the properties of the Entry

     
            if (alreadyAdded == false)
            AddToList(nameText);
        

        }

       

        public void AddToList(string nameTextRe)
        {
               string roundCountStr = roundCount.ToString();

                if (roundCount < 10)
                    roundCountStr = "0" + roundCountStr;//Convert to two digit.

                String myDate = DateTime.Now.ToString("dd.MM.yyyy");

                localList.Add("Round: " + roundCountStr + " " + myDate + nameTextRe);

                localList.Sort();// sorts the highscore list again with the new score.
                localList.Reverse();

                alreadyAdded = true;



                SaveScore(nameTextRe);
           

            /*
            else if (difFlag == "medium")
            {
                string roundCountStr = roundCount.ToString();

                if (roundCount < 10)
                    roundCountStr = "0" + roundCountStr;//Convert to two digit.

                String myDate = DateTime.Now.ToString("dd.MM.yyyy");

                HighScores.mediumHighScores.Add("Round: " + roundCountStr + " " + myDate + nameTextRe);

                HighScores.mediumHighScores.Sort();// sorts the highscore list again with the new score.
                HighScores.mediumHighScores.Reverse();

                alreadyAdded = true;



                SaveScore(nameTextRe);
            }
            */

        }

        public static async Task<string> ReadFileContent(string fileName, IFolder rootFolder)
        {
            ExistenceCheckResult exist = await rootFolder.CheckExistsAsync(fileName);

            string text = null;
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await rootFolder.GetFileAsync(fileName);
                text = await file.ReadAllTextAsync();
            }

            return text;
        }

        /*
        private string fileText;
        

        public string FileText
        {
            get { return fileText; }
            set
            {
                if (FileText == value) return;
                fileText = value;
                OnPropertyChanged();
            }
        }

        public async Task WriteFile()

        {
            var file =
                await
                    FileSystem.Current.LocalStorage.CreateFileAsync("test.txt", CreationCollisionOption.ReplaceExisting);
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            using (var writer = new StreamWriter(stream))
            {
                await writer.WriteAsync(FileText);
            }
        }
        public async Task ReadFile()
        {
            var file =
                await
                    FileSystem.Current.LocalStorage.GetFileAsync("test.txt");
            using (var stream = await file.OpenAsync(FileAccess.Read))
            using (var reader = new StreamReader(stream))
            {
                FileText = await reader.ReadToEndAsync();
            }
        }

*/
        /*
       void Entry_TextChanged(object sender, TextChangedEventArgs e)
       {
          //var oldText = e.OldTextValue;
           var newText = e.NewTextValue;

           if (alreadyAdded == false)
           AddToList(newText);

       }*/
        /*
                  string roundToCheckStr = roundToCheck.ToString();

                  if (roundToCheck < 10)
                      roundToCheckStr = "0" + roundToCheckStr;


                  HighScores.highScores.Add("Round: " + roundToCheckStr);

                  HighScores.highScores.Sort();// sorts the highscore list again with the new score.
                  HighScores.highScores.Reverse();

                  SaveScore();
                  */
        /*
           List<Image> pictureList = new List<Image>();

           var image = new Image{};
           image.HeightRequest = 100;

           for (int i = 0; i < GameReport.pickedFoodList.Count; i++)
           {
               image.Source = GameReport.pickedFoodList[i].ToString();

               pictureList.Add(image);

           }

           //shownItems.Text = pictureList.Count.ToString();

           myList.FlowItemsSource = pictureList;

           myList.FlowUseAbsoluteLayoutInternally = true;
           myList.FlowColumnCount = 3;
           myList.FlowRowBackgroundColor = Color.Green;
           myList.FlowColumnExpand = FlowColumnExpand.Proportional;
           */



    }// End of main Class 
}// End of Namespace
