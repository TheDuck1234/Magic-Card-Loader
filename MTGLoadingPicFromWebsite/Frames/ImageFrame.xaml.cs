using System.Windows;
using MTGLoadingPicFromWebsite.Core;
using MTGLoadingPicFromWebsite.Core.Image;

namespace MTGLoadingPicFromWebsite.Frames
{
    /// <summary>
    /// Interaction logic for ImageFrame.xaml
    /// </summary>
    public partial class ImageFrame
    {
        private string CardName { get; set; }
        public ImageFrame(string cardName)
        {
            InitializeComponent();
            LoadImage(cardName);
        }

        private void LoadImage(string cardName)
        {
            CardName = cardName;
            Window.Title = cardName;
            var bitmap = ImageLoader.LoadImage(CardName);
            Image.Source = bitmap;
            if (CacheManager.IsCache(AppSettings.GetAppSettingImagePath(), CardName))
            {
                DownloadeButton.IsEnabled = false;
            }
        }

        private void DownloadeButton_Click(object sender, RoutedEventArgs e)
        {
            ImageLoader.DownloadeImage(CardName);
            DownloadeButton.IsEnabled = false;
        }
    }
}
