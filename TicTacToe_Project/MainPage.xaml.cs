using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace TicTacToe_Project
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();        
        }
        

        //زر الذهاب للاسم واللعب
        private async void Button_Clicked_Playing(object sender, EventArgs e)
        {          
            //في حالة عدم وجود اسم يذهب الى صفحة ادخال الاسم
            if (!Application.Current.Properties.ContainsKey("PlayerOneName") || !Application.Current.Properties.ContainsKey("PlayerTwoName"))
            {
                await Navigation.PushAsync(new PlayerNameEntry());
            }
            // في حالة وجود اسامي مدخله مسبقا يذهب لصفحة اللعبه
            else
            {
                // استعادة قيم اللاعبين من خصائص التطبيق وإرسالها إلى صفحة اللعبة في حالة ادخال اسم
                string playerOneName = Application.Current.Properties["PlayerOneName"].ToString();
                string playerTwoName = Application.Current.Properties["PlayerTwoName"].ToString();

                // ارسال قيم الاسمي لصفحة اللعبه
                await Navigation.PushAsync(new GamePage(playerOneName, playerTwoName));  
            }
        }
        // زر الاعدادات
        private async void Button_Clicked_PlayerName(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new SettingPage());
        }
        // زر من نحن
        private async void Button_Clicked_AboutUS(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutUs());
        }
        // اغلاق اللعبة
        private void Button_Clicked_Exit(object sender, EventArgs e)
        {            
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }      
    }   
}