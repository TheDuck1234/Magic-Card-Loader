using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for CardImageWindow.xaml
    /// </summary>
    public partial class CardImageWindow
    {
        private readonly XmlCardManager _cardManager;
        private DateTime _starttime;
        private int _count;

        public CardImageWindow( Window window, int count)
        {
            _count = count;
            InitializeComponent();
            Owner = window;
            _cardManager = new XmlCardManager(XmlCardLoader.CardsNameList());
            ListBox.ItemsSource = _cardManager.ToNames(_cardManager.Cards);
            SetupCombobox();
            STextBox.Text = "Card Name";
            STextBox.TouchEnter += new EventHandler<TouchEventArgs>(Enter);
            STextBox.TouchEnter += new EventHandler<TouchEventArgs>(Leave);
        }

        private void Leave(object sender, EventArgs e)
        {
            if (STextBox.Text != "Card Name" && STextBox.Text == string.Empty)
            {
                STextBox.Text = "Card Name";
            }
        }

        private void Enter(object sender, EventArgs e)
        {
            if (STextBox.Text != string.Empty && STextBox.Text == "Card Name" )
            {
                STextBox.Text = string.Empty;
            }
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
            //SetComboBox.ItemsSource = XmlCardLoader.
            TypeComboBox.ItemsSource = list;
            TypeComboBox.SelectedItem = TypeComboBox.Items[0];
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
            
        }

        private void Search()
        {
            var lists = XmlCardManager.Partition(_cardManager.Cards, 5);

            //Setup for Timer
			_starttime = DateTime.Now;
            var tasks = lists.Select(list => Task.Factory.StartNew(() =>
            {
                _worker = WorkerLoader.Load(info.WorkerType);
                _worker.Progressed += (o, args) =>
                {
                    lock (this)
                    {
                        _count++;
                    }
                    Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart) delegate
                    {
                        // ReSharper disable once RedundantCast
                        var procent = ((100*_count)/_cardManager.Cards.Count);
                        ProgressBar.Value = procent;
                        //ProgressLabel.Content = "Progress: " + procent + "%";
                        //CountLabel.Content = "Total items cleanse: " + count;
                        if (_count > 0)
                        {
                            EstimateTimeLabel.Content = "Estimate Time: " + EstimateTime(excelResults.Count).ToString();
                        }
                    });
                };
            }
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
