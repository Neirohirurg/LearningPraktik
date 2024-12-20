﻿using System;
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

namespace Ex8
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow _mainWindow;
        private Window3 window3;
        private DataService db;

        public Window1(MainWindow mainWindow)
        {
            InitializeComponent();
            

            db = new DataService();
            this.DataContext = new Partner();
            this._mainWindow = mainWindow;
            this.partnerTypes.ItemsSource = db.GetPartnersTypes();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            InitWindow3();
        }

        private void InitWindow3()
        {
            if (this.window3 == null || !this.window3.IsLoaded)
            {
                this.window3 = new Window3(this);
                this.window3.Show();
            }
            else this.window3.Activate();
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Partner partner = (Partner)this.DataContext;
                bool success = db.AddPartner(partner);
                if (success) 
                {                   
                    UpdatePartnersList();
                    MessageBox.Show("Запись добавлена"); 
                }

                this.Close();
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void UpdatePartnersList()
        {
            this._mainWindow.partnersList.ItemsSource = db.GetPartners();
        }
    }
}
