using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for CardImageWindow.xaml
    /// </summary>
    public partial class CardImageWindow
    {
        private readonly XmlCardManager _cardManager;
        public CardImageWindow( Window window)
        {
            InitializeComponent();
            Owner = window;
            _cardManager = new XmlCardManager(XmlCardLoader.CardsNameList());
           // ListBox.ItemsSource = _cardManager.ToNames(_cardManager.Cards);
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
            TypeComboBox.ItemsSource = list;
            TypeComboBox.SelectedItem = TypeComboBox.Items[0];
        }

        private void STextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            //ListBox.ItemsSource = _cardManager.ToNames(
                //XmlCardLoader.CardSearch(STextBox.Text, TypeComboBox.SelectedItem.ToString()));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Owner.Show();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            Owner.Close();
        }
    }
}
