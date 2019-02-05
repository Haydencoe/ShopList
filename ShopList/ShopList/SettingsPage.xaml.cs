using System;
using System.Collections.Generic;
using PCLStorage;

using Xamarin.Forms;

namespace ShopList
{
    public partial class SettingsPage : ContentPage
    {
        public SQLDatabase sqlDatabase;

        public SettingsPage()
        {
            InitializeComponent();

            if (Sound.soundFlag == false)
            {
                soundButton.Image = "toggleOff";
                soundLabel.Text = "Sound off";
                soundImage.Source = "setNoSound";  
            }


            if (Sound.soundFlag == true)
            {
                soundButton.Image = "toggleOn";
                soundLabel.Text = "Sound on";
                soundImage.Source = "setSound";
            }

        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
           
            //await DisplayAlert("Delete Folder Result", Entry + " has been deleted!", "OK");
            var answer = await DisplayAlert("Warning!", "Are you sure you want to delete your high scores?", "Yes", "Cancel");

            if (answer == true)
            {
               
                sqlDatabase = new SQLDatabase();
                sqlDatabase.DeleteAllEasyHighscores();
                sqlDatabase.DeleteAllMediumHighscores();
                sqlDatabase.DeleteAllHardHighscores();

            }

        }

        private void SoundButton_Clicked(object sender, EventArgs e)
        {
            if (Sound.soundFlag == true)
            {
                soundButton.Image = "toggleOff";
                soundLabel.Text = "Sound off";
                soundImage.Source = "setNoSound";
                Sound.soundFlag = false;
               

            }

            else 
            {
                soundButton.Image = "toggleOn";
                soundLabel.Text = "Sound on";
                soundImage.Source = "setSound";
                Sound.soundFlag = true;

            }

            SaveSound(Sound.soundFlag);
        
        }


        private async void SaveSound(bool soundSave)
        {

            string soundSaveStr = soundSave.ToString();

            String folderName = "settings";
            IFolder folder = FileSystem.Current.LocalStorage;
            folder = await folder.CreateFolderAsync(folderName, CreationCollisionOption.ReplaceExisting);


            String filename = "soundSettings.txt";
            IFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

            await file.WriteAllTextAsync(soundSaveStr);


        }

    }
}
