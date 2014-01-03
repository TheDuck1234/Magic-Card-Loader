using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGLoadingPicFromWebsite.Core.Xml
{
    public class XmlCard
    {
        public string Name { get; set; }
        public string Set { get; set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public string Rarity { get; set; }
        public string Manacost { get; set; }
        public string Ability { get; set; }
        public string Power { get; set; }
        public string Color { get; set; }
        public string Toughness { get; set; }
    }
}
