using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SredniaOcen
{
    internal class FrameWaga
    {
        public List<Button> btnOceny = new List<Button>();
        FlexLayout MiejsceNaOceny;
        Entry entryNowaOcena;

        public double Waga;

        Srednia srednia;
        public FrameWaga(Srednia srednia,Color bg, double waga)
        {
            Waga= waga;
            StackLayout c = new StackLayout();
            #region Grid 1
            Grid g1 = new Grid();
            g1.Margin = new Thickness(0, 0, 0, 20);

            ColumnDefinitionCollection two = new ColumnDefinitionCollection();
            two.Add(new ColumnDefinition() { Width = GridLength.Star });
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
            //entryNowaOcena.FontSize = 19;
            entryNowaOcena.Keyboard = Keyboard.Numeric;
            entryNowaOcena.Placeholder = "Ocena";
            Grid.SetColumn(entryNowaOcena, 1);

            Button btnDodajNowaOcene = new Button();
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
            f.CornerRadius=20;
            f.Margin = new Thickness(5);
            MainPage.main.Children.Add(f);
            this.srednia = srednia;
        }

        private void BtnDodajNowaOcene_Clicked(object sender, EventArgs e)
        {
            double d;
            if (double.TryParse(entryNowaOcena.Text, out d))
            {
                entryNowaOcena.Text = "";
                Button b = GetBtnOcena(d);
                btnOceny.Add(b);
                MiejsceNaOceny.Children.Add(b);
                srednia.UpdateSrednia();
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
            MiejsceNaOceny.Children.Remove((Button)sender);
            srednia.UpdateSrednia();
        }
    }
}
