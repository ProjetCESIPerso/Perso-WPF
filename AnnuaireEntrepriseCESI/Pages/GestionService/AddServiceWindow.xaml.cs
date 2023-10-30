using AnnuaireEntrepriseCESI.DTOs;
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

namespace AnnuaireEntrepriseCESI.Pages.GestionService
{
    /// <summary>
    /// Logique d'interaction pour AddService.xaml
    /// </summary>
    public partial class AddServiceWindow : Window
    {
        private readonly IServiceService _serviceService;

        public AddServiceWindow()
        {
            _serviceService = new ServiceService();
            InitializeComponent();
        }

        private void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            AddService();
        }

        private void NameService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddService();
            }
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private async void AddService()
        {
            //Vérification si service inexistant
            ServiceDTO result = _serviceService.GetByName(NameService.Text).Result;

            if (result.Name == NameService.Text)
            {
                MessageBox.Show($"Le service ({NameService.Text}) existe déjà !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(NameService.Text != "")
                {
                    Service serviceToAdd = new();
                    serviceToAdd.Name = NameService.Text;
                    await _serviceService.AddService(serviceToAdd);
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
