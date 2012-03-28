using System;
using Microsoft.Phone.Controls;

namespace Garifzyanov.Toolkit.Application
{
    public static class ApplicationExtensions
    {
         public static void Navigate(this System.Windows.Application application, Uri uri)
         {
             ((PhoneApplicationFrame)application.RootVisual).Navigate(uri);
         }

        public static void Navigate(this System.Windows.Application application, string uri)
        {
            Navigate(application, new Uri(uri, UriKind.Relative));
        }
    }
}