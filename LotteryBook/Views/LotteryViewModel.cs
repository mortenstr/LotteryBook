using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LotteryBook.Model;
using LotteryBook.Program.Util;
using LotteryBook.Program.Views.Draw;
using LotteryBook.Program.Views.Settings;
using Microsoft.Practices.Prism.Commands;

namespace LotteryBook.Program.Views
{
    public class LotteryViewModel : NotifyPropertyChangedBase, IDisposable
    {
        private ICommand m_StartCommand;
        private ICommand m_SettingsCommand;
        private ICommand m_CloseDetailViewCommand;
        private bool m_DrawInProgress;

        // private ICommand m_MakeDrawCommand;
        public event EventHandler DrawTicketStarted;

        public LotteryViewModel()
        {
            LotteryManager = LotteryManager.GetInstance();
        }

        public bool DrawInProgress
        {
            get
            {
                return m_DrawInProgress;
            }

            set
            {
                if (!LotteryManager.Bucket.Any())
                {
                    // Indicate that no draws are longer possible
                    m_DrawInProgress = true;
                }
                else
                {
                    m_DrawInProgress = value;
                }

                OnPropertyChanged("DrawInProgress");
            }
        }

        public LotteryManager LotteryManager { get; private set; }

        public ICommand StartCommand
        {
            get
            {
                if (m_StartCommand == null)
                {
                    m_StartCommand = new DelegateCommand(OnStart);
                }

                return m_StartCommand;
            }
        }

        public ICommand SettingsCommand
        {
            get
            {
                if (m_SettingsCommand == null)
                {
                    m_SettingsCommand = new DelegateCommand(OnSettings);
                }

                return m_SettingsCommand;
            }
        }

        public ICommand CloseDetailViewCommand
        {
            get
            {
                if (m_CloseDetailViewCommand == null)
                {
                    m_CloseDetailViewCommand = new DelegateCommand(OnCloseDetailView);
                }

                return m_CloseDetailViewCommand;
            }
        }

        public FrameworkElement DetailView { get; set; }

        public LotteryTicketsBook SelectedLotteryBook { get; set; }

        public int SoldTickets => LotteryManager.TicketsSold;

        public virtual void OnDrawTicketStarted()
        {
            var handler = DrawTicketStarted;
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void RemoveLotteryBook(LotteryTicketsBook lotteryTicketsBook)
        {
            LotteryManager.RemoveLotteryBook(lotteryTicketsBook);
            Update();
        }

        public void RemoveAllLotteryBooks()
        {
            LotteryManager.RemoveAllLotteryBooks();
            Update();
        }

        public void Update()
        {
            LotteryManager.UpdateBucket();
            OnPropertyChanged("DetailView");
            OnPropertyChanged("SoldTickets");
        }

        private void OnStart()
        {
            if (DetailView == null)
            {
                DetailView = new StartLotteryUC(this);
                Animation.AnimateTo(DetailView, Visibility.Visible, null);
                Update();
            }
        }

        private void OnSettings()
        {
            if (DetailView == null)
            {
                DetailView = new SettingsUC(this);
                Animation.AnimateTo(DetailView, Visibility.Visible, null);
                Update();
            }
        }

        private void OnCloseDetailView()
        {
            Animation.AnimateTo(DetailView, Visibility.Hidden, OnDetailViewClosed);
        }

        private void OnDetailViewClosed()
        {
            DetailView = null;
            Update();
        }

        public void Draw()
        {
            if (DetailView is StartLotteryUC startLotteryUC && DrawInProgress == false)
            {
                startLotteryUC.Draw();
            }
        }

        public void Dispose()
        {
            DetailView = null;
        }
    }
}