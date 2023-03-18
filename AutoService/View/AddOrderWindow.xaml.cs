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
    /// Логика взаимодействия для AddOrderWindow.xaml
    /// </summary>
    public partial class AddOrderWindow : Window
    {
        public Order order;
        public AddOrderWindow()
        {
            InitializeComponent();
            var clients = App.db.Client.ToList();
            ClientsCmbBox.ItemsSource = clients;
            ServicesCmbBox.ItemsSource = App.db.Service.ToList();
            order = new Order();
            order.Date = DateTime.Now;
            order.AmoutPrice = 0;
            order.Employee = App.user;
            order.OrderPosition = new List<OrderPosition>();
            DataContext = order;
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddserviceBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ServicesCmbBox.SelectedItem == null)
                return;

            var orderPosition = new OrderPosition();
            orderPosition.Service = (Service)ServicesCmbBox.SelectedItem;
            orderPosition.Order = order;
            order.OrderPosition.Add(orderPosition);
            AmountPrice.Text = (Convert.ToDecimal(AmountPrice.Text.Replace("руб.", "").Trim()) + orderPosition.Service.Price).ToString();
            DataContext = null;
            DataContext = order;
        }

        private void DeleteOrderPositionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(((Button)sender).CommandParameter.ToString());
                var removeItem = order.OrderPosition.Where(x => x.Id == id).FirstOrDefault();
                order.OrderPosition.Remove(removeItem);
                AmountPrice.Text = (Convert.ToDecimal(AmountPrice.Text.Replace("руб.", "").Trim()) - removeItem.Service.Price).ToString();
                DataContext = null;
                DataContext = this.order;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message);
            }
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsCmbBox.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента");
                return;
            }
            try
            {
                order.Date = Convert.ToDateTime(DtPicker.Text);
                order.AmoutPrice = Convert.ToDecimal(AmountPrice.Text.Trim());
                order.Client = (Client)ClientsCmbBox.SelectedItem;
                App.db.Order.Add(order);
                App.db.SaveChanges();
                MessageBox.Show("Успешно добавлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message);
            }
        }
    }
}
