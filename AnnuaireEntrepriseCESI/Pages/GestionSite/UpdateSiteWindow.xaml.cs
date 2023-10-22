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
    /// Logique d'interaction pour UpdateSiteWindow.xaml
    /// </summary>
    public partial class UpdateSiteWindow : Window
    {
        private readonly ISiteService _siteService;
        string OldSiteName { get; set; }
        int SiteId { get; set; }

        public UpdateSiteWindow(Site site)
        {
            InitializeComponent();

            _siteService = new SiteService();

            NameSite.DataContext = site;

            OldSiteName = site.Town;
            SiteId = site.Id;
        }

        private void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            ModifySite();
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void NameSite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ModifySite();
            }
        }

        private void ModifySite()
        {
            //Vérification doublon
            Site result = _siteService.GetById(SiteId).Result;
            if (result.Town == NameSite.Text || OldSiteName == NameSite.Text)
            {
                MessageBox.Show($"Le site entré ({NameSite.Text}) existe déjà !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (NameSite.Text != "")
                {
                    Site site = new Site();
                    site.Town = NameSite.Text;
                    site.Id = SiteId;
                    _siteService.UpdateSite(OldSiteName, site);
                    MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Veuillez remplir le nom du site !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
