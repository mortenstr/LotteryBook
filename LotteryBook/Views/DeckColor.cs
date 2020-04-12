using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace LotteryBook.Views
{
    public static class DeckColor
    {
        private const string PinkColor   = "#FFC897AA";
        private const string YellowColor = "#FFCCBC66";
        private const string GreenColor  = "#FF98BC84";
        private const string BlueColor   = "#FF83A2D1";
        private const string WhiteColor  = "#FFFAFAFA";

        private static Dictionary<string, string> m_Colors = new Dictionary<string, string>();

        static DeckColor()
        {
            m_Colors.Add(PinkColor, "Rosa");
            m_Colors.Add(YellowColor, "Gul");
            m_Colors.Add(GreenColor, "Grønn");
            m_Colors.Add(BlueColor, "Blå");
            m_Colors.Add(WhiteColor, "Hvit");
        }

        public static List<string> Colors
        {
            get
            {
                return m_Colors.Keys.ToList();
            }
        }

        public static string DefaultColor
        {
            get
            {
                return "Rosa";
            }
        }

        public static string GetName(Brush brush)
        {
            string brushName;
            if (m_Colors.TryGetValue(brush.ToString(), out brushName))
            {
                return brushName;
            }

            return "Unknown";
        }

        public static string GetColorRGB(string colorName)
        {
            foreach (var key in m_Colors.Keys)
            {
                string name;
                if (m_Colors.TryGetValue(key, out name) && name == colorName)
                {
                    return key;
                }
            }

            return "#FFFFFFFF";
        }

        public static Brush GetColorBrush(string colorName)
        {
            return new BrushConverter().ConvertFromString(GetColorRGB(colorName)) as SolidColorBrush;
        }
    }
}