using System;
using System.Collections.Generic;
using System.Linq;
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

        public MainWindow()
        {
            InitializeComponent();       
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChangeSizeHeader();
/*            ChangeSizeFooter();*/
        }
        
        public void ChangeSizeHeader()
        {
            this.headerLabel.Width = this.Width - (this.logo.Width * 2 + (this.logo.Margin.Right + this.logo.Margin.Left));
            this.header.Height = this.Height * 0.08;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.window1 == null || !this.window1.IsLoaded)
            {
                window1 = new Window1();
                window1.Show();
            }
            else
            {
                window1.Activate();
            }
        }

        private void itemExample_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.window2 == null || !this.window2.IsLoaded)
            {
                window2 = new Window2();
                window2.Show();
            }
            else
            {
                this.window2.Activate();
            }
        }

    }


    class EditorDB
    {


        public EditorDB()
        {

        }
    }
}
