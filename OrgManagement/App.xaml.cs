using System;
using System.Globalization;
using System.Windows;
using System.Windows.Markup;

namespace OrgManagement
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var lang = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);
            var metadata = new FrameworkPropertyMetadata(lang);
            FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), metadata);
        }
    }
}
