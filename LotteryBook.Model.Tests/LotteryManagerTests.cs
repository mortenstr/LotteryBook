using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LotteryBook.Model.Tests
{
    [TestClass]
    public class LotteryManagerTests
    {
        [TestMethod]
        public void ShouldHaveEmptyBucketWhenInitialized()
        {
            var lotteryManager = new LotteryManager();
            Assert.AreEqual(0, lotteryManager.Bucket.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataException))]
        public void ShouldDrawLotteryTicketWithEmptyBucketExceptionThrown()
        {
            var lotteryManager = new LotteryManager();
            lotteryManager.DrawLotteryTicket();
        }

        [TestMethod]
        public void ShouldDrawLotteryTicketWithOneLotteryBookAdded()
        {
            var lotteryManager = new LotteryManager();
            lotteryManager.AddLotteryBook(new LotteryTicketsBook("Blue", 'D'));
            var expectedTicketsSold = lotteryManager.Bucket.Count;
            var expectedBucketCountAfterDraw = lotteryManager.Bucket.Count - 1;

            lotteryManager.DrawLotteryTicket();

            Assert.IsNotNull(lotteryManager.LastTicketDrawn);
            Assert.AreEqual(1, lotteryManager.PreviousDraws.Count);
            Assert.AreEqual(expectedBucketCountAfterDraw, lotteryManager.Bucket.Count);
            Assert.AreEqual(expectedTicketsSold, lotteryManager.TicketsSold);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetLotteryTicketsBookAndExpectedTickets), DynamicDataSourceType.Method)]
        public void ShouldHaveTicketsInBucket(int expectedTickets, List<LotteryTicketsBook> lotteryTicketsBooks)
        {
            var lotteryManager = new LotteryManager();

            foreach (var lotteryTicketsBook in lotteryTicketsBooks)
            {
                lotteryManager.AddLotteryBook(lotteryTicketsBook);
            }

            Assert.AreEqual(expectedTickets, lotteryManager.Bucket.Count);
        }

        public static IEnumerable<object[]> GetLotteryTicketsBookAndExpectedTickets()
        {
            return new List<object[]>
            {
                new object[] {100, new List<LotteryTicketsBook>
                {
                    new LotteryTicketsBook("Red", 'A')
                }},
                new object[] {200, new List<LotteryTicketsBook>
                {
                    new LotteryTicketsBook("Red", 'A'),
                    new LotteryTicketsBook("Green", 'B')
                }},
                new object[] {4, new List<LotteryTicketsBook>
                {
                    new LotteryTicketsBook("Red", 'A', "5-100"),
                }},
                new object[] {185, new List<LotteryTicketsBook>
                {
                    new LotteryTicketsBook("Red", 'A'),
                    new LotteryTicketsBook("Green", 'B', "86-100")
                }},
                new object[] {176, new List<LotteryTicketsBook>
                {
                    new LotteryTicketsBook("Red", 'A', "72-80,90-95"),
                    new LotteryTicketsBook("Green", 'B', "92-100")
                }},
            };
        }
    }
}