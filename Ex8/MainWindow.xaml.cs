using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex8
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Window1 window1;
        private Window2 window2;
        private HistoryWindow window3;
        private DataService DataService;

        public MainWindow()
        {
            InitializeComponent();


            this.DataService = new DataService();
            List<Partner> partners = DataService.GetPartners();
            this.partnersList.ItemsSource = partners;
        }


        public List<Sale> GetSalesForSelectedPartner(Partner partner)
        {
            if (partner == null || string.IsNullOrEmpty(partner.PartnerName))
            {
                MessageBox.Show("Выберите партнера с корректным именем.");
                return new List<Sale>(); 
            }
            var sales = DataService.GetSales();

            var filteredSales = from sale in sales
                                where sale.PartnerName == partner.PartnerName
                                select sale;

            return filteredSales.ToList();
        }

        private void InitWindow1()
        {
            if (this.window1 == null || !this.window1.IsLoaded)
            {
                this.window1 = new Window1(this);
                this.window1.Show();
            }
            else
            {
                this.window1.Activate();
            }
        }
        private void InitWindow2()
        {
            if (this.window2 == null || !this.window2.IsLoaded)
            {
                this.window2 = new Window2(this);
                this.window2.Show();
            }
            else
            {
                this.window2.Activate();
            }
        }
        private void AddPartnerDataToWindow2(object selectedItem)
        {
            ListBoxItem selectedPartner = (ListBoxItem)selectedItem;
            Partner partner = (Partner)selectedPartner.DataContext;
            this.window2.DataContext = partner;
        }

        private void AddPartnerDataToWindow2(Partner selectedItem)
        {
            this.window2.DataContext = selectedItem;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            InitWindow1();

        }
        private void itemExample_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            InitWindow2();
            AddPartnerDataToWindow2(sender);

        }

        private void partnersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (partnersList.SelectedItem != null)
            {

                changeButton.IsEnabled = true;
                historyButton.IsEnabled = true;
            }
            else
            {
                historyButton.IsEnabled = false;
                changeButton.IsEnabled = false;
            }
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            InitWindow2();
            AddPartnerDataToWindow2((Partner)this.partnersList.SelectedItem);
        }

        private void historyButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.window3 == null || !this.window3.IsLoaded)
            {
                this.window3 = new HistoryWindow(this);
                GetSalesForSelectedPartner((Partner)this.partnersList.SelectedItem);
                this.window3.Show();
            }
            else
            {
                this.window3.Activate();
            }
        }

    }
}
