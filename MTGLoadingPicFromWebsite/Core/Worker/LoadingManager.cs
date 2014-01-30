using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Core.Worker
{
    public interface ILoading
    {
        void OnProgressed();
        event EventHandler<EventArgs> Progressed;
        LoadingResult GoLoadingResult();
    }

    public class LoadingResult
    {
        public List<XmlCard> XmlCards { get; set; }
    }
    public static class LoadingManager
    {
        public static ILoading GetLoader(string target)
        {
            if (target.ToLower().Equals("xmlcard"))
            {
                return new XmlCardLoader();
            }
            return null;
        }
    }
}
