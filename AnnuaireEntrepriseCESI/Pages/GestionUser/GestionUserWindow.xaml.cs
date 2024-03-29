﻿using AnnuaireEntrepriseCESI.DTOs;
using AnnuaireEntrepriseCESI.Interfaces;
using AnnuaireEntrepriseCESI.Models;
using AnnuaireEntrepriseCESI.Pages.GestionSite;
using AnnuaireEntrepriseCESI.Services;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace AnnuaireEntrepriseCESI.Pages.GestionUser
{
    /// <summary>
    /// Logique d'interaction pour GestionUserWindow.xaml
    /// </summary>
    public partial class GestionUserWindow : Window
    {
        private readonly IUserService _userService;

        public GestionUserWindow()
        {
            _userService = new UserService();

            InitializeComponent();

            RecupUser();
        }

        private async void RecupUser()
        {
            try
            {
                List<UserDTO> listUser = await _userService.GetAllUsers();

                DataUser.ItemsSource = listUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Une erreur est survenue lors de la récupération de la liste des utilisateurs \nErreur : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            UserFiltres(searchBar.Text);
        }

        private async void UserFiltres(string recherche)
        {
            List<UserDTO> listUser = await _userService.GetAllUsers();

            List<UserDTO> listUserFiltre = new List<UserDTO>();

            foreach (UserDTO user in listUser)
            {
                if (user.Name.Contains(recherche) || user.Surname.Contains(recherche) || user.Email.Contains(recherche) || user.PhoneNumber.Contains(recherche) || user.MobilePhone.Contains(recherche))
                {
                    listUserFiltre.Add(user);
                }
            }

            DataUser.ItemsSource = listUserFiltre;
        }

        private void BtnBackClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            var addUser = new AddUserWindow();
            var result = addUser.ShowDialog();

            if (result == true)
            {
                RecupUser();
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
            var context = sender_context!.DataContext as UserDTO;

            var updateUser = new UpdateUserWindow(context);
            var result = updateUser.ShowDialog();
            if (result == true)
            {
                RecupUser();
            }
            else
            {
                RecupUser();
                MessageBox.Show("La modification a été annulée", "Annulation", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            //Demander la confirmation de suppression
            var sender_context = sender as Button;

            var context = sender_context!.DataContext as UserDTO;

            var resultMsgBoxDelete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer l'utilisateur : '" + context!.Id + "' ?", "Confirmer la suppression", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resultMsgBoxDelete == MessageBoxResult.Yes)
            {
                await _userService.DeleteUser(context.Id);
                MessageBox.Show("Utilisateur supprimé", "Suppression", MessageBoxButton.OK, MessageBoxImage.Information);
                RecupUser();
            }
            RecupUser();
        }
    }
}
