using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ShopList
{
    public partial class MediumGamePage : ContentPage
    {
        public bool singleSelected = true;
        public bool multiSelected = false;

        public SQLDatabase sqlDatabase;
        public static MediumHighscore mediumHighScore = new MediumHighscore();

        public MediumGamePage()
        {
            InitializeComponent();


            topScore.Source = ImageSource.FromFile("topScore");
            singleIcon.Source = ImageSource.FromFile("playerSelect");
            multiIcon.Source = ImageSource.FromFile("greyPlayerSelect");


            singleplayer.TextColor = Color.Black;
            multiplayer.TextColor = Color.Gray;

        }

        protected override void OnAppearing()
        {
            singleIcon.Source = ImageSource.FromFile("playerSelect");
            singleIcon.ScaleTo(1, 1);
            multiIcon.Source = ImageSource.FromFile("greyPlayerSelect");

            singleplayer.TextColor = Color.Black;
            multiplayer.TextColor = Color.Gray;

            singleSelected = true;
            multiSelected = false;


            sqlDatabase = new SQLDatabase();

            try { 
           
             mediumHighScore = sqlDatabase.GetMediumHighscore(1);

            roundText.Text = "Round " + mediumHighScore.Round;
            roundText.FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label));

             if (!string.IsNullOrEmpty(mediumHighScore.Name))
             nameText.Text = "By " + mediumHighScore.Name;

            }// End of try.

            catch
            {
                roundText.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                roundText.Text = "You have no highscores, \n let's go make some!";
                nameText.Text = "";
            }

             SpinMe();
        }


        public async void SpinMe()
        {

            await multiIcon.ScaleTo(0.5, 1);

            await goButtonMedium.RotateTo(360, 500);
            goButtonMedium.Rotation = 0;


        }


        private async void goButtonMedium_Clicked(object sender, EventArgs e)
        {


            string playerMode = "";

            if (singleSelected == true)
                playerMode = "single";

            else if (multiSelected == true)
                playerMode = "multi";


            await Navigation.PushModalAsync(new PlayingGamePage("medium", playerMode));

        }

        private void Single_Clicked(object sender, EventArgs e)
        {

            if (singleSelected == false)
            {
                singleSelected = true;
                multiSelected = false;

                singleplayer.TextColor = Color.Black;
                multiplayer.TextColor = Color.Gray;


                singleIcon.Source = "playerSelect";
                singleIcon.ScaleTo(1, 1000);

                multiIcon.Source = "greyPlayerSelect";
                multiIcon.ScaleTo(0.5, 1000);


            }


        }

        private void Multi_Clicked(object sender, EventArgs e)
        {
        
            if (multiSelected == false)
            {
                multiSelected = true;
                singleSelected = false;

                multiplayer.TextColor = Color.Black;
                singleplayer.TextColor = Color.Gray;


                multiIcon.Source = "playerSelect";
                multiIcon.ScaleTo(1, 1000);

                singleIcon.Source = "greyPlayerSelect";
                singleIcon.ScaleTo(0.5, 1000);
          
          
          }

        }


    } // end of Class.

}// end of namespace
