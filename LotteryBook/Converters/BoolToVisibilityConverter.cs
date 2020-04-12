using System.Windows;

namespace LotteryBook.Program.Converters
{
    public sealed class BoolToVisibilityConverter : BoolConverter<Visibility>
    {
        public BoolToVisibilityConverter()
            : base(Visibility.Visible, Visibility.Collapsed)
        {
        }
    }
}