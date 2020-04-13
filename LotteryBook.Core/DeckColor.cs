using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace LotteryBook.Model
{
    public static class DeckColor
    {
        private static List<BrushElement> m_Brushes = new List<BrushElement> 
            { 
                new BrushElement("Rosa", "#FFC897AA"),
                new BrushElement("Gul", "#FFCCBC66"),
                new BrushElement("Grønn", "#FF98BC84"),
                new BrushElement("Blå", "#FF83A2D1"),
                new BrushElement("Hvit", "#FFFAFAFA")
            };

        public static IEnumerable<BrushElement> Colors => m_Brushes.ToList();

        public static string DefaultColor => m_Brushes[0].Name;

        public static string GetName(Brush brush)
        {
            var brushName = m_Brushes.FirstOrDefault(x => x.RGB == brush.ToString())?.Name;
            if (string.IsNullOrEmpty(brushName) == false)
            {
                return brushName;
            }

            return "Unknown";
        }

        public static string GetColorRGB(string colorName)
        {
            var brush = m_Brushes.FirstOrDefault(x => x.Name == colorName);
            if (brush != null)
            {
                return brush.RGB;
            }

            return "#FFFFFFFF";
        }

        public static Brush GetColorBrush(string colorName)
        {
            return new BrushConverter().ConvertFromString(GetColorRGB(colorName)) as SolidColorBrush;
        }
    }
}