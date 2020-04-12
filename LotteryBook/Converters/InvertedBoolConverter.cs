namespace LotteryBook.Converters
{
    public sealed class InvertedBoolConverter : BoolConverter<bool>
    {
        public InvertedBoolConverter()
            : base(false, true)
        {
        }
    }
}