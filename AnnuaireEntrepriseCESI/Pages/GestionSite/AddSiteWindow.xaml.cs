using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using AnnuaireEntrepriseCESI.Services;
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

namespace AnnuaireEntrepriseCESI.Pages.GestionSite
{
    /// <summary>
    /// Logique d'interaction pour AddSiteWindow.xaml
    /// </summary>
    public partial class AddSiteWindow : Window
    {
        private readonly ISiteService _siteService;

        public AddSiteWindow()
        {
            _siteService = new SiteService();
            InitializeComponent();
        }

        private void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            AddSite();
        }

        private void NameSite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddSite();
            }
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void AddSite()
        {
            //Vérification si service inexistant
            Site result = _siteService.GetByName(NameSite.Text).Result;

            if (result.Town == NameSite.Text)
            {
                MessageBox.Show($"Le service ({NameSite.Text}) existe déjà !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (NameSite.Text != "")
                {
                    Site siteToAdd = new();
                    siteToAdd.Town = NameSite.Text;
                    _siteService.AddSite(siteToAdd);
                    MessageBox.Show("Ajout enregistré", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le nom du service !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
