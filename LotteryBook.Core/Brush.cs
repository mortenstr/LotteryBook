namespace LotteryBook.Model
{
    public class BrushElement
    {
        public BrushElement(string name, string rgb)
        {
            Name = name;
            RGB = rgb;
        }

        public string Name { get; }

        public string RGB { get; }
    }
}