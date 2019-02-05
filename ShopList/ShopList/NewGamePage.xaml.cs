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
    public partial class NewGamePage : ContentPage
    {
        public NewGamePage()
        {
            InitializeComponent();

           
        }


        /*
        private async void startGameButton_Clicked(object sender, EventArgs e)
        {

        
            await Navigation.PushModalAsync(new playingGamePage());// Loads game play page
        }
*/



    }
}