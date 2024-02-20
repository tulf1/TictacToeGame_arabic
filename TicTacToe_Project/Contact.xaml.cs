using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contact : ContentPage
    {
        public Contact()
        {
            InitializeComponent();
        }
        // زر الارسال لاايميل
        private async void Button_Clicked_Send(object sender, EventArgs e)
        {

            // التحقق من ادخال نص للارسال
            if (string.IsNullOrWhiteSpace(Messagetxt.Text) || myPicker.SelectedIndex == -1)
             {
                if (string.IsNullOrWhiteSpace(Messagetxt.Text) && myPicker.SelectedIndex == -1) 
                {
                    //في حالة عدم اختيار الموضوع وادخال الرسالة 
                    await DisplayAlert("تنبيه", "الرجاء اختيار موضوع الرسالة وإدخال الرسالة المراد ارسالها!!", "موافق");
                    Messagetxt.PlaceholderColor = Color.FromHex("#FF3D3D");
                    EnterMassage.Text = "لم تدخل رسالة";
                    SendEmailButton.Text = "اضغط بعد ادخال المطلوب";

                    myPicker.TextColor = Color.FromHex("#FF3D3D");
                    lblPicker.Text = "لم اقم بادخال الرسالة";
                    lblPicker.TextColor = Color.FromHex("#FF3D3D");
                }
                else if (string.IsNullOrWhiteSpace(Messagetxt.Text))
                {
                    //في حالة عدم ادخال الرسالة 
                    await DisplayAlert("تنبيه", "الرجاء إدخال الرسالة المراد ارسالها!!", "موافق");
                    Messagetxt.PlaceholderColor = Color.FromHex("#FF3D3D");
                    EnterMassage.Text = "لم تدخل رسالة";
                    SendEmailButton.Text = "اضغط بعد ادخال الرسالة";

                    //اعادة تهيات اختيار الموضع
                    myPicker.TextColor = Color.Black;
                    lblPicker.Text = "اختر موضوع الرسالة : ";
                    lblPicker.TextColor = Color.Black;
                }
                else
                {
                    //في حالة عدم اختيار موضوع الرسالة 
                    await DisplayAlert("تنبيه", "الرجاء إدخال موضوع الرسالة المراد ارسالها!!", "موافق");
                    myPicker.TextColor = Color.FromHex("#FF3D3D");
                    lblPicker.Text = "لم اقم بادخال الرسالة";
                    lblPicker.TextColor = Color.FromHex("#FF3D3D");
                    SendEmailButton.Text = "اضغط بعد ادخل الموضوع";

                    Messagetxt.PlaceholderColor = Color.Black;
                    EnterMassage.Text = "";                   
                }
            }
            else
            {
                //اعادة تعيين اختيار الموضوع الرسالة والزر
                Messagetxt.PlaceholderColor = Color.Black;
                EnterMassage.Text = "";
                SendEmailButton.Text = "ارسال";

                myPicker.TextColor = Color.Black;
                lblPicker.Text = "اختر موضوع الرسالة : ";
                lblPicker.TextColor = Color.Black;
                try
                {
                    var emailRecipient = "example@gmail.com"; //الايميل المستقبل
                    var subject = myPicker.SelectedItem.ToString();   //الموضوع         
                    var body = Messagetxt.Text;//الرسالة
                    // تجهيز الايميل
                    await Launcher.OpenAsync(new Uri($"mailto:{emailRecipient}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}"));
                    // فتح الايميل ووضع المحتوى
                    var message = new EmailMessage
                    {
                        Subject = myPicker.SelectedItem.ToString(),
                        Body = Messagetxt.Text,
                        To = new List<string> { emailRecipient }
                    };
                    await Email.ComposeAsync(message);

                }
                // في حالة عدم وجود برنامج ايميل
                catch (FeatureNotSupportedException)
                {
                    await DisplayAlert("خطأ", "الإيميل غير مدعوم على هذا الجهاز. الرجاء تثبيت تطبيق إيميل (ملاحظة : قد تظهر هذه الرسالة حتى في حالة وجود الايميل).", "موافق");
                }
            }
        }
        // اختيار الموضوع
        private void myPicker_SelectedIndexChanged(object sender, EventArgs e)
        {           
            // Editor تغيير محتوى نص داخل 
            switch (myPicker.SelectedIndex)
            {
                case 0:
                    Messagetxt.Placeholder = "ادخل اقتراحك هنا";
                    break;
                case 1:
                    Messagetxt.Placeholder = "ادخل المشكلة الي واجهتك هنا";
                    break;
                case 2:
                    Messagetxt.Placeholder = "ادخل الرسالة هنا";
                    break;
                default:
                    Messagetxt.Placeholder = "ادخل الرسالة هنا";
                    break;
            }
        }
       
        //تعليمات
        private async void ContactInfo_Tapped(object sender, EventArgs e)
        {
            await DisplayAlert("تعليمات", "في هذه الصفحة يمكنك التواصل مع مطورين اللعبة, حيث عند اختيارك لموضوع وادخال رسالة سيتم تحويلك الى برنامج الايميل لارسال الرسالة (ملاحظة : يجب وجود برنامج الايميل).",
                              "فهمت");
        }
    }
}
