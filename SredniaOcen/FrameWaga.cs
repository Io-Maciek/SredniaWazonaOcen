using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SredniaOcen
{
    internal class FrameWaga : IComparable<FrameWaga>
    {
        public List<Button> btnOceny = new List<Button>();
        FlexLayout MiejsceNaOceny;
        Entry entryNowaOcena;
        Button btnDodajNowaOcene;

        public double Waga;

        Srednia srednia;

        public Frame frame;

        readonly static int ButtonOcenaSize;

        static FrameWaga()
        {
            if(Device.RuntimePlatform == Device.Android)
            {
                ButtonOcenaSize = 45;
            }
            else
            {
                ButtonOcenaSize = 37;
            }
        }


        public FrameWaga(Srednia srednia, Color bg, double waga)
        {
            Waga = waga;
            StackLayout c = new StackLayout();
            #region Grid 1
            Grid g1 = new Grid();
            g1.Margin = new Thickness(0, 0, 0, 20);

            ColumnDefinitionCollection two = new ColumnDefinitionCollection();
            two.Add(new ColumnDefinition() { Width = GridLength.Star });
            two.Add(new ColumnDefinition() { Width = GridLength.Auto });
            two.Add(new ColumnDefinition() { Width = GridLength.Auto });
            g1.ColumnDefinitions = two;

            Label lblWaga = new Label();
            Label lblWagaWartosc = new Label();
            lblWaga.Text = "Waga";
            lblWaga.FontSize = 19;
            Grid.SetColumn(lblWaga, 0);

            lblWagaWartosc.Text = waga.ToString();
            lblWagaWartosc.FontSize = 19;
            Grid.SetColumn(lblWagaWartosc, 1);

            g1.Children.Add(lblWaga);
            g1.Children.Add(lblWagaWartosc);

            Button btnCloseFrame = new Button();
            btnCloseFrame.Text = "x";
            btnCloseFrame.BackgroundColor = Color.Red;
            btnCloseFrame.FontSize = 13;
            btnCloseFrame.Opacity = 0.5f;
            btnCloseFrame.WidthRequest = ButtonOcenaSize;
            btnCloseFrame.HeightRequest = ButtonOcenaSize;
            btnCloseFrame.Padding = new Thickness(0, 0, 0, 5);
            btnCloseFrame.Clicked += BtnCloseFrame_Clicked;

            Grid.SetColumn(btnCloseFrame, 2);
            g1.Children.Add(btnCloseFrame);


            #endregion
            c.Children.Add(g1);
            #region Grid 2
            Grid g2 = new Grid();

            ColumnDefinitionCollection three = new ColumnDefinitionCollection();
            three.Add(new ColumnDefinition() { Width = GridLength.Star });
            three.Add(new ColumnDefinition() { Width = GridLength.Auto });
            three.Add(new ColumnDefinition() { Width = GridLength.Auto });
            g2.ColumnDefinitions = three;

            Label lblOceny = new Label();
            lblOceny.Text = "Oceny";
            lblOceny.FontSize = 19;

            entryNowaOcena = new Entry();
            entryNowaOcena.Completed += EntryNowaOcena_Completed;
            //entryNowaOcena.FontSize = 19;
            entryNowaOcena.Keyboard = Keyboard.Numeric;
            entryNowaOcena.Placeholder = "Ocena";
            Grid.SetColumn(entryNowaOcena, 1);

            btnDodajNowaOcene = new Button();
            btnDodajNowaOcene.Text = "Dodaj";
            btnDodajNowaOcene.Clicked += BtnDodajNowaOcene_Clicked;
            Grid.SetColumn(btnDodajNowaOcene, 2);
            g2.Children.Add(lblOceny);
            g2.Children.Add(entryNowaOcena);
            g2.Children.Add(btnDodajNowaOcene);

            #endregion
            c.Children.Add(g2);

            #region ScrollView
            ScrollView scrollView = new ScrollView();
            MiejsceNaOceny = new FlexLayout();
            MiejsceNaOceny.Wrap = FlexWrap.Wrap;
            scrollView.Content = MiejsceNaOceny;
            scrollView.HeightRequest = 121;
            #endregion
            c.Children.Add(scrollView);



            Frame f = new Frame();
            f.Content = c;
            f.BackgroundColor = bg;
            f.WidthRequest = 250;
            f.HeightRequest = 250;
            f.CornerRadius = 20;
            f.Margin = new Thickness(5);
            //MainPage.main.Children.Add(f);
            frame = f;
            this.srednia = srednia;
        }

        /// <summary>
        /// Uruchamia metodę w klasie <c>Srednia</c>
        /// </summary>
        private void BtnCloseFrame_Clicked(object sender, EventArgs e)
        {
            srednia.CloseFrame(this);
        }

        /// <summary>
        /// Obsługuje potwierdzenie (np. guzik enter) i wywołuje metodę dodającą nową ocenę
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntryNowaOcena_Completed(object sender, EventArgs e)
        {
            BtnDodajNowaOcene_Clicked(btnDodajNowaOcene, e);
        }

        /// <summary>
        /// Dodaje nową ocenę do listy w formie guzika, który można kliknąć i usunąć. Aktualizuje średnią o nową ocenę
        /// </summary>
        private void BtnDodajNowaOcene_Clicked(object sender, EventArgs e)
        {
            if (entryNowaOcena.Text != null && double.TryParse(entryNowaOcena.Text, out double d))
            {
                entryNowaOcena.Text = "";
                Button b = GetBtnOcena(d);
                btnOceny.Add(b);
                MiejsceNaOceny.Children.Add(b);
                srednia.UpdateSrednia();
            }
        }

        /// <summary>
        /// Tworzy usuwalny guzik
        /// </summary>
        /// <param name="ocena">Wartość oceny, którą będzie miał guzik</param>
        /// <returns>Guzik, który po naciśnięciu się usuwa z listy i aktualizuje średnią</returns>
        private Button GetBtnOcena(double ocena)
        {
            Button btnocena = new Button()
            {
                Margin = new Thickness(5, 2, 5, 2),
                WidthRequest = ButtonOcenaSize,
                HeightRequest =ButtonOcenaSize,
                Text = ocena.ToString()
            };
            btnocena.Clicked += Btnocena_Clicked;
            return btnocena;
        }

        /// <summary>
        /// Kliknięcie guzika z wartością oceny. Usuwa go z listy ocen i aktualizuje średnią
        /// </summary>
        private void Btnocena_Clicked(object sender, EventArgs e)
        {
            btnOceny.Remove((Button)sender);
            MiejsceNaOceny.Children.Remove((Button)sender);
            srednia.UpdateSrednia();
        }

        public int CompareTo(FrameWaga other)
        {
            return Waga.CompareTo(other.Waga);
        }
    }
}
