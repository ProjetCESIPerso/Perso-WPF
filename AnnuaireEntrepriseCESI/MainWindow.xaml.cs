using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using AnnuaireEntrepriseCESI.Pages;
using AnnuaireEntrepriseCESI.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace AnnuaireEntrepriseCESI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;
        private readonly ISiteService _siteService;

        public MainWindow()
        {
            _userService = new UserService();
            _serviceService = new ServiceService();
            _siteService = new SiteService();
            InitializeComponent();

            RecupListeUser();

            RecupListeService();

            RecupListeSite();
        }

        private void RecupListeUser()
        {
            try
            {
                List<UserDTO> listUser = _userService.GetAllUsers().Result;

                dataGrid.ItemsSource = listUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des employées \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RecupListeSite()
        {
            List<Site> listSite = _siteService.GetAllSite().Result;

            foreach (Site site in listSite)
            {
                SiteComboBox.Items.Add(site.Town);
            }
        }

        private void RecupListeService()
        {
            List<Service> listService = _serviceService.GetAllService().Result;

            foreach (Service service in listService)
            {
                serviceComboBox.Items.Add(service.Name);
            }
        }

        private void BtnQuitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Admin_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.B)
            {
                Login login = new Login();
                login.Show();
            }
        }
    }
}
