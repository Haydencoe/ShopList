using System;
using System.Collections.Generic;
using Naxam.Controls.Forms;
using Xamarin.Forms;

namespace ShopList
{
    public partial class GameSetupPage : TopTabbedPage
    {
        public GameSetupPage()
        {
            InitializeComponent();

            this.CurrentPageChanged += PageChanged;

            void PageChanged(object sender, EventArgs args)
            {

                var currentPage = CurrentPage as HardGamePage;
                currentPage?.SpinMe();

                 var currentPage2 = CurrentPage as EasyGamePage;
                currentPage2?.SpinMe();

                var currentPage3 = CurrentPage as MediumGamePage;
                currentPage3?.SpinMe();

            }



        }
    }
}
