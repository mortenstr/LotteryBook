using System;
using System.Xml.Serialization;

namespace LotteryBook.DataAccess.Data
{
    [Serializable]
    [XmlRoot("document")] 
    public class LotteryBookData
    {
        [XmlElement] 
        public string ColorName { get; set; }

        [XmlElement] 
        public char Letter { get; set; }

        [XmlElement] 
        public bool WholeBookSold { get; set; }

        [XmlElement]
        public string TicketsLeftRange { get; set; }
    }
}