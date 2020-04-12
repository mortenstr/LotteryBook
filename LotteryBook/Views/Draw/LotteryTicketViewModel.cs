using System.Windows;

namespace LotteryBook.Views.Draw
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