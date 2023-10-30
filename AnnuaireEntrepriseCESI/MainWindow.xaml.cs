using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using AnnuaireEntrepriseCESI.Pages;
using AnnuaireEntrepriseCESI.Pages.GestionService;
using AnnuaireEntrepriseCESI.Pages.GestionSite;
using AnnuaireEntrepriseCESI.Pages.GestionUser;
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
            List<SiteDTO> listSite = _siteService.GetAllSite().Result;

            SiteComboBox.Items.Clear();

            foreach (SiteDTO site in listSite)
            {
                SiteComboBox.Items.Add(site.Town);
            }
        }

        private void RecupListeService()
        {
            List<ServiceDTO> listService = _serviceService.GetAllService().Result;

            serviceComboBox.Items.Clear();

            foreach (ServiceDTO service in listService)
            {
                serviceComboBox.Items.Add(service.Name);
            }
        }

        private void SearchBarClicked(object sender, EventArgs e)
        {
            UserFiltres(RechercheTextBox.Text);
        }

        private void UserFiltres(string recherche)
        {
            List<UserDTO> listUser = _userService.GetAllUsers().Result;

            List<UserDTO> listUserFiltre = new List<UserDTO>();

            foreach (UserDTO user in listUser)
            {
                if (user.Name.Contains(recherche) || user.Surname.Contains(recherche) || user.Email.Contains(recherche) || user.PhoneNumber.Contains(recherche) || user.MobilePhone.Contains(recherche))
                {
                    listUserFiltre.Add(user);
                }
            }

            dataGrid.ItemsSource = listUserFiltre;
        }

        private void SearchSiteChanged(object sender, EventArgs e)
        {
            List<UserDTO> listUser = _userService.GetAllUsers().Result;

            List<UserDTO> listUserFiltre = new List<UserDTO>();

            foreach (UserDTO user in listUser)
            {
                if (user.SiteName == SiteComboBox.SelectedValue.ToString())
                {
                    listUserFiltre.Add(user);
                }
            }

            dataGrid.ItemsSource = listUserFiltre;
        }

        private void SearchServiceChanged(object sender, EventArgs e)
        {
            List<UserDTO> listUser = _userService.GetAllUsers().Result;

            List<UserDTO> listUserFiltre = new List<UserDTO>();

            foreach (UserDTO user in listUser)
            {
                if (user.ServiceName == serviceComboBox.SelectedValue.ToString())
                {
                    listUserFiltre.Add(user);
                }
            }

            dataGrid.ItemsSource = listUserFiltre;
        }
        private void BtnQuitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Admin_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control && e.Key == Key.B)
            {
                var login = new Login();
                var result = login.ShowDialog();
                
                if (result == true)
                {
                    BtnGestionService.Visibility = Visibility.Visible;
                    BtnGestionSite.Visibility = Visibility.Visible;
                    BtnGestionUser.Visibility = Visibility.Visible;
                }
            }
        }

        private void BtnGestionServiceClick(object sender, RoutedEventArgs e)
        {
            GestionService gestionService = new GestionService();
            var result = gestionService.ShowDialog();

            if (result == true || result == false)
            {
                RecupListeService();
            }
        }

        private void BtnGestionSiteClick(object sender, RoutedEventArgs e)
        {
            GestionSiteWindow gestionSite = new GestionSiteWindow();
            var result = gestionSite.ShowDialog();

            if (result == true || result == false)
            {
                RecupListeSite();
            }
        }

        private void BtnGestionUserClick(object sender, RoutedEventArgs e)
        {
            GestionUserWindow gestionUser = new GestionUserWindow();
            var result = gestionUser.ShowDialog();

            if (result == true || result == false)
            {
                RecupListeUser();
            }
        }
    }
}
