using AutoService.DB;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            NameTxt.Text += App.user.Fio;
            sDtPicker.SelectedDate = DateTime.Now.AddMonths(-1);
            poDtPicker.SelectedDate = DateTime.Now;
            Update();
        }

        public void Update()
        {
            OrdersLstView.ItemsSource = App.db.Order.Where(x => x.Employee.Id == App.user.Id && (x.Date >= sDtPicker.SelectedDate && x.Date <= poDtPicker.SelectedDate) && x.Client.Fio.Contains(PoiskTxt.Text)).ToList();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            new AutorizationWindow().Show();
            this.Close();
        }

        private void OrdersLstView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OrdersLstView.SelectedItem == null)
                return;    
            var window = new DetailInfoOfOrderWindow((Order)OrdersLstView.SelectedItem);
            window.Closing += (q, w) => Update();
            window.ShowDialog();
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddOrderWindow();
            window.Closing += (q, w) => Update();
            window.ShowDialog();
        }

        private void LookAtClientsBtn_Click(object sender, RoutedEventArgs e)
        {
            new LookAtClientsWindow().ShowDialog();
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProfileWindow().ShowDialog();
        }

        private void sDtPicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sDtPicker.SelectedDate == null || poDtPicker.SelectedDate == null)
            {
                return;
            }

            Update();
        }

        private void PoiskTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }
    }
}
