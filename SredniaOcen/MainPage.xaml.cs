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



            for (int i = 0; i < 6; i++)
            {
                s.AddNewWaga(Color.FromHex("#36F0"+i+i),i + 1);
            }

        }


    }
}
