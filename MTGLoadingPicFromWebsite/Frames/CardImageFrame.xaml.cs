using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MTGLoadingPicFromWebsite.Core.Worker;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for CardImageWindow.xaml
    /// </summary>
    public partial class CardImageWindow
    {
        private bool f = true;
        private XmlCardManager _cardManager;
        private DateTime _starttime;
        private int _count;
        private bool _firstSearch = false;

        public CardImageWindow( Window window)
        {
            InitializeComponent();
            Owner = window;
            Search();
            STextBox.Text = "Card Name";
            f = false;
        }


        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var imageFrame = new ImageFrame(ListBox.SelectedItem.ToString());
            imageFrame.Show();
        }

        private void SetupCombobox()
        {
            var list = new List<string>
            {
                "All Cards",
                "Creature",
                "Land",
                "Enchantment",
                "Sorcery"
            };
            TypeComboBox.ItemsSource = list;
            TypeComboBox.SelectedIndex = 0;

            SetComboBox.ItemsSource = XmlCardLoader.GetAllSetList();
            SetComboBox.SelectedIndex = 0;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Owner.Show();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            FirstSearch();
            //test button now 
        }

        private void Search()
        {
            //var lists = XmlCardManager.Partition(_cardManager.Cards, 5);
            var result = LoadingFrame.Load(this.Owner, "xmlcard");
            _cardManager = new XmlCardManager(result.XmlCards);
            ListBox.ItemsSource = _cardManager.ToNames(_cardManager.Cards);
            SetupCombobox();
        }
        private void FirstSearch()
        {
            var xmlloader = new XmlCardLoader();
            ProgressBar.Visibility = Visibility.Visible;
            xmlloader.Progressed += (o, args) =>
            {
                lock (this)
                {
                    _count++;
                }
                Console.WriteLine(_count);
            };
            _cardManager = new XmlCardManager(xmlloader.CardsNameList());
            ListBox.ItemsSource = _cardManager.ToNames(_cardManager.Cards);
            SetupCombobox();

            ProgressBar.Visibility = Visibility.Hidden;
        }

        private List<XmlCard> SetCards(List<XmlCard> cards)
        {
            var newCards = new List<XmlCard>();

            return newCards;
        }

    }
}
