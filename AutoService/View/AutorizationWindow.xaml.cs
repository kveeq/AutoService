using AutoService.DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoService.View
{
    /// <summary>
    /// Логика взаимодействия для AutorizationWindow.xaml
    /// </summary>
    public partial class AutorizationWindow : Window
    {
        public AutorizationWindow()
        {
            InitializeComponent();
            App.user = null;
            //AddImage();
            AddImageEmployee();
        }

        private void AddImageCar()
        {
            string path = "C:\\Users\\Ильсаф\\Downloads\\porshe911.jpeg";

            var f = App.db.Car.Where(x => x.Id == 2).FirstOrDefault();
            f.Photo = File.ReadAllBytes(path);
            App.db.SaveChanges();
        } 
        private void AddImageEmployee()
        {
            string path = "C:\\Users\\Ильсаф\\Downloads\\sotr2.jpg";

            var f = App.db.Employee.Where(x => x.Id == 2).FirstOrDefault();
            f.Photo = File.ReadAllBytes(path);
            App.db.SaveChanges();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.db.Auth.Where(x => x.Login == LoginTxt.Text && x.Password == PassTxt.Password).FirstOrDefault();

            if(user == null)
            {
                MessageBox.Show("Неправильный пароль или логин!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            App.user = user.Employee;
            new HomeWindow().Show();
            this.Close();
        }
    }
}
