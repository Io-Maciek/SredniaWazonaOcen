using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SredniaOcen.Controls
{
    public class OknoWaga : Frame,IComparable<OknoWaga>
    {
        public List<Button> btnOceny = new List<Button>();
        FlexLayout MiejsceNaOceny;
        Entry EntryNowaOcena;
        Button btnDodajNowaOcene;

        double Waga;

        public double Text
        {
            get
            {
                return Waga;
            }
            set
            {
                Waga = value;
                lblWagaWartosc.Text = value.ToString();
            }
        }

        readonly static int ButtonOcenaSize;

        public event EventHandler ClickedAdd
        {
            add
            {
                EntryNowaOcena.Completed += value;
                btnDodajNowaOcene.Clicked += value;
            }
            remove
            {
                EntryNowaOcena.Completed -= value;
                btnDodajNowaOcene.Clicked -= value;
            }
        }

        public readonly Button btnCloseFrame;

        public event EventHandler ClickedClose
        {
            add
            {
                btnCloseFrame.Clicked += value;

            }
            remove
            {
                btnCloseFrame.Clicked -= value;

            }
        }

        public Action AktualizacjaSredniej;



        Label lblWagaWartosc;


        static OknoWaga()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                ButtonOcenaSize = 45;
            }
            else
            {
                ButtonOcenaSize = 37;
            }
        }

        public OknoWaga() : this(Color.FromHex("#3674c9"), 0) { }

        public OknoWaga(double waga) : this(Color.FromHex("#3674c9"), waga) { }


        public OknoWaga(Color bg, double waga)
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
            lblWagaWartosc = new Label();
            lblWaga.Text = "Waga";
            lblWaga.FontSize = 19;
            Grid.SetColumn(lblWaga, 0);

            lblWagaWartosc.Text = waga.ToString();
            lblWagaWartosc.FontSize = 19;
            Grid.SetColumn(lblWagaWartosc, 1);

            g1.Children.Add(lblWaga);
            g1.Children.Add(lblWagaWartosc);

            btnCloseFrame = new Button();
            btnCloseFrame.Text = "x";
            btnCloseFrame.BackgroundColor = Color.Red;
            btnCloseFrame.FontSize = 13;
            btnCloseFrame.Opacity = 0.5f;
            btnCloseFrame.WidthRequest = ButtonOcenaSize;
            btnCloseFrame.HeightRequest = ButtonOcenaSize;
            btnCloseFrame.Padding = new Thickness(0, 0, 0, 5);

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

            EntryNowaOcena = new Entry();
            EntryNowaOcena.Completed += EntryNowaOcena_Completed;
            //entryNowaOcena.FontSize = 19;
            EntryNowaOcena.Keyboard = Keyboard.Numeric;
            EntryNowaOcena.Placeholder = "Ocena";
            Grid.SetColumn(EntryNowaOcena, 1);

            btnDodajNowaOcene = new Button();
            btnDodajNowaOcene.Text = "Dodaj";
            btnDodajNowaOcene.Clicked += BtnDodajNowaOcene_Clicked;
            Grid.SetColumn(btnDodajNowaOcene, 2);
            g2.Children.Add(lblOceny);
            g2.Children.Add(EntryNowaOcena);
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

            
            Content = c;
            BackgroundColor = bg;
            WidthRequest = 250;
            HeightRequest = 250;
            CornerRadius = 20;
            Margin = new Thickness(5);
        }



        /// <summary>
        /// Obsługuje potwierdzenie (np. guzik enter) i wywołuje metodę dodającą nową ocenę
        /// </summary>
        private void EntryNowaOcena_Completed(object sender, EventArgs e)
        {
            BtnDodajNowaOcene_Clicked(btnDodajNowaOcene, e);
        }

        /// <summary>
        /// Dodaje nową ocenę do listy w formie guzika, który można kliknąć i usunąć. Aktualizuje średnią o nową ocenę
        /// </summary>
        private void BtnDodajNowaOcene_Clicked(object sender, EventArgs e)
        {
            if (EntryNowaOcena.Text != null && double.TryParse(EntryNowaOcena.Text, out double d))
            {
                EntryNowaOcena.Text = "";
                Button b = GetBtnOcena(d);
                btnOceny.Add(b);
                MiejsceNaOceny.Children.Add(b);
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
                HeightRequest = ButtonOcenaSize,
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
            AktualizacjaSredniej();
        }

        public int CompareTo(OknoWaga other)
        {
            return Waga.CompareTo(other.Waga);
        }
    }
}
