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
using System.Windows.Shapes;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for Deckframexaml.xaml
    /// </summary>
    public partial class DeckFrame : Window
    {
        public DeckFrame(Window window)
        {
            this.Owner = window;
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
