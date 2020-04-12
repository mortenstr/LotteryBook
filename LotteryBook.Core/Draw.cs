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

        public LotteryTicket Ticket { get; private set; }

        public DateTime Time { get; private set; }
    }
}