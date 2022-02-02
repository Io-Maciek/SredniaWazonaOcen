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
        public static Frame wagaModel;
        public static FlexLayout layout;

        public MainPage()
        {
            InitializeComponent();
            wagaModel = frameWagaModel;
            layout = mainLayout;

            Srednia s = new Srednia(wagaModel,3,lblSrednia);

        }


    }
}
