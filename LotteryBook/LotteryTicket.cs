using System.Windows.Media;
using LotteryBook.Views;

namespace LotteryBook
{
    public class LotteryTicket
    {
        public Brush Color { get; set; }

        public string ColorName
        {
            get
            {
                return DeckColor.GetName(Color);
            }
        }

        public int Number { get; set; }

        public char Letter { get; set; }

        public bool AreSame(LotteryTicket ticket)
        {
            if (Color.Equals(ticket.Color) && Letter == ticket.Letter && Number == ticket.Number)
            {
                return true;
            }

            return false;
        }
    }
}
