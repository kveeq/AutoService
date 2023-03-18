using AutoService.DB;
using System.Windows;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PolomkaDB2Entities db = new PolomkaDB2Entities();
        public static Employee user;
    }
}
