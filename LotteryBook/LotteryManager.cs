using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Xml;
using System.Xml.Serialization;
using LotteryBook.Data;
using LotteryBook.Generator;
using LotteryBook.Views.Settings;

namespace LotteryBook
{
    public class LotteryManager : NotifyPropertyChangedBase
    {
        private const string StoredLotteryBooksFile = @"SavedLotteryBooks.xml";

        private static LotteryManager m_Instance;
        private LotteryTicket m_LastTicketDrawn;

        private LotteryManager()
        {
            LotteryBooks = new ObservableCollection<LotteryTicketsBook>();
            PreviousDraws = new List<Draw>();
            Bucket = new List<LotteryTicket>();
            Settings = new LotterySettings();
            Load();
        }

        public ObservableCollection<LotteryTicketsBook> LotteryBooks { get; private set; }

        public LotterySettings Settings { get; set; }

        public List<LotteryBookData> LotteryBooksData
        {
            get
            {
                var lotteryBookDataList = new List<LotteryBookData>();
                foreach (var lotteryBook in LotteryBooks)
                {
                    var data = new LotteryBookData()
                    {
                        ColorName = lotteryBook.ColorName, 
                        Letter = lotteryBook.Letter,
                        TicketsLeftRange = lotteryBook.TicketsLeftRange, 
                        WholeBookSold = lotteryBook.WholeBookSold
                    };

                    lotteryBookDataList.Add(data);
                }

                return lotteryBookDataList;
            }
        }

        public List<Draw> PreviousDraws { get; private set; }

        public int TicketsSold
        {
            get
            {
                return LotteryBooks.Sum(lotteryTicketsBook => lotteryTicketsBook.LotteryTickets.Count);
            }
        }

        public List<LotteryTicket> Bucket { get; private set; }

        public bool BucketContainTickets
        {
            get
            {
                return Bucket.Count > 0;
            }
        }

        public LotteryTicket LastTicketDrawn
        {
            get
            {
                return m_LastTicketDrawn;
            }

            set
            {
                m_LastTicketDrawn = value;
                OnPropertyChanged("LastTicketDrawn");
            }
        }

        public static LotteryManager GetInstance()
        {
            if (m_Instance == null)
            {
                m_Instance = new LotteryManager();
            }

            return m_Instance;
        }

        public void DrawLotteryTicket()
        {
            var draw = DrawGenerator.DrawLotteryTicket(Bucket);
            PreviousDraws.Add(draw);
            Bucket.Remove(draw.Ticket);

            LastTicketDrawn = draw.Ticket;
            OnPropertyChanged("BucketContainTickets");
        }

        public void AddLotteryBook(LotteryTicketsBook ticketsBook)
        {
            LotteryBooks.Add(ticketsBook);
            FillBucket();
        }

        public void RemoveLotteryBook(LotteryTicketsBook ticketsBook)
        {
            LotteryBooks.Remove(ticketsBook);
            FillBucket();
        }

        public void RemoveAllLotteryBooks()
        {
            LotteryBooks.Clear();
            FillBucket();
        }

        public void FillBucket()
        {
            EmptyBucket();

            foreach (var lotteryBook in LotteryBooks)
            {
                Bucket.AddRange(lotteryBook.LotteryTickets);
            }

            foreach (var previousDraw in PreviousDraws)
            {
                Bucket.RemoveAll(x => x.AreSame(previousDraw.Ticket));
            }

            Bucket.Shuffle();
        }

        public void Reset()
        {
            EmptyBucket();
            PreviousDraws.Clear();
        }

        public void UpdateBucket()
        {
            EmptyBucket();
            FillBucket();
        }

        private void EmptyBucket()
        {
            Bucket.Clear();
        }

        private void Load()
        {
            if (File.Exists(StoredLotteryBooksFile) == false)
            {
                return;
            }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(StoredLotteryBooksFile);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(List<LotteryBookData>);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        var lotteryBooksDataList = (List<LotteryBookData>)serializer.Deserialize(reader);
                        foreach (var bookData in lotteryBooksDataList)
                        {
                            AddLotteryBook(new LotteryTicketsBook(bookData.ColorName, bookData.Letter, bookData.WholeBookSold, bookData.TicketsLeftRange));
                        }

                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception exception)
            {
                string message = string.Format(
                    "{0}\r\rSannsynligvis formatendring på fila '{1}'. Slett den, og start på nytt.", 
                    exception.Message, 
                    StoredLotteryBooksFile);
                MessageBox.Show(message, "Lesing av lagret data");
            }
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(List<LotteryBookData>));

            StreamWriter file = new StreamWriter(StoredLotteryBooksFile);
            serializer.Serialize(file, LotteryBooksData);
            file.Close();
        }
    }
}