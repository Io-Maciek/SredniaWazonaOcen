using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SredniaOcen
{
    internal class Srednia
    {

        Label lblWaga;
        Entry entryNowaOcena;
        Button btnNowaOcena;
        FlexLayout LayoutNaOceny;

        //<Button x:Name="btnOcena" Margin="5,2,5,2" WidthRequest="50" HeightRequest="0" Text="5"></Button>


        List<Button> btnOceny = new List<Button>();

        Label lblSrednia;

        public Srednia(Frame wagaFrame, double wartoscWagi, Label srednia)
        {
            lblWaga=(Label)wagaFrame.Content.FindByName("lblWaga");
            lblWaga.Text=wartoscWagi.ToString();
            entryNowaOcena= (Entry)wagaFrame.Content.FindByName("entryNowaOcena");
            btnNowaOcena= (Button)wagaFrame.Content.FindByName("btnDodajNowaOcena");

            btnNowaOcena.Clicked += BtnNowaOcena_Clicked;

            LayoutNaOceny = (FlexLayout)wagaFrame.Content.FindByName("MiejsceNaOceny");

            lblSrednia = srednia;
        }

        private void BtnNowaOcena_Clicked(object sender, EventArgs e)
        {
            double d;
            if(double.TryParse(entryNowaOcena.Text,out d))
            {
                entryNowaOcena.Text = "";
                Button b = GetBtnOcena(d);
                btnOceny.Add(b);
                LayoutNaOceny.Children.Add(b);
                UpdateSrednia();
            }
            else
            {
                entryNowaOcena.Text = "";
            }
        }

        private Button GetBtnOcena(double ocena)
        {
            Button btnocena = new Button()
            {
                Margin = new Thickness(5, 2, 5, 2),
                WidthRequest = 37,
                HeightRequest = 37,
                Text = ocena.ToString()
            };
            btnocena.Clicked += Btnocena_Clicked;
            return btnocena;
        }

        private void Btnocena_Clicked(object sender, EventArgs e)
        {
            btnOceny.Remove((Button)sender);
            LayoutNaOceny.Children.Remove((Button)sender);
            UpdateSrednia();
        }

        void UpdateSrednia()
        {
            double suma = 0;
            foreach (var item in btnOceny)
            {
                suma += double.Parse(item.Text);
            }
            lblSrednia.Text = (suma / btnOceny.Count).ToString();
        }
    }
}
