using System.Windows;
using LotteryBook.Program.Properties;

namespace LotteryBook.Program
{
    public class UserPreferences
    {
        public UserPreferences()
        {
            // Load the settings
            Load();

            // Size it to fit the current screen
            SizeToFit();

            // Move the window at least partially into view
            MoveIntoView();
        }

        public int WindowTop { get; set; }

        public int WindowLeft { get; set; }

        public int WindowHeight { get; set; }

        public int WindowWidth { get; set; }

        public WindowState WindowState { get; set; }

        public void Save()
        {
            if (WindowState != WindowState.Minimized)
            {
                Settings.Default.WindowTop = WindowTop;
                Settings.Default.WindowLeft = WindowLeft;
                Settings.Default.WindowHeight = WindowHeight;
                Settings.Default.WindowWidth = WindowWidth;

                Settings.Default.Save();
            }
        }

        public void SizeToFit()
        {
            if (WindowHeight > SystemParameters.VirtualScreenHeight)
            {
                WindowHeight = (int) SystemParameters.VirtualScreenHeight;
            }

            if (WindowWidth > SystemParameters.VirtualScreenWidth)
            {
                WindowWidth = (int) SystemParameters.VirtualScreenWidth;
            }
        }

        public void MoveIntoView()
        {
            if (WindowTop + WindowHeight / 2 > SystemParameters.VirtualScreenHeight)
            {
                WindowTop = (int) SystemParameters.VirtualScreenHeight - WindowHeight;
            }

            if (WindowLeft + WindowWidth / 2 > SystemParameters.VirtualScreenWidth)
            {
                WindowLeft = (int) SystemParameters.VirtualScreenWidth - WindowWidth;
            }

            if (WindowTop < 0)
            {
                WindowTop = 0;
            }

            if (WindowLeft < 0)
            {
                WindowLeft = 0;
            }
        }

        private void Load()
        {
            WindowTop = Settings.Default.WindowTop;
            WindowLeft = Settings.Default.WindowLeft;
            WindowHeight = Settings.Default.WindowHeight;
            WindowWidth = Settings.Default.WindowWidth;
        }
    }
}