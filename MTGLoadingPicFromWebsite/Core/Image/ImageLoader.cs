using System;
using System.IO;
using System.Windows.Media.Imaging;
using MTGLoadingPicFromWebsite.Core.Website;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Core.Image
{
    public static class ImageLoader
    {
        public static bool DownloadeImage(string cardName)
        {
            var card = XmlCardLoader.GetCard(cardName, "name");
            if (card == null) return false;
            var url = AppSettings.GetAppSettingCardSite() + card.Id + "&type=card";
                
            var fileName = AppSettings.GetAppSettingImagePath() + cardName.ToLower() + ".jpg";
            WizardsImageHandler.DownloadRemoteImageFile(url, fileName);
            return true;
        }

        public static BitmapImage LoadImage(string cardName)
        {
            BitmapImage image = null;

            if (CacheManager.IsCache(AppSettings.GetAppSettingImagePath(),cardName))
            {
                var cardname = cardName.ToLower() + ".jpg";
                var path = new FileInfo(cardname).Directory;
                image = new BitmapImage(new Uri(path + "/CardsCache/" + cardname));
            }
            if (image != null) return image;

            var card = XmlCardLoader.GetCard(cardName, "name");
            if (card != null)
            {
                image =
                    new BitmapImage(
                        new Uri(AppSettings.GetAppSettingCardSite() + card.Id +
                                "&type=card"));
            }

            return image;
        }

        public static bool IsExistsCard(string cardName)
        {
            var card = XmlCardLoader.GetCard(cardName, "name");
            return card != null;
        }
    }
}
