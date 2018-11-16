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
    public partial class AboutPage : ContentPage
    {
       
        public AboutPage()
        {
            InitializeComponent();


            //var credit1 = new Label { }; 

            aboutLabel.Text = 

                "I'm Hayden Coe, I created this app called Shoplist. Shoplist has been made as a cross platform for use in my Computer Science BSc project. " +
                "\n\n © Hayden Coe 2018. ";

            aboutLabel.HorizontalTextAlignment = TextAlignment.Start;


            creditLabel.Text =

                           "Food Icons: \nIcons made by https://smashicons.com from www.flaticon.com." +

                           "\n\nSmall Star Icon from high scores page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nNew high score Star Icon from end of round page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nNew top high score Podium Icon from end of round page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nNo new high score disappointment Icon from end of round page: \nIcon made by https://www.swifticons.com from www.flaticon.com";




        }




    }

}