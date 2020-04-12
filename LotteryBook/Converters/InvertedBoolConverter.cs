namespace LotteryBook.Program.Converters
{
    public sealed class InvertedBoolConverter : BoolConverter<bool>
    {
        public InvertedBoolConverter()
            : base(false, true)
        {
        }
    }
}