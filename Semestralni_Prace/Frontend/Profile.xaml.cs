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

namespace SemPrace.Frontend
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// TODO: Zakomponovat Edit záznamu a jak to udělat 1. možnost, jít do záznamů, vybrat záznam,
    ///       RMB a vybrat edit, ale to nevím jestli bude tady fungovat, jako v JaveFX 
    /// </summary>
    public partial class Profile : Window
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Odhlaseni(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
        private void Pridani(object sender, RoutedEventArgs e)
        {
            PacientAdd pridani = new PacientAdd();
            pridani.Show();
            this.Close();
        }
    }
}
