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
    /// Logique d'interaction pour UpdateService.xaml
    /// </summary>
    public partial class UpdateService : Window
    {
        private readonly IServiceService _serviceService;
        string OldServiceName { get; set; }
        int ServiceId { get; set; }

        public UpdateService(ServiceDTO service)
        {
            InitializeComponent();

            _serviceService = new ServiceService();

            NameService.DataContext = service;

            OldServiceName = service.Name;
            ServiceId = service.Id;
        }

        private void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            ModifyService();
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void NameService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ModifyService();
            }
        }

        private async void ModifyService()
        {
            //Vérification doublon
            ServiceDTO result =  _serviceService.GetById(ServiceId).Result;
            if (result.Name == NameService.Text || OldServiceName == NameService.Text)
            {
                MessageBox.Show($"Le service entré ({NameService.Text}) existe déjà !", "Doublon", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if(NameService.Text != "") 
                {
                    Service service = new Service();
                    service.Name = NameService.Text;
                    service.Id = ServiceId;
                    await _serviceService.UpdateService(OldServiceName, service);
                    MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
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
