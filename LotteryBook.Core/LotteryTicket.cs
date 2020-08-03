using System.Windows.Media;

namespace LotteryBook.Model
{
    public class LotteryTicket
    {
        public Brush Color { get; set; }

        public int Number { get; set; }

        public char Letter { get; set; }

        // TODO: Equals...
        public bool AreSame(LotteryTicket ticket)
        {
            return Color.Equals(ticket.Color) && Letter == ticket.Letter && Number == ticket.Number;
        }
    }
}
