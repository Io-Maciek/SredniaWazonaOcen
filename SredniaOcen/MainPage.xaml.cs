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

            /*
             * TODO funkcja: dodawanie nowych okien z wagą
             */

            s.AddNewWaga(3);
            s.AddNewWaga(5);
            s.AddNewWaga(6);
            s.AddNewWaga(2);
            s.AddNewWaga(4);
            s.AddNewWaga(1);

        }


    }
}
