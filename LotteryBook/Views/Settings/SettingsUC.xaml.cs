using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LotteryBook.Views.Settings
{
    /// <summary>
    /// The user control for presenting and editing of the lottery books.
    /// </summary>
    public partial class SettingsUC : UserControl
    {
        private LotteryViewModel m_ViewModel;
        private Grid m_Grid;
        private ProcessLotteryBookUC m_AddLotteryBook;

        public SettingsUC(LotteryViewModel viewModel)
        {
            InitializeComponent();

            m_ViewModel = viewModel;
            DataContext = m_ViewModel;

            Unloaded += (sender, args) => LotteryManager.GetInstance().Save();
        }

        private void RemoveBookButton_Click(object sender, RoutedEventArgs e)
        {
            m_ViewModel.RemoveLotteryBook(lotteryBooksGrid.SelectedItem as LotteryTicketsBook);
        }

        private void RemoveAllBooksButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Er du sikker på at du vil fjerne alle loddbøkene?", "Fjern alle", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No) == MessageBoxResult.Yes)
            {
                m_ViewModel.RemoveAllLotteryBooks();                
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_AddLotteryBook == null)
            {
                var viewModel = new LotteryTicketsBook(DeckColor.DefaultColor, 'A', true, string.Empty);
                m_AddLotteryBook = new ProcessLotteryBookUC(viewModel, CloseAddDialog, Operation.Add);
                ProcessLotteryBook();
            }
        }

        private void EditBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (m_AddLotteryBook == null)
            {
                m_AddLotteryBook = new ProcessLotteryBookUC(m_ViewModel.SelectedLotteryBook, CloseAddDialog, Operation.Edit);
                ProcessLotteryBook();
            }
        }

        private void ProcessLotteryBook()
        {
            m_Grid = new Grid { HorizontalAlignment = HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
            m_Grid.Background = Brushes.Gray;
            m_Grid.Opacity = 0.7;

            m_AddLotteryBook.HorizontalAlignment = HorizontalAlignment.Center;
            m_AddLotteryBook.VerticalAlignment = VerticalAlignment.Center;

            mainGrid.Children.Add(m_Grid);
            mainGrid.Children.Add(m_AddLotteryBook);
        }

        private void CloseAddDialog()
        {
            if (m_Grid != null)
            {
                mainGrid.Children.Remove(m_AddLotteryBook);
                mainGrid.Children.Remove(m_Grid);
                m_ViewModel.Update();
                m_Grid = null;
                m_AddLotteryBook = null;
            }
        }
    }
}