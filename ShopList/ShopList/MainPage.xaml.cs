using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PCLStorage;
using Newtonsoft.Json;

namespace ShopList
{
    public static class Sound
    {
        public static bool soundFlag = false;

    }

    public static class Notifications
    {
        public static int notFlag = 0;

    }



    public partial class MainPage : ContentPage
    {
        public AbsoluteLayout layout = new AbsoluteLayout
        {
            VerticalOptions = LayoutOptions.FillAndExpand
        };

        public StackLayout aboStack = new StackLayout
        {
           // VerticalOptions = LayoutOptions.CenterAndExpand
            //BackgroundColor = Color.FromHex("#CC4c4c4c")
        };

        public String difFlag = "medium";


        public StackLayout aboStackBlack = new StackLayout
        {

            BackgroundColor = Color.FromHex("#CC4c4c4c")
        };

        protected override void OnAppearing()
        {

            if (Notifications.notFlag == 0)
            {

                notFrame.Opacity = 0;
                //notFrame.IsVisible = false;

            }

            if (Sound.soundFlag == false)
                sound.Image = "noSound";

            if (Sound.soundFlag == true)
                sound.Image = "Sound";


            // For debugging
            /*
            string notStr = Notifications.notFlag.ToString();
            string highStr = HighScores.highScores.Count.ToString();
            Title = notStr +" "+highStr;
            */

        }

        //public Image image = new Image { Source = "logo" };

        public MainPage()
        {
            InitializeComponent();

            difImage.Source = difFlag + "Text";

            glowImage.Source = difFlag + "Glow";
            glowImage.WidthRequest = this.Width;

            NavigationPage.SetHasNavigationBar(this, false);
            // BackgroundImage = "backG";

            //Content.Content = back;



            logoImage.Source = ImageSource.FromFile("logo");


            if (Sound.soundFlag == false)
                sound.Image = "noSound";

            if (Sound.soundFlag == true)
                sound.Image = "Sound";

           


            plusFrame.TranslateTo(0, 500, 1);

            infoFrame.ScaleTo(0.2, 1);
            infoFrame.IsVisible = false;

            settingsFrame.ScaleTo(0.2, 1);
            settingsFrame.IsVisible = false;

            soundFrame.ScaleTo(0.2, 1);
            soundFrame.IsVisible = false;

            scoresFrame.ScaleTo(0.2, 1);
            scoresFrame.IsVisible = false;

            notFrame.Opacity = 0;


            Load();// load settings/ highscores in. 

            buttonAni();
        }
        public int a = 0;

        public int flag = 0;

        public bool plusFlag = false;

        public int loop = 1;

        public async void buttonAni()
        {
            int a = 1;

            while (a > 0)
            {
                await play.ScaleTo(1.1, 1000);
                await play.ScaleTo(1, 1000);
            }
           
        }


