using System;
using System.Windows;
using System.Windows.Media.Animation;

namespace Pharmacy
{
    public static class MenuItemAnimations
    {
        public static void Invisible(FrameworkElement element, DependencyProperty property)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                To = 0,
                Duration = TimeSpan.FromSeconds(0.25)
            };
            animation.Completed += (sender, e) =>
            {
                element.Visibility = Visibility.Collapsed;
            };
            element.BeginAnimation(property, animation);
        }
        public static void Visible(FrameworkElement element, DependencyProperty property, float height)
        {
            element.Visibility = Visibility.Visible;
            DoubleAnimation animation = new DoubleAnimation
            {
                To = height,
                Duration = TimeSpan.FromSeconds(0.25)
            };
            element.BeginAnimation(property, animation);
        }
    }
}
