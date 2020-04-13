using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LotteryBook.Model.Generator
{
    public static class DrawGenerator
    {
        private static readonly Random m_Generator = new Random(Environment.TickCount);

        public static Draw DrawLotteryTicket(List<LotteryTicket> tickets)
        {
            if (!tickets.Any())
            {
                throw new InvalidDataException("No tickets in the bucket. Not possible to draw ticket.");
            }
            
            var random = new Random();
            var index = m_Generator.Next(0, tickets.Count);
            return new Draw(tickets[index]);
        }
    }
}