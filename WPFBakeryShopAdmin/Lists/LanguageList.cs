using System;
using System.Collections.Generic;
using System.Windows;
using WPFBakeryShopAdmin.Models;

namespace WPFBakeryShopAdmin.Lists
{
    public class LanguageList
    {
        public static List<ItemLanguage> LIST { get; set; }
        private static ResourceDictionary _enlishDictionary;
        private static ResourceDictionary _vnDictionary;
        static LanguageList()
        {
            LIST = new List<ItemLanguage>();
            LIST.Add(new ItemLanguage("Tiếng Việt", "/Resources/Images/vn-flag.png", "vi"));
            LIST.Add(new ItemLanguage("English", "/Resources/Images/uk-flag.png", "en"));

            _enlishDictionary = new ResourceDictionary();
            _vnDictionary = new ResourceDictionary();

            _enlishDictionary.Source = new Uri("..\\Resources\\Languages\\StringResources.en.xaml", UriKind.Relative);
            _vnDictionary.Source = new Uri("..\\Resources\\Languages\\StringResources.vn.xaml", UriKind.Relative);
        }


        public static void SwitchLanguage(int selectedIndex)
        {
            Application.Current.Resources.MergedDictionaries.Remove(_enlishDictionary);
            Application.Current.Resources.MergedDictionaries.Remove(_vnDictionary);
            switch (selectedIndex)
            {
                case 0:
                    Application.Current.Resources.MergedDictionaries.Add(_vnDictionary);

                    break;
                case 1:
                    Application.Current.Resources.MergedDictionaries.Add(_enlishDictionary);
                    break;
                default:
                    Application.Current.Resources.MergedDictionaries.Add(_vnDictionary);
                    break;
            }
        }
    }
}
