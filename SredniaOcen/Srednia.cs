using SredniaOcen.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SredniaOcen
{
    public class Srednia
    {
        public int index = 0;
        List<OknoWaga> frameWagi = new List<OknoWaga>();

        Label lblSrednia;

        public Srednia(Label srednia)
        {

            lblSrednia = srednia;
        }

        public void AddNewWaga(Color frameColor, double waga)
        {
            // jeśli nie istnieje już taka waga
            if (frameWagi.Where(n => n.Text == waga).Count() == 0)
            {
                OknoWaga temp = new OknoWaga(frameColor, waga);
                temp.ClickedClose += CloseFrame;
                temp.ClickedAdd += ClickedAddOcene;
                temp.AktualizacjaSredniej = UpdateSrednia;

                frameWagi.Add(temp);
                frameWagi.Sort();

                MainPage.main.Children.Clear();
                foreach (var item in frameWagi)
                {
                    MainPage.main.Children.Add(item);
                }
            }
        }


        /// <summary>
        /// Dodawanie nowego okna z wagą (z sortowaniem według wagi). Nie pozwala na dodanie dwóch takich samych wartości wag
        /// </summary>
        public void AddNewWaga(double waga)
        {
            AddNewWaga(Color.FromHex("#3674c9"), waga);
        }


        /// <summary>
        /// Wywoływana po naciśnięciu guzika dodania nowej oceny. Aktualizuje średnią
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickedAddOcene(object sender, EventArgs e)
        {
            UpdateSrednia();
        }


        /// <summary>
        /// Zlicza wszystkie okna wagi, liczy średnią i wpisuje ją do Label <c>lblSrednia</c>
        /// </summary>
        public void UpdateSrednia()
        {
            double suma = 0;
            double lacznieWagi = 0;
            foreach (var item in frameWagi)
            {

                    lacznieWagi += item.Text*item.btnOceny.Count;
                    for (int i = 0; i < item.btnOceny.Count; i++)
                    {
                        suma += double.Parse(item.btnOceny[i].Text) * item.Text;
                    }
                
            }

            if (lacznieWagi == 0)
                lblSrednia.Text = "0";
            else
                lblSrednia.Text = (suma / lacznieWagi).ToString("0.##");
        }


        /// <summary>
        /// Wywoływana po naciśnięciu guzika wyłączenia okna z wagą. Usuwa z listy i aktualizuje średnią
        /// </summary>
        internal void CloseFrame(object sender,EventArgs e)
        {
            Grid tGrid = (Grid)((Button)sender).Parent;
            StackLayout tStack = (StackLayout)tGrid.Parent;
            OknoWaga t = (OknoWaga)tStack.Parent;

            frameWagi.Remove(t);

            MainPage.main.Children.Clear();
            foreach (var item in frameWagi)
            {
                MainPage.main.Children.Add(item);
            }
            UpdateSrednia();
        }
    }
}