        //Loads in scores 
        public async void Load()
        {
            IFolder rootFolder = FileSystem.Current.LocalStorage;
            IFolder highScoresFolder = await rootFolder.CreateFolderAsync("highScores", CreationCollisionOption.OpenIfExists);

            string fileContent = await ReadFileContent("highScoreString.txt", highScoresFolder);
            string mediumFileContent = await ReadFileContent("mediumHighScoreString.txt", highScoresFolder);
            string hardFileContent = await ReadFileContent("hardHighScoreString.txt", highScoresFolder);

            //theLabel.Text = fileContent;// Debugging


            if (HighScores.highScores.Count == 0 || HighScores.mediumHighScores.Count == 0 || HighScores.hardHighScores.Count == 0)
            {
                try// try to load any scores saved on phone
                {
                    //Easy Load.
                    // Deserialise string
                    dynamic highScoreList = JsonConvert.DeserializeObject<dynamic>(fileContent);
                    // Add each string into the list
                    foreach (var highScore in highScoreList)
                    {
                        HighScores.highScores.Add(highScore?.ToString());
                    }


                    //Put list in decending ordere
                    HighScores.highScores.Sort();
                    HighScores.highScores.Reverse();

                    //Medium Load.
                    // Deserialise string
                    dynamic mediumHighScoreList = JsonConvert.DeserializeObject<dynamic>(mediumFileContent);
                    // Add each string into the list
                    foreach (var mediumHighScore in mediumHighScoreList)
                    {
                        HighScores.mediumHighScores.Add(mediumHighScore?.ToString());
                    }


                    //Put list in decending ordere
                    HighScores.mediumHighScores.Sort();
                    HighScores.mediumHighScores.Reverse();

                    //Hard Load.
                    // Deserialise string
                    dynamic hardHighScoreList = JsonConvert.DeserializeObject<dynamic>(hardFileContent);
                    // Add each string into the list
                    foreach (var hardHighScore in hardHighScoreList)
                    {
                        HighScores.hardHighScores.Add(hardHighScore?.ToString());
                    }


                    //Put list in decending ordere
                    HighScores.hardHighScores.Sort();
                    HighScores.hardHighScores.Reverse();



                }

                catch// No scores saved on phone so add an empty highscore entry 
                {

                    flag = 1;// indicator of no saved scores 

                }


            }// End of if.

            else// this is for when nothing needs to be loaded. 
            {
                
            }

            //*************************************************
            // Load settings in


            IFolder settingsFolder = await rootFolder.CreateFolderAsync("settings", CreationCollisionOption.OpenIfExists);

            string settingsContent = await ReadFileContent("soundSettings.txt", settingsFolder);

            //theLabel.Text = fileContent;// Debugging

              

                try// try to load any settings saved on phone
                {

                    if (settingsContent == "True")
                    {
                        Sound.soundFlag = true;
                    }

                    else
                    {
                        Sound.soundFlag = false;
                    }

                }

                catch// No scores saved on phone so add an empty highscore entry 
                {

                    Sound.soundFlag = false;// indicator of no saved scores 

                }


           


        }//End of load.

        private async void LeftButton_Clicked(object sender, EventArgs e)
        {
            if (difFlag == "medium")
            {
                leftArrow.IsEnabled = false;
                rightArrow.IsEnabled = false;

                await difImage.ScaleTo(0, 250);

                leftArrow.Opacity = 0;
                rightArrow.Opacity = 100;
                difFlag = "easy";
                difImage.Source = "easyText";


                await Task.WhenAll(
                  glowImage.FadeTo(0, 1000),
                  glowImage3.FadeTo(1, 700),


                difImage.ScaleTo(1, 500));

                leftArrow.IsEnabled = true;
                rightArrow.IsEnabled = true;

            }

            if (difFlag == "hard")
            {
                leftArrow.IsEnabled = false;
                rightArrow.IsEnabled = false;

                await difImage.ScaleTo(0, 250);

                leftArrow.Opacity = 100;
                rightArrow.Opacity = 100;
                difFlag = "medium";
                difImage.Source = "mediumText";


                await Task.WhenAll(
                  glowImage2.FadeTo(0, 1000),
                  glowImage.FadeTo(1, 700),


                difImage.ScaleTo(1, 500));

                leftArrow.IsEnabled = true;
                rightArrow.IsEnabled = true;

            }

        }


        private async void RightButton_Clicked(object sender, EventArgs e)
        {

            if (difFlag == "medium")
            {
                leftArrow.IsEnabled = false;
                rightArrow.IsEnabled = false;

                await difImage.ScaleTo(0, 250);

                leftArrow.Opacity = 100;
                rightArrow.Opacity = 0;
                difFlag = "hard";
                difImage.Source = "hardText";


                await Task.WhenAll(
                  glowImage.FadeTo(0, 1000),
                  glowImage2.FadeTo(1, 700),


                difImage.ScaleTo(1, 500));

                leftArrow.IsEnabled = true;
                rightArrow.IsEnabled = true;

            }

            if (difFlag == "easy")
            {

                leftArrow.IsEnabled = false;
                rightArrow.IsEnabled = false;

                await difImage.ScaleTo(0, 250);

                leftArrow.Opacity = 100;
                rightArrow.Opacity = 100;
                difFlag = "medium";
                difImage.Source = "mediumText";


                await Task.WhenAll(
                  glowImage3.FadeTo(0, 1000),
                  glowImage.FadeTo(1, 700),


                difImage.ScaleTo(1, 500));

                leftArrow.IsEnabled = true;
                rightArrow.IsEnabled = true;

            }

        }

