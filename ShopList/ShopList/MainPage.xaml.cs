using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PCLStorage;




namespace ShopList
{
    public static class Sound
    {
        public static string soundFlag = "false";

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

        public int todayConfirm = 0;

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

            if (Sound.soundFlag == "false")
                sound.Image = "noSound";

            if (Sound.soundFlag == "true")
                sound.Image = "sound";
        }

        public static List<Data> localData = new List<Data>();
        public SQLDatabase sqlDatabase;

        public MainPage()
        {
            InitializeComponent();

            // DATA gathering ****

            sqlDatabase = new SQLDatabase();
            localData = sqlDatabase.GetAllData();
            if (localData.Count > 0)
            {
                foreach (Data data in localData)
                {
                    if (data.CreatedOn == DateTime.Today)
                    {
                        todayConfirm = 1;
                    }
                
                }
            }


            if (todayConfirm == 0)
            {
                if (localData.Count < 30)
                {
                    Data data = new Data();
                    data.CreatedOn = DateTime.Today;

                    sqlDatabase.AddData(data);
                }
            }

            // DATA gathering ****

            if (Device.RuntimePlatform == Device.Android)
            {
                optionsFrame.Margin = new Thickness(0, -40, 0, 0);
            }


            NavigationPage.SetHasNavigationBar(this, false);

            logoImage.Source = ImageSource.FromFile("logo");

            if (Sound.soundFlag == "false")
                sound.Image = "noSound";

            if (Sound.soundFlag == "true")
                sound.Image = "sound";

            plusFrame.TranslateTo(0, 700, 1);

            infoFrame.ScaleTo(0.2, 1);
            infoFrame.IsVisible = false;

            settingsFrame.ScaleTo(0.2, 1);
            settingsFrame.IsVisible = false;

            soundFrame.ScaleTo(0.2, 1);
            soundFrame.IsVisible = false;

            scoresFrame.ScaleTo(0.2, 1);
            scoresFrame.IsVisible = false;

            notFrame.Opacity = 0;

            ButtonAni();
        }
       

        public int flag = 0;

        int a = 0;

        public bool plusFlag = false;

        public int loop = 1;

        public async void ButtonAni()
        {
            int a = 1;

            while (a > 0)
            {
                await play.ScaleTo(1.1, 1000);
                await play.ScaleTo(1, 1000);
            }
           
        }

        //Loads in scores 
        public async Task<string> Load()
        {
            // Load settings in
            IFolder rootFolder = FileSystem.Current.LocalStorage;

            IFolder settingsFolder = await rootFolder.CreateFolderAsync("settings", CreationCollisionOption.OpenIfExists);

            string settingsContent = await ReadFileContent("soundSettings.txt", settingsFolder);

                try// try to load any settings saved on the device.
                {
                    if (settingsContent == "true")
                    {
                       Sound.soundFlag = "true";
                    }

                    else
                    {
                        Sound.soundFlag = "false";
                    }
                }
                catch// No settings saved on the device.
                {
                    Sound.soundFlag = "false";// indicator of no saved scores 
                }

            Console.WriteLine("Sound Flag: "+Sound.soundFlag);

           return await Task.FromResult(settingsContent);



        }//End of load.



        private async void newGameButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameSetupPage());
        }


        private async void scoresButton_Clicked(object sender, EventArgs e)
        {
            notFrame.Opacity = 0;
           
            Notifications.notFlag = 0;

            await Navigation.PushAsync(new BottomNavigationPage());
        }
    

        private async void settingsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void aboutButton_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new AboutPage());
           
        }

        private void soundButton_Clicked(object sender, EventArgs e)
        {
            if (Sound.soundFlag == "false")
            {
                sound.Image = "sound";
                Sound.soundFlag = "true";
            }
            else
            {
                sound.Image = "noSound";
                Sound.soundFlag = "false";
            }

            SaveSound(Sound.soundFlag);
        }


        private async void optionsButton_Clicked(object sender, EventArgs e)
        {
            Sound.soundFlag = await Load();

            if (Sound.soundFlag == "false")
                sound.Image = "noSound";

            if (Sound.soundFlag == "true")
                sound.Image = "sound";

            AbsoluteLayout.SetLayoutBounds(aboStack, new Rectangle(0, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(aboStack, AbsoluteLayoutFlags.All);

            AbsoluteLayout.SetLayoutBounds(aboStackBlack, new Rectangle(0.5, 0.5, 1, 1));
            AbsoluteLayout.SetLayoutFlags(aboStackBlack, AbsoluteLayoutFlags.All);

         
            await Task.WhenAll(
            
                playFrame.TranslateTo(0, -this.Height, 250),
                optionsFrame.TranslateTo(0, -this.Height, 250)

          );

         
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

                );

                plusFrame.Rotation = 45;
                plus.Image = "cross";

                //Notification alert
                if (Notifications.notFlag > 0)
                {

                    Console.WriteLine("Notifications Count: " + Notifications.notFlag);

                    if (Notifications.notFlag > 0 && Notifications.notFlag < 10)
                    {
                        Console.WriteLine("if entered");


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
                            case 4:
                                notFrame.Source = "not4";
                                break;
                            case 5:
                                notFrame.Source = "not5";
                                break;
                            case 6:
                                notFrame.Source = "not6";
                                break;
                            case 7:
                                notFrame.Source = "not7";
                                break;
                            case 8:
                                notFrame.Source = "not8";
                                break;
                            case 9:
                                notFrame.Source = "not9";
                                break;
                        }

                        await Task.WhenAll(

                            notFrame.TranslateTo(120, -40, 1),
                            notFrame.FadeTo(1, 500)
                   

                         );

                    }// End of if.

                    else
                    {
                        Console.WriteLine("else entered");

                        notFrame.Source = "not9Plus";

                        await Task.WhenAll(

                            notFrame.TranslateTo(120, -40, 1),
                            notFrame.FadeTo(1, 500)

                      );
                    }

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

            a = 0;

            playFrame.IsVisible = true;
            optionsFrame.IsVisible = true;

            notFrame.Opacity = 0;

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

            layout.Children.Remove(notFrame);
         
            layout.Children.Remove(buttonLayout);

            layout.Children.Remove(aboStackBlack);

            back.Children.Add(logoStack);

            back.Children.Add(midStack);
         
            back.Children.Add(difStack);
         
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


        private async void SaveSound(string soundSave)
        {
            string soundSaveStr = soundSave.ToString();

            String folderName = "settings";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);

            String filename = "soundSettings.txt";
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync(soundSaveStr);

        }

    }// End of Class

}// End of namespace

