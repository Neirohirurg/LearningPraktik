using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Ex8
{
    /// <summary>
    /// Логика взаимодействия для HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        private MainWindow _parentWindow;
        public HistoryWindow(MainWindow parentWindow)
        {
            InitializeComponent();
            this._parentWindow = parentWindow;

            Partner selectedPartner = (Partner)this._parentWindow.partnersList.SelectedItem;

            if (selectedPartner != null)
            {
                List<Sale> partnerSales = _parentWindow.GetSalesForSelectedPartner(selectedPartner);

                this.salesList.ItemsSource = partnerSales;
            }
            
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            this._parentWindow.Activate();
        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void calcMethod_Click(object sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            MessageBox.Show(rand.Next(4000000, 30000000).ToString());
        }
    }
}
