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

namespace SemPrace.Frontend
{
    /// <summary>
    /// Interaction logic for PacientAdd.xaml
    /// TODO: Zakomponovat řádek pro smrt...
    /// </summary>
    public partial class PacientAdd : Window
    {
        public PacientAdd()
        {
            InitializeComponent();
        }
        private void Zpet(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile();
            profile.Show();
            this.Close();
        }
    }
}