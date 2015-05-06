using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DanxExamProject.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DanxMainView : Page
    {
        private int _i;
        readonly List<string> _newsList = new List<string>() { "Danx Bla", "Danx - ----", "NewText", "Dandandnandnadna", "Lawl", "Yay" };


        public DanxMainView()
        {

            this.InitializeComponent();

            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 3);
            t.Start();
            
            t.Tick += TOnTick;
            
            
        }

        private void TOnTick(object sender, object o)
        {
            
           ChangeBlock.Text = _newsList[_i];
            if (_i != _newsList.Count-1) _i++;
            else _i = 0;
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Task.Delay(100);
            Frame.Navigate(typeof (loggedIn));
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (loggedIn));
        }

       

        
        


    }
}
