using System.Windows;
using System.Windows.Controls;

namespace LotteryBook.Program.Views.Draw
{
    /// <summary>
    /// The user control for running the drawing of lottery tickets
    /// and presenting the animations regarding this.
    /// </summary>
    public partial class StartLotteryUC : UserControl
    {
        private LotteryViewModel m_ViewModel;

        public StartLotteryUC(LotteryViewModel viewModel)
        {
            InitializeComponent();

            m_ViewModel = viewModel;
            m_ViewModel.DrawInProgress = false;
            DataContext = m_ViewModel;
        }

        public void Draw()
        {
            lotteryTicket.DrawFinished += (o, args) => m_ViewModel.DrawInProgress = false;
            m_ViewModel.DrawInProgress = true;
            lotteryTicket.DrawTicket();
        }

        private void OnDrawClick(object sender, RoutedEventArgs e)
        {
            Draw();
        }
    }
}