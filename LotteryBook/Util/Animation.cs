using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace LotteryBook.Program.Util
{
    public static class Animation
    {
        public static void AnimateTo(FrameworkElement detailView, Visibility visibility, Action onCompleted)
        {
            double fromOpacity = 0;
            double toOpacity = 1;

            if (visibility == Visibility.Hidden)
            {
                fromOpacity = 1;
                toOpacity = 0;
            }

            detailView.Opacity = fromOpacity;

            var sb = new Storyboard();
            var animation = new DoubleAnimation(fromOpacity, toOpacity, new Duration(new TimeSpan(0, 0, 0, 0, 300)));
            Storyboard.SetTarget(animation, detailView);
            Storyboard.SetTargetProperty(animation, new PropertyPath(UIElement.OpacityProperty));
            sb.Children.Add(animation);
            if (onCompleted != null)
            {
                sb.Completed += (sender, args) => onCompleted();
            }

            sb.Begin();
        }         
    }
}