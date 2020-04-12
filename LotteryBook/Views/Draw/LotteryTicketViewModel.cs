using System.Windows;
using LotteryBook.Model;

namespace LotteryBook.Program.Views.Draw
{
    public class LotteryTicketViewModel : DependencyObject
    {
        public LotteryTicketViewModel()
        {
            LotteryManager = LotteryManager.GetInstance();
        }

        public LotteryManager LotteryManager { get; private set; }

    }
}