using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LotteryBook.Views.Settings
{
    public partial class SmallColorPicker : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register(
                "SelectedColor", 
                typeof(Color), 
                typeof(SmallColorPicker), 
                new FrameworkPropertyMetadata(OnSelectedColorChanged));

        // Using a DependencyProperty as the backing store for SelectedBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBrushProperty =
            DependencyProperty.Register(
                "SelectedBrush", 
                typeof(Brush), 
                typeof(SmallColorPicker), 
                new FrameworkPropertyMetadata(OnSelectedBrushChanged));

        public static readonly RoutedEvent ColorChangedEvent =
            EventManager.RegisterRoutedEvent(
                "ColorChanged", 
                RoutingStrategy.Bubble, 
                typeof(RoutedPropertyChangedEventHandler<Color>), 
                typeof(SmallColorPicker));

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add
            {
                AddHandler(ColorChangedEvent, value);
            }

            remove
            {
                RemoveHandler(ColorChangedEvent, value);
            }
        }

        public SmallColorPicker()
        {
            InitializeComponent();
            InitializeColors();
        }

        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }

            set
            {
                SetValue(SelectedColorProperty, value);
            }
        }

        public Brush SelectedBrush
        {
            get
            {
                return (Brush)GetValue(SelectedBrushProperty);
            }

            set
            {
                SetValue(SelectedBrushProperty, value);
            }
        }

        public static Color HsvToColor(double hue, double sat, double val)
        {
            int i;
            double aa, bb, cc, f;
            double r, g, b;
            r = g = b = 0;

            if (sat == 0)
            {
                // Gray scale
                r = g = b = val;
            }
            else
            {
                if (hue == 1.0)
                {
                    hue = 0;
                }

                hue *= 6.0;
                i = (int)Math.Floor(hue);
                f = hue - i;
                aa = val * (1 - sat);
                bb = val * (1 - (sat * f));
                cc = val * (1 - (sat * (1 - f)));
                switch (i)
                {
                    case 0:
                        r = val;
                        g = cc;
                        b = aa;
                        break;
                    case 1:
                        r = bb;
                        g = val;
                        b = aa;
                        break;
                    case 2:
                        r = aa;
                        g = val;
                        b = cc;
                        break;
                    case 3:
                        r = aa;
                        g = bb;
                        b = val;
                        break;
                    case 4:
                        r = cc;
                        g = aa;
                        b = val;
                        break;
                    case 5:
                        r = val;
                        g = aa;
                        b = bb;
                        break;
                }
            }

            return Color.FromRgb((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
        }

        public void InitializeColors()
        {
            Picker.Items.Clear();

            foreach (string color in DeckColor.Colors)
            {
                AddColor((Color)ColorConverter.ConvertFromString(color));                
            }
        }

        protected virtual void OnColorChanged(Color oldValue, Color newValue)
        {
            RoutedPropertyChangedEventArgs<Color> args = new RoutedPropertyChangedEventArgs<Color>(oldValue, newValue);
            args.RoutedEvent = ColorChangedEvent;
            RaiseEvent(args);
        }

        private static void OnSelectedColorChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SmallColorPicker cp = obj as SmallColorPicker;
            Debug.Assert(cp != null);

            Color newColor = (Color)args.NewValue;
            Color oldColor = (Color)args.OldValue;

            if (newColor == oldColor)
            {
                return;
            }

            // When the SelectedColor changes, set the selected value of the combo box
            if (cp.Picker.SelectedValue == null || (Color)cp.Picker.SelectedValue != newColor)
            {
                // Add the color if not found
                if (!cp.Picker.Items.Contains(newColor))
                {
                    cp.Picker.Items.Add(newColor);
                }
            }

            // Also update the brush
            cp.SelectedBrush = new SolidColorBrush(newColor);

            cp.OnColorChanged(oldColor, newColor);
        }

        private static void OnSelectedBrushChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            SmallColorPicker cp = (SmallColorPicker)obj;
            SolidColorBrush newBrush = (SolidColorBrush)args.NewValue;

            // SolidColorBrush oldBrush = (SolidColorBrush)args.OldValue;
            if (cp.SelectedColor != newBrush.Color)
            {
                cp.SelectedColor = newBrush.Color;
            }
        }

        /// <summary>
        /// Add a color to the ColorPicker list
        /// </summary>
        /// <param name="c"></param>
        private void AddColor(Color c)
        {
            Picker.Items.Add(c);
        }
    }
}