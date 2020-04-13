using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace LotteryBook.Model
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

        public LotteryTicketsBook(string color, char letter, string ticketsLeftRange = null)
        {
            Color = DeckColor.GetColorBrush(color);
            Letter = letter;
            TicketsLeftRange = ticketsLeftRange;
            WholeBookSold = string.IsNullOrEmpty(ticketsLeftRange);
        }

        public List<LotteryTicket> LotteryTickets
        {
            get
            {
                var lotteryTickets = new List<LotteryTicket>();
                for (var i = 1; i <= 100; i++)
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
            get => (string)GetValue(TicketsLeftRangeProperty);
            set => SetValue(TicketsLeftRangeProperty, value);
        }

        public bool WholeBookSold
        {
            get => (bool)GetValue(WholeBookSoldProperty);
            set => SetValue(WholeBookSoldProperty, value);
        }

        public Brush Color
        {
            get => (Brush)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public char Letter
        {
            get => (char)GetValue(LetterProperty);
            set => SetValue(LetterProperty, value);
        }

        public string ColorName => DeckColor.GetName(Color);

        private bool IsEqualOrWithinRange(int value)
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
                    int.TryParse(rangeNumber[0], out var min);
                    int.TryParse(rangeNumber[1], out var max);

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
            if (d is LotteryTicketsBook book && book.WholeBookSold)
            {
                book.TicketsLeftRange = string.Empty;
            }
        }
    }
}