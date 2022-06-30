using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace WpfApp3
{
    public partial class MainWindow : Window
    {
        Model model = new Model();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
        }

        private void Zeruj(object sender, RoutedEventArgs e)
        {
            model.Zeruj();
        }

        private void Resetuj(object sender, RoutedEventArgs e)
        {
            model.Resetuj();
        }

        private void Cofnij(object sender, RoutedEventArgs e)
        {
            model.Cofnij();
        }

        private void Cyfra(object sender, RoutedEventArgs e)
        {
            model.DopiszCyfre(
                (string)((Button)sender).Content
                );
        }

        private void ZmieńZnak(object sender, RoutedEventArgs e)
        {
            model.ZmieńZnak();
        }

        private void PostawPrzecinek(object sender, RoutedEventArgs e)
        {
            model.PostawPrzecinek();
        }
        private void Dodawanie(object sender, RoutedEventArgs e)
        {
            model.DziałanieZwykłe(
                ((Button)sender).Content.ToString()
                );
        }

        private void Odejmowanie(object sender, RoutedEventArgs e)
        {
            model.DziałanieZwykłe(
                ((Button)sender).Content.ToString()
                );
        }

        private void Mnożenie(object sender, RoutedEventArgs e)
        {
            model.DziałanieZwykłe(
                ((Button)sender).Content.ToString()
                );
        }

        private void Dzielenie(object sender, RoutedEventArgs e)
        {
            model.DziałanieZwykłe(
                ((Button)sender).Content.ToString()
                );
        }

        private void Potęgowanie(object sender, RoutedEventArgs e)
        {
            model.DziałanieJednoargumentowe(
                ((Button)sender).Content.ToString()
                );
        }

        private void Pierwiastkowanie(object sender, RoutedEventArgs e)
        {
            model.DziałanieJednoargumentowe(
                ((Button)sender).Content.ToString()
                );
        }

        private void OdwracanieLiczby(object sender, RoutedEventArgs e)
        {
            model.DziałanieJednoargumentowe(
                ((Button)sender).Content.ToString()
                );
        }

        private void DziałanieProcentowe(object sender, RoutedEventArgs e)
        {
            model.Procent();
        }

        private void PodajWynik(object sender, RoutedEventArgs e)
        {
            model.PodajWynik();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }

    class Model : INotifyPropertyChanged 
    { 
        public event PropertyChangedEventHandler PropertyChanged;

        double 
            liczbaA,
            liczbaB
            ;

        string buforDziałania = null;

        bool 
            flagaUłamka = false,
            flagaDziałania = false
            ;

        string buforIO = "0";
        
        public string IO
        {
            get { return buforIO; }
            set
            {
                buforIO = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IO"));
            }
        }

        public double LiczbaA
        {
            get => liczbaA;
            set
            {
                liczbaA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }

        public double LiczbaB
        { 
            get => liczbaB;
            set
            {
                liczbaB = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }

        public string BuforDziałania
        { 
            get => buforDziałania;
            set
            {
                buforDziałania = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Bufory"));
            }
        }

        public string Bufory
        {
            get 
            {
                if (buforDziałania == null)
                    return "";
                if (flagaDziałania == false)
                    return $"{liczbaA} {buforDziałania}";
                return $"{liczbaA} {buforDziałania} {liczbaB}";
            }
        }

        internal void DziałanieJednoargumentowe(string działanie)
        {
            BuforDziałania = działanie;
            flagaDziałania = true;
            LiczbaA = double.Parse(buforIO);
            IO = WykonajDziałanie().ToString();
        }

        public void DziałanieZwykłe(string znak)
        {
            if(BuforDziałania == null)
            {
                BuforDziałania = znak;
                LiczbaA = double.Parse(buforIO);
                flagaDziałania = true;
            } 
            else
            {
                BuforDziałania = znak;
                flagaDziałania = true;
                LiczbaB = double.Parse(buforIO);
                LiczbaA = WykonajDziałanie();
                IO = LiczbaA.ToString();
            }
        }

        internal void PodajWynik()
        {
            if (flagaDziałania == false)
            {
                LiczbaB = double.Parse(buforIO);
                flagaDziałania = true;
            }
            LiczbaA = WykonajDziałanie();
            IO = LiczbaA.ToString();
        }

        internal double WykonajDziałanie()
        {
            if (BuforDziałania == "+")
                return LiczbaA + LiczbaB;
            else if (BuforDziałania == "-")
                return LiczbaA - LiczbaB;
            else if (BuforDziałania == "*")
                return LiczbaA * LiczbaB;
            else if (BuforDziałania == "/")
                return LiczbaA / LiczbaB;
            else if (BuforDziałania == "x²")
                return liczbaA * liczbaA;
            else if (BuforDziałania == "√")
                return Math.Pow(liczbaA, 0.5);
            else if (BuforDziałania == "1/x")
                return 1.0 / liczbaA;
            else
                return 0;
        }

        internal void Zeruj()
        {
            flagaUłamka = false;
            flagaDziałania = false;
            IO = "0";
        }

        internal void Resetuj()
        {
            Zeruj();
            BuforDziałania = default;
            LiczbaA = default;
            LiczbaB = default;
        }

        internal void Cofnij()
        {
            if (buforIO == "0")
                return;
            else if (
                buforIO == "0,"
                || 
                buforIO == "-0,"
                || 
                ((buforIO)[0] == '-' && buforIO.Length == 2)
                )
                Zeruj();
            else
            {
                char usuwamyZnak = buforIO[buforIO.Length - 1];
                IO = buforIO.Substring(0, buforIO.Length - 1);
                if (buforIO.Length == 0)
                    IO = "0";
                if (usuwamyZnak == ',')
                    flagaUłamka = false;
            }
        }

        internal void DopiszCyfre(string cyfra)
        {
            if (flagaDziałania)
                Zeruj();
            if (buforIO == "0")
                buforIO = "";
            IO += cyfra;
        }

        internal void ZmieńZnak()
        {
            flagaDziałania = false;
            if (buforIO == "0")
                return;
            else if (buforIO[0] == '-')
                IO = buforIO.Substring(1);
            else
                IO = '-' + IO;
        }

        internal void PostawPrzecinek()
        {
            if (flagaDziałania)
                Zeruj();
            if (flagaUłamka || buforIO[buforIO.Length - 1] == ',')
                return;
            else
            {
                IO += ',';
                flagaUłamka = true;
            }
        }

        internal void Procent()
        {
            flagaDziałania = true;
            LiczbaB = double.Parse(buforIO) / 100 * liczbaA;
            PodajWynik();
        }
    }
}
