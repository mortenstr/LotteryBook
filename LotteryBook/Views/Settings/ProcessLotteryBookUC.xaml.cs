using System;
using System.Windows;
using System.Windows.Controls;
using LotteryBook.Model;

namespace LotteryBook.Program.Views.Settings
{
    /// <summary>
    /// Interaction logic for ProcessLotteryBookUC.
    /// </summary>
    public partial class ProcessLotteryBookUC : UserControl
    {
        private readonly Action m_Close;
        private readonly Operation m_Operation;
        private LotteryTicketsBook m_ViewModel;

        public ProcessLotteryBookUC(LotteryTicketsBook viewModel, Action close, Operation operation)
        {
            InitializeComponent();

            m_ViewModel = viewModel;
            m_Close = close;
            m_Operation = operation;

            if (operation == Operation.Add)
            {
                title.Text = "Legg til ny loddbok";
            } 
            else if (operation == Operation.Edit)
            {
                title.Text = "Endre loddbok";
            }
            
            DataContext = m_ViewModel;
        }

        private void AddBookOK_Click(object sender, RoutedEventArgs e)
        {
            if (m_Operation == Operation.Add)
            {
                LotteryManager.GetInstance().AddLotteryBook(m_ViewModel);
            }

            Close();
        }

        private void Close()
        {
            if (m_Close != null)
            {
                m_Close();
            }            
        }
    }
}