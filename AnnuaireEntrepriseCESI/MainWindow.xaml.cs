﻿using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using AnnuaireEntrepriseCESI.Pages;
using AnnuaireEntrepriseCESI.Pages.GestionService;
using AnnuaireEntrepriseCESI.Pages.GestionSite;
using AnnuaireEntrepriseCESI.Pages.GestionUser;
using AnnuaireEntrepriseCESI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<UserDTO> listUser { get; set; } = new List<UserDTO>();

        public List<UserDTO> listUserFiltre { get; set; } = new List<UserDTO>();

        public List<ServiceDTO> listService { get; set; } = new List<ServiceDTO>();

        public List<SiteDTO> listSite { get; set; } = new List<SiteDTO>();

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

        private async void RecupListeService()
        {
            listService = await _serviceService.GetAllService();

            ComboBoxService.Items.Clear();

            ComboBoxService.Items.Add("");

            ComboBoxService.SelectedItem = 0;

            foreach (var item in listService)
            {
                ComboBoxService.Items.Add(item.Name);
            }
        }

        private void ComboBoxService_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listUserFiltre.Clear();

            if (ComboBoxService.SelectedItem != null || ComboBoxService.SelectedItem.ToString().Length > 0)
            {
                if (ComboBoxSite.SelectedItem == null || ComboBoxSite.SelectedItem.ToString().Length == 0)
                {
                    foreach (UserDTO user in listUser)
                    {
                        if (user.Service.Name.Contains(ComboBoxService.SelectedItem.ToString()))
                        {
                            listUserFiltre.Add(user);
                        }
                    }
                }
                else
                {
                    foreach (UserDTO user in listUser)
                    {
                        if (user.Service.Name.Contains(ComboBoxService.SelectedItem.ToString()) && user.Site.Town.Contains(ComboBoxSite.SelectedItem.ToString()))
                        {
                            listUserFiltre.Add(user);
                        }
                    }
                }

                dataGrid.ItemsSource = null;

                dataGrid.ItemsSource = listUserFiltre;
            }
            else
            {
                RecupListeUser();
            }
        }

        private void RecupListeSite()
        {
            listSite = _siteService.GetAllSite().Result;

            ComboBoxSite.Items.Clear();

            ComboBoxSite.Items.Add("");

            ComboBoxSite.SelectedItem = 0;

            foreach (var item in listSite)
            {
                ComboBoxSite.Items.Add(item.Town);
            }
        }

        private void ComboBoxSite_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listUserFiltre.Clear();
      
            if (ComboBoxSite.SelectedItem != null || ComboBoxSite.SelectedItem.ToString().Length > 0)
            {
                if (ComboBoxService.SelectedItem == null || ComboBoxService.SelectedItem.ToString().Length == 0)
                {
                    foreach (UserDTO user in listUser)
                    {
                        if (user.Site.Town.Contains(ComboBoxSite.SelectedItem.ToString()))
                        {
                            listUserFiltre.Add(user);
                        }
                    }
                }
                else
                {
                    foreach (UserDTO user in listUser)
                    {
                        if (user.Service.Name.Contains(ComboBoxService.SelectedItem.ToString()) && user.Site.Town.Contains(ComboBoxSite.SelectedItem.ToString()))
                        {
                            listUserFiltre.Add(user);
                        }
                    }
                }

                dataGrid.ItemsSource = null;

                dataGrid.ItemsSource = listUserFiltre;
            }
            else
            {
                RecupListeUser();
            }
        }

        private void RecupListeUser()
        {
            try
            {
                listUser.Clear();

                listUser = _userService.GetAllUsers().Result;

                dataGrid.ItemsSource = null;

                dataGrid.ItemsSource = listUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des employées \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchBarClicked(object sender, EventArgs e)
        {
            UserFiltres(RechercheTextBox.Text);
        }

        private void UserFiltres(string recherche)
        {
            listUserFiltre.Clear();

            if (recherche.Length > 0 && ComboBoxService.SelectedItem == null && ComboBoxSite.SelectedItem == null)
            {
                foreach (UserDTO user in listUser)
                {
                    if (user.Name.ToLower().Contains(recherche.ToLower()) || user.Surname.ToLower().Contains(recherche.ToLower()) || user.Email.Contains(recherche) || user.PhoneNumber.Contains(recherche) || user.MobilePhone.Contains(recherche))
                    {
                        listUserFiltre.Add(user);
                    }
                }

                dataGrid.ItemsSource = null;

                dataGrid.ItemsSource = listUserFiltre;
            }
            else if (recherche.Length > 0 && ComboBoxService.SelectedItem == null && ComboBoxSite.SelectedItem != null)
            {
                foreach (UserDTO user in listUser)
                {
                    if (user.Site.Town == ComboBoxSite.SelectedItem.ToString())
                    {
                        if (user.Name.ToLower().Contains(recherche.ToLower()) || user.Surname.ToLower().Contains(recherche.ToLower()) || user.Email.Contains(recherche) || user.PhoneNumber.Contains(recherche) || user.MobilePhone.Contains(recherche))
                        {
                            listUserFiltre.Add(user);
                        }
                    }
                }

                dataGrid.ItemsSource = null;

                dataGrid.ItemsSource = listUserFiltre;
            }
            else if (recherche.Length > 0 && ComboBoxService.SelectedItem != null && ComboBoxSite.SelectedItem != null)
            {
                foreach (UserDTO user in listUser)
                {
                    if (user.Site.Town == ComboBoxSite.SelectedItem.ToString() && user.Service.Name == ComboBoxService.SelectedItem.ToString())
                    {
                        if (user.Name.ToLower().Contains(recherche.ToLower()) || user.Surname.ToLower().Contains(recherche.ToLower()) || user.Email.Contains(recherche) || user.PhoneNumber.Contains(recherche) || user.MobilePhone.Contains(recherche))
                        {
                            listUserFiltre.Add(user);
                        }
                    }
                }

                dataGrid.ItemsSource = null;

                dataGrid.ItemsSource = listUserFiltre;
            }
            else
            {
                if (ComboBoxService.SelectedItem != null || ComboBoxSite.SelectedItem != null)
                {
                    listUserFiltre.Clear();

                    if (ComboBoxSite.SelectedItem != null || ComboBoxSite.SelectedItem.ToString().Length > 0)
                    {
                        if (ComboBoxService.SelectedItem == null || ComboBoxService.SelectedItem.ToString().Length == 0)
                        {
                            foreach (UserDTO user in listUser)
                            {
                                if (user.Site.Town.Contains(ComboBoxSite.SelectedItem.ToString()))
                                {
                                    listUserFiltre.Add(user);
                                }
                            }
                        }
                        else
                        {
                            foreach (UserDTO user in listUser)
                            {
                                if (user.Service.Name.Contains(ComboBoxService.SelectedItem.ToString()) && user.Site.Town.Contains(ComboBoxSite.SelectedItem.ToString()))
                                {
                                    listUserFiltre.Add(user);
                                }
                            }
                        }

                        dataGrid.ItemsSource = null;

                        dataGrid.ItemsSource = listUserFiltre;
                    }
                }
                else
                {
                    RecupListeUser();
                    listUserFiltre.Clear();
                }
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
            gestionService.ShowDialog();
        }

        private void BtnGestionSiteClick(object sender, RoutedEventArgs e)
        {
            GestionSiteWindow gestionSite = new GestionSiteWindow();
            gestionSite.ShowDialog();
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
