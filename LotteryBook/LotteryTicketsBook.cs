using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using LotteryBook.Views;

namespace LotteryBook
{
    /// <summary>
    /// Todo: This class should be serialized to XML...
    /// </summary>
    public class LotteryTicketsBook : DependencyObject
    {
        public static readonly DependencyProperty LetterProperty =
            DependencyProperty.Register("Letter", typeof(char), typeof(LotteryTicketsBook), new PropertyMetadata(default(char)));

        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register("Color", typeof(Brush), typeof(LotteryTicketsBook), new PropertyMetadata(default(Brush)));

        public static readonly DependencyProperty WholeBookSoldProperty =
            DependencyProperty.Register("WholeBookSold", typeof(bool), typeof(LotteryTicketsBook), new PropertyMetadata(default(bool), WholeBookSoldCallback));

        public static readonly DependencyProperty TicketsLeftRangeProperty =
            DependencyProperty.Register("TicketsLeftRange", typeof(string), typeof(LotteryTicketsBook), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LotteryTicketsProperty =
            DependencyProperty.Register(
                "LotteryTickets", 
                typeof(List<LotteryTicket>), 
                typeof(LotteryTicketsBook), 
                new PropertyMetadata(default(List<LotteryTicket>)));

        public LotteryTicketsBook(string color, char letter, bool wholeBookSold, string ticketsLeftRange)
        {
            Color = DeckColor.GetColorBrush(color);
            Letter = letter;
            TicketsLeftRange = ticketsLeftRange;
            WholeBookSold = wholeBookSold;
        }

        public List<LotteryTicket> LotteryTickets
        {
            get
            {
                var lotteryTickets = new List<LotteryTicket>();
                for (int i = 1; i <= 100; i++)
                {
                    if (!WholeBookSold && IsEqualOrWithinRange(i))
                    {
                        continue;
                    }

                    lotteryTickets.Add(new LotteryTicket { Color = Color, Letter = Letter, Number = i });
                }

                return lotteryTickets;
            }
        }

        public string TicketsLeftRange
        {
            get
            {
                return (string)GetValue(TicketsLeftRangeProperty);
            }

            set
            {
                SetValue(TicketsLeftRangeProperty, value);
            }
        }

        public bool WholeBookSold
        {
            get
            {
                return (bool)GetValue(WholeBookSoldProperty);
            }

            set
            {
                SetValue(WholeBookSoldProperty, value);
            }
        }

        public Brush Color
        {
            get
            {
                return (Brush)GetValue(ColorProperty);
            }

            set
            {
                SetValue(ColorProperty, value);
            }
        }

        public char Letter
        {
            get
            {
                return (char)GetValue(LetterProperty);
            }

            set
            {
                SetValue(LetterProperty, value);
            }
        }

        public string ColorName
        {
            get
            {
                return DeckColor.GetName(Color);
            }
        }

        public bool IsEqualOrWithinRange(int value)
        {
            if (string.IsNullOrEmpty(TicketsLeftRange))
            {
                return false;
            }

            if (TicketsLeftRange.Equals(value.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            foreach (string item in TicketsLeftRange.Split(','))
            {
                if (item.Equals(value.ToString(CultureInfo.InvariantCulture), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }

                var rangeNumber = item.Split('-');
                if (rangeNumber.Length == 2)
                {
                    int min;
                    int max;

                    int.TryParse(rangeNumber[0], out min);
                    int.TryParse(rangeNumber[1], out max);

                    if (value >= min && value <= max)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static void WholeBookSoldCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            LotteryTicketsBook book = d as LotteryTicketsBook;
            if (book != null && book.WholeBookSold)
            {
                book.TicketsLeftRange = string.Empty;
            }
        }
    }
}