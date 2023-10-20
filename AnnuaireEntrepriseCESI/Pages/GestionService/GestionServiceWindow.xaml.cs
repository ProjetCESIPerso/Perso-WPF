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
    /// Logique d'interaction pour GestionService.xaml
    /// </summary>
    public partial class GestionService : Window
    {
        private readonly IServiceService _serviceService;

        public GestionService()
        {
            _serviceService = new ServiceService();
            InitializeComponent();

            RecupService();
        }

        private void RecupService()
        {
            try
            {
                List<Service> listService = _serviceService.GetAllService().Result;

                DataService.ItemsSource = listService;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des employées \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            var addService = new AddServiceWindow();
            var result = addService.ShowDialog();

            if (result == true)
            {
                RecupService();
            }
            else
            {
                MessageBox.Show("L'ajout a été annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnUpdateClick(object sender, RoutedEventArgs e)
        {
            //Affichage d'une form avec la possibilité de modifier l'employée
            var sender_context = sender as Button;
            var context = sender_context!.DataContext as Service;

            var updateService = new UpdateService(context);
            var result = updateService.ShowDialog();
            if (result == true)
            {
                RecupService();
            }
            else
            {
                RecupService();
                MessageBox.Show("La modification a été annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;

            var context = sender_context!.DataContext as Service;

            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer le service : '" + context!.Name + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                _serviceService.DeleteService(context.Name);
            }
            RecupService();
        }
    }
}
