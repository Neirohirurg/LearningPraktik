using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        private DataService DataService;

        public MainWindow()
        {
            InitializeComponent();

            this.DataService = new DataService();
            List<Partner> partners = DataService.GetPartners();
            this.partnersList.ItemsSource = partners;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.window1 == null || !this.window1.IsLoaded)
            {
                this.window1 = new Window1();
                this.window1.Show();
            }
            else
            {
                this.window1.Activate();
            }
        }

        private void itemExample_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.window2 == null || !this.window2.IsLoaded)
            {
                this.window2 = new Window2();
                UpdateData(sender, this.window2);

                this.window2.partnerTypes.DataContext = DataService.PartnerTypes;
                this.window2.Show();
            }
            else
            {
                this.window2.Activate();
            }

        }

        private void UpdateData(object sender, Window window)
        {
            ListBoxItem selectedPartner = (ListBoxItem)sender;

            Partner partner = (Partner)selectedPartner.DataContext;

            window.DataContext = partner;
            
        }

        private void InsertData(object sender, Window window)
        {
            Partner partner = (Partner)sender;
            DataService.AddPartner(partner);
        }
            

        private void itemExample_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.changeButton.IsEnabled = true;
        }
    }
}
