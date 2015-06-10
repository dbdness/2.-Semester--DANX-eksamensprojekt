    using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
    using System.Xml;
    using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using DanxExamProject.Handler;


namespace DanxExamProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DanxMainPage : Page
    {
       public static DispatcherTimer t = new DispatcherTimer();
        DispatcherTimer v = new DispatcherTimer();
        public static List<string> Newlist = new List<string>();
        List<string> ValueList = new List<string>();
        List<string> ValueTextList = new List<string>(); 
        private int _i = 0;
        private int _v = 0;
        private int _vt = 0;
        private static List<Canvas> _canvasList;
        public static Canvas MainScreenCanvas = new Canvas();
        public static Canvas MainScreenLoginCanvas = new Canvas();
        public static Canvas AdminToolsCanvas = new Canvas();
        public static RadioButton SickDayRButton = new RadioButton();
        public static RadioButton VacationDayRButton = new RadioButton();
        public static TextBlock UiWelcomeMessage = new TextBlock();

      

        public DanxMainPage()
        {
            this.InitializeComponent();

            _canvasList = new List<Canvas>(){MainCanvas, LoginCanvas};
            MainScreenCanvas = MainCanvas;
            MainScreenLoginCanvas = LoginCanvas;
            AdminToolsCanvas = AdminTools;
            SickDayRButton = SickDayRadioButton;
            VacationDayRButton = VacationDayRadioButton;
            UiWelcomeMessage = WelcomeMessage;

            DatePicker.MinYear = new DateTimeOffset(new DateTime(2014, 01, 01));
            DatePicker.MaxYear = new DateTimeOffset(new DateTime(2020, 01, 01));

            Newlist.Add("DANX WINS TENDER OF NORDIC WAREHOUSE FOR BSH");
            Newlist.Add("DANX GROUP EXPANDS INTO THE BALTIC COUNTRIES");
            Newlist.Add("CSR AT DANX - Knæk cancer, Unicef, Danske Hospitalsklovne");
            Newlist.Add("HYUNDAI MOBIS - The Service & Logistic organization for Kia and Hyundai Automotive parts extends the cooperation with DANX ");
            ValueList.Add("EQUALITY");
            ValueList.Add("QUALITY");
            ValueList.Add("FLEXIBILITY");
            ValueList.Add("CREATIVITY");
            ValueList.Add("AVAILABILITY");
            ValueList.Add("PRIDE");
            ValueTextList.Add(@"We are all equals, performing different roles to achieve the same goal. 
We treat our customers, partners and colleagues with the same respect that we want to achieve ourselves. ");
            ValueTextList.Add("We strive for 100% in everything we do – in that way we ensure that our customers meet their customers’ high expectations. ");
            ValueTextList.Add("Flexibility is our mindset and the key to our success.");
            ValueTextList.Add("As pioneers we think out of the box and create solutions for our customers’ needs. We are never satisfied and are constantly looking for new ways to improve.");
            ValueTextList.Add("We ensure our customers’ availability of spare parts through personal care and availability.");
            ValueTextList.Add("We are proud of our customers, our company and our people - we take pride in everything we do.");


            t.Interval = new TimeSpan(0,0,3);
            t.Start();
            t.Tick += DanxTick;
            

            v.Interval = new TimeSpan(0,0,7);
            v.Start();
            v.Tick += ValueTick;
            v.Tick += ValueTextTick;




        }

        private void ValueTextTick(object sender, object e)
        {
            ValueText.Text = ValueTextList[_vt];
            if (_vt != ValueTextList.Count - 1) _vt++;
            else _vt = 0;
        }


        private void ValueTick(object sender, object e)
        {
            ValueBlock.Text = ValueList[_v];
            if (_v != ValueList.Count - 1) _v++;
            else _v = 0;
        }

        private void DanxTick(object sender, object e)
        {
            NewsBlock.Text = Newlist[_i];
            if (_i != Newlist.Count - 1) _i++;
            else _i = 0;

        }

        public static void CloseCanvases()
        {
            foreach(var c in _canvasList) c.Visibility = Visibility.Collapsed;
           
        }

        

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.InformationToolFlyout.Hide();
            InsertNameBox.Text = String.Empty;
            InsertManagerBox.Text = String.Empty;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.SalaryToolFlyout.Hide();
            InsertSalaryNumberBox.Text = String.Empty;
            InsertVacationDaysBox.Text = String.Empty;
            InsertSickDaysBox.Text = String.Empty;
            InsertWorkedDaysBox.Text = String.Empty;
        }

        private void ExportToExcelAppBarButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            ExportToExcelAppBarButton.Label = "Export to Excel";
        }

        private void ExportToExcelAppBarButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            ExportToExcelAppBarButton.Label = String.Empty;
        }

        private void AdminEditAppBarButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AdminEditAppBarButton.Label = "Edit selected employee";
        }

        private void AdminEditAppBarButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AdminEditAppBarButton.Label = String.Empty;
        }

        private void AdminTableViewAppBarButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AdminTableViewAppBarButton.Label = "Employees";
        }

        private void AdminTableViewAppBarButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AdminTableViewAppBarButton.Label = String.Empty;
        }

        private void AddVacationSickDayAppBarButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AddVacationSickDayAppBarButton.Label = "Add vacation- or sickdays";
        }

        private void AddVacationSickDayAppBarButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AddVacationSickDayAppBarButton.Label = String.Empty;
        }

        private async void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            this.InformationToolFlyout.Hide();
            await Task.Delay(2500);
            InsertNameBox.Text = String.Empty;
            InsertManagerBox.Text = String.Empty;
        }

        private async void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            this.SalaryToolFlyout.Hide();
            await Task.Delay(2500);
            InsertSalaryNumberBox.Text = String.Empty;
            InsertVacationDaysBox.Text = String.Empty;
            InsertSickDaysBox.Text = String.Empty;
            InsertWorkedDaysBox.Text = String.Empty;
        }

       

        
            
        }


    }

