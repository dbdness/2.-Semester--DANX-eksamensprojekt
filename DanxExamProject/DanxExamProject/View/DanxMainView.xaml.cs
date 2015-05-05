using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace DanxExamProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        DispatcherTimer t = new DispatcherTimer();
        List<string> Newlist = new List<string>();
        private int _i = 0;

        public MainPage()
        {
            this.InitializeComponent();

            Newlist.Add("DANX WINS TENDER OF NORDIC WAREHOUSE FOR BSH");
            Newlist.Add("DANX GROUP EXPANDS INTO THE BALTIC COUNTRIES");
            Newlist.Add("CSR AT DANX");
            Newlist.Add("HYUNDAI MOBIS");

            t.Interval = new TimeSpan(0,0,15);
            t.Start();
            t.Tick += DanxTick;

            

        }

        private void DanxTick(object sender, object e)
        {
            NewsBlock.Text = Newlist[_i];
            if (_i != Newlist.Count - 1) _i++;
            else _i = 0;

        }
            
        }


    }

