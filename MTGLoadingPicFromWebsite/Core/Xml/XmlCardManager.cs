using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGLoadingPicFromWebsite.Core.Xml
{
    public class XmlCardManager
    {
        public List<XmlCard> Cards { get; private set; }

        public XmlCardManager(List<XmlCard> xmlCards)
        {
            Cards = xmlCards;
        }

        public List<XmlCard> FindCardsBySet(string set)
        {
            return Cards.Where(card => String.Equals(card.Set.ToLower(), set.ToLower(), StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        public List<String> ToNames(List<XmlCard> cards )
        {
            return cards.Select(card => card.Name).ToList();
        }
    }
}
