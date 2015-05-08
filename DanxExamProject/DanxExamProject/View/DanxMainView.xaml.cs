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
        DispatcherTimer v = new DispatcherTimer();
        List<string> Newlist = new List<string>();
        List<string> ValueList = new List<string>();
        List<string> ValueTextList = new List<string>(); 
        private int _i = 0;
        private int _v = 0;
        private int _vt = 0;

        public MainPage()
        {
            this.InitializeComponent();

            Newlist.Add("DANX WINS TENDER OF NORDIC WAREHOUSE FOR BSH");
            Newlist.Add("DANX GROUP EXPANDS INTO THE BALTIC COUNTRIES");
            Newlist.Add("CSR AT DANX");
            Newlist.Add("HYUNDAI MOBIS");
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


            t.Interval = new TimeSpan(0,0,15);
            t.Start();
            t.Tick += DanxTick;

            v.Interval = new TimeSpan(0,0,10);
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
            
        }


    }

