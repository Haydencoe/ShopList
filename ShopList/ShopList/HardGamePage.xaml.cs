using System;
using System.Collections.Generic;

using Plugin.BLE;

using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;

using Xamarin.Forms;

namespace ShopList
{
    public partial class HardGamePage : ContentPage
    {

        public bool singleSelected = true;
        public bool multiSelected = false;
        public bool multiScreenSelected = false;

        public SQLDatabase sqlDatabase;
        public static HardHighscore hardHighScore = new HardHighscore();

        IDevice device;


        public HardGamePage()
        {
            InitializeComponent();

            topScore.Source = ImageSource.FromFile("topScore");
            singleIcon.Source = ImageSource.FromFile("playerSelect");
            multiIcon.Source = ImageSource.FromFile("greyPlayerSelect");

            singleplayer.TextColor = Color.Black;
            multiplayer.TextColor = Color.Gray;
            multiplayerScreen.TextColor = Color.Gray;

            blue.Opacity = 0;


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
                hardHighScore = sqlDatabase.GetHardHighscore(1);
                roundText.Text = "Round " + hardHighScore.Round;

                if (!string.IsNullOrEmpty(hardHighScore.Name))
                    nameText.Text = "By " + hardHighScore.Name;

            }

            catch
            {
                roundText.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
                roundText.Text = "You have no highscores, \n let's go make some!";
                nameText.Text = "";
            }


            SpinMe();


            device = ConnectedDevice.globalDevice;

            if (device != null)
            {
                deviceName.Text = "Connected to: " + "\n" + device.Name;

                Console.WriteLine("Log: " + device.NativeDevice + device.Id + device.State + device.AdvertisementRecords);

                Bluetooth();
            }


        }


        public async void Bluetooth()
        {
            var services = await device.GetServicesAsync();
            //var characteristics = await services.GetCharacteristicsAsync();

            foreach (IService a in services)
            Console.WriteLine(a);
        }


        public async void SpinMe()
        {
            await singleIcon.ScaleTo(0.75, 1);
            await multiIcon.ScaleTo(0.5, 1);
            await multiScreenIcon.ScaleTo(0.5, 1);

            await goButtonHard.RotateTo(360, 500);
            goButtonHard.Rotation = 0;
        }


        private async void goButtonHard_Clicked(object sender, EventArgs e)
        {


            string playerMode = "";

            if (singleSelected == true)
                playerMode = "single";

            else if (multiSelected == true)
                playerMode = "multi";


            await Navigation.PushModalAsync(new PlayingGamePage("hard", playerMode));

        }

        private void Single_Clicked(object sender, EventArgs e)
        {

            if (singleSelected == false)
            {
                singleSelected = true;
                multiSelected = false;
                multiScreenSelected = false;

                singleplayer.TextColor = Color.Black;
                multiplayer.TextColor = Color.Gray;
                multiplayerScreen.TextColor = Color.Gray;

                singleIcon.Source = "playerSelect";
                singleIcon.ScaleTo(0.75, 1000);

                multiIcon.Source = "greyPlayerSelect";
                multiIcon.ScaleTo(0.5, 1000);

                multiScreenIcon.Source = "greyPlayerSelect";
                multiScreenIcon.ScaleTo(0.5, 1000);

                blue.FadeTo(0, 500);
            }


        }

        private void Multi_Clicked(object sender, EventArgs e)
        {

            if (multiSelected == false)
            {
                multiSelected = true;
                singleSelected = false;
                multiScreenSelected = false;

                multiplayer.TextColor = Color.Black;
                singleplayer.TextColor = Color.Gray;
                multiplayerScreen.TextColor = Color.Gray;

                multiIcon.Source = "playerSelect";
                multiIcon.ScaleTo(0.75, 1000);

                singleIcon.Source = "greyPlayerSelect";
                singleIcon.ScaleTo(0.5, 1000);

                multiScreenIcon.Source = "greyPlayerSelect";
                multiScreenIcon.ScaleTo(0.5, 1000);



                blue.FadeTo(0, 500);
            
            }

        }

        private void Multi_Screen_Clicked(object sender, EventArgs e)
        {

            if (multiScreenSelected == false)
            {
                multiScreenSelected = true;
                multiSelected = false;
                singleSelected = false;

                multiplayerScreen.TextColor = Color.Black;
                multiplayer.TextColor = Color.Gray;
                singleplayer.TextColor = Color.Gray;


                multiScreenIcon.Source = "playerSelect";
                multiScreenIcon.ScaleTo(0.75, 1000);

                multiIcon.Source = "greyPlayerSelect";
                multiIcon.ScaleTo(0.5, 1000);

                singleIcon.Source = "greyPlayerSelect";
                singleIcon.ScaleTo(0.5, 1000);

                blue.FadeTo(1, 500);

            }

        }


        private async void Blue_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushModalAsync(new BluetoothScreenPage());
        }




    } // End of Class.

} // End of namespace. 