        private async void newGameButton_Clicked(object sender, EventArgs e)
        {
           

            await Navigation.PushModalAsync(new PlayingGamePage(difFlag));
        }

        private async void scoresButton_Clicked(object sender, EventArgs e)
        {
            notFrame.Opacity = 0;
            //notFrame.IsVisible = true;
            Notifications.notFlag = 0;

            //notFrame.Source = null;
            await Navigation.PushAsync(new ScoresPage(flag));
        }

        private async void settingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void aboutButton_Clicked(object sender, EventArgs e)
        {

           // await infoFrame.ScaleTo(100, 500);

            await Navigation.PushAsync(new AboutPage());
            //await infoFrame.ScaleTo(1, 1);
        }

        private void soundButton_Clicked(object sender, EventArgs e)
        {
            if (Sound.soundFlag == false)
            {
                sound.Image = "sound";
                Sound.soundFlag = true;
            }
            else
            {
                sound.Image = "noSound";
                Sound.soundFlag = false;
            }
        }


        private async void optionsButton_Clicked(object sender, EventArgs e)
        {


            // aboStack.Children.Add(Content.Content);
           

            AbsoluteLayout.SetLayoutBounds(aboStack, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(aboStack, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(aboStackBlack, new Rectangle(0.5, 0.5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(aboStackBlack, AbsoluteLayoutFlags.All);

            //
            //AbsoluteLayout.SetLayoutBounds(difStack, new Rectangle(0.5, Content.Height, 1, 1));
            //AbsoluteLayout.SetLayoutFlags(difStack, AbsoluteLayoutFlags.All);
            //

         
           
            await Task.WhenAll(
            

                playFrame.TranslateTo(0, -this.Height, 250),
                optionsFrame.TranslateTo(0, -this.Height, 250)



          );
            // aboStack.Children.Add(back);
            //playFrame.Opacity = 0;
            //optionsFrame.Opacity = 0;

            //layout.Children.Add(playFrame);
            //layout.Children.Add(optionsFrame);

            //layout.Children.Add(difStack);//


            aboStack.Children.Add(logoStack);
            aboStack.Children.Add(midStack);
           
            aboStack.Children.Add(difStack);


            layout.Children.Add(aboStack);

            aboStackBlack.Opacity = 0;


            layout.Children.Add(aboStackBlack);



            layout.Children.Add(buttonLayout);
            layout.Children.Add(notFrame);





            AbsoluteLayout.SetLayoutBounds(buttonLayout, new Rectangle(0.5, 0.5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(buttonLayout, AbsoluteLayoutFlags.All);

            Content = layout;

           
           // var newBounds = new Rectangle(0.5, 0.5, this.Width, this.Height);

            await Task.WhenAll(
                plusFrame.TranslateTo(0, 0, 250),

               // aboStackBlack.LayoutTo(newBounds, 500, Easing.CubicInOut)

               aboStackBlack.FadeTo(1, 500)
            );

            logoImage.Source = ImageSource.FromFile("blurredLogo");

            //playFrame.IsVisible = false;
            //optionsFrame.IsVisible = false;

            if (plusFlag == false)
            {
            

                settingsFrame.IsVisible = true;
                await Task.WhenAll(
                   settingsFrame.ScaleTo(1, 150),
                    settingsFrame.TranslateTo(0,-100 , 150));

                scoresFrame.IsVisible = true;
                await Task.WhenAll(
                   scoresFrame.ScaleTo(1, 150),
                    scoresFrame.TranslateTo(100, 0, 150));



                infoFrame.IsVisible = true;
                await Task.WhenAll(
                infoFrame.ScaleTo(1, 150),
                    infoFrame.TranslateTo(0, 100, 150)
                );

                soundFrame.IsVisible = true;
                await Task.WhenAll(
                   soundFrame.ScaleTo(1, 150),
                    soundFrame.TranslateTo(-100, 0 , 150),

                   plusFrame.RotateTo(45, 500)


                //blackBox.ScaleTo(0.1, 1),
                //aboStackBlack.FadeTo(0.9, 100),
                //blackBox.ScaleTo(1, 1000)
                   


                );

                plusFrame.Rotation = 45;
                plus.Image = "cross";

                //Notification alert
                if (Notifications.notFlag > 0)
                {
                   
                    switch (Notifications.notFlag)
                    {
                        case 1:
                            notFrame.Source = "not1";
                            break;
                        case 2:
                            notFrame.Source = "not2";
                            break;
                        case 3:
                            notFrame.Source = "not3";
                            break;
                    }


                   
                    await Task.WhenAll(

                        notFrame.TranslateTo(120, -40, 1),
                        //notLabel.TranslateTo(120, -20, 1),

                        notFrame.FadeTo(1, 500)
                       // notLabel.FadeTo(1, 500)

                );

                }//End of if
                    


                plusFlag = true;

                int b = 0;
                settings.Rotation = 0;
                a = 1;
                while (a > 0)
                {

                    b = b + 45;
                    await settings.RotateTo(b, 500);

                    settings.Rotation = b;
                }


            } //End of if

        }


        private async void PlusButton_Clicked(object sender, EventArgs e)
        {

            //playFrame.Opacity = 1;
            //optionsFrame.Opacity = 1;


            a = 0;

            playFrame.IsVisible = true;
            optionsFrame.IsVisible = true;

            notFrame.Opacity = 0;
            //notFrame.IsVisible = false;
            await Task.WhenAll(

                   
                   infoFrame.ScaleTo(0, 250),
                   infoFrame.TranslateTo(0, -50, 250),
                  
                    settingsFrame.ScaleTo(0, 250),
                    settingsFrame.TranslateTo(0, 100, 250),

                    scoresFrame.ScaleTo(0, 250),
                    scoresFrame.TranslateTo(-80, 20, 250),

                    soundFrame.ScaleTo(0, 250),
                    soundFrame.TranslateTo(80, 20, 250),






                   plusFrame.RelRotateTo(-45, 500)


                );

             



                plus.Image = "plus";



            logoImage.Source = ImageSource.FromFile("logo");


            infoFrame.IsVisible = false;


             plusFlag = false;// Flag for doing an options toggle switch NOT NEEDED

         
           

            //Put the buttons back
            await Task.WhenAll(

                aboStackBlack.FadeTo(0, 500),

                plusFrame.TranslateTo(0, this.Height + 100, 250),

                  playFrame.TranslateTo(0, 15, 500),
                  optionsFrame.TranslateTo(0, 15, 500)



              );


            // Put layout back to normal.

            layout.Children.Remove(aboStack);
            //layout.Children.Remove(difStack);

            layout.Children.Remove(notFrame);
            //aboStack.Children.Remove(image);
            //aboStack.Children.Remove(back);

            //layout.Children.Remove(playFrame);
            //layout.Children.Remove(optionsFrame);

            layout.Children.Remove(buttonLayout);

            // layout.Children.Remove(glowImage);


            layout.Children.Remove(aboStackBlack);

            //fullStack.Children.Add(back);


            //back.Children.Add(playFrame);
            //back.Children.Add(optionsFrame);
            back.Children.Add(logoStack);

            //back.Children.Add(playFrame);
            //back.Children.Add(optionsFrame);

            back.Children.Add(midStack);
            //back.Children.Add(buttonLayout);



            back.Children.Add(difStack);
            //back.Children.Add(glowAbo);


            // back.Children.Add(difStack);




            Content = back;




            await Task.WhenAll(
                  playFrame.TranslateTo(0, 0, 150),
                  optionsFrame.TranslateTo(0, 0, 150)
            );

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


    }// End of Class

    }// End of namespace

