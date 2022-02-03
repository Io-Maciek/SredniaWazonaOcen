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
        List<FrameWaga> frameWagi = new List<FrameWaga>();

        Label lblSrednia;

        public Srednia(Label srednia)
        {

            lblSrednia = srednia;
        }


        /// <summary>
        /// Dodawanie nowego okna z wagą (z sortowaniem według wagi). Nie pozwala na dodanie dwóch takich samych wartości wag
        /// </summary>
        public void AddNewWaga(double waga)
        {
            // jeśli nie istnieje już taka waga
            if (frameWagi.Where(n => n.Waga == waga).Count() == 0)
            {
                frameWagi.Add(new FrameWaga(this, Color.FromHex("#3674c9"), waga));
                frameWagi.Sort();

                MainPage.main.Children.Clear();
                foreach (var item in frameWagi)
                {
                    MainPage.main.Children.Add(item.frame);
                }
            }
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

                    lacznieWagi += item.Waga*item.btnOceny.Count;
                    for (int i = 0; i < item.btnOceny.Count; i++)
                    {
                        suma += double.Parse(item.btnOceny[i].Text) * item.Waga;
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
        /// <param name="frameWaga"></param>
        internal void CloseFrame(FrameWaga frameWaga)
        {
            frameWagi.Remove(frameWaga);
            frameWagi.Sort();

            MainPage.main.Children.Clear();
            foreach (var item in frameWagi)
            {
                MainPage.main.Children.Add(item.frame);
            }
            UpdateSrednia();
        }
    }
}
