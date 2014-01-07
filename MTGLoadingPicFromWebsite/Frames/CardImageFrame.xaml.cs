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
        private XmlCardManager _cardManager;
        private DateTime _starttime;
        private int _count;
        private bool _firstSearch = false;

        public CardImageWindow( Window window)
        {
            InitializeComponent();
            Owner = window;
            //Setup();
            STextBox.Text = "Card Name";
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
            if (!_firstSearch)
            {
                FSearch();
                _firstSearch = true;
            }
        }

        private void Search()
        {
            var lists = XmlCardManager.Partition(_cardManager.Cards, 5);

            //Setup for Timer
            _starttime = DateTime.Now;
            var tasks = lists.Select(list => Task.Factory.StartNew(() =>
            {
                var worker = new SearchWorker();
                worker.Progressed += (o, args) =>
                {
                    lock (this)
                    {
                        _count++;
                    }
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
                    {
                        // ReSharper disable once RedundantCast
                        //var procent = ((100 * _count) / _cardManager.Cards.Count);
                        ProgressBar.Value = 10;
                        //ProgressLabel.Content = "Progress: " + procent + "%";
                        //CountLabel.Content = "Total items cleanse: " + count;
                        if (_count > 0)
                        {
                            //EstimateTimeLabel.Content = "Estimate Time: " + EstimateTime(excelResults.Count).ToString();
                        }
                    });
                };

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
                ListBox.Items.Clear();
                ListBox.ItemsSource = results;

                //Reset();

            });
        }
        private void FSearch()
        {
            var xmlloader = new XmlCardLoader();
            ProgressBar.Visibility = Visibility.Visible;
            
            _cardManager = new XmlCardManager(xmlloader.CardsNameList());
            ListBox.ItemsSource = _cardManager.ToNames(_cardManager.Cards);
            SetupCombobox();

            ProgressBar.Visibility = Visibility.Hidden;
        }

        private TimeSpan EstimateTime(int max)
        {
            var timespent = DateTime.Now - _starttime;
            var secondsremaining = (int)(timespent.TotalSeconds / _count * (max - _count));
            var timespan = TimeSpan.FromSeconds(secondsremaining);
            return timespan;
        }
        private List<XmlCard> SetCards(List<XmlCard> cards)
        {
            var newCards = new List<XmlCard>();

            return newCards;
        }

    }
}
