using System.Windows;
using LotteryBook.Model;

namespace LotteryBook.Program.Views.Settings
{
    public class ProcessLotteryBookViewModel : DependencyObject
    {
        public static readonly DependencyProperty DeckColorProperty =
            DependencyProperty.Register("DeckColor", typeof(string), typeof(ProcessLotteryBookViewModel), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty LetterProperty =
            DependencyProperty.Register("Letter", typeof(char), typeof(ProcessLotteryBookViewModel), new PropertyMetadata(default(char)));

        public static readonly DependencyProperty TicketsLeftRangeProperty =
            DependencyProperty.Register("TicketsLeftRange", typeof(string), typeof(ProcessLotteryBookViewModel), new PropertyMetadata(default(string)));

        public static readonly DependencyProperty WholeBookSoldProperty =
            DependencyProperty.Register("WholeBookSold", typeof(bool), typeof(ProcessLotteryBookViewModel), new PropertyMetadata(default(bool)));

        public ProcessLotteryBookViewModel()
        {
            TicketsLeftRange = "3-45";
        }

        public bool WholeBookSold
        {
            get => (bool)GetValue(WholeBookSoldProperty);
            set => SetValue(WholeBookSoldProperty, value);
        }

        public string TicketsLeftRange
        {
            get => (string)GetValue(TicketsLeftRangeProperty);
            set => SetValue(TicketsLeftRangeProperty, value);
        }

        public char Letter
        {
            get => (char)GetValue(LetterProperty);
            set => SetValue(LetterProperty, value);
        }

        public string DeckColor
        {
            get => (string)GetValue(DeckColorProperty);
            set => SetValue(DeckColorProperty, value);
        }

        //public LotteryTicketsBook GetLotteryTicketBook()
        //{
        //    return new LotteryTicketsBook(DeckColor, Letter, WholeBookSold, TicketsLeftRange);
        //}
    }
}