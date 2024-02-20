using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
        }
        // الذهاب لصفحة تغيير الاسم
        private async void Button_Clicked_PlayerName(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayerNameEntry());
        }
        // الذهاب لصفحة التواصل معنا
        private async void Button_Clicked_Contact(object sender, EventArgs e)
        {            
            await Navigation.PushAsync(new Contact());          
        }
        // تعليمات الصفحة
        private async void infoSetting_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("تعليمات", "صفحة الاعدادات, يمكنك هنا الذهاب لتغيير اسامي اللاعبين في حالة رغبتك بذلك, او التواصل مع فريق التطوير",
                             "فهمت");
        }
    }
}