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
        private int _count;
        private XmlCardManager _cardManager;

        public CardImageWindow( Window window)
        {
            InitializeComponent();
            Owner = window;
            LoadingXmL();
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
            Search2();
            //test button now 
        }

        private void LoadingXmL()
        {
            var result = LoadingFrame.Load(this.Owner, "xmlcard");
            _cardManager = new XmlCardManager(result.XmlCards);
            ListBox.ItemsSource = _cardManager.ToNames(_cardManager.Cards);
            SetupCombobox();
        }
        private void Search()
        {
            ProgressBar.Visibility = Visibility.Visible;
            var lists = XmlCardManager.Partition(_cardManager.Cards, 5);

            var tasks = lists.Select(list => Task.Factory.StartNew(() =>
            {
                var worker = new SearchWorker();
                var result = worker.SearchCards(list.ToList(), TypeComboBox.SelectedItem.ToString(),
                    SetComboBox.SelectedItem.ToString(), STextBox.Text);
                return result;
            })).ToList();
            Task.Factory.StartNew(() =>
            {   
                var results = new List<XmlCard>();
                // Wait till all tasks completed

                foreach (var result in tasks.Select(task => task.Result))
                {
                    results.AddRange(result.ToList());
                }

                ListBox.ItemsSource = results;

            });


            ProgressBar.Visibility = Visibility.Hidden;
        }

        private void Search2()
        {
            ProgressBar.Visibility = Visibility.Visible;

            var worker = new SearchWorker();
            var result = worker.SearchCards(_cardManager.Cards, TypeComboBox.SelectedItem.ToString(),
                SetComboBox.SelectedItem.ToString(), STextBox.Text);
            ListBox.ItemsSource = result.Select(xmlCard => xmlCard.Name).ToList();
            ProgressBar.Visibility = Visibility.Hidden;
        }

        private List<XmlCard> SetCards(List<XmlCard> cards)
        {
            var newCards = new List<XmlCard>();

            return newCards;
        }

    }
}
