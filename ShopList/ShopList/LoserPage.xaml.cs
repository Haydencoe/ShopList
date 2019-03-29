using System;
using System.Collections.Generic;

using PCLStorage;
using System.Threading.Tasks;
using DLToolkit.Forms.Controls;
using System.Linq;

using Xamarin.Forms;

namespace ShopList
{

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
        public bool alreadyAdded = false;
        public bool newHigh = false;
        public bool newAward = false;

        public int roundCount;
        public int loadedGamesPlayed;

        public string nameText;
        public string difFlag;
        public string playerMode;

        //public Grid pickedGridLayout = new Grid();

        public List<string> localList = new List<string>();

        public static List<HardHighscore> localHighScores = new List<HardHighscore>();
        public static List<MediumHighscore> localMediumScores = new List<MediumHighscore>();
        public static List<EasyHighscore> localEasyScores = new List<EasyHighscore>();
        public static List<Data> localData = new List<Data>();

        public SQLDatabase sqlDatabase;
        public static List<Trophies> localTrophyList = new List<Trophies>();
        public List<Trophies> newTrophiesList = new List<Trophies>();

        public LoserPage(int correctCount, int foodToFind, int roundCountRe, string difFlagIn, string playerModeIn)
        {
            // Sets local report lists to the ones brought in from playing page.
            roundCount = roundCountRe;
            difFlag = difFlagIn;

            InitializeComponent();

            // test.Text = GameReport.pickedFoodList.Count.ToString();

            //**** Data Gathering ******************** 
            sqlDatabase = new SQLDatabase();
            localData = sqlDatabase.GetAllData();

            foreach (Data data in localData)
            {
                if (data.CreatedOn == DateTime.Today)
                {
                    data.GamesPlayed = data.GamesPlayed + 1;

                    sqlDatabase.UpdateData(data);


                    if (roundCount > data.RoundCountHigh)
                    {
                        data.RoundCountHigh = roundCount;
                        sqlDatabase.UpdateData(data);
                    }
                }
            }
            //**** Data Gathering ******************** 

            nameEntry.IsVisible = false;

            playerMode = playerModeIn;

            mainMenu.Text = "Main Menu";
            restart.Text = "Restart Game";

            scoreLabel.HorizontalTextAlignment = TextAlignment.Center;
            scoreLabel.Text = "In the last round\nYou got " + correctCount + " out of " + foodToFind + " correct.";

            newHSLabel.HorizontalTextAlignment = TextAlignment.Center;
            nameEntry.HorizontalTextAlignment = TextAlignment.Center;

            sqlDatabase = new SQLDatabase();

            localTrophyList = sqlDatabase.GetAllTrophies();


            //******** HIGHSCORE CHECKER **************************************//
            // Start of Easy diff if.
            if (difFlag == "easy")
            {
                localEasyScores = sqlDatabase.GetAllEasyHighscores();
                Console.WriteLine(localEasyScores.Count);
         
                if (localEasyScores.Count < 10)
                {
                    nameEntry.IsVisible = true;
                    LoadName();
                    newHigh = true;
                    int topScore = 0;
                    if (localEasyScores.Count > 0)
                    {
                        EasyHighscore topScoreStrE = localEasyScores[0];
                        Int32.TryParse(topScoreStrE.Round, out topScore);
                    }

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

                else if (localEasyScores.Count == 0)
                {
                    endGame.Source = "podium2";
                    newHSLabel.Text = "Wow! \n New Top Highscore!";
                }

            else if (localEasyScores.Count == 10)
            {
                ScoreChecker(roundCount);
            }

        }// End of Easy diff if.

            // Start of Medium diff if.
            if (difFlag == "medium")
            {
                localMediumScores = sqlDatabase.GetAllMediumHighscores();
                Console.WriteLine(localMediumScores.Count);

                if (localMediumScores.Count < 10)
                {
                    nameEntry.IsVisible = true;
                    LoadName();
                    newHigh = true;
                    int topScore = 0;
                    if (localMediumScores.Count > 0)
                    {
                        MediumHighscore topScoreStrM = localMediumScores[0];
                        Int32.TryParse(topScoreStrM.Round, out topScore);
                    }

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

                else if (localMediumScores.Count == 0)
                {
                    endGame.Source = "podium2";
                    newHSLabel.Text = "Wow! \n New Top Highscore!";
                }

                else if (localMediumScores.Count == 10)
                {
                    ScoreChecker(roundCount);
                }

            }// End of Medium diff if.

            //Start of Hard diff if.
            if (difFlag == "hard")
            {
                localHighScores = sqlDatabase.GetAllHardHighscores();
                Console.WriteLine(localHighScores.Count);

                if (localHighScores.Count < 10)
                {
                    nameEntry.IsVisible = true;
                    LoadName();
                    newHigh = true;
                    int topScore = 0;
                    if (localHighScores.Count > 0)
                    {
                        HardHighscore topScoreStrH = localHighScores[0];
                        Int32.TryParse(topScoreStrH.Round, out topScore);
                    }

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

                else if (localHighScores.Count == 0)
                {
                    endGame.Source = "podium2";
                    newHSLabel.Text = "Wow! \n New Top Highscore!";
                }

                else if (localHighScores.Count == 10)
                {
                    ScoreChecker(roundCount);
                }

            }// End of Hard diff if.


            //******** TROPHY / AWARD CHECKER **************************************//

            AwardChecker(); // After score checker perfrom an award check 

            //******** BUTTON PRESSES **************************************//

            mainMenu.Clicked += mainMenu_Clicked;

            restart.Clicked += restart_Clicked;

            //******** POST GAME REPORT **************************************//

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


                     var imageReport = new Image { HeightRequest = 100, BackgroundColor = Color.Transparent };

                     itemStr = GameReport.pickedFoodList[counter].ToString();
                     int itemInt = GameReport.pickedFoodList[counter];

                    // iOS stuff
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        imageReport.Source = (itemStr);//sets the images to go into the grid

                    }

                    // Android stuff
                    if (Device.RuntimePlatform == Device.Android)
                    {
                        imageReport.Source = ("image" + itemStr);//sets the images to go into the grid

                    }

                    pickedGridLayout.Children.Add(imageReport, columnIndex, rowIndex);

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
            shoppingListGridLayout.ColumnDefinitions.Add(new ColumnDefinition());

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

                    var imagePicked = new Image { HeightRequest = 100, BackgroundColor = Color.Transparent };

                    shownStr = List.foodList[shownCounter].ToString();


                    //iOS stuff
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        imagePicked.Source = shownStr;//sets the images to go into the grid
                    }

                    //Android stuff
                    if (Device.RuntimePlatform == Device.Android)
                    {
                       imagePicked.Source = "image" + shownStr;//sets the images to go into the grid
                    }

                    shoppingListGridLayout.Children.Add(imagePicked, columnIndex, rowIndex);

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


        private async void mainMenu_Clicked(object sender, EventArgs e)
        {
            string nText = nameEntry.Text;

            if (alreadyAdded == false && newHigh == true)
            { 
                AddToList(nText); 
            }

            if (newAward == true)
            {
                SaveAwards(nText);
            }

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
            {
                AddToList(nText);
            }

            if (newAward == true)
            {
                SaveAwards(nText);
            }

            await Application.Current.MainPage.Navigation.PopModalAsync(true);

            //await Navigation.PushModalAsync(new playingGamePage());// Loads game play page
        }



        private void ScoreChecker(int roundToCheck)
        {
            Console.WriteLine("Score Checked");

            int intScore = 0;

            if (difFlag == "easy")
            {

                foreach (EasyHighscore strScore in localEasyScores)
                {

                    string prtStrScore = strScore.Round;

                    Int32.TryParse(prtStrScore, out intScore);


                    if (roundToCheck > intScore)
                    {
                        LoadName();
                        //Enter Name for new highscore.
                        nameEntry.IsVisible = true;

                        newHigh = true;

                        string topScoreStr = localEasyScores[0].Round;
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

                        localEasyScores.RemoveAt(localEasyScores.Count - 1);//Remove the lowest score

                        break;
                    }

                    else
                    {
                        endGame.Source = "sad";

                    }

                }// End of foreach

            }// End of easy if.

            if (difFlag == "medium")
            {

                foreach (MediumHighscore strScore in localMediumScores)
                {

                    string prtStrScore = strScore.Round;
                    Int32.TryParse(prtStrScore, out intScore);


                    if (roundToCheck > intScore)
                    {
                        LoadName();
                        //Enter Name for new highscore.
                        nameEntry.IsVisible = true;

                        newHigh = true;

                        string topScoreStr = localMediumScores[0].Round;
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

                        localMediumScores.RemoveAt(localMediumScores.Count - 1);//Remove the lowest score

                        break;
                    }

                    else
                    {
                        endGame.Source = "sad";

                    }

                }// End of foreach

            }// End of medium if.


            if (difFlag == "hard")
            {

                foreach (HardHighscore strScore in localHighScores)
                {

                    string prtStrScore = strScore.Round;
                    Int32.TryParse(prtStrScore, out intScore);

                    if (roundToCheck > intScore)
                    {
                        LoadName();
                        //Enter Name for new highscore.
                        nameEntry.IsVisible = true;

                        newHigh = true;

                        string topScoreStr = localHighScores[0].Round;
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

                        localHighScores.RemoveAt(localHighScores.Count - 1);//Remove the lowest score

                        break;
                    }

                    else
                    {
                        endGame.Source = "sad";

                    }

                }// End of foreach

            }// End of hard if.



        }// End of roundToCheck



        public async void AwardChecker()
        {

            loadedGamesPlayed = await GamesPlayed();
            Console.WriteLine("Games played: " + loadedGamesPlayed);


            //**** DEBUGGING ********//
            Console.WriteLine("Trophies Already Earned: ");
            foreach (Trophies trophy in localTrophyList)
            {
                Console.WriteLine(trophy.TrophyPic);
            }
            //**** DEBUGGING ********//

            //******* NOVICE AWARD ***********//
            var matchNoviceAward = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "noviceAward", StringComparison.CurrentCulture));
            Console.WriteLine(matchNoviceAward.Any());

            if (roundCount == 2 && matchNoviceAward.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Novice Award";
                trophy.TrophyPic = "noviceAward";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of novice award.

            //******* Bronze 5 AWARD ***********//
            var matchBronze5Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "bronze5Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchBronze5Award.Any());

            if (roundCount >= 5 && matchBronze5Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Bronze 5 Award";
                trophy.TrophyPic = "bronze5Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of bronze 5 award.


            //******* Silver 10 AWARD ***********//
            var matchSilver10Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "silver10Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchSilver10Award.Any());

            if (roundCount >= 10 && matchSilver10Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Silver 10 Award";
                trophy.TrophyPic = "silver10Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of silver 10 award.

            //******* Gold 25 AWARD ***********//
            var matchGold25Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "gold25Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchGold25Award.Any());


            if (roundCount >= 25 && matchGold25Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Gold 25 Award";
                trophy.TrophyPic = "gold25Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of gold 25 award.

            //******* Platinum 50 AWARD ***********//
            var matchPlatinum50Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "platinum50Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchPlatinum50Award.Any());


            if (roundCount >= 50 && matchPlatinum50Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Platinum 50 Award";
                trophy.TrophyPic = "platinum50Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of platinum 50 award.

            //******* Hayden's 100 AWARD ***********//
            var matchHaydens100Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "haydens100Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchHaydens100Award.Any());

            if (roundCount >= 100 && matchHaydens100Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Hayden's 100 Award";
                trophy.TrophyPic = "haydens100Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of Hayden's 100 award.


            //******* Multi AWARD ***********//
            var matchMultiAward = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "multiAward", StringComparison.CurrentCulture));
            Console.WriteLine(matchMultiAward.Any());


            if ( playerMode == "multi" && matchMultiAward.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Multiplayer Award";
                trophy.TrophyPic = "multiAward";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of multi award.

            //******* Multi 20 AWARD ***********//
            var matchMulti20Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "multi20Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchMulti20Award.Any());


            if (playerMode == "multi" && roundCount >= 20 && matchMulti20Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Multiplayer 20 Award";
                trophy.TrophyPic = "multi20Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of multi award.


            //******* Played 20 AWARD ***********//
            var matchPlayed20Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "played20Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchPlayed20Award.Any());


            if (loadedGamesPlayed == 20 && matchPlayed20Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Played 20 Games Award";
                trophy.TrophyPic = "played20Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of played 20 award.

            //******* Played 50 AWARD ***********//
            var matchPlayed50Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "played50Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchPlayed50Award.Any());


            if (loadedGamesPlayed == 50 && matchPlayed50Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Played 50 Games Award";
                trophy.TrophyPic = "played50Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of played 50 award.

            //******* Played 100 AWARD ***********//
            var matchPlayed100Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "played100Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchPlayed100Award.Any());


            if (loadedGamesPlayed == 100 && matchPlayed100Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Played 100 Games Award";
                trophy.TrophyPic = "played100Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of played 100 award.

            //******* Pro Hard 20 AWARD ***********//
            var matchProHard20Award = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "proHard20Award", StringComparison.CurrentCulture));
            Console.WriteLine(matchProHard20Award.Any());


            if (roundCount >= 20 && difFlag == "hard" && matchProHard20Award.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Pro on Hard 20 Award";
                trophy.TrophyPic = "proHard20Award";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of pro on hard 20 award.

            //******* Goldfish AWARD ***********//
            var matchGoldfishAward = localTrophyList.Where(Trophies => String.Equals(Trophies.TrophyPic, "goldfishAward", StringComparison.CurrentCulture));
            Console.WriteLine(matchGoldfishAward.Any());

            if (roundCount == 1 && matchGoldfishAward.Any() == false)
            {
                Trophies trophy = new Trophies();

                trophy.Trophy = "Goldfish Award";
                trophy.TrophyPic = "goldfishAward";

                newTrophiesList.Add(trophy);
                newAward = true;

            }// End of goldfish award.


            //**** DEBUGGING ********//
            Console.WriteLine("Trophies Earned During This Game: ");
            foreach (Trophies trophy in newTrophiesList)
            {
                Console.WriteLine(trophy.TrophyPic);
            }
            //**** DEBUGGING ********//

            if (newAward == true)
            {
                awardStack.IsVisible = true;

                if (newTrophiesList.Count > 1)
                    newAwardLabel.Text = "You earned new awards!";

                int rowAmount = newTrophiesList.Count;
                int counter = 0;

                for (int rowIndex = 0; rowIndex < rowAmount; rowIndex++) // Adds all the Rows
                {
                    for (int columnIndex = 0; columnIndex < 1; columnIndex++)// Adds all the columns 
                    {

                        if (counter == newTrophiesList.Count)// Amount of trophies to display
                        {
                            break;
                        }

                        var trophyStack = new StackLayout { };
                        var imageTrophy = new Image {HeightRequest = 100, HorizontalOptions = LayoutOptions.Center};
                        var trophyLabel = new Label { FontAttributes = FontAttributes.Bold, FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalOptions = LayoutOptions.Center };

                        trophyStack.Children.Add(imageTrophy);
                        trophyStack.Children.Add(trophyLabel);

                        //iOS stuff
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            imageTrophy.Source = newTrophiesList[counter].TrophyPic;//sets the images to go into the grid
                        }

                        //Android stuff
                        if (Device.RuntimePlatform == Device.Android)
                        {
                           imageTrophy.Source = newTrophiesList[counter].TrophyPic;//sets the images to go into the grid
                        }


                        trophyLabel.Text = newTrophiesList[counter].Trophy;

                        awardsGridLayout.Children.Add(trophyStack, columnIndex, rowIndex);

                        counter++;

                    }// End of for column

                }// End of for row

            }// End of if newAward == true

         }// End of award checker.


        void Entry_Completed(object sender, EventArgs e)
        {
            nameText = ((Entry)sender).Text; //cast sender to access the properties of the Entry

            if (alreadyAdded == false)
            {
                AddToList(nameText);
            }

            if (newAward == true)
            {

                SaveAwards(nameText);
            }

        }

       
        public void AddToList(string nameTextRe)
        {
               string roundCountStr = roundCount.ToString();

                if (roundCount < 10)
                    roundCountStr = "0" + roundCountStr;//Convert to two digit.

            if (difFlag == "easy")
            {
                EasyHighscore newScore = new EasyHighscore();

                newScore.Round = roundCountStr;
                newScore.Name = nameTextRe;
                newScore.CreatedOn = DateTime.Now;
                sqlDatabase.AddEasyHighscore(newScore);

                localEasyScores.Add(newScore);
                localEasyScores.Sort((p, q) => string.Compare(p.Round, q.Round, StringComparison.CurrentCulture));

                sqlDatabase = new SQLDatabase();
                sqlDatabase.DeleteAllEasyHighscores();

                int counter = localEasyScores.Count;

                foreach (EasyHighscore score in localEasyScores)
                {
                    score.ID = counter;
                    sqlDatabase.AddEasyHighscore(score);
                    counter--;
                }

            }// End of easy if.

            if (difFlag == "medium")
            {
                MediumHighscore newScore = new MediumHighscore();

                newScore.Round = roundCountStr;
                newScore.Name = nameTextRe;
                newScore.CreatedOn = DateTime.Now;
                sqlDatabase.AddMediumHighscore(newScore);

                localMediumScores.Add(newScore);
                localMediumScores.Sort((p, q) => string.Compare(p.Round, q.Round, StringComparison.CurrentCulture));

                sqlDatabase = new SQLDatabase();
                sqlDatabase.DeleteAllMediumHighscores();

                int counter = localMediumScores.Count;

                foreach (MediumHighscore score in localMediumScores)
                {
                    score.ID = counter;
                    sqlDatabase.AddMediumHighscore(score);
                    counter--;
                }

            }// End of medium if.

            if (difFlag == "hard")
            {
                HardHighscore newScore = new HardHighscore();

                newScore.Round = roundCountStr;
                newScore.Name = nameTextRe;
                newScore.CreatedOn = DateTime.Now;
                sqlDatabase.AddHardHighscore(newScore);


                localHighScores.Add(newScore);

                localHighScores.Sort((p, q) => string.Compare(p.Round, q.Round, StringComparison.CurrentCulture));

                sqlDatabase = new SQLDatabase();

                sqlDatabase.DeleteAllHardHighscores();

                int counter = localHighScores.Count;

                foreach (HardHighscore score in localHighScores)
                {
                    score.ID = counter;

                    sqlDatabase.AddHardHighscore(score);

                    counter--;
                }

            }// End of hard if.

            alreadyAdded = true;

            SaveName(nameTextRe);
           
        }


        private async void SaveName(string nameSave)
        {
            Notifications.notFlag++;// New high score notification counter increase.


            String folderName = "highScores";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);


            String scoreName = "highScoreName.txt";
            IFile file2 = await folder.CreateFileAsync(scoreName, CreationCollisionOption.ReplaceExisting);

            await file2.WriteAllTextAsync(nameSave);

        }


        private void SaveAwards(String nameRe) 
        {
            foreach(Trophies trophy in newTrophiesList)
            {
                trophy.Name = nameRe;
                trophy.CreatedOn = DateTime.Now;

                sqlDatabase.AddTrophy(trophy);

            }
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

        public static async Task<int> GamesPlayed()
        {
            int intTotalGames = 1;

            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder gamesPlayedFolder = await rootFolder.CreateFolderAsync("Games Played", CreationCollisionOption.OpenIfExists);

            ExistenceCheckResult exist = await gamesPlayedFolder.CheckExistsAsync("gamesPlayed.txt");

            string totalGames = null;
            if (exist == ExistenceCheckResult.FileExists)
            {
                IFile file = await gamesPlayedFolder.GetFileAsync("gamesPlayed.txt");
                totalGames = await file.ReadAllTextAsync();

                Int32.TryParse(totalGames, out intTotalGames);
                intTotalGames++;

            }

            String folderName = "Games Played";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);


            String gamesPlayedFile = "gamesPlayed.txt";
            IFile file2 = await gamesPlayedFolder.CreateFileAsync(gamesPlayedFile, CreationCollisionOption.ReplaceExisting);

            string saveTotalGames = intTotalGames.ToString(); 

            await file2.WriteAllTextAsync(saveTotalGames);

            return intTotalGames;
        }


    }// End of main Class 

}// End of Namespace
