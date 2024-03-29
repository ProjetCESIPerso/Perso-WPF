﻿using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using AnnuaireEntrepriseCESI.Pages.GestionService;
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
    /// Logique d'interaction pour GestionSiteWindow.xaml
    /// </summary>
    public partial class GestionSiteWindow : Window
    {
        private readonly ISiteService _siteService;
        private readonly IUserService _userService;

        public GestionSiteWindow()
        {
            _siteService = new SiteService();
            _userService = new UserService();
            InitializeComponent();

            RecupSite();
        }

        private void RecupSite()
        {
            try
            {
                List<SiteDTO> listSite = _siteService.GetAllSite().Result;

                DataSite.ItemsSource = listSite;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des sites \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            RecupSiteFiltre(searchBarSite.Text);
        }

        private void RecupSiteFiltre(string text)
        {
            List<SiteDTO> listSite = _siteService.GetAllSite().Result;

            List<SiteDTO> listSiteFiltre = new List<SiteDTO>();

            foreach (SiteDTO item in listSite)
            {
                if (item.Town.ToLower().Contains(text.ToLower()))
                {
                    listSiteFiltre.Add(item);
                }
            }

            DataSite.ItemsSource = listSiteFiltre;
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            var addSite = new AddSiteWindow();
            var result = addSite.ShowDialog();

            if (result == true)
            {
                RecupSite();
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
            var context = sender_context!.DataContext as SiteDTO;

            var updateSite = new UpdateSiteWindow(context);
            var result = updateSite.ShowDialog();
            if (result == true)
            {
                RecupSite();
            }
            else
            {
                RecupSite();
                MessageBox.Show("La modification a été annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;

            var context = sender_context!.DataContext as SiteDTO;

            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer le site : '" + context!.Town + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                //Récupération du nombre d'attribution du service à un employé
                int NbAttribution = await _userService.GetNbOfAttributionToSite(context!.Id);


                if (NbAttribution > 0)
                {
                    MessageBox.Show("Impossible de supprimer le service, il est attribué à " + NbAttribution + " employé(s) !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    await _siteService.DeleteSite(context.Town);
                    RecupSite();
                }
            }
            RecupSite();
        }
    }
}
