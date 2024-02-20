using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Project
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
            //اضافة الصغحات
            MainPage = new NavigationPage(new MainPage());          
        }       
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }
        
        protected override void OnResume()
        {
        }

      
    }
}

