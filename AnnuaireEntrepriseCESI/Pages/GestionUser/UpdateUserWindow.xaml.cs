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

            ServiceName = userDTO.Service.Name;
            SiteName = userDTO.Site.Town;

            RecupListeSiteEtService();
        }

        private void RecupListeSiteEtService()
        {
            List<SiteDTO> listSites = _siteService.GetAllSite().Result;
            List<ServiceDTO> listServices = _serviceService.GetAllService().Result;

            foreach (SiteDTO site in listSites)
            {
                SiteUser.Items.Add(site.Town);
            }

            foreach (ServiceDTO service in listServices)
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

        private async void ModifyUser()
        {
            User user = new User();

            user.Name = NameUser.Text;
            user.Surname = SurnameUser.Text;
            user.Email = MailUser.Text;
            user.PhoneNumber = PhoneNumber.Text;
            user.MobilePhone = MobilePhone.Text;

            if (SiteName != SiteUser.SelectedValue)
            {
                SiteDTO siteDTO = await _siteService.GetByName(SiteUser.SelectedValue.ToString());
                Site site = new Site();
                site.Id = siteDTO.Id;
                site.Town = siteDTO.Town;

                user.SiteId = siteDTO.Id;
                user.Site = site;
            }
            else
            {
                UserDTO userBDD = await _userService.GetById(UserId);

                user.SiteId = userBDD.SiteId;
                user.Site = userBDD.Site;
            }

            if (ServiceName != ServiceUser.SelectedValue) 
            {
                ServiceDTO serviceDTO = await _serviceService.GetByName(ServiceUser.SelectedValue.ToString());
                Service service = new Service();
                service.Id = serviceDTO.Id;
                service.Name = serviceDTO.Name;

                user.ServiceId = serviceDTO.Id;
                user.Service = service;
            }
            else
            {
                UserDTO userBDD = await _userService.GetById(UserId);
                user.ServiceId = userBDD.ServiceId;
                user.Service = userBDD.Service;
            }

            if (VerifDonnees(user) == true)
            {
                await _userService.UpdateUser(UserId, user);
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
