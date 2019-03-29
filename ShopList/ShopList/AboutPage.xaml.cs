using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Messaging;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        public SQLDatabase sqlDatabase;
        public int meCLicked = 0;
        public static List<Data> localData = new List<Data>();

        public AboutPage()
        {
            InitializeComponent();

            sqlDatabase = new SQLDatabase();
            localData = sqlDatabase.GetAllData();


            Animation();

            //var credit1 = new Label { }; 

            aboutLabel.Text =

                "I'm Hayden Coe, I created this app called Shoplist, Shoplist has been made cross-platform using Xamarin Forms. It was created for use in my Computer Science BSc project at the University of Lincoln, researching the benifits of educational apps in primary education." +
                "\n\nIf you have any questions, hit me up on my socials!" +
                "\n\n © Hayden Coe 2018. " +
                "\n\n\nIf you would like to help with my research you can email me a report of the data collected about your usage of the app." +
                "\nThe report is a maximum of 30 days of use and includes: Daily Total games played, highscores view amount, trophies view amount and highest score reached.";


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

        public async void Me_Clicked(object sender, EventArgs e)
        {
            meCLicked++;

            if (meCLicked == 5)
            {
                await Me.RotateTo(360, 2000);
                Me.Rotation = 0;
                meCLicked = 0;
            }
        }


        

        public void Email_Clicked(object sender, EventArgs e)
        {



            String emailStartText = "Hi Hayden, here's my app usage report:";
            string emailBodyText;

            List<String> localString = new List<String>();
            int counter = 0;

            foreach (Data data in localData)
            {
                counter++;

                string a = "\n\nDay " + counter + ": " + " Games played: " + data.GamesPlayed + " Highscores Viewed: " + data.HighscoresViewed +
                    " Trophies Viewed: " + data.TrophiesViewed + " Highest Round: " + data.RoundCountHigh;

                localString.Add(a);

            }

            emailBodyText = String.Join(String.Empty, localString);


            var email = new EmailMessageBuilder()
            .To("shoplistgameapp@gmail.com")
            .Subject("ShopList App Usage Report")
            .Body(emailStartText+emailBodyText)
            .Build();

            var emailTask = CrossMessaging.Current.EmailMessenger;

            emailTask.SendEmail(email);

            Console.WriteLine("Email Click");
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