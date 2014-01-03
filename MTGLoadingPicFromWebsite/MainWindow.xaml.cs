using System.Windows;
using MTGLoadingPicFromWebsite.Frames;

namespace MTGLoadingPicFromWebsite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CardsButton_Click(object sender, RoutedEventArgs e)
        {
            var cardImageWindow = new CardImageWindow(this);
            cardImageWindow.Show();
            this.Hide();
        }

        private void DeckButton_Click(object sender, RoutedEventArgs e)
        {
            var deck = new DeckFrame(this);
            deck.Show();
            Hide();
        }
    }
}
