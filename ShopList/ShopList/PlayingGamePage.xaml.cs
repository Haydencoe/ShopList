using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ShopList
{
    public static class List
    {
        // Strores all the items shown to the user.

        public static List<int> foodList = new List<int>();
    }

        [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayingGamePage : ContentPage
    {
        public int foodArrayCount = 0;
        public string difFlag;
        public string playerMode;

        public PlayingGamePage(string difFlagIn, string playerModeIn)
        {

            InitializeComponent();

            List.foodList.Clear();
            GameReport.pickedFoodList.Clear();

            difFlag = difFlagIn;
            playerMode = playerModeIn;

            //listLabel.Text = difFlag;

            gridLayout.IsVisible  = false;
            timeStack.IsVisible   = true;
            selectStack.IsVisible = true;
            roundStack.IsVisible  = true;

            selectStack.Opacity = 0;// Faded out.
            roundStack.Opacity  = 0;// Faded out.
            timeStack.Opacity   = 0;// Faded out.
     
        }

        public bool localLLF = true;

        protected override void OnAppearing()
        {
            List.foodList.Clear();
            GameReport.pickedFoodList.Clear();

            // Code for game information to be hidden
            timeStack.Opacity   = 0;// Faded out.
            selectStack.Opacity = 0;// Faded out.
            roundStack.Opacity  = 0;// Faded out.
            //

            //Clear the grid to refill.
            gridLayout.ColumnDefinitions.Clear();
            gridLayout.RowDefinitions.Clear();

            //Make sure everything is reset
            correctCount = 0;// resets the correct amount found to 0
            foodToSelect = 0;// resets the left to select amount  to 0
            selectCount  = 0;// resets the select amount  to 0
            roundCount   = 1;// Starts again from the first round.

           // gridLayout.Children.Clear();
            //storeList.Clear();

            //
            listLabel.IsVisible = true;
            listImage.IsVisible = true;
            //

            listLabel.Opacity = 1;

            //await roundStack.TranslateTo(-this.Width, 0, 5);//hide it at the start
            //await selectStack.TranslateTo(-this.Width, 0, 5);//hide it at the start

            listImages(); //Call method to show the list of food images to find.
        
            base.OnAppearing();
        }

        //Local varibles 
        //public string difficulty = "easy";
        //***************** DIFFCULTY VARIBLE ***************************
        //public static int[] randomFoodToShow = new int[foodArrayCount];// images to put in random order.

      

        // ******************Lists needed for the post game report*******************

        // Stores the wrong items that were picked.
        public List<int> wrongFoodList = new List<int>();

       
        // *******************************************************************

        //List that stores all the selected items
        //public List<int> selectedFoodList = new List<int>();

        //List that stores all the items in grid
        public List<string> storeList = new List<string>();
      
        public int _duration = 30;

        public Image itemImage = new Image {}; 

        public int selectCount  = 0;
        public int correctCount = 0;
        public int roundCount   = 1;

        //public Button imageButton = new Button {HeightRequest = 120 }; 

        public int foodToFind   = 0;
       
        public int foodToSelect = 0;

        public int foodToShow   = 0;

        public StackLayout centerStack;

        public AbsoluteLayout layout = new AbsoluteLayout { };

        public int[] randomFoodToShow = new int[0];

        public async void listImages()   
        {

            await roundStack.TranslateTo (-this.Width, 0, 1);//hide it in the left at the start.
            await selectStack.TranslateTo(-this.Width, 0, 1);//hide it in the left at the start.
            await timeStack.TranslateTo  ( this.Width, 0, 1);//hide it in the right at the start.
            selectStack.Opacity = 1;// Faded in.
            roundStack.Opacity  = 1;// Faded in.
            timeStack.Opacity   = 1;// Faded in.

            // Code for game information to be hidden
            //timeStack.Opacity     = 0;// Faded out.
            // selectStack.Opacity = 0;// Faded out.
            //roundStack.Opacity  = 0;// Faded out.
            //
            gridLayout.Opacity  = 0;// Faded out.

            int x = List.foodList.Count;        
           
            if (roundCount == 1)
            {

                foodToShow = FoodToShowMeth();// amount of items to show on the first round.

                // Randomised once at the start of the game.
                randomFoodToShow = new int[177];

                // Array to store the randomised index of items, 177 is the total number of food images availble.
                randomFoodToShow = GetRandomizedArray(177);

                foodToFind = foodToShow;
            }

            else
            {
                foodToFind++;
                foodToShow = 1;
            }

            foodToSelect = foodToFind;

                if(roundCount == 1)
                {

                bool a = false;// first loop counter

                for (int i = x; i < (foodToShow + x); i++)
                {

                    if (a == true)
                    {
                        await listImage.FadeTo(0, 500); // Fade out nicely if more than one loop.
                        listImage.Source = null;
                    }

                    else
                    {
                        listImage.Opacity = 0;// Already faded out for first loop
                    }

                    List.foodList.Add(randomFoodToShow[i]);// Adds the new picked item to the stored list. 

                    string foodString = randomFoodToShow[i].ToString(); // Converts the int to string to call the image number.  

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        //iOS stuff
                        listImage.Source = ImageSource.FromFile(foodString); // displays the picked image.
                    }

                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        //Android stuff
                        listImage.Source = ImageSource.FromFile("image" + foodString); // displays the picked image.
                    }


                    if (a == true)
                    {
                        await Task.Delay(500);// time to load image
                    }
                    // if (localLLF == false)
                    //{//
                    // await listLabel.FadeTo(1, 500);// Fade in.
                    //
                    //}

                    await listImage.FadeTo(1, 500);// Fade in.

                    a = true;

                    // await listImage.FadeTo(0, 500); // Fade out nicely if more than one loop.

                }

            }// End of if.

                else
                {
                   
                    List.foodList.Add(randomFoodToShow[foodToShow + x]);// Adds the new picked item to the stored list. 

                    string foodString = randomFoodToShow[foodToShow + x].ToString(); // Converts the int to string to call the image number.  

                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        //iOS stuff
                        listImage.Source = ImageSource.FromFile(foodString); // displays the picked image.
                    }
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        //Android stuff
                        listImage.Source = ImageSource.FromFile("image" + foodString); // displays the picked image.
                    }

                    await Task.Delay(500);// time to change image

                    listLabel.IsVisible = true;
                    listImage.IsVisible = true;

                    await listLabel.FadeTo(1, 500); // Fade in.

                    await listImage.FadeTo(1, 500);// Fade back in.
                    await listImage.FadeTo(0, 500);// Fade back out.

                 }// End of else.

                //if (roundCount == 1)
                //{
                    await listImage.FadeTo(0, 500); // Fade out.
                    await listLabel.FadeTo(0, 500); // Fade out.
                    //localLLF = false;
                //}  

                  
            
           // }// End of for loop.


            gridPage();// Call grid layout method.

        }// End of listImages().



        public async void gridPage()
        {
            listImage.IsVisible = false;
            listLabel.IsVisible = false;

            gridLayout.IsEnabled = true;//

            gridLayout.IsVisible = true;//Fix for bug that needs more investigating
           
            // if statement to only load in all the rows and columns on the first round.
            if (roundCount == 1 )
            {
                gridLayout.RowDefinitions.Add(new RowDefinition());
                gridLayout.RowDefinitions.Add(new RowDefinition());
                gridLayout.RowDefinitions.Add(new RowDefinition());
                gridLayout.RowDefinitions.Add(new RowDefinition());
                gridLayout.RowDefinitions.Add(new RowDefinition());
                gridLayout.RowDefinitions.Add(new RowDefinition());
                gridLayout.RowDefinitions.Add(new RowDefinition());

                gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
                gridLayout.ColumnDefinitions.Add(new ColumnDefinition());
            
            }

           
            var productIndex = 0;


            //***************** DIFFCULTY VARIBLE: NUMBER OF ITEMS DISPLAYED***************************
            // DIFFCULTY VARIBLE VALUE CREATION
            foodArrayCount = FoodArraySize(); // 24 for medium on 1st round.

            //int[] randomL = GetRandomizedArray(foodArrayCount);// Array to store randomised index so grid is reordered each time
           
             List<int> temp = new List<int>(); // New temp list. 

            for (int i = 0; i < foodArrayCount; i++)
            {
                // Load the temp list with the first count of items to fill the gtid with from the randomFoodToShow order to ensure the items that were shown appear in the grid.
                temp.Add(randomFoodToShow[i]);
            }

            int[] randomL = temp.ToArray(); // Convert the list to an array for shuffle.

            Shuffle<int>(randomL); // Shuffle the array each time so the grid has a new layout each round


            int rowsNeeded = (foodArrayCount/3)+1; // Calculation for the amount of rows to create in the grid to fit them all in. 


            for (int rowIndex = 0; rowIndex < rowsNeeded; rowIndex++) // Adds all the Rows
            {
                for (int columnIndex = 0; columnIndex < 3; columnIndex++)// Adds all the columns 
                {         

                    //***************** DIFFCULTY VARIBLE ***************************
                    if (productIndex == foodArrayCount)// Amount of images to display
                    {
                        break;
                    }

       
                    string productString = randomL[productIndex].ToString();

                    productIndex++;

                    itemImage = new Image {HeightRequest = 120 };

                    var imageButton = new Button {HeightRequest = 120};

                    imageButton.Clicked += imageButton_Clicked;


                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        //iOS stuff
                        imageButton.Image = (productString);//sets the images to go into the grid
                    
                        gridLayout.Children.Add(imageButton, columnIndex, rowIndex);
                    }
                   
                    else if (Device.RuntimePlatform == Device.Android)
                    {
                        //Android stuff
                        //imageButton.Image = ("image"+productString);//sets the images to go into the grid
                   
                        itemImage.Source = ("image" + productString);

                        imageButton.BackgroundColor = Color.Transparent;

                        gridLayout.Children.Add(itemImage, columnIndex, rowIndex);

                        gridLayout.Children.Add(imageButton, columnIndex, rowIndex);
                    }



                    string a = columnIndex.ToString();
                    string b = rowIndex.ToString();

                    //if the row no. is single digit, it's coverted into a double digit.
                    if (b.Length == 1)
                        b = "0" + b;


                    string local = a + b + productString;

                    storeList.Add(local);
                }
            }
            // timeLabel.Text =  _duration + " Round: " + roundCount;
            // timeLabel.Text = "Time remaining: ss Round: 1 ";

            string selectLabelString = foodToSelect.ToString();
            selectLabel.Text = "Select: " + selectLabelString;

            roundLabel.Text = "Round: " + roundCount;

            // Increases the time to select all items 
            if (roundCount > 1)
                _duration = TimeToShowMeth();

            timeLabel.Text = "00:"+_duration;// Filler till time loads

            //Code to show game information.
            timeStack.IsVisible   = true;
            selectStack.IsVisible = true;
            roundStack.IsVisible  = true;
            //
            gridLayout.IsVisible  = true;//Show grid.


            // var layout = new AbsoluteLayout {};

            //mainStack.Children.Remove(layout);
            // layout.Children.Clear();


            AbsoluteLayout.SetLayoutBounds(scroll, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(scroll, AbsoluteLayoutFlags.All);

            layout.Children.Add(scroll);

            AbsoluteLayout.SetLayoutBounds(gameInfo, new Rectangle(0, 0, 1, 0.1));
            AbsoluteLayout.SetLayoutFlags(gameInfo, AbsoluteLayoutFlags.All);

            layout.Children.Add(gameInfo);


            //layout.IsVisible = true;
            mainStack.Children.Add(layout);


            await gridLayout.FadeTo(1, 700);// Fade in.


            //Code for game information to appear.
            await Task.WhenAll(

           roundStack.TranslateTo (roundStack.Width  - roundStack.Width,  0, 500),//slide in from left
           selectStack.TranslateTo(selectStack.Width - selectStack.Width, 0, 500),//slide in from left
           timeStack.TranslateTo  (-(timeStack.Width - timeStack.Width),  0, 500) //slide in from right

            );
            //

            StartTimer(_duration);// Start the count down timer.

        }// End of Grid page Method 

      

        private async void imageButton_Clicked(object sender, EventArgs e)
        {

            if (Sound.soundFlag == true)
            {
                var player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
                player.Load("NFF-choice-good.wav");

                player.Play();// Cammand the play the loaded sound.
            }

            foodToSelect--;

            string selectLabelString  = foodToSelect.ToString();

            selectLabel.Text = "Select: " + selectLabelString;

            selectCount++;

            var buttonStyle = new Style(typeof(Button))
            {
                Setters = {
                new Setter { Property = BackgroundColorProperty, Value = Color.White },
                new Setter { Property = Button.CornerRadiusProperty, Value = 5 },
                new Setter { Property = Button.BorderColorProperty, Value = "#FFBC2D" },
                new Setter { Property = Button.BorderWidthProperty, Value = 3 },
                new Setter { Property = View.MarginProperty, Value = (2,2,2,2) },
                

                }// End of setters
            };
           
            (sender as Button).Style = buttonStyle;


            (sender as Button).IsEnabled = false;// Prevents the same item being selected more than once.



            string selectCountText = selectCount.ToString();           

            var selButtonIndex = new Button { };
        
            // Assign the selected button row and column inside the grid.
            var row = (int)((BindableObject)sender).GetValue(Grid.RowProperty);
            var column = (int)((BindableObject)sender).GetValue(Grid.ColumnProperty);

            string a = column.ToString();
            string b = row.ToString();

            if (b.Length == 1)// Changes a 1 digit row grid ref to a 2
                b = "0" + b;

            string c = a + b;

            int z = 0;// Counter for wrong item checker.




            for (int i = 0; i < storeList.Count; i++)
            {
                 
                string d = storeList[i].Substring(0, 3);

                if (c == d)// finds the selected grid ref
                {
                    // Adds the picked items number to the pickedList
                    Int32.TryParse(storeList[i].Substring(3), out int pickedInt);
                    GameReport.pickedFoodList.Add(pickedInt);
                    //

                    for (int j = 0; j < List.foodList.Count; j++)
                    {
                        string shownFood = List.foodList[j].ToString();

                        // checks if the selected grid ref contains an item to be found.


                        if (storeList[i].Substring(3) == shownFood)
                        {
                            correctCount++;

                            // Adds right item number to right list.
                            Int32.TryParse(storeList[i].Substring(3), out int rightInt);
                           

                            GameReport.rightFoodList.Add(rightInt);
                            //

                            break;
                        }

                        // Else, will be invoked each time the selected item hasn't match with the checked agains't item in the list.
                        else
                        {

                            //This would be invoked if the selected button pressed was confirmed wrong once the whole list has been checked.

                            z++;
                            if (z == List.foodList.Count)
                            {
                                // await DisplayAlert("Alert", "You have selected a wrong item", "OK");// Debugging

                                // Adds wrong items number to wrong list
                                Int32.TryParse(storeList[i].Substring(3), out int wrongInt);
                                wrongFoodList.Add(wrongInt);
                                //
                            }
                             
                        }// End of else.

                    }// End of for loop.

                    break;   // Break the loop once the grid ref has been found.
                }

                else
                {
                    //This would be invoked if the grid button pressed couldn't be found. 

                }


            }
            List<int> listRow = new List<int>();
            List<int> listColum = new List<int>(); 

            listRow.Add(row);
            listColum.Add(column);

            selButtonIndex.HorizontalOptions = LayoutOptions.Start;
            selButtonIndex.VerticalOptions = LayoutOptions.Start;
            selButtonIndex.Text = selectCountText;
            selButtonIndex.BackgroundColor = Color.FromHex("#FFBC2D");
            selButtonIndex.TextColor = Color.White;
            selButtonIndex.CornerRadius = 5;
            selButtonIndex.WidthRequest = 25;
            selButtonIndex.HeightRequest = 25;

            gridLayout.Children.Add(selButtonIndex, column, row);// adds the selected item indicator 


            if (foodToSelect == 0)//all items have been picked
            {
                gridLayout.IsEnabled = false;// Stops anymore items being selected after the round has finished.
                await Task.Delay(1000); // Waits 1 second before the end of the round.
                EndOfRound();// Calls the end of the round method.
               
            }
        
        }// end of image button clicked



        int[] GetRandomizedArray(int n)
        {
            return GetRandomizedEnumerable(n).ToArray();
        }

        IEnumerable<int> GetRandomizedEnumerable(int n)// Array randomiser.
        {
            var random = new Random();
            var l = Enumerable.Range(1, n).ToList();
            foreach (var r in Enumerable.Range(0, n).Reverse().Select(i => random.Next(i + 1)))
            {
                yield return l[r];
                l.RemoveAt(r);
            }
        }


        public async void StartTimer(int seconds)
        {
            // Tick every second while game is in progress
            while (seconds > 0 && foodToSelect > 0)
                
            {
                //string s = TimeSpan.FromSeconds(_duration).ToString(@"ss");
                await Task.Delay(1000); // Waits 1 second between each number.

                seconds--; // Count down a number

                TimeSpan time = TimeSpan.FromSeconds(seconds);

                //here backslash is must to tell that colon is
                //not the part of format, it just a character that we want in output
                string s = time.ToString(@"mm\:ss");
                //string s = seconds.ToString(@"MM:ss");// converts the 'numbers' to string format for the label.

                timeLabel.Text =  s ;// display the current number.
             }

            if (seconds == 0)
            {
                StackLayout timeUpStack = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand, Opacity = 100 };
                Image timeUpImage = new Image { VerticalOptions = LayoutOptions.Center, Opacity = 100 };
                Label timeUpLabel = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Opacity = 100, FontSize = 36 };
                timeUpImage.Source = ("noTime");
                timeUpLabel.Text = "Times Up!!!";
                timeUpStack.Children.Add(timeUpImage);
                timeUpStack.Children.Add(timeUpLabel);
                Content = timeUpStack;

                await Task.Delay(1000); // Wait 2 seconds before round finishes.

                await Task.WhenAll(
                    timeUpLabel.FadeTo(0, 500),
                    timeUpImage.FadeTo(0, 500));

                //Hide elements for end of round.
                await roundStack.TranslateTo (-this.Width, 0, 1);//hide it at the start
                await selectStack.TranslateTo(-this.Width, 0, 1);//hide it at the start
                await timeStack.TranslateTo  (-this.Width, 0, 1);//hide it at the start

                timeStack.IsVisible = false;
                selectStack.IsVisible = false;
                roundStack.IsVisible = false;

                gridLayout.Children.Clear();
                storeList.Clear();

                await Navigation.PushModalAsync(new LoserPage(correctCount, foodToFind, roundCount, difFlag, playerMode));// Loads the loser page

                Content = mainStack;
            }
        }

        public async void EndOfRound()
        {
            if (foodToFind > correctCount)
            {
                await roundStack.TranslateTo (-this.Width, 0, 1);//hide it at the start
                await selectStack.TranslateTo(-this.Width, 0, 1);//hide it at the start
                await timeStack.TranslateTo  (-this.Width, 0, 1);//hide it at the start

                timeStack.IsVisible   = false;
                selectStack.IsVisible = false;
                roundStack.IsVisible  = false;
              
                gridLayout.Children.Clear();
                storeList.Clear();

                await Navigation.PushModalAsync(new LoserPage(correctCount, foodToFind, roundCount, difFlag, playerMode));// Loads the loser page

                //Make sure everything is reset
                //correctCount = 0;// resets the correct amount found to 0
                //foodToSelect = 0;// resets the left to select amount  to 0
                //selectCount = 0;// resets the select amount  to 0

                // gridLayout.ColumnDefinitions.Clear();
                //gridLayout.RowDefinitions.Clear();

            }

            else
            {
                // Clear end of round report lists
                wrongFoodList.Clear();
                GameReport.rightFoodList.Clear();
                GameReport.pickedFoodList.Clear();
                //

                //Hide elements for end of round.
        
                await gridLayout.FadeTo(0, 400);
                gridLayout.IsVisible = false;

                //Code to hide the game play information.
                await Task.WhenAll(
                selectStack.TranslateTo(-this.Width, 0, 500),
                roundStack.TranslateTo(-this.Width, 0, 500),
                timeStack.TranslateTo(this.Width, 0, 500)
                );
                //

               
                selectStack.IsVisible = false;
                roundStack.IsVisible  = false;
                timeStack.IsVisible   = false;

                // Good work info displayed centered.
                centerStack = new StackLayout { VerticalOptions = LayoutOptions.CenterAndExpand, Opacity = 0 };
                Image centerImage = new Image { VerticalOptions = LayoutOptions.Center, Opacity = 100, WidthRequest = 200, HeightRequest = 200 };
                Label centerLabel = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Opacity = 100, FontSize = 36 };
                centerImage.Source = ("greenTick");
                centerLabel.Text = "Nice!";
                centerStack.Children.Add(centerImage);
                centerStack.Children.Add(centerLabel);


                //******** Multiplayer mode switch over  *****************

                if (playerMode == "multi")
                {
                    Label multiLabel = new Label { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Opacity = 100, FontSize = 20, Margin = new Thickness(0, 20, 0, 0), HorizontalTextAlignment = TextAlignment.Center }; 
                    Button continueButton = new Button {  HeightRequest = 50, WidthRequest = 200,  HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, Opacity = 100, FontSize = 20, Margin = new Thickness(0, 20, 0, 0) };

                    continueButton.Clicked += Continue_Clicked;
                    continueButton.Image = "goButton";
                    multiLabel.Text = "Now swap players \n then press the go button";
                    centerStack.Children.Add(multiLabel);
                    centerStack.Children.Add(continueButton);

                    Content = centerStack;

                    await centerStack.FadeTo(1, 200);// Fade in.
                }

                // ***********************************************

                else
                {
                    Content = centerStack;

                    await centerStack.FadeTo(1, 200);// Fade in.

                    await Task.Delay(600);// time to see the image
                    await centerStack.FadeTo(0, 200);// Fade back out.

                    //await Task.Delay(500); // Waits 1 second

                    Content = mainStack;

                    correctCount = 0;// resets the correct amount found to 0
                    foodToSelect = 0;// resets the left to select amount  to 0
                    selectCount  = 0;// resets the select amount  to 0

                    gridLayout.Children.Clear();
                    storeList.Clear();

                    roundCount++;

                    listImages();// Calls for the next item to be added and shown.

                }
            
            }
        }


        private async void Continue_Clicked(object sender, EventArgs e)
        {
            await centerStack.FadeTo(0, 200);// Fade back out.

            //await Task.Delay(500); // Waits 1 second

            Content = mainStack;

            correctCount = 0;// resets the correct amount found to 0
            foodToSelect = 0;// resets the left to select amount  to 0
            selectCount = 0;// resets the select amount  to 0

            gridLayout.Children.Clear();
            storeList.Clear();

            roundCount++;

            listImages();// Calls for the next item to be added and shown.

        }


        // Decides how many items to display per round of the game.
        public int FoodArraySize()
        {
            int arraySize = 0;

            if (roundCount == 1)
            {
                switch (difFlag)
                {
                    case "easy":
                        arraySize = 21;
                        break;
                    case "medium":
                        arraySize = 24;
                        break;
                    case "hard":
                        arraySize = 27;
                        break;
                }
            }

            if (roundCount > 1)
            {
                switch (difFlag)
                {
                    case "easy":
                        arraySize = foodArrayCount + 3;
                        break;
                    case "medium":
                        arraySize = foodArrayCount + 6;
                        break;
                    case "hard":
                        arraySize = foodArrayCount + 9;
                        break;
                }
            }

            return arraySize;
        }

        // Decides how many items to show at the beginning of the game.
        public int FoodToShowMeth()
        {
            int foodToShowL = 0;

            switch (difFlag)
            {
                case "easy":
                    foodToShowL = 1;
                    break;
                case "medium":
                    foodToShowL = 2;
                    break;
                case "hard":
                    foodToShowL = 3;
                    break;
            }

            return foodToShowL;

        }

        public int TimeToShowMeth()
        {

            int time = 0;

            switch (difFlag)
            {
                case "easy":
                    time = _duration + 3;
                    break;
                case "medium":
                    time = _duration + 2;
                    break;
                case "hard":
                    time = _duration + 1;
                    break;
            }


            return time;
        }


        /// <summary>
        /// Used in Shuffle(T).
        /// </summary>
        static Random _random = new Random();

        /// <summary>
        /// Shuffle the array.
        /// </summary>
        /// <typeparam name="T">Array element type.</typeparam>
        /// <param name="array">Array to shuffle.</param>
        static void Shuffle<T>(T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                int r = i + _random.Next(n - i);
                T t = array[r];
                array[r] = array[i];
                array[i] = t;
            }
        }


    }// End of Class




}//End of namespace


//Spare Code:


/*
       if (roundCount > 1)
       {
           switch (difFlag)
           {
               case "easy":
                   foodArrayCount = foodArrayCount+3;
                   break;
               case "medium":
                   foodArrayCount = foodArrayCount+6;
                   break;
               case "hard":
                   foodArrayCount = foodArrayCount+9;
                   break;
           }

       }
       */
