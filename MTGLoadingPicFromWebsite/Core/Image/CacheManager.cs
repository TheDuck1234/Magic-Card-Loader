using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGLoadingPicFromWebsite.Core.Image
{
    public static class CacheManager
    {
        public static bool IsCache(string path, string cardName)
        {
            return File.Exists(path+cardName.ToLower()+".jpg");
        }

        public static void DeleteAllCache(string path)
        {
            var files = Directory.GetFiles(path);

            foreach (var filename in files)
            {
                File.Delete(filename);
            }
        }
    }
}
