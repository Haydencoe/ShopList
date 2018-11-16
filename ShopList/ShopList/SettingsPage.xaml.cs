using System;
using System.Collections.Generic;
using PCLStorage;

using Xamarin.Forms;

namespace ShopList
{
    public partial class SettingsPage : ContentPage
    {
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

            var Entry = "highScores";

            IFolder rootFolder = FileSystem.Current.LocalStorage;

           
            //await DisplayAlert("Delete Folder Result", Entry + " has been deleted!", "OK");
            var answer = await DisplayAlert("Warning!", "Are you sure you want to delete your high scores?", "Yes", "Cancel");

            if (answer == true)
            {
                // Delete Folder entered in EntDeleteSubFolder
                var subfolderExists = await rootFolder.CheckExistsAsync(Entry);
                if (subfolderExists != ExistenceCheckResult.FolderExists)
                {
                    await DisplayAlert("Delete Folder Result", Entry + " doesn't exist", "OK");
                    return;
                }
                IFolder subfolder = await rootFolder.GetFolderAsync(Entry);
                await subfolder.DeleteAsync();

                Notifications.notFlag = 0;

                HighScores.highScores.Clear();
                HighScores.mediumHighScores.Clear();
                HighScores.hardHighScores.Clear();


            }

            //Title = answer.ToString();

            //await DisplayAlert("","Highscores deleted!", "OK");

            //deleteLabel.Text = "Scores deleted!";




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








        /*
        //Delete Folder
        async void DeleteFolder(object sender, EventArgs e)
        {
            var Entry = "Highscores";

            IFolder rootFolder = FileSystem.Current.LocalStorage

            // Delete Folder entered in EntDeleteSubFolder
            var subfolderExists = await rootFolder.CheckExistsAsync(Entry);
            if (subfolderExists != ExistenceCheckResult.FolderExists)
            {
                await DisplayAlert("Delete Folder Result", Entry + " doesn't exist", "OK");
                return;
            }
            IFolder subfolder = await rootFolder.GetFolderAsync(Entry);
            await subfolder.DeleteAsync();
            await DisplayAlert("Delete Folder Result", Entry + " has been deleted!", "OK");
       
            deleteLabel.Text = "Scores deleted!";
        }
        */

    }
}
