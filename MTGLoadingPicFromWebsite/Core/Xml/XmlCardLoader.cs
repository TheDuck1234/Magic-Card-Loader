using System;
using System.Collections.Generic;
using System.Xml;

namespace MTGLoadingPicFromWebsite.Core.Xml
{
    public class XmlCardLoader
    {
        private static readonly string Path = System.IO.Path.Combine(AppSettings.GetAppSettingXmlPath(), AppSettings.GetAppSettingFile());
        public event EventHandler<EventArgs> Progressed;

        public virtual void OnProgressed()
        {
            var handler = Progressed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }
        public List<XmlCard> CardsNameList()
        {
            var cardsList = new List<XmlCard>();
            var xDoc = new XmlDocument();
            xDoc.Load(Path);

            var cards = xDoc.GetElementsByTagName("card");
            for (int i = 0; i < cards.Count; i++)
            {
                var xmlElement = cards[i]["name"];
                if (xmlElement != null)
                {
                    OnProgressed();
                    cardsList.Add(MakeXmlCard(cards[i]));
                }
            }
            return cardsList;
        }

        public static XmlCard GetCardByName(string name)
        {
            XmlCard xmlCard = null;
            var xDoc = new XmlDocument();
            xDoc.Load(Path);
            var cards = xDoc.GetElementsByTagName("card");
            var found = false;
            var count = 0;
            while(!found && count < cards.Count ){
                var xmlElement = cards[count]["name"];
                if (xmlElement != null && xmlElement.InnerText == name)
                {
                    found = true;
                    xmlCard = MakeXmlCard(cards[count]);
                }
                count++;
            }
            return xmlCard;
        }

        public static XmlCard GetCard(string card,string from)
        {
            XmlCard xmlCard = null;
            var xDoc = new XmlDocument();
            xDoc.Load(Path);
            var cards = xDoc.GetElementsByTagName("card");
            var found = false;
            var count = 0;
            while (!found && count < cards.Count)
            {
                var xmlElement = cards[count][from];
                if (xmlElement != null && String.Equals(xmlElement.InnerText, card, StringComparison.CurrentCultureIgnoreCase))
                {
                    found = true;
                    xmlCard = MakeXmlCard(cards[count]);
                }
                count++;
            }
            return xmlCard;
        }

        public static List<XmlCard> CardSearch(string text, string target)
        {
            var xmlCard = new List<XmlCard>();
            var xDoc = new XmlDocument();
            xDoc.Load(Path);
            var cards = xDoc.GetElementsByTagName("card");
            for(var x = 0; x < cards.Count;x++)
            {
                var xmlElement = cards[x];

                if (xmlElement != null)
                {
                    if (CardCheck(text, target, xmlElement))
                    {
                        xmlCard.Add(MakeXmlCard(xmlElement));
                    }
                }
            }
            return xmlCard;
        }

        private static bool CardCheck(string text, string target, XmlNode xmlElement)
        {
            var check = false;
            switch (target)
            {
                case "All Cards":
                    if (NameCheck(text, xmlElement))
                    {
                        check = true;
                    }
                    break;
                case "Creature":
                    {
                        var element = xmlElement["type"];
                        if (element == null)
                        {
                            return false;
                        }
                        var targetElement = CutTrash(element.InnerText);
                        if (!targetElement.StartsWith("Creature")) return false;
                        if (NameCheck(text, xmlElement))
                        {
                            check = true;
                        }
                    }
                    break;
                case "Land":
                    {
                        var element = xmlElement["type"];
                        if (element == null)
                        {
                            return false;
                        }
                        var targetElement = CutTrash(element.InnerText);
                        if (targetElement.StartsWith("Basic Land") || targetElement.StartsWith("Land"))
                        {
                            if (NameCheck(text, xmlElement))
                            {
                                check = true;
                            }
                        }
                    }
                    break;
            }
            return check;
        }

        public static List<string> GetAllSetList()
        {
            var setlist = new List<string>();
            setlist.Add("All Sets");
            var xDoc = new XmlDocument();
            xDoc.Load(Path);

            var cards = xDoc.GetElementsByTagName("set");
            for (int i = 0; i < cards.Count; i++)
            {
                var xmlElement = cards[i]["name"];
                if (xmlElement != null)
                {
                    setlist.Add(xmlElement.InnerText);
                }
            }
            return setlist;
        } 

        private static bool NameCheck(string text, XmlNode targetElement)
        {
            bool check = false;
            var xmlElement = targetElement["name"];
            if (xmlElement != null)
            {
                var targetText = CutTrash(xmlElement.InnerText).ToLower();
                if (targetText.StartsWith(text.ToLower()))
                {
                    check = true;
                }
                else if (targetText.Contains(" "))
                {
                    var allText = targetText.Split(' ');
                    foreach (var s in allText)
                    {
                        if (s.StartsWith(text.ToLower()))
                        {
                            check = true;
                        }   
                    }
                }
            }
            return check;
        }

        private static XmlCard MakeXmlCard(XmlNode xmlNode)
        {
            var xmlcard = new XmlCard();

            var xmlElement = xmlNode["name"];
            if (xmlElement != null) xmlcard.Name = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["id"];
            if (xmlElement != null) xmlcard.Id = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["set"];
            if (xmlElement != null) xmlcard.Set = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["type"];
            if (xmlElement != null) xmlcard.Type = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["rarity"];
            if (xmlElement != null) xmlcard.Rarity = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["manacost"];
            if (xmlElement != null) xmlcard.Manacost = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["color"];
            if (xmlElement != null) xmlcard.Color = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["power"];
            if (xmlElement != null) xmlcard.Power = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["toughness"];
            if (xmlElement != null) xmlcard.Toughness = CutTrash(xmlElement.InnerXml);
            xmlElement = xmlNode["ability"];
            if (xmlElement != null) xmlcard.Ability = CutTrash(xmlElement.InnerXml);

            return xmlcard;
        }

        private static string CutTrash(string text)
        {
            var newText = text;
            if (!newText.StartsWith("<!")) return newText;
            newText = newText.Substring(9);
            var cut = newText.IndexOf("]", StringComparison.Ordinal);
            newText = newText.Substring(0, cut);
            return newText;
        }
    }
}
