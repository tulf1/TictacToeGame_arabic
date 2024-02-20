using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace TicTacToe_Project
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private System.Timers.Timer timer; //المؤقت
        private int secondsElapsed = 0; //عدد الثواني لزيادتها
        private bool isPlayer1Turn; //نبديل اللاعبين
        private string[,] gameBoard; //تجهيز لوحة اللعب
        private string Player1Name = ""; //اسم اللاعب الاول
        private string Player2Name = ""; //اسم اللاعب الثاني
        private int XWinCount = 0; //عدد فوز اللاعب الاول
        private int OWinCount = 0; //عدد فوز اللاعب الثاني
        private int DrawCount = 0; //عدد التعادلات
        private int ResultAllCount = 0; //مجموع القيم
        public GamePage(string Player1Name, string Player2Name)
        {
            InitializeComponent();

            //مساوات القيم المرسله
            this.Player1Name = Player1Name;
            this.Player2Name = Player2Name;
            // جعل الاسامي تظهر مباشرة
            Player1.Text = Player1Name;
            Player2.Text = Player2Name;

            //حساب فوز خساره تعادل المجموع
            XWinScore.Text = XWinCount.ToString();
            OWinScore.Text = OWinCount.ToString();
            DrawScore.Text = DrawCount.ToString();
            ResultAllScore.Text = ResultAllCount.ToString();

            //وضع مؤفت لجعل اللعبه اكثر تفاعليه
            timer = new System.Timers.Timer(1000); // يتحدث كل  ثانية 
            timer.Elapsed += OnTimerElapsed; //زيادة الوقت بالثانيه
            timer.AutoReset = true; //تفعيل اعادة تلقائي


            // تسوية الخلفية البرمجية للبوردر
            gameBoard = new string[3, 3];
            //للبدء بالعبه
            ResetGame();
            RandomPlayer();
          
        }
        // تنفيذ عند الضغط على الزر
        private async void Button_Clicked_Game(object sender, EventArgs e)
        {
            //وضع قيم للازارير
            Xamarin.Forms.ImageButton button = (Xamarin.Forms.ImageButton)sender;
            int row = Grid.GetRow(button); //للافقي
            int col = Grid.GetColumn(button); //للعامودي          
           

            // التحقق من أن الخانة فارغة قبل وضع الصور
            if (gameBoard[row, col] == null)
            {

                // وضع العلامة المناسبة للمستخدم الحالي
                if (isPlayer1Turn)
                {
                    button.Source = "x_img128.png"; //وضع صورة X 
                    gameBoard[row, col] = "X"; //وضع قيمة افتراضية في الخلفية تستخدم في التحقق
                    PlayerTurnText.Text = "دور اللاعب : " + Player2Name + " O"; //نص تبديل اللاعب
                }
                else
                {                    
                    button.Source = "o_img128.png"; //وضع صورة O 
                    gameBoard[row, col] = "O"; //وضع قيمة افتراضية في الخلفية تستخدم في التحقق
                    PlayerTurnText.Text = "دور اللاعب : " + Player1Name + " X"; //نص تبديل اللاعب                 
                }
               
                    // تغيير اللاعب
                    isPlayer1Turn = !isPlayer1Turn;
                
                // X حالة الفوز
                if (CheckForWin("X"))
                {
                    DisableButtons(); // تعطيل الأزرار
                    timer.Stop(); //ايقاف الوفت
                    bool OkResetX = await DisplayAlert("مبرووك!!", Player1Name + " الفائز!", "حسنا", "الغاء"); //رسالة التهنئة

                    //حساب فوز X
                    XWinCount++;
                    XWinScore.Text = XWinCount.ToString(); //عرض الرقم
                    ResultAllCount++; //زيادة المجموع
                    ResultAllScore.Text = ResultAllCount.ToString(); //عرض الرقم

                    if (OkResetX)
                    {
                        ResetGame(); //اعادة تهيأت اللعبة
                        // إعادة تمكين الأزرار بعد إغلاق رسالة التنبيه
                        EnableButtons();
                        timer.Start(); //اعادة تعغيل الوقت
                    }
                    else
                    {
                        timer.Stop(); //ايقاف الوقت
                    }
                }
                // O حالة الفوز
                else if (CheckForWin("O"))
                {
                    DisableButtons(); // تعطيل الأزرار
                    timer.Stop(); //ايقاف الوفت
                    bool OkResetO = await DisplayAlert("مبرووك!!", Player2Name + " الفائز!", "حسنا", "الغاء"); //رسالة التهنئة

                    //حساب فوز O
                    OWinCount++;
                    OWinScore.Text = OWinCount.ToString(); //عرض الرقم
                    ResultAllCount++; //زيادة المجموع
                    ResultAllScore.Text = ResultAllCount.ToString(); //عرض الرقم

                    if (OkResetO)
                    {
                        ResetGame(); //اعادة تهيأت اللعبة
                        // إعادة تمكين الأزرار بعد إغلاق رسالة التنبيه
                        EnableButtons();
                        timer.Start(); //اعادة تعغيل الوقت
                    }
                    else
                    {
                        timer.Stop(); //ايقاف الوقت
                    }
                }
                // حالة التعادل
                else if (CheckForDraw())
                {
                    DisableButtons(); // تعطيل الأزرار
                    timer.Stop(); //ايقاف الوفت
                    bool OkResetD = await DisplayAlert("النتيجة تعادل!!", "لم يفز احد", "حسنا", "الغاء");

                    //حساب التعادل
                    DrawCount++;
                    DrawScore.Text = DrawCount.ToString(); //عرض الرقم
                    ResultAllCount++; //زيادة المجموع
                    ResultAllScore.Text = ResultAllCount.ToString(); //عرض الرقم

                    if (OkResetD)
                    {
                        ResetGame(); //اعادة تهيأت اللعبة
                        timer.Start(); //اعادة تعغيل الوقت
                    }
                    else
                    {
                        timer.Stop(); //ايقاف الوقت
                    }
                }              
            }
        }

        private bool CheckForWin(string player)
        {
            // التحقق من الفائز بجميع الاحتمالات
            // يتم ارسال قيمة ويتم التحقق من المساوة gameBoard

            // التحقق من الصفوف
            for (int i = 0; i < 3; i++)
            {
                if (gameBoard[i, 0] == player && gameBoard[i, 1] == player && gameBoard[i, 2] == player)
                {
                    //تغيير خلفية الصف الفائز
                    if (i == 0)
                    {
                        button0.BackgroundColor = Color.FromHex("#62A833"); //الصف الاول
                        button1.BackgroundColor = Color.FromHex("#62A833");
                        button2.BackgroundColor = Color.FromHex("#62A833");
                    }
                    else if (i == 1)
                    {
                        button3.BackgroundColor = Color.FromHex("#62A833"); //الصف الثاني
                        button4.BackgroundColor = Color.FromHex("#62A833");
                        button5.BackgroundColor = Color.FromHex("#62A833");
                    }
                    else if (i == 2)
                    {
                        button6.BackgroundColor = Color.FromHex("#62A833"); //الصف الثالث
                        button7.BackgroundColor = Color.FromHex("#62A833");
                        button8.BackgroundColor = Color.FromHex("#62A833");
                    }

                    return true;
                }
            }

            // التحقق من الأعمدة
            for (int j = 0; j < 3; j++)
            {
                if (gameBoard[0, j] == player && gameBoard[1, j] == player && gameBoard[2, j] == player)
                {
                    //تغيير خلفية العامود الفائز 
                    if (j == 0)
                    {
                        button0.BackgroundColor = Color.FromHex("#62A833"); //العامود الاول
                        button3.BackgroundColor = Color.FromHex("#62A833");
                        button6.BackgroundColor = Color.FromHex("#62A833");
                    }
                    else if (j == 1)
                    {
                        button1.BackgroundColor = Color.FromHex("#62A833"); //العامود الثاني
                        button4.BackgroundColor = Color.FromHex("#62A833");
                        button7.BackgroundColor = Color.FromHex("#62A833");
                    }
                    else if (j == 2)
                    {
                        button2.BackgroundColor = Color.FromHex("#62A833"); //العامود الثالث
                        button5.BackgroundColor = Color.FromHex("#62A833");
                        button8.BackgroundColor = Color.FromHex("#62A833");
                    }
                    return true;
                }
            }

            // التحقق من القطر الرئيسي وتغيير خلفيته
            if (gameBoard[0, 0] == player && gameBoard[1, 1] == player && gameBoard[2, 2] == player)
            {
                button0.BackgroundColor = Color.FromHex("#62A833");
                button4.BackgroundColor = Color.FromHex("#62A833");
                button8.BackgroundColor = Color.FromHex("#62A833");

                return true;
            }
            // التحقق من القطر الفرعي وتغيير خلفيته
            if (gameBoard[0, 2] == player && gameBoard[1, 1] == player && gameBoard[2, 0] == player)
            {
                button2.BackgroundColor = Color.FromHex("#62A833");
                button4.BackgroundColor = Color.FromHex("#62A833");
                button6.BackgroundColor = Color.FromHex("#62A833");

                return true;
            }
            return false;
        }

        // التحقق من حالة التعادل
        private bool CheckForDraw()
        {
            // يتم التحقق من كل الخانات للتأكد من عدم وجود فائز     
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] == null)
                        return false;
                }
            }
            return true;
        }

        // إعادة تهيئة لوحة اللعبة
        private void ResetGame()
        {
            //اعادة تهيأت الخلفية
            button0.BackgroundColor = Color.FromHex("#FFF2D7");
            button1.BackgroundColor = Color.FromHex("#FFF2D7");
            button2.BackgroundColor = Color.FromHex("#FFF2D7");
            button3.BackgroundColor = Color.FromHex("#FFF2D7");
            button4.BackgroundColor = Color.FromHex("#FFF2D7");
            button5.BackgroundColor = Color.FromHex("#FFF2D7");
            button6.BackgroundColor = Color.FromHex("#FFF2D7");
            button7.BackgroundColor = Color.FromHex("#FFF2D7");
            button8.BackgroundColor = Color.FromHex("#FFF2D7");

            // يتم مسح جميع العلامات من الأزرار وإعادة تهيئة لوحة اللعبة
            GameGrid.Children.ForEach(button =>
             {
                 ((Xamarin.Forms.ImageButton)button).Source = "";
             });
            // تهيئة اللوحة        
            gameBoard = new string[3, 3];

            secondsElapsed = 0; //
            TimeCountlbl.Text = "00:00"; // إعادة تعيين النص للعداد


            EnableButtons();
        }
        // تعطيل الأزرار من اجل عدم امكانية استخدامها اثناء ظهور رسالة التنبيه وتجنب خطأ تغيير الفائز
        private void DisableButtons()
        {
            button0.IsEnabled = false;
            button1.IsEnabled = false;
            button2.IsEnabled = false;
            button3.IsEnabled = false;
            button4.IsEnabled = false;
            button5.IsEnabled = false;
            button6.IsEnabled = false;
            button7.IsEnabled = false;
            button8.IsEnabled = false;
        }

        // اعادة تفعيل الازار
        private void EnableButtons()
        {
            button0.IsEnabled = true;
            button1.IsEnabled = true;
            button2.IsEnabled = true;
            button3.IsEnabled = true;
            button4.IsEnabled = true;
            button5.IsEnabled = true;
            button6.IsEnabled = true;
            button7.IsEnabled = true;
            button8.IsEnabled = true;
        }

        // الاختياير العشوائي للاعبين
        private async void RandomPlayer()
        {
            PlayerTurnText.Text = "تحديد دور الاعب...";
            // تأخير لمدة ثلاث ثواني
            await Task.Delay(3000);

            Random random = new Random();
            isPlayer1Turn = random.Next(2) == 0; //اختيار عشوائي

            timer.Start();
            // التبديل بين اللاعبين
            if (isPlayer1Turn)
            {
                PlayerTurnText.Text = "دور اللاعب : " + Player1Name + " X";
            }
            else
            {
                PlayerTurnText.Text = "دور اللاعب : " + Player2Name + " O";
            }
        }
        //التوقيت
        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            //تهيأت الوقت
            Device.BeginInvokeOnMainThread(() =>
            {
                secondsElapsed++; //زيادة الوقت
                TimeCountlbl.Text = TimeSpan.FromSeconds(secondsElapsed).ToString(@"mm\:ss"); //شكل عرض التوقيت
            });
        }
        // ايكون الرجوع للصحفة الرئيسية
        private async void ToolbarItem_Clicked_Home(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
        // ايكون اعادة التعيين
        private void ToolbarItem_Clicked_Refresh(object sender, EventArgs e)
        {
            ResetGame();
            timer.Start();
        }
        // ايكون تعديل الاسم
        private async void ToolbarItem_Clicked_Rename(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayerNameEntry());
        }
        // ايكون التعليمات
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("طريقة اللعب", "يقوم البرنامج بختيار لاعب عشوائي, ومن ثم يضغط على احد المربعات التي بالاسفل ويذهب للاعب الاخر الى ان يفوز احدهم," +
                            " وتحسب النقاط بالاعلى.", "فهمت");
        }
    }
}