using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DANXprototype
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void Login()
        {
            if (LoginBox.Text != "1234") return;
            MainCanvas.Visibility = Visibility.Collapsed;
            StandardLoginCanvas.Visibility = Visibility.Visible;
            LoginBox.Text = "";
        }

        private void Manage()
        {
            if (ManageBox.Text != "4321") return;
            StandardLoginCanvas.Visibility = Visibility.Collapsed;
            AdminManageCanvas.Visibility = Visibility.Visible;
            ManageBox.Text = "";
        }

        private void NewDatabaseView()
        {
            PastRegDatabase.Visibility = Visibility.Collapsed;
            WorkerListDB.Visibility = Visibility.Visible;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {
            NewDatabaseView();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void LoginBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter) return;
            Login();
            

        }

        private void ManageBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != VirtualKey.Enter) return;
            Manage();
        }
    }
}
