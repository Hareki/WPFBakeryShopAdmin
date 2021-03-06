using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFBakeryShopAdmin.Utilities
{
    public class MessageUtils
    {
        public static void ShowSuccessMessage(UserControl view, TextBlock greenMessage, Snackbar greenSB, StackPanel greenContent, string message)
        {
            view.Dispatcher.Invoke(() =>
            {
                greenMessage.Text = message;

                greenSB.MessageQueue?.Enqueue(
                greenContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        public static void ShowSuccessMessage(Window view, TextBlock greenMessage, Snackbar greenSB, StackPanel greenContent, string message)
        {
            view.Dispatcher.Invoke(() =>
            {
                greenMessage.Text = message;

                greenSB.MessageQueue?.Enqueue(
                greenContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        public static async Task<bool> ShowConfirmMessage(Border dialogContent, TextBlock titleTB, TextBlock messageTB, StackPanel confirmContent
            , StackPanel errorContent, string title, string message)
        {
            confirmContent.Visibility = Visibility.Visible;
            errorContent.Visibility = Visibility.Collapsed;

            titleTB.Text = title;
            messageTB.Text = message;
            var result = await DialogHost.Show(dialogContent);
            return System.Convert.ToBoolean(result);
        }
        public static async Task ShowErrorMessage(Border dialogContent, TextBlock titleTB, TextBlock messageTB, StackPanel confirmContent
            , StackPanel errorContent, string title, string message)
        {
            confirmContent.Visibility = Visibility.Collapsed;
            errorContent.Visibility = Visibility.Visible;
            titleTB.Text = title;
            messageTB.Text = message;
            await DialogHost.Show(dialogContent);
        }
    }
}
