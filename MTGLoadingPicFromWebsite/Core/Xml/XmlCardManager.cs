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
        public static List<T>[] Partition<T>(List<T> list, int totalPartitions)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            if (totalPartitions < 1)
                throw new ArgumentOutOfRangeException("totalPartitions");

            var partitions = new List<T>[totalPartitions];

            var maxSize = (int)Math.Ceiling(list.Count / (double)totalPartitions);
            var k = 0;

            for (var i = 0; i < partitions.Length; i++)
            {
                partitions[i] = new List<T>();
                for (var j = k; j < k + maxSize; j++)
                {
                    if (j >= list.Count)
                        break;
                    partitions[i].Add(list[j]);
                }
                k += maxSize;
            }

            return partitions;
        }
    }
}
