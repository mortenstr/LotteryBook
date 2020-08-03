using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using LotteryBook.Model;

namespace LotteryBook.Program.Views
{
    /// <summary>
    /// Interaction logic for the class
    /// </summary>
    public partial class LotteryView : Window
    {
        private static LotteryView m_MainWindow;

        private Rect m_NonFullScreenRect;

        public LotteryView()
        {
            m_MainWindow = this;

            InitializeComponent();

            ViewModel = new LotteryViewModel();
            DataContext = ViewModel;

            KeyUp += MainWindow_KeyUp;

            LoadWindowPositionAndSize();
            Closing += (sender, args) =>
            {
                SaveWindowPositionAndSize();
                ViewModel.Dispose();
                LotteryManager.GetInstance().Save();
            };

            m_NonFullScreenRect = new Rect(new Point(Left, Top), new Size(Width, Height));

            ToggleFullScreen(false);
        }

        public LotteryViewModel ViewModel { get; private set; }

        public static LotteryView GetInstance()
        {
            return m_MainWindow;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            ToggleFullScreen(WindowState == WindowState.Maximized);
            base.OnStateChanged(e);
        }

        private void LoadWindowPositionAndSize()
        {
            var userPreferences = new UserPreferences();

            Height = userPreferences.WindowHeight;
            Width = userPreferences.WindowWidth;
            if (userPreferences.WindowLeft == 0 && userPreferences.WindowTop == 0)
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                Top = userPreferences.WindowTop;
                Left = userPreferences.WindowLeft;
            }
        }

        private void SaveWindowPositionAndSize()
        {
            var userPreferences = new UserPreferences();

            if (WindowState != WindowState.Maximized)
            {
                userPreferences.WindowHeight = (int)Height;
                userPreferences.WindowWidth = (int)Width;
            }

            userPreferences.WindowTop = (int)Top;
            userPreferences.WindowLeft = (int)Left;
            userPreferences.Save();
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                ViewModel.Draw();
            }
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ToggleFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton button)
            {
                ToggleFullScreen(button.IsChecked == true);
            }
        }

        private void ToggleFullScreen(bool fullscreen)
        {
            if (fullscreen)
            {
                m_NonFullScreenRect = new Rect(new Point(Left, Top), new Size(Width, Height));

                Width = SystemParameters.PrimaryScreenWidth + 7;
                Height = SystemParameters.PrimaryScreenHeight;
                Left = -7;
                Top = 0;

                WindowStyle = WindowStyle.None;
                ResizeMode = ResizeMode.NoResize;
            }
            else
            {
                Width = m_NonFullScreenRect.Width;
                Height = m_NonFullScreenRect.Height;
                Left = m_NonFullScreenRect.Left;
                Top = m_NonFullScreenRect.Top;

                WindowStyle = WindowStyle.ThreeDBorderWindow;
                ResizeMode = ResizeMode.CanResize;
            }

            showFullScreenToggleButton.IsChecked = WindowStyle == WindowStyle.None;
        }
    }
}