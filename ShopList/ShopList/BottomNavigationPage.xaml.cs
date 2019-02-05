using System;
using System.Collections.Generic;
using BottomBar.XamarinForms;
using Xamarin.Forms;

namespace ShopList
{
    public partial class BottomNavigationPage : BottomBarPage
    {

        public BottomNavigationPage()
        {
            InitializeComponent();

            InitPages();

        }

        private void InitPages()
        {
            //BottomNavigationPage bottomBarPage = new BottomNavigationPage();

            this.FixedMode = true;
            this.BarTheme = BarThemeTypes.Light;
            this.BarTextColor = Color.FromHex("#DEC69D");

            //this.BarTextColor = Color.Blue; // Setting Color of selected Text and Icon
           
            Children.Add(new ScoresPage(1)
            {
                Title = "Highscores",
                Icon = "navList.png",
              
            });

            Children.Add(new TrophiesPage()
            {
                Title = "Trophies",
                Icon = "navAward.png",

            });

        }// End of InitPages.

    }// End of class.

}// End of namespace.
