using AutoService.DB;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace AutoService.View
{
    /// <summary>
    /// Логика взаимодействия для DetailInfoOfOrderWindow.xaml
    /// </summary>
    public partial class DetailInfoOfOrderWindow : Window
    {
        private Order Order;
        public DetailInfoOfOrderWindow(Order order)
        {
            InitializeComponent();
            this.Order = order;
            DataContext = this.Order;
        }

        private void SaveDetailInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.db.SaveChanges();
                MessageBox.Show("Успешно сохранён!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message);
            }
        }

        private void DeleteOrderPositionBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var id = Convert.ToInt32(((Button)sender).CommandParameter.ToString());
                var removeItem = App.db.OrderPosition.Where(x => x.Id == id).FirstOrDefault();
                Order.AmoutPrice -= removeItem.Service.Price;
                Order.OrderPosition.Remove(removeItem);
                DataContext = null;
                DataContext = this.Order;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message);
            }
        }

        private void PecatChekBtn_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new SaveFileDialog();
            ofd.AddExtension = true;
            ofd.DefaultExt = "docx";
            ofd.Filter = "Только вордовские файлы|*.docx";

            if (ofd.ShowDialog().Value)
                SetDataWord(ofd.FileName);
        }

        public void SetDataWord(string fileName)
        {
            int i = 1;

            Microsoft.Office.Interop.Word._Application word_app = new Microsoft.Office.Interop.Word.Application
            {
                Visible = false
            };

            object missing = Type.Missing;
            Microsoft.Office.Interop.Word._Document word_doc = word_app.Documents.Add(
                ref missing, ref missing, ref missing, ref missing);

            Microsoft.Office.Interop.Word.Paragraph para = word_doc.Paragraphs.Add(ref missing);
           
            para.Range.Text = "Чек оплаты оказанных услуг";
            object style_name = "Заголовок 1";
            para.Range.set_Style(ref style_name);
            para.Range.InsertParagraphAfter();
            para.Range.Font.Name = "Courier New";
            para.Range.Font.Size = 23;
            para.Range.Text = $"Номер заказа: {Order.Id}";
            para.Range.InsertParagraphAfter();
            para.Range.Text = $"Клиент заказа: {Order.Client.Fio}";
            para.Range.InsertParagraphAfter();
            para.Range.Text = $"Дата заказа: {Order.Date}";
            para.Range.InsertParagraphAfter();
            para.Range.Text = $"Сумма заказа: {Order.AmoutPrice}";
            para.Range.InsertParagraphAfter();
            para.Range.Text = $"Список выполненных работ: ";
            para.Range.InsertParagraphAfter();
            foreach (var item in Order.OrderPosition)
            {
                para.Range.Text = $"{i}) {item.Service.Name} - {item.Service.Price} руб. ";
                para.Range.InsertParagraphAfter();
                i++;
            }
            para.Range.Text = $"Сотрудник выполнивший заказ: {Order.Employee.Fio}";
            para.Range.InsertParagraphAfter();


            // Сохраним текущий шрифт и начнем с использования Courier New.
            string old_font = para.Range.Font.Name;
            para.Range.Font.Name = "Courier New";
            para.Range.Font.Size = 23;

            // Сохраним документ.
            object filename = fileName;

            word_doc.SaveAs(ref filename, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing,
                ref missing);
            object save_changes = false;
            word_doc.Close(ref save_changes, ref missing, ref missing);
            word_app.Quit(ref save_changes, ref missing, ref missing);
        }

        private void DeleteDetailInfoBtn_Click(object sender, RoutedEventArgs e)
        {
            App.db.Order.Remove(Order);
            App.db.SaveChanges();
            MessageBox.Show("Успешно удален");
            this.Close();
        }
    }
}
