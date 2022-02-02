using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SredniaOcen
{
    public partial class MainPage : ContentPage
    {
        public static FlexLayout main;
        public MainPage()
        {
            InitializeComponent();
            main = mainLayout;
            Srednia s = new Srednia(lblSrednia);

            s.AddNewWaga(Color.FromHex("#642000"), 1);
            s.AddNewWaga(Color.FromHex("#642022"), 2);
            s.AddNewWaga(Color.FromHex("#642044"), 3);
            s.AddNewWaga(Color.FromHex("#642066"), 4);
            s.AddNewWaga(Color.FromHex("#642078"), 5);
            s.AddNewWaga(Color.FromHex("#6420AA"), 6);
        }


    }
}
