using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Calculator.Infrastructure;

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для App.xaml.
    /// Точка входа приложения: хранит только состояние выбранной темы
    /// для каждого окна, сама логика перекраски вынесена в
    /// Infrastructure.ThemeManager.
    /// </summary>
    public partial class App : Application
    {
        public int MainWindowTheme = 1;
        public int EngWindowTheme = 3;

        public void ChangeTheme(int themeIndex)
        {
            ThemeManager.ChangeTheme(this, themeIndex);
        }
    }
}
