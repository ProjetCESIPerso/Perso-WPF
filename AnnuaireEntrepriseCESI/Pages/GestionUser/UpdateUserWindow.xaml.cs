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

namespace AnnuaireEntrepriseCESI.Pages.GestionUser
{
    /// <summary>
    /// Logique d'interaction pour UpdateUserWindow.xaml
    /// </summary>
    public partial class UpdateUserWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IServiceService _serviceService;
        private readonly ISiteService _siteService;

        int UserId { get; set; }

        string ServiceName { get; set; }

        string SiteName { get; set; }

        public UpdateUserWindow(UserDTO userDTO)
        {
            InitializeComponent();

            _userService = new UserService();
            _serviceService = new ServiceService();
            _siteService = new SiteService();

            UserId = userDTO.Id;

            NameUser.DataContext = userDTO;
            SurnameUser.DataContext = userDTO;
            MailUser.DataContext = userDTO;
            PhoneNumber.DataContext = userDTO;
            MobilePhone.DataContext = userDTO;

            ServiceName = userDTO.ServiceName;
            SiteName = userDTO.SiteName;

            RecupListeSiteEtService();
        }

        private void RecupListeSiteEtService()
        {
            List<Site> listSites = _siteService.GetAllSite().Result;
            List<Service> listServices = _serviceService.GetAllService().Result;

            foreach (Site site in listSites)
            {
                SiteUser.Items.Add(site.Town);
            }

            foreach (Service service in listServices)
            {
                ServiceUser.Items.Add(service.Name);
            }

            ServiceUser.SelectedItem = ServiceName;
            SiteUser.SelectedItem = SiteName;
        }

        private void BtnValiderClick(object sender, RoutedEventArgs e)
        {
            ModifyUser();
        }

        private void BtnRetourClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ModifyUser()
        {
            User user = new User();

            user.Name = NameUser.Text;
            user.Surname = SurnameUser.Text;
            user.Email = MailUser.Text;
            user.PhoneNumber = PhoneNumber.Text;
            user.MobilePhone = MobilePhone.Text;

            if (SiteName != SiteUser.SelectedValue)
            {
                user.SiteId = _siteService.GetByName(SiteUser.SelectedValue.ToString()).Id;
            }

            if (ServiceName != ServiceUser.SelectedValue) 
            {
                user.ServiceId = _serviceService.GetByName(ServiceUser.SelectedValue.ToString()).Id;
            }

            if (VerifDonnees(user) == true)
            {
                _userService.UpdateUser(UserId, user);
                MessageBox.Show("Modification enregistrée", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Veuillez remplir tout les champs !", "Modification", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private bool VerifDonnees(User user)
        {
            if (user == null) { return false; }
            if (user.Name == null || user.Name == string.Empty) { return false; }
            if (user.Surname == null || user.Surname == string.Empty) { return false; }
            if (user.Email == null || user.Email == string.Empty) { return false; }
            if (user.PhoneNumber == null || user.PhoneNumber == string.Empty) { return false; }
            if (user.MobilePhone == null || user.MobilePhone == string.Empty) { return false; }
            if (user.ServiceId == null) { return false; }
            if (user.SiteId == null) { return false; }
            
            return true;
        }
    }
}
