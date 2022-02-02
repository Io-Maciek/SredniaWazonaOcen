using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace SredniaOcen
{
    public class Srednia
    {

        List<FrameWaga> frameWagi = new List<FrameWaga>();

        Label lblSrednia;

        public Srednia(Label srednia)
        {

            lblSrednia = srednia;
        }

        public void AddNewWaga(Color bg, double waga)
        {
            frameWagi.Add(new FrameWaga(this, bg, waga));
        }



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
            {
                lblSrednia.Text = "0";

            }else
                lblSrednia.Text = (suma / lacznieWagi).ToString("0.##");
        }
    }
}
