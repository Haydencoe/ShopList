using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ShopList
{
    public partial class EasyGamePage : ContentPage
    {
        public bool singleSelected = true;
        public bool multiSelected = false;

        public SQLDatabase sqlDatabase;
        public static EasyHighscore easyHighScore = new EasyHighscore();

        public EasyGamePage()
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

            try
            {
                easyHighScore = sqlDatabase.GetEasyHighscore(1);

                roundText.Text = "Round " + easyHighScore.Round;

                if (!string.IsNullOrEmpty(easyHighScore.Name))
                nameText.Text = "By " + easyHighScore.Name;

            }

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
            await singleIcon.ScaleTo(0.75, 1);
            await multiIcon.ScaleTo(0.5, 1);

            await goButtonEasy.RotateTo(360, 500);
            goButtonEasy.Rotation = 0;


        }


        private async void goButtonEasy_Clicked(object sender, EventArgs e)
        {


            string playerMode = "";

            if (singleSelected == true)
                playerMode = "single";

            else if (multiSelected == true)
                playerMode = "multi";


            await Navigation.PushModalAsync(new PlayingGamePage("easy", playerMode));

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
                singleIcon.ScaleTo(0.75, 1000);

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
                multiIcon.ScaleTo(0.75, 1000);

                singleIcon.Source = "greyPlayerSelect";
                singleIcon.ScaleTo(0.5, 1000);


            }

        }






    } // end of Class.

}// end of namespace
