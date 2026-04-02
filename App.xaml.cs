using System.Windows;
using IronBarCode;
using Microsoft.Win32;

namespace QRCodeToolbox;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        IronBarCode.License.LicenseKey = "IRONSUITE.ALBERTASSAAD.GMAIL.COM.25628-F514C20B77-BTJX6-TMDTDO5NJ54D-PC7Z64JKSJAX-TTHSIXSYX2MD-MXB7HPEKDZ7B-RGJ7FEPS63D3-V5O5UXAQCWWB-DC7CBH-TCE4MRXFHZKQUA-DEPLOYMENT.TRIAL-F3HL76.TRIAL.EXPIRES.27.MAR.2026";

        base.OnStartup(e);

        ApplyTheme(IsSystemDarkMode());
        SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
    }

    protected override void OnExit(ExitEventArgs e)
    {
        SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
        base.OnExit(e);
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.General)
        {
            Dispatcher.Invoke(() => ApplyTheme(IsSystemDarkMode()));
        }
    }

    private static bool IsSystemDarkMode()
    {
        object? value = Registry.GetValue(
            @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize",
            "AppsUseLightTheme",
            1);
        return value is int i && i == 0;
    }

    private void ApplyTheme(bool isDark)
    {
        string source = isDark ? "Themes/Dark.xaml" : "Themes/Light.xaml";
        var dict = Resources.MergedDictionaries[0];
        dict.Source = new Uri(source, UriKind.Relative);
    }
}
