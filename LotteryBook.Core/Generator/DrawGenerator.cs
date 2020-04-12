using System;
using System.Collections.Generic;

namespace LotteryBook.Model.Generator
{
    public static class DrawGenerator
    {
        private static Random m_Generator = new Random(Environment.TickCount);

        public static Draw DrawLotteryTicket(List<LotteryTicket> tickets)
        {
            var random = new Random();
            int index = m_Generator.Next(0, tickets.Count);
            return new Draw(tickets[index]);
        }
    }
}