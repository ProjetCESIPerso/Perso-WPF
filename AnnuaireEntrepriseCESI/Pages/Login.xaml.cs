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

namespace AnnuaireEntrepriseCESI.Pages
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnLoginClick(object sender, RoutedEventArgs e)
        {
            if (PassWordTextBox.Password == "1234")
            {
                DialogResult = true; 
            }
            else
            {
                MessageBox.Show("Le mot de passe est incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                PassWordTextBox.Clear();
            }
        }
    }
}
