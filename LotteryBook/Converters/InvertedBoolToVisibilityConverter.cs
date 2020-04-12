using System.Windows;

namespace LotteryBook.Converters
{
    public sealed class InvertedBoolToVisibilityConverter : BoolConverter<Visibility>
    {
        public InvertedBoolToVisibilityConverter()
            : base(Visibility.Collapsed, Visibility.Visible)
        {
        }
    }
}