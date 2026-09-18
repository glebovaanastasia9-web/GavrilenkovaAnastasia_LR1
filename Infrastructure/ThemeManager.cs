using System.Windows;
using System.Windows.Media;

namespace Calculator.Infrastructure
{
    /// <summary>
    /// Сквозная инфраструктурная логика переключения цветовых тем.
    /// Раньше находилась в App.xaml.cs (точка входа приложения), хотя
    /// не относится к его прямой обязанности — инициализации приложения.
    /// </summary>
    public static class ThemeManager
    {
        public static void ChangeTheme(Application app, int themeIndex)
        {
            var converter = new BrushConverter();

            if (themeIndex == 1)
            {
                app.Resources["ColorBg"] = (SolidColorBrush)converter.ConvertFrom("#B8C7D9");
                app.Resources["ColorMainText"] = (SolidColorBrush)converter.ConvertFrom("#1B2638");
                app.Resources["ColorHistText"] = (SolidColorBrush)converter.ConvertFrom("#6B7A90");
                app.Resources["ColorNumBtn"] = (SolidColorBrush)converter.ConvertFrom("#D7E1EC");
                app.Resources["ColorEqBtn"] = (SolidColorBrush)converter.ConvertFrom("#00AEEF");
                app.Resources["ColorActBtn"] = (SolidColorBrush)converter.ConvertFrom("#F8FAFC");
                app.Resources["ColorNumText"] = (SolidColorBrush)converter.ConvertFrom("#1B2638");
                app.Resources["ColorActText"] = (SolidColorBrush)converter.ConvertFrom("#3B82F6");
                app.Resources["ColorEqText"] = (SolidColorBrush)converter.ConvertFrom("#FFFFFF");
                app.Resources["ColorBorder"] = (SolidColorBrush)converter.ConvertFrom("#B8C7D9");
            }
            else if (themeIndex == 2)
            {
                app.Resources["ColorBg"] = (SolidColorBrush)converter.ConvertFrom("#1A0B2E");
                app.Resources["ColorMainText"] = (SolidColorBrush)converter.ConvertFrom("#FFE6F1");
                app.Resources["ColorHistText"] = (SolidColorBrush)converter.ConvertFrom("#C58FFF");
                app.Resources["ColorNumBtn"] = (SolidColorBrush)converter.ConvertFrom("#2D1B4E");
                app.Resources["ColorEqBtn"] = (SolidColorBrush)converter.ConvertFrom("#00F0FF");
                app.Resources["ColorActBtn"] = (SolidColorBrush)converter.ConvertFrom("#3A2566");
                app.Resources["ColorNumText"] = (SolidColorBrush)converter.ConvertFrom("#FFFFFF");
                app.Resources["ColorActText"] = (SolidColorBrush)converter.ConvertFrom("#00F0FF");
                app.Resources["ColorEqText"] = (SolidColorBrush)converter.ConvertFrom("#1A0B2E");
                app.Resources["ColorBorder"] = (SolidColorBrush)converter.ConvertFrom("#1A0B2E");
            }
            else if (themeIndex == 3)
            {
                app.Resources["ColorBg"] = (SolidColorBrush)converter.ConvertFrom("#0A0A0B");
                app.Resources["ColorMainText"] = (SolidColorBrush)converter.ConvertFrom("#FFFFFF");
                app.Resources["ColorHistText"] = (SolidColorBrush)converter.ConvertFrom("#A1A1A6");
                app.Resources["ColorNumBtn"] = (SolidColorBrush)converter.ConvertFrom("#1E1E21");
                app.Resources["ColorEqBtn"] = (SolidColorBrush)converter.ConvertFrom("#D90452");
                app.Resources["ColorActBtn"] = (SolidColorBrush)converter.ConvertFrom("#2D2D32");
                app.Resources["ColorNumText"] = (SolidColorBrush)converter.ConvertFrom("#F5F5F7");
                app.Resources["ColorActText"] = (SolidColorBrush)converter.ConvertFrom("#D90452");
                app.Resources["ColorEqText"] = (SolidColorBrush)converter.ConvertFrom("#FFFFFF");
                app.Resources["ColorBorder"] = (SolidColorBrush)converter.ConvertFrom("#0A0A0B");
            }
        }
    }
}
