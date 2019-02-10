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

            Animation();

            //var credit1 = new Label { }; 

            aboutLabel.Text =

                "I'm Hayden Coe, I created this app called Shoplist, Shoplist has been made cross-platform using Xamarin Forms. It was created for use in my Computer Science BSc project at the University of Lincoln, researching the benifits of educational apps in primary education." +
                "\n\nIf you have any questions, hit me up on my socials!"+
                "\n\n © Hayden Coe 2018. ";

            aboutLabel.HorizontalTextAlignment = TextAlignment.Start;


            creditLabel.Text =

                           "Food Icons: \nIcon pack made by https://smashicons.com from www.flaticon.com." +

                           "\n\nSmall Star Icon from high scores page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nNew high score Star Icon from end of round page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nDeleting bin icon: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nSound/ no sound Icons: \nIcons made by https://smashicons.com from www.flaticon.com." +

                           "\n\nNew top high score Podium Icon from end of round page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nNo new high score disappointment Icon from end of round page: \nIcon made by https://www.swifticons.com from www.flaticon.com" +

                           "\n\nTrophies and awards icons: \nIcon pack made by https://www.freepik.com from www.flaticon.com."+

                           "\n\nSocial Network icons: \nIcon pack made by Pixel perfect from www.flaticon.com." +

                           "\n\nPlay triangle button: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nTrophy on play page: \nIcon made by https://www.freepik.com from www.flaticon.com." +

                           "\n\nTrophy in bottom navbar: \nIcon made by Vectors Market from www.flaticon.com." +

                           "\n\nSwap player in playing page: \nIcon made by Kiranshastry from www.flaticon.com." +

                           "\n\nList in bottom navbar: \nIcon made by DinoSoftLab from www.flaticon.com." +

                           "\n\nOn and off switch icons: \nIcons made by Pixel perfect from www.flaticon.com.";

        }

        public void Twitter_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://twitter.com/haydenjamescoe"));
        }

        public void Linkedin_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://www.linkedin.com/in/hayden-coe-61598445"));
        }

        public void Github_Clicked(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("https://github.com/haydencoe"));
        }

        public void Me_Clicked(object sender, EventArgs e)
        {

        }

        public async void Animation()
        {
            await twitterButton.TranslateTo (0, -100, 1);
            await githubButton.TranslateTo  (0, -100, 1);
            await linkedinButton.TranslateTo(0, -100, 1);

            await twitterButton.TranslateTo (0, 0, 500);
            await linkedinButton.TranslateTo(0, 0, 500);
            await githubButton.TranslateTo  (0, 0, 500);

        }


    }// End of class.
}// End of namespace.