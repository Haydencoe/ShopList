using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopList
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartGamePage : ContentPage
    {



        public StartGamePage()
        {

            InitializeComponent();
        }

            /*
            timeLabel.Text = "3";

            StartTimer();


        }


        private int _duration = 0;

        public async void StartTimer()
        {



            _duration = 3;


            // Tick every second while game is in progress
            while (_duration > 0)
            {
                //string s = TimeSpan.FromSeconds(_duration).ToString(@"ss");
                //await Task.Delay(1000); // Waits 1 second between each number.

                // await timeLabel.FadeTo(0, 300); // Fade out.

                _duration--; // Count down a number
                string s = _duration.ToString();// converts the 'numbers' to string format for the label.
                timeLabel.Text = s;// display the current number.

                // await timeLabel.FadeTo(1, 300);// Fade back in.
            }


            timeLabel.Text = "Let's Go!!";

            // await Task.Delay(1000); // Waits 1 second
            await Navigation.PushModalAsync(new playingGamePage());// Loads game play page
        }
*/




    }
}