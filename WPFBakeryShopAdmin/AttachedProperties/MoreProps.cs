using System;
using System.Windows;
using System.Windows.Controls;

namespace WPFBakeryShopAdmin.AttachedProperties
{
    public class MoreProps
    {
        public static readonly DependencyProperty MarginRightProperty = DependencyProperty.RegisterAttached(
            "MarginRight",
            typeof(string),
            typeof(MoreProps),
            new UIPropertyMetadata(OnMarginRightPropertyChanged));

        public static string GetMarginRight(FrameworkElement element)
        {
            return (string)element.GetValue(MarginRightProperty);
        }

        public static void SetMarginRight(FrameworkElement element, string value)
        {
            element.SetValue(MarginRightProperty, value);
        }

        private static void OnMarginRightPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            var element = obj as FrameworkElement;

            if (element != null)
            {
                int value;
                if (Int32.TryParse((string)args.NewValue, out value))
                {
                    var margin = element.Margin;
                    margin.Top = value/2;
                    margin.Left = value/2;
                    element.Margin = margin;
                }
            }
        }
    }
}
