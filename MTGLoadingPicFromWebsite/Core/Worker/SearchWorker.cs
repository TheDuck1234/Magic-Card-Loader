﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using MTGLoadingPicFromWebsite.Core.Xml;

namespace MTGLoadingPicFromWebsite.Core.Worker
{
    public class SearchWorker
    {
        event EventHandler<EventArgs> Progressed;

        protected virtual void OnProgressed()
        {
            var handler = Progressed;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public List<XmlCard> SearchCards(List<XmlCard> cards,string type,string set,string text)
        {
            var list = new List<XmlCard>();

            if (!type.Equals("All") && !set.Equals("All"))
            {
                foreach (var xmlCard in cards.Where(xmlCard => xmlCard.Set == set && CardCheck(xmlCard, type, text)))
                {
                    list.Add(xmlCard);
                    OnProgressed();
                }
            }

            return list;
        }
        private static bool CardCheck(XmlCard card, string type, string text)
        {
            var check = false;
            switch (type)
            {
                case "All Cards":
                    if (NameCheck(text, card))
                    {
                        check = true;
                    }
                    break;
                case "Creature":
                    {
                        if (card.Type == null && card.Type != "Creature")
                        {
                            return false;
                        }
                        if (NameCheck(text, card))
                        {
                            check = true;
                        }
                    }
                    break;
                case "Land":
                    {
                        if (card.Type == null && card.Type != "Land")
                        {
                            return false;
                        }
                        if (NameCheck(text, card))
                        {
                            check = true;
                        }
                    }
                    break;
            }
            return check;
        }
        private static bool NameCheck(string text,XmlCard xmlCard)
        {
            var check = false;
            if (xmlCard == null) return false;
            var targetText = xmlCard.Name;
            if (targetText.StartsWith(text.ToLower()))
            {
                check = true;
            }
            else if (targetText.Contains(" "))
            {
                var allText = targetText.Split(' ');
                foreach (var s in allText.Where(s => s.StartsWith(text.ToLower())))
                {
                    check = true;
                }
            }
            return check;
        }

    }
}