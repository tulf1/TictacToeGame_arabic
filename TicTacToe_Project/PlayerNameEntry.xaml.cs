using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerNameEntry : ContentPage
    {
       
        public PlayerNameEntry()
        {
            InitializeComponent();         
        }

        //الذهاب للعبه
        private async void Button_Clicked_Entry_Player(object sender, EventArgs e)
        {
           
            //التحقق من ادخال المستخدم لقيم
            if (string.IsNullOrWhiteSpace(PlayerOneName.Text) || string.IsNullOrWhiteSpace(PlayerTwoName.Text))
            {
                if (string.IsNullOrWhiteSpace(PlayerOneName.Text) && string.IsNullOrWhiteSpace(PlayerTwoName.Text))
                {
                    //في حالة عدم ادخال اي قيمة
                    await DisplayAlert("تنبيه", "الرجاء إدخال اسم لكل من اللاعبين!!", "موافق");
                    EnterName1.Text = "الرجاء أدخال اسم اللاعب الاول";
                    PlayerOneName.PlaceholderColor = Color.FromHex("#FF3D3D");

                    EnterName2.Text = "الرجاء أدخال اسم اللاعب الثاني";
                    PlayerTwoName.PlaceholderColor = Color.FromHex("#FF3D3D");

                    lbloneplay.TextColor = Color.FromHex("#FF3D3D");
                    lbltwoplay.TextColor = Color.FromHex("#FF3D3D");

                    myButton.Text = "اضغط بعد ادخال الاسامي";
                }
                else if (string.IsNullOrWhiteSpace(PlayerOneName.Text))
                {
                    //في حالة عدم ادخال قيمة في الاسم الاول
                    await DisplayAlert("تنبيه", "الرجاء إدخال اسم للاعب الاول!!", "موافق");
                    EnterName1.Text = "الرجاء أدخال اسم اللاعب الاول";
                    PlayerOneName.PlaceholderColor = Color.FromHex("#FF3D3D");
                    PlayerOneName.Placeholder = "لم تدخل الاسم الاول";
                    lbloneplay.TextColor = Color.FromHex("#FF3D3D");

                    //اعادة ضبط الاسم الثاني
                    EnterName2.Text = "";
                    PlayerTwoName.PlaceholderColor = Color.FromHex("");
                    lbltwoplay.TextColor = Color.Black;
                    PlayerTwoName.Placeholder = "وهنا ايضا";

                    myButton.Text = "اضغط بعد ادخال اللاعب الاول";
                }
                else
                {
                    //في حالة عدم ادخال قيمة في الاسم الثاني
                    await DisplayAlert("تنبيه", "الرجاء إدخال اسم للاعب الثاني!!", "موافق");
                    EnterName2.Text = "الرجاء أدخال اسم اللاعب الثاني";
                    PlayerTwoName.PlaceholderColor = Color.FromHex("#FF3D3D");
                    PlayerTwoName.Placeholder = "لم تدخل الاسم الثاني";
                    lbltwoplay.TextColor = Color.FromHex("#FF3D3D");

                    //اعادة ضبط الاسم الاول
                    EnterName1.Text = "";
                    PlayerOneName.PlaceholderColor = Color.FromHex("");
                    lbloneplay.TextColor = Color.Black;
                    PlayerOneName.Placeholder = "ادخل الاسم هنا";

                    myButton.Text = "اضغط بعد ادخال اللاعب الثاني";
                }
               
            }
            //في حالة ادخال القيم
            else
            {
                //الذهاب لصغحة اللعبه
                await Navigation.PushAsync(new GamePage(PlayerOneName.Text, PlayerTwoName.Text));

                Application.Current.Properties["PlayerOneName"] = PlayerOneName.Text; // حفظ الاسم الاول
                Application.Current.Properties["PlayerTwoName"] = PlayerTwoName.Text; // حفظ الاسم الثاني
                await Application.Current.SavePropertiesAsync();  // حفظ الخصائص

                //اعادة ضبط الاسم الاول
                EnterName1.Text = "";
                PlayerOneName.PlaceholderColor = Color.FromHex("");
                PlayerOneName.Placeholder = "ادخل الاسم هنا";
                //اعادة ضبط الاسم الثاني               
                EnterName2.Text = "";
                PlayerTwoName.PlaceholderColor = Color.FromHex("");
                PlayerTwoName.Placeholder = "لم تدخل الاسم الثاني";
                // اعادة ضبط الزر
                lbloneplay.TextColor = Color.Black;
                lbltwoplay.TextColor = Color.Black;
                myButton.Text = "لعب";
            }
           
        }
        // تقوم بعرض الاسامي عند الدخول للصفحة في حالة وجودها مسبقا
        protected override void OnAppearing()
        {
            base.OnAppearing();

            // استعادة القيم عند عرض الانتري للاسم الاول
            if (Application.Current.Properties.ContainsKey("PlayerOneName"))
            {
                PlayerOneName.Text = Application.Current.Properties["PlayerOneName"].ToString();
            }
            // استعادة القيم عند عرض الانتري للاسم الثاني
            if (Application.Current.Properties.ContainsKey("PlayerTwoName"))
            {
                PlayerTwoName.Text = Application.Current.Properties["PlayerTwoName"].ToString();
            }
        }

        // زر التعليمات
        private async void ImageButton_Clicked_Info(object sender, EventArgs e)
        {
            await DisplayAlert("تعليمات", "في هذه الصفحة يجب عليك ادخل اسم لكل لاعب, حتى يمكننك اللعب ولا تستطيع الذهاب الى صفحة اللعبة دون ادخال اسم",
                              "فهمت");
        }
    }
}