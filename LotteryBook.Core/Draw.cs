using System;

namespace LotteryBook.Model
{
    public class Draw
    {
        public Draw(LotteryTicket ticket)
        {
            Ticket = ticket;
            Time = DateTime.Now;
        }

        public LotteryTicket Ticket { get; }

        public DateTime Time { get; }
    }
}