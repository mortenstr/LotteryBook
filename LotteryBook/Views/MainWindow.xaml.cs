using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LotteryBook.Views
{
    /// <summary>
    /// Interaction logic for the class
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow m_MainWindow;

        public MainWindow()
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

            ToggleFullScreen(false);
        }

        public LotteryViewModel ViewModel { get; private set; }

        public static MainWindow GetInstance()
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
            ToggleButton button = sender as ToggleButton;
            if (button != null)
            {
                ToggleFullScreen(button.IsChecked == true);
            }
        }

        private void ToggleFullScreen(bool fullscreen)
        {
            if (fullscreen)
            {
                WindowState = WindowState.Maximized;
                WindowStyle = WindowStyle.None;
            }
            else
            {
                WindowState = WindowState.Normal;
                WindowStyle = WindowStyle.ThreeDBorderWindow;
            }

            showFullScreenToggleButton.IsChecked = WindowStyle == WindowStyle.None;
        }
    }
}